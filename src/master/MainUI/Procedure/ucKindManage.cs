using AntdUI;
using MainUI.Model;
using System.ComponentModel;

namespace MainUI.Procedure
{
    public partial class ucKindManage : ucBaseManagerUI
    {
        private readonly ModelBLL modelBLL = new();
        private System.ComponentModel.BindingList<ModelsType> NewModels;
        private ModelsType modelsType = new();
        private readonly ModelTypeBLL modelTypeBLL = new();

        public ucKindManage() => InitializeComponent();

        private void ucModelManage_Load(object sender, EventArgs e) => LoadData();

        private void LoadData()
        {
            try
            {
                NewModels = new BindingList<ModelsType>();

                // 配置树形表格样式
                Tables.TreeArrowStyle = TableTreeStyle.Button;
                Tables.DefaultExpand = false; // 默认不展开
                Tables.TreeButtonSize = 25;

                // 定义列结构 - 关键是KeyTree的配置
                Tables.Columns = [
                    new Column("ID", "ID") { Align = ColumnAlign.Center, KeyTree = "", Visible = false },
                    new Column("ModelTypeName", "名称") { Align = ColumnAlign.Center, KeyTree = "NewModels", Width = "200" },
                    new Column("Mark", "备注/图号") { Align = ColumnAlign.Center, Width = "auto" },
                    new Column("CreateTime", "创建时间") { Align = ColumnAlign.Center, Width = "150" },
                    new Column("IsRelease", "状态") { Align = ColumnAlign.Center, Width = "100" }
                ];

                // 加载主表数据（产品类型）
                LoadMainData();

                Tables.Binding(NewModels);
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"加载数据时出错：{ex.Message}");
            }
        }

        private void LoadMainData()
        {
            // 获取主表数据
            List<ModelsType> mainDataList = modelTypeBLL.GetAllModelType();

            foreach (var mainItem in mainDataList)
            {
                // 为每个主类型加载子数据
                List<NewModels> childDataList = modelTypeBLL.GetNewModels(mainItem.ID, true);

                // 转换为适合显示的格式
                var childModels = childDataList.Select(child => new NewModels
                {
                    ID = child.ID,
                    ModelName = child.ModelName,
                    ModelTypeName = child.ModelName, // 在子行中显示型号名称
                    Mark = child.DrawingNo ?? child.Mark ?? "", // 显示图号或备注
                    CreateTime = child.CreateTime ?? "",
                    IsRelease = child.IsRelease,
                    TypeID = child.TypeID,
                    DrawingNo = child.DrawingNo,
                    ReleaseTime = child.ReleaseTime,
                    UpdateTime = child.UpdateTime,
                    IsDeleted = child.IsDeleted
                }).ToArray();

                // 设置主表项的子数据
                mainItem.NewModels = childModels;

                NewModels.Add(mainItem);
            }
        }

        private void Tables_CellClick(object sender, TableClickEventArgs e)
        {
            if (e.Record is ModelsType model)
            {
                modelsType = model;
            }
            else if (e.Record is NewModels childModel)
            {
                // 如果点击的是子项，找到对应的主类型
                var parentModel = NewModels.FirstOrDefault(m => m.ID == childModel.TypeID);
                if (parentModel != null)
                {
                    modelsType = parentModel;
                }
            }
        }

        private void Tables_CellDoubleClick(object sender, TableClickEventArgs e)
        {
            if (e.Record is ModelsType model)
            {
                EditMainData(model);
            }
            else if (e.Record is NewModels childModel)
            {
                EditChildData(childModel);
            }
        }

        private void EditMainData(ModelsType model)
        {
            using frmModelTypeEdit edit = new(model);
            if (edit.ShowDialog() == DialogResult.OK)
            {
                LoadData(); // 重新加载数据
            }
        }

