using RW.DSL;
using RW.DSL.Procedures;

namespace MainUI.Service
{
    /// <summary>
    /// DSL服务类，用于加载和管理DSL文件
    /// </summary>
    /// <param name="dslPath">DSL路径</param>
    public class DSLService(string dslPath)
    {
        private readonly Dictionary<string, DSLProcedure> _procedureModules = [];

        /// <summary>
        /// 加载DSL文件
        /// </summary>
        /// <param name="modelId">型号ID</param>
        /// <param name="paraConfig">系统参数类</param>
        public void LoadDSL(int modelId, ParaConfig paraConfig)
        {
            if (modelId <= 0)
            {
                throw new ArgumentException("模型ID必须大于0", nameof(modelId));
            }

            try
            {
                _procedureModules.Clear();
                LoadAndInitializeProcedures(modelId, paraConfig);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("加载DSL文件错误", ex);
                MessageHelper.MessageOK($"加载DSL文件错误：{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 加载和初始化程序
        /// </summary>
        /// <param name="modelId">型号ID</param>
        /// <param name="paraConfig">系统参数类</param>
        private void LoadAndInitializeProcedures(int modelId, ParaConfig paraConfig)
        {
            var testStep = new TestStepBLL();
            var testItems = testStep.GetTestItems(modelId);

            if (testItems.Count == 0)
            {
                MessageHelper.MessageOK("未找到测试项目");
                return;
            }

            var visitor = DSLFactory.CreateVisitor();
            var moduleParameters = new Dictionary<string, object>
            {
                ["参数"] = paraConfig
            };

            string modelName = VarHelper.TestViewModel.ModelName;
            foreach (var testItem in testItems)
            {
                LoadSingleProcedure(testItem, modelName, moduleParameters, visitor);
            }
        }

        /// <summary>
        /// 加载单个程序
        /// </summary>
        /// <param name="testItem">根据型号ID配置好的步骤表[TestStep]</param>
        /// <param name="modelName">DSL名称</param>
        /// <param name="moduleParameters">自定义模块集合</param>
        /// <param name="visitor"></param>
        private void LoadSingleProcedure(TestStepNewModel testItem, string modelName,
            Dictionary<string, object> moduleParameters, object visitor = null)
        {
            string processName = testItem.ProcessName;
            string dslFilePath = Application.StartupPath + Path.Combine(dslPath, modelName, $"{processName}.rw1");

            if (!File.Exists(dslFilePath))
            {
                MessageHelper.MessageOK($"试验项点：{processName},找不到自动试验文件！");
                return;
            }

            var procedure = DSLFactory.CreateProcedure(dslFilePath);
            procedure.AddModules(moduleParameters);
            procedure.AddModules(ModuleComponent.Instance.GetList());
            procedure.DomainEventInvoked += (sender, name, output) =>
            {
                try
                {
                    if (name == "试验提示")
                    {
                        // 处理试验提示
                    }
                    Debug.WriteLine($"监听事件：{name}  值数量：{output.Count}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("事件触发错误：" + ex.Message);
                }
            };

            _procedureModules.Add(processName, procedure);
        }

        /// <summary>
        /// 获取指定名称的程序
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DSLProcedure GetProcedure(string name)
        {
            return _procedureModules.TryGetValue(name, out var procedure) ? procedure : null;
        }

        /// <summary>
        /// 确定集合中是否存在具有指定名称的过程
        /// </summary>
        /// <param name="name">检查是否存在的程序的名称</param>
        public bool HasProcedure(string name) => _procedureModules.ContainsKey(name);
    }
}