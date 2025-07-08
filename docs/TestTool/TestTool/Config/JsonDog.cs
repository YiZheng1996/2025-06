using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTool.Forms;
using TestTool.Messages.Kepserverv6;
using TestTool.Test;

namespace TestTool.Config
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
            Parent bc = new Parent
            {
                parentName = singleton.parentName,
                childName = singleton.childName,

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
            JToken b = parent.FirstOrDefault(p => (string)p["parentName"] == singleton.parentName && (string)p["childName"] == singleton.childName);
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
            JArray parent = json["Form"] as JArray;
            if (parent == null)
            {
                return null;
            }
            List<Parent> li = new List<Parent>();
            li = JsonConvert.DeserializeObject<List<Parent>>(parent.ToString());
            return li;
        }
        public static void ReviseParent()
        {

        }
        public static void AddChild(SingletonStatus singleton, Child item)
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            string jsonString = File.ReadAllText(filePath);
            //string jsonString = File.ReadAllText(filePath, encoding);
            JObject json = JObject.Parse(jsonString);
            JArray parent = json["Form"] as JArray;
            JToken b = parent.FirstOrDefault(p => (string)p["parentName"] == singleton.parentName && (string)p["childName"] == singleton.childName);
            if (!(b["childSteps"] is JArray child))
            {
                child = new JArray();
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
            JToken b = parent.FirstOrDefault(p => (string)p["parentName"] == singleton.parentName && (string)p["childName"] == singleton.childName);
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

        public static List<Child> ReadChild(SingletonStatus singleton)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            List<Parent> basicClasses = new List<Parent>();
            basicClasses = ReadParent();
            if (basicClasses == null)
            {
                return null;
            }
            Parent b = basicClasses.FirstOrDefault(p => p.parentName == singleton.parentName && p.childName == singleton.childName);
            if (b == null)
            {
                return null;
            }
            return b.childSteps;
        }
        public static void ReviseChild()
        {

        }
        public static void AddParameter(SingletonStatus singleton, Object obj_parameter)
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            //string jsonString = File.ReadAllText(filePath, encoding);
            string jsonString = File.ReadAllText(filePath);
            JObject json = JObject.Parse(jsonString);
            JArray parent = json["Form"] as JArray;
            JToken b = parent.FirstOrDefault(p => (string)p["parentName"] == singleton.parentName && (string)p["childName"] == singleton.childName);
            JArray child = b["childSteps"] as JArray;
            string addString = JsonConvert.SerializeObject(obj_parameter, Formatting.Indented);
            JObject stepItem = JObject.Parse(addString);
            child[singleton.stepNum]["stepParameter"] = stepItem;
            string updatedJsonString = json.ToString(Formatting.Indented); 
            File.WriteAllText(filePath, updatedJsonString);
        }
        public static void DeleteParameter()
        {

        }
        public static object ReadParameter(List<Child> li, int index)
        {
            return li[index].stepParameter;
        }
        public static void ReviseParameter()
        {

        }
        public static List<Parent> ReadList(string _name)
        {
            List<Parent> li = new List<Parent>();
            List<Parent> res = new List<Parent>();
            li = ReadParent();
            if (li == null)
            {
                return null;
            }
            foreach (Parent c in li)
            {
                if (c.parentName == _name)
                {
                    res.Add(c);
                }

            }
            return res;
        }
        public static Parent ReadChild(string _name)
        {
            List<Parent> li = new List<Parent>();
            li = ReadParent();
            if (li == null)
            {
                return null;
            }
            foreach (Parent c in li)
            {
                if (c.childName == _name)
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
        public static void ReferishKepServerAddress()
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            string jsonString = File.ReadAllText(filePath);
            JObject json = JObject.Parse(jsonString);
            List<KepServerItem> li = new List<KepServerItem>();
            json["opc"]["opcItem"] = new JArray();
            string updatedJsonString = json.ToString(Formatting.Indented); 
            File.WriteAllText(filePath, updatedJsonString);
        }
        public static void WriteKepServerAddress(KepServerItem kpItem)
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            //string jsonString = File.ReadAllText(filePath, encoding);
            string jsonString = File.ReadAllText(filePath);
            JObject json = JObject.Parse(jsonString);
            JArray parent = json["opc"]["opcItem"] as JArray;
            string addString = JsonConvert.SerializeObject(kpItem, Formatting.Indented);
            JObject item = JObject.Parse(addString);
            parent.Add(item);
            string updatedJsonString = json.ToString(Formatting.Indented); 
            File.WriteAllText(filePath, updatedJsonString);
        }
        public static List<KepServerItem> ReadKepServerAddress()
        {
            if (!File.Exists(filePath))
            {
                return null;
            }
            //string jsonString = File.ReadAllText(filePath, encoding);
            string jsonString = File.ReadAllText(filePath);
            JObject json = JObject.Parse(jsonString);
            JArray parent = json["opc"]["opcItem"] as JArray;
            if (parent == null)
            {
                return null;
            }
            List<KepServerItem> li = new List<KepServerItem>();
            li = JsonConvert.DeserializeObject<List<KepServerItem>>(parent.ToString());
            return li;
        }
        public static List<VarItem> ReadVarItems()
        {
            if (!File.Exists(filePath))
            {
                return null;
            }
            //string jsonString = File.ReadAllText(filePath, encoding);
            string jsonString = File.ReadAllText(filePath);
            JObject json = JObject.Parse(jsonString);
            JArray items = json["variable"] as JArray;
            if (items == null)
            {
                return null;
            }
            List<VarItem> li = new List<VarItem>();
            li = JsonConvert.DeserializeObject<List<VarItem>>(items.ToString());
            return li;
        }
        public static int ReadVarItemPosition(string varItemName)
        {
            int position = 0;
            if (!File.Exists(filePath))
            {
                return position;
            }
            //string jsonString = File.ReadAllText(filePath, encoding);
            string jsonString = File.ReadAllText(filePath);
            JObject json = JObject.Parse(jsonString);
            JArray items = json["variable"] as JArray;
            if (items == null)
            {
                return position;
            }
            List<VarItem> li = new List<VarItem>();
            li = JsonConvert.DeserializeObject<List<VarItem>>(items.ToString());
            foreach(VarItem k in li)
            {
                if(k.varName == varItemName)
                {
                    return position;
                }
                position++;
            }
            return position;
        }
        public static void WriteVarItems(VarItem varItem)
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            //string jsonString = File.ReadAllText(filePath, encoding);
            string jsonString = File.ReadAllText(filePath);
            JObject json = JObject.Parse(jsonString);
            JArray parent = json["variable"] as JArray;
            string addString = JsonConvert.SerializeObject(varItem, Formatting.Indented);
            JObject item = JObject.Parse(addString);
            parent.Add(item);
            string updatedJsonString = json.ToString(Formatting.Indented); 
            File.WriteAllText(filePath, updatedJsonString);
        }
        public static void ReferishVarItems()
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            string jsonString = File.ReadAllText(filePath);
            JObject json = JObject.Parse(jsonString);
            List<KepServerItem> li = new List<KepServerItem>();
            json["variable"] = new JArray();
            string updatedJsonString = json.ToString(Formatting.Indented); 
            File.WriteAllText(filePath, updatedJsonString);
        }
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