        private void EditChildData(NewModels model)
        {
            // 转换为Models对象
            var modelsObj = new Models
            {
                ID = model.ID,
                ModelName = model.ModelName,
                Mark = model.Mark,
                TypeID = model.TypeID,
                CreateTime = model.CreateTime,
                UpdateTime = model.UpdateTime,
                IsDeleted = model.IsDeleted,
                DrawingNo = model.DrawingNo,
                IsRelease = model.IsRelease,
                ReleaseTime = model.ReleaseTime
            };

            using frmModelEdit edit = new(modelsObj);
            if (edit.ShowDialog() == DialogResult.OK)
            {
                LoadData(); // 重新加载数据
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var selectedData = Tables.FocusedRow;
            if (Tables.SelectedIndex >= 0 && selectedData is ModelsType selectedType)
            {
                // 为选中的主类型添加子型号
                AddChildData(selectedType);
            }
            else
            {
                // 添加新的主类型
                EditMainData(null);
            }
        }

        private void AddChildData(ModelsType parentType)
        {
            var newModel = new Models
            {
                TypeID = parentType.ID,
                ModelName = "",
                Mark = "",
                DrawingNo = "",
                CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                IsDeleted = false,
                IsRelease = false,
                ReleaseTime = "未发布"
            };

            using frmModelEdit edit = new(newModel);
            if (edit.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (Tables.SelectedIndex < 0)
            {
                MessageHelper.MessageOK("请先选择要编辑的记录");
                return;
            }

            // 使用FocusedRow代替SelectedData
            var selectedData = Tables.FocusedRow; 
            if (selectedData is ModelsType mainData)
            {
                EditMainData(mainData);
            }
            else if (selectedData is NewModels childData)
            {
                EditChildData(childData);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedData = Tables.FocusedRow; 
            if (selectedData == null)
            {
                MessageHelper.MessageOK("请先选择要删除的记录");
                return;
            }

            string itemType = "";
            bool success = false;

            if (selectedData is ModelsType mainData)
            {
                itemType = "产品类型";
                var result = MessageHelper.MessageYes($"确认删除类型 [{mainData.ModelTypeName}] 及其所有型号？", TType.Warn);
                if (result == DialogResult.OK)
                {
                    success = modelTypeBLL.Delete(mainData.ID, mainData.ModelTypeName);
                }
            }
            else if (selectedData is NewModels childData)
            {
                itemType = "产品型号";
                var result = MessageHelper.MessageYes($"确认删除型号 [{childData.ModelName}]？", TType.Warn);
                if (result == DialogResult.OK)
                {
                    success = modelBLL.Delete(childData.ID);
                }
            }

            if (success)
            {
                MessageHelper.MessageOK("删除成功！");
                LoadData();
            }
            else if (selectedData != null)
            {
                MessageHelper.MessageOK("删除失败！");
            }
        }

        // 展开/折叠所有节点
        private void btnExpandAll_Click(object sender, EventArgs e)
        {
            Tables.DefaultExpand = !Tables.DefaultExpand;
            LoadData();
        }

        // 发布选中的型号
        private void btnRelease_Click(object sender, EventArgs e)
        {
            var selectedData = Tables.FocusedRow; 
            if (!(selectedData is NewModels childModel))
            {
                MessageHelper.MessageOK("请选择要发布的产品型号");
                return;
            }

            if (childModel.IsRelease)
            {
                MessageHelper.MessageOK("该型号已经发布");
                return;
            }

            var result = MessageHelper.MessageYes($"确认发布型号 [{childModel.ModelName}] ？", TType.Info);
            if (result == DialogResult.OK)
            {
                //bool success = modelBLL.Release(childModel.ID);
                //if (success)
                //{
                //    MessageHelper.MessageOK("发布成功！");
                //    LoadData();
                //}
                //else
                //{
                //    MessageHelper.MessageOK("发布失败！");
                //}
            }
        }

        // 格式化显示内容
        //private void Tables_CellFormatting(object sender, TableCellEventArgs e)
        //{
        //    // 根据数据类型调整显示内容
        //    if (e.Record is ModelsType mainType)
        //    {
        //        switch (e.Column.Key)
        //        {
        //            case "CreateTime":
        //                e.Value = "—"; // 主类型不显示创建时间
        //                break;
        //            case "IsRelease":
        //                e.Value = "主分类"; // 主类型显示"主分类"
        //                break;
        //        }
        //    }
        //    else if (e.Record is NewModels childModel)
        //    {
        //        switch (e.Column.Key)
        //        {
        //            case "IsRelease":
        //                e.Value = childModel.IsRelease ? "已发布" : "未发布";
        //                break;
        //        }
        //    }
        //}
    }
}