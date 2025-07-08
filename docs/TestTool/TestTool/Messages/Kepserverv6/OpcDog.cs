using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTool.Config;
using OPCAutomation;
namespace TestTool.Messages.Kepserverv6
{
    internal class OpcDog
    {
        public static OpcClass opc;
        public static string status;
        private static string OpcConnect(List<string> str)
        {
            opc = new OpcClass();
            OpcObject.SetAddress(str);
            status = opc.OpcConnect();
            return status;
        }
        public static void OpcDispose()
        {
            if (opc != null)
            {
                opc.OPCClose();
            }

        }
        public static string SearchAddr(string plcName)
        {
            List<string> li = new List<string>();
            string channelName = JsonDog.ReadKepServerChannelName();
            string deviceName = JsonDog.ReadKepServerDeviceName();
            string groupName = JsonDog.ReadKepServerGroupName();
            List<KepServerItem> li0 = new List<KepServerItem>();
            li0 = JsonDog.ReadKepServerAddress();
            string str0 = channelName + "." + deviceName + "." + groupName + ".";
            //string str = "";
            if (li0.Count == 0)
            {
                return null;
            }
            foreach (KepServerItem item in li0)
            {
                if(item.kepName == plcName)
                {
                    return str0 + item.kepAddr;
                }
            }
            return null;
        }
        public static void SetOpc()
        {
            List<string> li = new List<string>();
            string channelName = JsonDog.ReadKepServerChannelName();
            string deviceName = JsonDog.ReadKepServerDeviceName();
            string groupName = JsonDog.ReadKepServerGroupName();
            List<KepServerItem> li0 = new List<KepServerItem>();
            li0 = JsonDog.ReadKepServerAddress();
            string str0 = channelName + "." + deviceName + "." + groupName + ".";
            string str = "";
            if (li0.Count == 0)
            {
                return;
            }
            for (int i = 0; i < li0.Count; i++)
            {
                str = str0 + li0[i].kepAddr;
                li.Add(str);

            }
            if (li.Count > 0)
            {
                OpcDog.OpcConnect(li);

            }
        }
        public static void Write(string plcName,object plcValue)
        {
            string plcAddr = SearchAddr(plcName);
            if(plcAddr == null)
            {
                return ;
            }
            foreach (OPCItem item in OpcObject.opcItemsList)
            {
                if(item.ItemID == plcAddr)
                {
                    item.Write(plcValue);
                    return;
                }
            }
        }
        public static object Read(string plcName)
        {
            string plcAddr = SearchAddr(plcName);
            if (plcAddr == null)
            {
                return null;
            }
            foreach (OPCItem item in OpcObject.opcItemsList)
            {
                if (item.ItemID == plcAddr)
                {
                    return item.Value;
                }
            }
            return null;
        }
    }
}
