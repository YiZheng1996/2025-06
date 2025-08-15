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

        /// <summary>
        /// ataGridView中指定列是否有重复值
        /// </summary>
        /// <param name="dataGridView">控件名</param>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        public static bool HasDuplicateValuesInColumn(DataGridView dataGridView, string columnName)
        {
            // 使用LINQ查询找到重复的项
            var duplicateValues = dataGridView.Rows
                .Cast<DataGridViewRow>() // 将Rows集合转换为IEnumerable<DataGridViewRow>
                .Select(row => row.Cells[columnName].Value) // 选择指定列的值
                .GroupBy(value => value) // 对值进行分组
                .Where(group => group.Count() > 1) // 找到有多个的组（即重复的值）
                .Any(); // 如果有任何重复，返回true

            return duplicateValues;
        }
    }
}
