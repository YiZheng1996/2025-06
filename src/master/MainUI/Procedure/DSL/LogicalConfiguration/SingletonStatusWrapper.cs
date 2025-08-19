using MainUI.Procedure.DSL.LogicalConfiguration.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MainUI.Procedure.DSL.LogicalConfiguration
{
    /// <summary>
    /// SingletonStatus 兼容性包装器
    /// 
    /// 这个类的目的：
    /// 1. 保持向后兼容性，现有代码不需要立即修改
    /// 2. 内部使用新的 IWorkflowStateService
    /// 3. 提供渐进式迁移路径
    /// 4. 迁移完成后可以删除这个类
    /// 
    /// 使用说明：
    /// - 现有代码可以继续使用 SingletonStatus.Instance
    /// - 新代码应该使用依赖注入的 IWorkflowStateService
    /// - 逐步将现有代码迁移到新接口
    /// </summary>
    public class SingletonStatusWrapper
    {
        private static readonly Lazy<SingletonStatusWrapper> _instance =
            new(() => new SingletonStatusWrapper());

        private readonly IWorkflowStateService _workflowState;

        /// <summary>
        /// 获取单例实例（兼容性方法）
        /// 
        /// 注意：在新代码中不要使用这个属性
        /// 应该通过依赖注入获取 IWorkflowStateService
        /// </summary>
        public static SingletonStatusWrapper Instance => _instance.Value;

        /// <summary>
        /// 私有构造函数
        /// 在实际应用中，这里需要从某个地方获取 IWorkflowStateService 实例
        /// 可能需要一个服务定位器模式或其他方式
        /// </summary>
        private SingletonStatusWrapper()
        {
            // 这里需要根据实际情况获取 IWorkflowStateService 实例
            // 例如：从一个全局的服务提供者获取
            _workflowState = ServiceLocator.GetService<IWorkflowStateService>();
        }

        #region 兼容性属性和方法

        /// <summary>
        /// 兼容性属性：T
        /// 委托给内部的 IWorkflowStateService
        /// </summary>
        public int T
        {
            get => _workflowState.T;
            set => _workflowState.T = value;
        }

        /// <summary>
        /// 兼容性属性：Status
        /// </summary>
        public bool Status
        {
            get => _workflowState.Status;
            set => _workflowState.Status = value;
        }

        /// <summary>
        /// 兼容性属性：StepNum
        /// </summary>
        public int StepNum
        {
            get => _workflowState.StepNum;
            set => _workflowState.StepNum = value;
        }

        /// <summary>
        /// 兼容性属性：ModelTypeName
        /// </summary>
        public string ModelTypeName
        {
            get => _workflowState.ModelTypeName;
            set => _workflowState.ModelTypeName = value;
        }

        /// <summary>
        /// 兼容性属性：ModelName
        /// </summary>
        public string ModelName
        {
            get => _workflowState.ModelName;
            set => _workflowState.ModelName = value;
        }

        /// <summary>
        /// 兼容性属性：ItemName
        /// </summary>
        public string ItemName
        {
            get => _workflowState.ItemName;
            set => _workflowState.ItemName = value;
        }

        /// <summary>
        /// 兼容性属性：StepName
        /// </summary>
        public string StepName
        {
            get => _workflowState.StepName;
            set => _workflowState.StepName = value;
        }

        /// <summary>
        /// 兼容性属性：Obj（变量列表）
        /// 
        /// 注意：这个属性返回的是动态列表的包装器
        /// 对返回列表的修改会反映到内部状态
        /// </summary>
        public CompatibilityVariableList Obj => new CompatibilityVariableList(_workflowState);

        /// <summary>
        /// 兼容性属性：IempSteps（步骤列表）
        /// 
        /// 注意：同样返回包装器，保持兼容性
        /// </summary>
        public CompatibilityStepList IempSteps => new CompatibilityStepList(_workflowState);

        /// <summary>
        /// 兼容性事件：MyStaticPropertyChanged
        /// 映射到新的 StepNumChanged 事件
        /// </summary>
        public event Action<int> MyStaticPropertyChanged
        {
            add => _workflowState.StepNumChanged += value;
            remove => _workflowState.StepNumChanged -= value;
        }

        #endregion
    }

    /// <summary>
    /// 变量列表的兼容性包装器
    /// 
    /// 这个类模拟 List&lt;object&gt; 的行为
    /// 但内部使用线程安全的 IWorkflowStateService
    /// </summary>
    public class CompatibilityVariableList
    {
        private readonly IWorkflowStateService _workflowState;

        internal CompatibilityVariableList(IWorkflowStateService workflowState)
        {
            _workflowState = workflowState;
        }

        /// <summary>
        /// 添加变量（兼容性方法）
        /// </summary>
        public void Add(object item)
        {
            _workflowState.AddVariable(item);
        }

        /// <summary>
        /// 移除变量（兼容性方法）
        /// </summary>
        public bool Remove(object item)
        {
            return _workflowState.RemoveVariable(item);
        }

        /// <summary>
        /// 清空变量（兼容性方法）
        /// </summary>
        public void Clear()
        {
            _workflowState.ClearVariables();
        }

        /// <summary>
        /// 获取指定类型的变量（兼容性方法）
        /// 这模拟了 LINQ 的 OfType&lt;T&gt; 方法
        /// </summary>
        public IEnumerable<T> OfType<T>() where T : class
        {
            return _workflowState.GetVariables<T>();
        }
    }

    /// <summary>
    /// 步骤列表的兼容性包装器
    /// </summary>
    public class CompatibilityStepList
    {
        private readonly IWorkflowStateService _workflowState;

        internal CompatibilityStepList(IWorkflowStateService workflowState)
        {
            _workflowState = workflowState;
        }

        /// <summary>
        /// 添加步骤（兼容性方法）
        /// </summary>
        public void Add(ChildModel item)
        {
            _workflowState.AddStep(item);
        }

        /// <summary>
        /// 移除步骤（兼容性方法）
        /// </summary>
        public bool Remove(ChildModel item)
        {
            return _workflowState.RemoveStep(item);
        }

        /// <summary>
        /// 清空步骤（兼容性方法）
        /// </summary>
        public void Clear()
        {
            _workflowState.ClearSteps();
        }

        /// <summary>
        /// 获取步骤数量（兼容性属性）
        /// </summary>
        public int Count => _workflowState.GetStepCount();

        /// <summary>
        /// 索引器（兼容性）
        /// </summary>
        public ChildModel this[int index] => _workflowState.GetStep(index);
    }

    /// <summary>
    /// 简单的服务定位器（临时解决方案）
    /// 
    /// 注意：这不是最佳实践，只是为了兼容性
    /// 在完整的 DI 容器中，应该有更好的解决方案
    /// </summary>
    public static class ServiceLocator
    {
        private static IServiceProvider _serviceProvider;

        public static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}