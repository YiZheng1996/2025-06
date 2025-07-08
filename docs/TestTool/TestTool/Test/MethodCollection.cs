using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTool.Config;
using TestTool.Messages.Kepserverv6;
using TestTool.Parameters.ListPar;
using TestTool.Parameters.ParCommunity;

namespace TestTool.Test
{
    internal class MethodCollection
    {
        private static SingletonStatus singletonStatus = SingletonStatus.Instance;
        public static void Test()
        {

        }
        public static void Method_DefineVar(dynamic obj)
        {

        }
        public static void Method_ReadPLC(dynamic obj)
        {

        }
        public static void Method_DelayTime(dynamic obj)
        {
            dynamic t = ((dynamic)obj).t;
            int time = (int)t;
            Thread.Sleep(time);
            singletonStatus.status = true;
        }

        public static void Method_WritePLC(dynamic obj)
        {
            
            try
            {
                OpcDog.SetOpc();
                int varPosition = 0;
                ListParameter_WritePLC li = new ListParameter_WritePLC();
                //dynamic li = ((dynamic)obj).LictPLC;
                li = JsonConvert.DeserializeObject<ListParameter_WritePLC>(obj.ToString());
                foreach (Parameter_WritePLC item in li.LictPLC)
                {
                    if (item.varName != "")
                    {
                        varPosition = JsonDog.ReadVarItemPosition(item.varName);
                        OpcDog.Write(item.plcName, singletonStatus.obj[varPosition]);
                    }
                    else if (item.plcValue != "")
                    {
                        OpcDog.Write(item.plcName, item.plcValue);
                    }
                }
                singletonStatus.status = true;
            }
            catch (Exception ex)
            {
                singletonStatus.status = false;
            }
        }
        public static void Mathod_VariableAssignment(dynamic obj)
        {

        }
    }
}
