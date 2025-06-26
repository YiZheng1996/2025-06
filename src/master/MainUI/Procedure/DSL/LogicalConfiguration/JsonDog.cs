using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MainUI.Procedure.DSL.LogicalConfiguration
{
    internal class JsonDog
    {
        //json文件需要是utf8格式
        //private static string jsonPath = Path.Combine(Environment.CurrentDirectory, "config", "init.json");
        public static string filePath;
        //private static Encoding encoding = Encoding.GetEncoding("GBK");
        //private static Encoding encoding = Encoding.GetEncoding("UTF8");


        public static void AddParent(SingletonStatus singleton)
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            string jsonString = File.ReadAllText(filePath);
            //string jsonString = File.ReadAllText(filePath, encoding);
            JObject json = JObject.Parse(jsonString);
            JArray parent = json["Form"] as JArray;
            Parent bc = new()
            {
                ModelTypeName = singleton.ModelName,
                ModelName = singleton.ModelTypeName,
                ItemName = singleton.ItemName,
            };
            string addString = JsonConvert.SerializeObject(bc, Formatting.Indented);
            JObject item = JObject.Parse(addString);
            parent.Add(item);
            string updatedJsonString = json.ToString(Formatting.Indented);
            File.WriteAllText(filePath, updatedJsonString);

        }
        public static void DeleteParent(SingletonStatus singleton)
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            string jsonString = File.ReadAllText(filePath);
            //string jsonString = File.ReadAllText(filePath, encoding);
            JObject json = JObject.Parse(jsonString);
            JArray parent = json["Form"] as JArray;
            JToken b = parent.FirstOrDefault(p => (string)p["parentName"] == singleton.ModelName && (string)p["childName"] == singleton.ModelTypeName);
            parent.Remove(b);
            string updatedJsonString = json.ToString(Formatting.Indented);
            File.WriteAllText(filePath, updatedJsonString);
        }

        public static List<Parent> ReadParent()
        {
            if (!File.Exists(filePath))
            {
                return null;
            }
            string jsonString = File.ReadAllText(filePath);
            JObject json = JObject.Parse(jsonString);
            if (json["Form"] is not JArray parent)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<List<Parent>>(parent.ToString());
        }
        public static void ReviseParent()
        {

        }
        public static void AddChild(SingletonStatus singleton, ChildModel item)
        {
            if (!File.Exists(filePath)) return;
            string jsonString = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(jsonString)) return;

            //string jsonString = File.ReadAllText(filePath, encoding);
            JObject json = JObject.Parse(jsonString);
            JArray parent = json["Form"] as JArray;
            JToken b = parent.FirstOrDefault(p => (string)p["parentName"] ==
                singleton.ModelName && (string)p["childName"] == singleton.ModelTypeName);
            if (b["childSteps"] is not JArray child)
            {
                child = [];
            }
            string addString = JsonConvert.SerializeObject(item, Formatting.Indented);
            JObject childItem = JObject.Parse(addString);
            child.Add(childItem);
            b["childSteps"] = child;
            string updatedJsonString = json.ToString(Formatting.Indented);
            File.WriteAllText(filePath, updatedJsonString);
        }
        public static void DeleteChild(SingletonStatus singleton, int index)
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            //string jsonString = File.ReadAllText(filePath, encoding);
            string jsonString = File.ReadAllText(filePath);
            JObject json = JObject.Parse(jsonString);
            JArray parent = json["Form"] as JArray;
            JToken b = parent.FirstOrDefault(p => (string)p["parentName"] == singleton.ModelName && (string)p["childName"] == singleton.ModelTypeName);
            JArray child = b["childSteps"] as JArray;
            if (child == null)
            {
                return;
            }
            child.RemoveAt(index);
            b["childSteps"] = child;
            string updatedJsonString = json.ToString(Formatting.Indented);
            File.WriteAllText(filePath, updatedJsonString);
        }

        /// <summary>
        /// 查找每个步骤的详细信息
        /// </summary>
        /// <param name="singleton"></param>
        /// <returns></returns>
        public static List<ChildModel> ReadChild(SingletonStatus singleton)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            List<Parent> basicClasses = ReadParent();
            if (basicClasses == null)
            {
                return null;
            }

            // 根据产品类型、产品型号、试验项点名称匹配，搜索是否存在试验步骤
            Parent b = basicClasses.FirstOrDefault(
                p =>
                p.ModelTypeName == singleton.ModelTypeName &&
                p.ModelName == singleton.ModelName &&
                p.ItemName == singleton.ItemName
            );
            if (b == null)
            {
                return null;
            }
            return b.ChildSteps;
        }
   
        public static void AddParameter(SingletonStatus singleton, Object obj_parameter)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            string jsonString = File.ReadAllText(filePath);
            JObject json = JObject.Parse(jsonString);
            JArray parent = json["Form"] as JArray;
            JToken b = parent.FirstOrDefault(
                p => 
                (string)p["ModelTypeName"] == singleton.ModelTypeName && 
                (string)p["ModelName"] == singleton.ModelName &&
                (string)p["ItemName"] == singleton.ItemName
            );
            JArray child = b["childSteps"] as JArray;
            string addString = JsonConvert.SerializeObject(obj_parameter, Formatting.Indented);
            JObject stepItem = JObject.Parse(addString);
            child[singleton.StepNum]["stepParameter"] = stepItem;
            string updatedJsonString = json.ToString(Formatting.Indented);
            File.WriteAllText(filePath, updatedJsonString);
        }

        public static void DeleteParameter()
        {

        }

        public static object ReadParameter(List<ChildModel> li, int index)
        {
            return li[index].StepParameter;
        }

        public static void ReviseParameter()
        {

        }

        public static List<Parent> ReadList(string _name)
        {
            List<Parent> res = [];
            List<Parent> li = ReadParent();
            if (li == null)
            {
                return null;
            }
            foreach (Parent c in li)
            {
                if (c.ModelTypeName == _name)
                {
                    res.Add(c);
                }

            }
            return res;
        }

        public static Parent ReadChild(string _name)
        {
            List<Parent> li = ReadParent();
            if (li == null)
            {
                return null;
            }
            foreach (Parent c in li)
            {
                if (c.ModelName == _name)
                {
                    return c;
                }

            }
            return null;
        }
        public static void WriteKepServerChannelName(string _name)
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            //string jsonString = File.ReadAllText(filePath, encoding);
            string jsonString = File.ReadAllText(filePath);
            JObject json = JObject.Parse(jsonString);
            json["opc"]["channel"] = _name;
            string updatedJsonString = json.ToString(Formatting.Indented);
            File.WriteAllText(filePath, updatedJsonString);
        }
        public static void WriteKepServerDeviceName(string _name)
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            //string jsonString = File.ReadAllText(filePath, encoding);
            string jsonString = File.ReadAllText(filePath);
            JObject json = JObject.Parse(jsonString);
            json["opc"]["device"] = _name;
            string updatedJsonString = json.ToString(Formatting.Indented);
            File.WriteAllText(filePath, updatedJsonString);
        }
        public static void WriteKepServerGroupName(string _name)
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            //string jsonString = File.ReadAllText(filePath, encoding);
            string jsonString = File.ReadAllText(filePath);
            JObject json = JObject.Parse(jsonString);
            json["opc"]["group"] = _name;
            string updatedJsonString = json.ToString(Formatting.Indented);
            File.WriteAllText(filePath, updatedJsonString);
        }

        public static string ReadKepServerGroupName()
        {
            if (!File.Exists(filePath))
            {
                return "";
            }
            //string jsonString = File.ReadAllText(filePath, encoding);
            string jsonString = File.ReadAllText(filePath);
            JObject json = JObject.Parse(jsonString);
            return json["opc"]["group"].ToString();
        }
        public static string ReadKepServerChannelName()
        {
            if (!File.Exists(filePath))
            {
                return "";
            }
            //string jsonString = File.ReadAllText(filePath, encoding);
            string jsonString = File.ReadAllText(filePath);
            JObject json = JObject.Parse(jsonString);
            return json["opc"]["channel"].ToString();
        }
        public static string ReadKepServerDeviceName()
        {
            if (!File.Exists(filePath))
            {
                return "";
            }
            //string jsonString = File.ReadAllText(filePath, encoding);
            string jsonString = File.ReadAllText(filePath);
            JObject json = JObject.Parse(jsonString);
            return json["opc"]["device"].ToString();
        }

     
        /// <summary>
        /// 获取变量列表
        /// </summary>
        /// <returns></returns>
        public static List<VarItem> ReadVarItems()
        {
            if (!File.Exists(filePath))
            {
                return null;
            }
            //string jsonString = File.ReadAllText(filePath, encoding);
            string jsonString = File.ReadAllText(filePath);
            JObject json = JObject.Parse(jsonString);
            if (json["Variable"] is not JArray items)
            {
                return null;
            }
            List<VarItem> li = new List<VarItem>();
            li = JsonConvert.DeserializeObject<List<VarItem>>(items.ToString());
            return li;
        }

        /// <summary>
        /// 读取变量项位置
        /// </summary>
        /// <param name="varItemName"></param>
        /// <returns></returns>
        public static int ReadVarItemPosition(string varItemName)
        {
            int position = 0;
            if (!File.Exists(filePath))
            {
                return position;
            }

            string jsonString = File.ReadAllText(filePath);
            JObject json = JObject.Parse(jsonString);
            if (json["variable"] is not JArray items)
            {
                return position;
            }

            List<VarItem> li = JsonConvert.DeserializeObject<List<VarItem>>(items.ToString());
            foreach (VarItem k in li)
            {
                if (k.VarName == varItemName)
                {
                    return position;
                }
                position++;
            }
            return position;
        }

        //public static void WriteVarItems(VarItem varItem)
        //{
        //    if (!File.Exists(filePath))
        //    {
        //        return;
        //    }
        //    //string jsonString = File.ReadAllText(filePath, encoding);
        //    string jsonString = File.ReadAllText(filePath);
        //    JObject json = JObject.Parse(jsonString);
        //    JArray parent = json["variable"] as JArray;
        //    string addString = JsonConvert.SerializeObject(varItem, Formatting.Indented);
        //    JObject item = JObject.Parse(addString);
        //    parent.Add(item);
        //    string updatedJsonString = json.ToString(Formatting.Indented);
        //    File.WriteAllText(filePath, updatedJsonString);
        //}
        //public static void ReferishVarItems()
        //{
        //    if (!File.Exists(filePath))
        //    {
        //        return;
        //    }
        //    string jsonString = File.ReadAllText(filePath);
        //    JObject json = JObject.Parse(jsonString);
        //    List<KepServerItem> li = new List<KepServerItem>();
        //    json["variable"] = new JArray();
        //    string updatedJsonString = json.ToString(Formatting.Indented);
        //    File.WriteAllText(filePath, updatedJsonString);
        //}

        public static List<FormStr> ReadFormString()
        {
            string sysPath = Path.Combine(Environment.CurrentDirectory, "init.json");
            if (!File.Exists(sysPath))
            {
                return null;
            }
            //string jsonString = File.ReadAllText(filePath, encoding);
            string jsonString = File.ReadAllText(sysPath);
            JObject json = JObject.Parse(jsonString);
            JArray items = json["FormString"] as JArray;
            if (items == null)
            {
                return null;
            }
            List<FormStr> li = new List<FormStr>();
            li = JsonConvert.DeserializeObject<List<FormStr>>(items.ToString());
            return li;
        }
    }
}
