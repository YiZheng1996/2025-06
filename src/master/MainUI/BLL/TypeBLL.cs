namespace MainUI.BLL
{
    public class TypeBLL
    {
        public List<Models> GetAllKind(int id) => VarHelper.fsql.Select<Models>()
            .Where(w => w.TypeID == id)
            .ToList();


        public bool IsExist(int typeid, string modelname) =>
            VarHelper.fsql.Select<Models>()
            .Where(a => a.TypeID == typeid && a.Name == modelname)
            .ToList().Count > 0;

        public bool Add(string modelName, int typeid, string mark, string lxname)
        {
            //NewFile(SpecificSymbol(lxname), SpecificSymbol(modelName));
            return VarHelper.fsql.Insert<Models>().AppendData(new Models
            {
                Name = modelName,
                TypeID = typeid,
                Mark = mark,
                CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            }).ExecuteAffrows() > 0;
        }

        public bool Delete(int modelID, string lxname, string xhname)
        {
            //deleteFile(SpecificSymbol(lxname), SpecificSymbol(xhname));
            return VarHelper.fsql.Delete<Models>()
            .Where(a => a.ID == modelID)
            .ExecuteAffrows() > 0;
        }

        public bool Update(int modelID, string name, int typeid, string mark, string lxname, string oldxhname)
        {
            //changeFileName(SpecificSymbol(name), SpecificSymbol(oldxhname), SpecificSymbol(lxname));
            return VarHelper.fsql.Update<Models>()
            .Set(a => a.Name, name)
            .Set(a => a.TypeID, typeid)
            .Set(a => a.Mark, mark)
            .Set(a => a.UpdateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            .Where(a => a.ID == modelID)
            .ExecuteAffrows() > 0;
        }


        void NewFile(string LX, string newName)
        {
            string rootDirectory = Application.StartupPath + "\\proc\\" + LX + "\\";
            bool s = AddFileName(rootDirectory + newName, false);
            if (s)
                MoveStepPara(LX, newName);
        }
        public bool AddFileName(string newFile, bool isFile)
        {
            if (isFile && !System.IO.File.Exists(newFile))
            {
                System.IO.File.Create(newFile);
            }

            if (!isFile && !System.IO.Directory.Exists(newFile))
            {
                System.IO.Directory.CreateDirectory(newFile);
            }

            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="LX">类型</param>
        /// <param name="filename">型号名称</param>
        void deleteFile(string LX, string filename)
        {
            string rootDirectory = Application.StartupPath + "\\proc\\" + LX + "\\";
            string path = rootDirectory + filename;
            bool s = DelFileName(path);
        }
        public bool DelFileName(string fileName)
        {
            try
            {
                if (System.IO.Directory.Exists(fileName))
                {
                    System.IO.Directory.Delete(fileName, true);

                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 修改名称
        /// </summary>
        /// <param name="filename">新名称</param>
        /// <param name="oldname">旧名称</param>
        /// <param name="LX">类型</param>
        void changeFileName(string filename, string oldname, string LX)
        {
            string rootDirectory = Application.StartupPath + "proc\\" + LX + "\\";
            string path = rootDirectory + oldname;
            if (Directory.Exists(path))
                Microsoft.VisualBasic.FileIO.FileSystem.RenameDirectory(path, filename);

        }
        /// <summary>
        /// 
        /// </summary>
        void MoveStepPara(string LXname, string xhname)
        {
            string rootDirectory = Application.StartupPath + "\\proc\\" + LXname;
            string path = rootDirectory + "\\" + xhname;
            var a = System.IO.Directory.GetFiles(rootDirectory);
            foreach (var item in a)
            {
                string fileName = Path.GetFileName(item);
                string targetPath = Path.Combine(path, fileName);
                FileInfo file = new FileInfo(item);
                if (file.Exists)
                {
                    file.CopyTo(targetPath, true);
                }

            }

        }
        public string SpecificSymbol(string gg)
        {
            while (true)
            {
                if (gg.IndexOf("%") > -1)
                {
                    int i = gg.IndexOf("%");
                    string a = gg.Substring(0, i);
                    string b = gg.Substring(i + 1, gg.Length - i - 1);
                    gg = a + b;
                }
                else if (gg.IndexOf(":") > -1)
                {
                    int i = gg.IndexOf(":");
                    string a = gg.Substring(0, i);
                    string b = gg.Substring(i + 1, gg.Length - i - 1);
                    gg = a + b;
                }
                else if (gg.IndexOf("/") > -1)
                {
                    int i = gg.IndexOf("/");
                    string a = gg.Substring(0, i);
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
    }
}
