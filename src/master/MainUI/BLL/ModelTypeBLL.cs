namespace MainUI.BLL
{
    public class ModelTypeBLL
    {
        /// <summary>
        /// 查找所有类型表列表
        /// </summary>
        /// <returns></returns>
        public List<ModelsType> GetAllModelType() =>
            VarHelper.fsql.Select<ModelsType>().ToList();

        /// <summary>
        /// 确定数据源中是否存在具有指定类型名称的模型
        /// </summary>
        /// <param name="TypeName"></param>
        /// <returns></returns>
        public bool IsExist(string TypeName) =>
            VarHelper.fsql
            .Select<ModelsType>()
            .Where(a => a.ModelTypeName == TypeName)
            .ToList().Count > 0;

        /// <summary>
        /// 添加类型表数据
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool Add(ModelsType models)
        {
            //NewFile(SpecificSymbol(TypeName));
            return VarHelper.fsql.Insert<ModelsType>().AppendData(new ModelsType
            {
                ModelTypeName = models.ModelTypeName,
                Mark = models.Mark
            }).ExecuteAffrows() > 0;
        }

        /// <summary>
        /// 删除类型表数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Delete(int id, string name = null)
        {
            //deleteFile(SpecificSymbol(name));
            return VarHelper.fsql.Delete<ModelsType>()
              .Where(a => a.ID == id)
              .ExecuteAffrows() > 0;
        }

        /// <summary>
        /// 修改类型表数据
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool Updata(ModelsType models)
        {
            //changeFileName(SpecificSymbol(name), SpecificSymbol(OldName));
            return VarHelper.fsql.Update<ModelsType>()
            .Set(a => a.ModelTypeName, models.ModelTypeName)
            .Set(a => a.Mark, models.Mark)
            .Where(a => a.ID == models.ID)
            .ExecuteAffrows() > 0;
        }

        /// <summary>
        /// 查看所有类型列表
        /// </summary>
        /// <returns></returns>
        public List<ModelsType> GetModels()
        {
            return VarHelper.fsql.Select<ModelsType>().ToList();
        }

        /// <summary>
        /// 查找所有型号列表
        /// </summary>
        /// <returns></returns>
        public List<Models> GetAllModels()
        {
            return VarHelper.fsql.Select<Models>().ToList();
        }

        /// <summary>
        /// 查找指定类型ID的所有型号
        /// </summary>
        /// <param name="typeID">类型表ID</param>
        /// <param name="IsRelease">是否查看未发布型号 
        /// false只查看发布型号 true查看所有型号</param>
        /// <returns></returns>
        public List<NewModels> GetNewModels(int typeID, bool IsRelease = false)
        {
            return VarHelper.fsql.Select<Models, ModelsType>()
                .LeftJoin((m, t) => m.TypeID == t.ID)
                .Where((m, t) => m.TypeID == typeID)
                .WhereIf(!IsRelease, (m, t) => m.IsRelease)
                .ToList((m, t) => new NewModels
                {
                    ModelTypeID = t.ID,
                    ModelTypeName = t.ModelTypeName,
                });
        }

        #region 其余

        private void NewFile(string newName)
        {
            string rootDirectory = Application.StartupPath + "\\proc\\";
            bool s = AddFileName(rootDirectory + newName, false);
        }

        public bool AddFileName(string newFile, bool isFile)
        {
            if (isFile && !File.Exists(newFile))
            {
                File.Create(newFile);
            }

            if (!isFile && !Directory.Exists(newFile))
            {
                Directory.CreateDirectory(newFile);
            }

            return true;
        }
        void deleteFile(string filename)
        {
            string rootDirectory = Application.StartupPath + "\\proc\\";
            string path = rootDirectory + filename;
            bool s = DelFileName(path);
        }
        public bool DelFileName(string fileName)
        {
            try
            {
                if (Directory.Exists(fileName))
                {
                    Directory.Delete(fileName, true);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        void changeFileName(string filename, string oldname)
        {
            string rootDirectory = Application.StartupPath + "\\proc\\";
            string path = rootDirectory + oldname;
            if (!System.IO.Directory.Exists(path))
                Microsoft.VisualBasic.FileIO.FileSystem.RenameDirectory(path, filename);
        }

        public string SpecificSymbol(string gg)
        {
            while (true)
            {
                if (gg.IndexOf('%') > -1)
                {
                    int i = gg.IndexOf('%');
                    string a = gg[..i];
                    string b = gg.Substring(i + 1, gg.Length - i - 1);
                    gg = a + b;
                }
                else if (gg.IndexOf(':') > -1)
                {
                    int i = gg.IndexOf(':');
                    string a = gg.Substring(0, i);
                    string b = gg.Substring(i + 1, gg.Length - i - 1);
                    gg = a + b;
                }
                else if (gg.IndexOf('/') > -1)
                {
                    int i = gg.IndexOf('/');
                    string a = gg[..i];
                    string b = gg.Substring(i + 1, gg.Length - i - 1);
                    gg = a + b;
                }
                else
                {
                    break;
                }
            }
            return gg;
        }

        #endregion
    }
}
