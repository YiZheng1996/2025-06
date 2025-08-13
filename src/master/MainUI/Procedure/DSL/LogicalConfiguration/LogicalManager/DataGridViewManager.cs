namespace MainUI.Procedure.DSL.LogicalConfiguration.LogicalManager
{

    /// <summary>
    /// DataGridView管理器类
    /// </summary>
    internal class DataGridViewManager(DataGridView grid, List<ChildModel> tempSteps)
    {

        /// <summary>
        /// 添加行数据到DataGridView
        /// </summary>
        /// <param name="stepName"></param>
        public void AddRow(string stepName)
        {
            grid.Rows.Add(stepName, grid.Rows.Count + 1);
        }

        /// <summary>
        /// 删除选中的行数据
        /// </summary>
        public void DeleteSelectedRow()
        {
            if (grid.SelectedRows.Count > 0)
            {
                var selectedRow = grid.SelectedRows[0];
                int index = selectedRow.Index;

                // 从临时存储中删除
                tempSteps.RemoveAt(index);

                // 从网格中删除
                grid.Rows.Remove(selectedRow);

                // 重新排序
                ReorderRows();
            }
        }

        /// <summary>
        /// 重新排序行
        /// </summary>
        private void ReorderRows()
        {
            // 更新网格中的步骤号
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                if (grid.Rows[i].Cells["ColStepNum"] != null)
                {
                    grid.Rows[i].Cells["ColStepNum"].Value = i + 1;
                }
            }

            // 更新临时存储中的步骤号
            for (int i = 0; i < tempSteps.Count; i++)
            {
                tempSteps[i].StepNum = i + 1;
            }
        }
    }
}
