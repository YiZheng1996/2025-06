namespace MainUI.BLL
{
    public class ModelBLL
    {
        public List<Models> GetAllKind(int id) => VarHelper.fsql.Select<Models>()
            .Where(w => w.TypeID == id)
            .ToList();


        public bool IsExist(int typeid, string modelname) =>
            VarHelper.fsql.Select<Models>()
            .Where(a => a.TypeID == typeid && a.ModelName == modelname)
            .ToList().Count > 0;

        public bool Add(Models models, string lxname = null)
        {
            //NewFile(SpecificSymbol(lxname), SpecificSymbol(modelName));
            return VarHelper.fsql.Insert<Models>().AppendData(new Models
            {
                ModelName = models.ModelName,
                TypeID = models.TypeID,
                Mark = models.Mark,
                DrawingNo = models.DrawingNo,
                ReleaseTime = "未发布",
                CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            }).ExecuteAffrows() > 0;
        }

        public bool Delete(int modelID, string lxname = null, string xhname = null)
        {
            //deleteFile(SpecificSymbol(lxname), SpecificSymbol(xhname));
            return VarHelper.fsql.Delete<Models>()
            .Where(a => a.ID == modelID)
            .ExecuteAffrows() > 0;
        }

        public bool Update(Models models, string lxname = null, string oldxhname = null)
        {
            //changeFileName(SpecificSymbol(name), SpecificSymbol(oldxhname), SpecificSymbol(lxname));
            return VarHelper.fsql.Update<Models>()
            .Set(a => a.ModelName, models.ModelName)
            .Set(a => a.TypeID, models.TypeID)
            .Set(a => a.Mark, models.Mark)
            .Set(a => a.DrawingNo, models.DrawingNo)
            .Set(a => a.UpdateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            .Where(a => a.ID == models.ID)
            .ExecuteAffrows() > 0;
        }

        /// <summary>
        /// 修改型号为发布状态
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool IsRelease(Models models)
        {
            return VarHelper.fsql.Update<Models>()
            .Set(a => a.IsRelease, true)
            .Set(a => a.ReleaseTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            .Where(a => a.ID == models.ID)
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
