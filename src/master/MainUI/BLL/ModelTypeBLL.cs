namespace MainUI.BLL
{
    public class ModelTypeBLL
    {
        public List<ModelsType> GetAllModelType() =>
            VarHelper.fsql.Select<ModelsType>().ToList();


        public bool IsExist(string TypeName) =>
            VarHelper.fsql
            .Select<ModelsType>()
            .Where(a => a.ModelTypeName == TypeName)
            .ToList().Count > 0;


        public bool Add(ModelsType models)
        {
            //NewFile(SpecificSymbol(TypeName));
            return VarHelper.fsql.Insert<ModelsType>().AppendData(new ModelsType
            {
                ModelTypeName = models.ModelTypeName,
                Mark = models.Mark
            }).ExecuteAffrows() > 0;
        }

        public bool Delete(int id, string name = null)
        {
            //deleteFile(SpecificSymbol(name));
            return VarHelper.fsql.Delete<ModelsType>()
              .Where(a => a.ID == id)
              .ExecuteAffrows() > 0;
        }


        public bool Updata(ModelsType models)
        {
            //changeFileName(SpecificSymbol(name), SpecificSymbol(OldName));
            return VarHelper.fsql.Update<ModelsType>()
            .Set(a => a.ModelTypeName, models.ModelTypeName)
            .Set(a => a.Mark, models.Mark)
            .Where(a => a.ID == models.ID)
            .ExecuteAffrows() > 0;
        }

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

        public List<ModelsType> GetModels()
        {
            return VarHelper.fsql.Select<ModelsType>().ToList();
        }

        public List<Models> GetAllModels()
        {
            return VarHelper.fsql.Select<Models>().ToList();
        }

        public List<NewModels> GetNewModels(int typeID)
        {
            return VarHelper.fsql.Select<Models, ModelsType>()
                .LeftJoin((m, t) => m.TypeID == t.ID)
                .Where((m, t) => m.TypeID == typeID)
                .ToList((m, t) => new NewModels
                {
                    ModelTypeID = t.ID,
                    ModelTypeName = t.ModelTypeName,
                });
        }
    }
}
