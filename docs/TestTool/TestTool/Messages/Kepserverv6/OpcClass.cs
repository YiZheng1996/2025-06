using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPCAutomation;
using System.Threading.Tasks;

namespace TestTool.Messages.Kepserverv6
{
    internal class OpcClass
    {
        #region 对象定义

        #region OPC服务相关

        /// <summary>OPCServer Object</summary>
        OPCServer KepServer;

        /// <summary>OPCGroup Object</summary>
        OPCGroups opcGroups;

        OPCItems opcitems;

        List<OPCItem> opcItemList;
        /// <summary>连接状态</summary>
        public static bool opc_connected = false;

        #endregion


        #endregion

        #region OPC服务器连接、关闭、读取

        /// <summary>OPC服务器连接与变量读取
        /// </summary>
        /// <returns>返回连接状态</returns>
        public string OpcConnect()
        {
            string retStr = "";

            if (OPCOpen())
            {
                if (KepServer.ServerState == (int)OPCServerState.OPCRunning)
                {
                    //retStr = "已连接到-" + KepServer.ServerName + "   ...";
                    retStr = "已连接到-" + KepServer.ServerName + "   ...";
                    //Console.WriteLine(retStr);
                }
                else
                {
                    retStr = "error - OPC状态：" + KepServer.ServerState.ToString() + "   ";//这里你可以根据返回的状态来自定义显示信息，请查看自动化接口API文档
                }
                if (!CreateGroup())
                {
                    retStr = "error - OPC服务创建组出错...";
                    Console.WriteLine(retStr);
                }
                else
                {
                    opc_connected = true;
                    //if (OpcItemSet.SetOPCItem() == false)
                    //{

                    //    retStr = "error - OPC项创建失败";
                    //}
                    //else
                    //{
                    //    //Console.WriteLine("okk");
                    //    opc_connected = true;
                    //}
                }
            }
            else
            {
                retStr = "error - OPC连接失败";
                opc_connected = false;
            }
            return retStr;

        }

        /// <summary>
        /// OPC服务打开
        /// </summary>
        private bool OPCOpen()
        {
            try
            {
                KepServer = new OPCServer();
                //KepServer.Connect("OPC.SimaticNET", null);//此处设置连接OPC服务器名
                //KepServer.Connect("S7200.OPCServer", null);//此处设置连接OPC服务器名
                KepServer.Connect("KEPware.KEPServerEx.V6", null);//此处设置连接OPC服务器名
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>OPC服务器关闭处理事件
        /// </summary>
        public void OPCClose()
        {
            if (!opc_connected)
            {
                return;
            }
            if (OpcObject.opcGroup_pub != null)
            {
                OpcObject.opcGroup_pub.DataChange -= new DIOPCGroupEvent_DataChangeEventHandler(DataChang.OPCGroup_pub_DataChange);
            }
            //if (OpcObject.opcGroup_DIO != null)
            //{
            //    OpcObject.opcGroup_DIO.DataChange -= new DIOPCGroupEvent_DataChangeEventHandler(RWAirplanePunch.DataChang.OPCGroup_DIO_DataChange);
            //}
            //if (OpcObject.opcGroup_AIO != null)
            //{
            //    OpcObject.opcGroup_AIO.DataChange -= new DIOPCGroupEvent_DataChangeEventHandler(RWAirplanePunch.DataChang.OPCGroup_AIO_DataChange);
            //}
            //if (OpcObject.opcGroup_Rang != null)
            //{
            //    OpcObject.opcGroup_Rang.DataChange -= new DIOPCGroupEvent_DataChangeEventHandler(RWAirplanePunch.DataChang.OPCGroup_Rang_DataChange);
            //}
            //if (OpcObject.opcGroup_Speed != null)
            //{
            //    OpcObject.opcGroup_Speed.DataChange -= new DIOPCGroupEvent_DataChangeEventHandler(RWAirplanePunch.DataChang.OPCGroup_Speed_DataChange);
            //}

            if (KepServer != null)
            {
                KepServer.Disconnect();
                KepServer = null;
            }
            opc_connected = false;
        }

        #endregion

        #region 创建、设置组

        /// <summary>创建组</summary>
        private bool CreateGroup()
        {
            try
            {
                opcGroups = KepServer.OPCGroups;

                OpcObject.opcGroup_pub = opcGroups.Add("Test");
         

                SetGroupProperty();//设置组的属性


                OpcObject.opcGroup_pub.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(DataChang.OPCGroup_pub_DataChange);

                opcitems = OpcObject.opcGroup_pub.OPCItems;
                if (!SetGroup())
                {
                    return false;
                }
                OpcObject.opcItemsList = opcItemList;
            }
            catch (Exception)
            {
                return false;

            }
            return true;
        }

        private bool SetGroup()
        {
            //opcItemList = new List<OPCItem>();
            List<string> li = new List<string>();
            li = OpcObject.opcAddress;
            if(li.Count == 0)
            {
                return false;
            }
            int i = 1;
            opcItemList = new List<OPCItem>();
            foreach (string item in li)
            {
                //opcitems.AddItem(item, i);
                opcItemList.Add(opcitems.AddItem(item,i));
                i++;
            }
            //opcItemList.Add(opcitems.Item);
            return true;
            //return OpcObject.opcItemsList.ToArray();
        }
        /// <summary>设置组属性</summary>
        private void SetGroupProperty()
        {
            KepServer.OPCGroups.DefaultGroupIsActive = true;
            KepServer.OPCGroups.DefaultGroupDeadband = 50;
            OpcObject.opcGroup_pub.UpdateRate = 1;
            OpcObject.opcGroup_pub.IsActive = true;
            OpcObject.opcGroup_pub.IsSubscribed = true;

            //设置刷新频率
            /*
            OpcObject.opcGroup_pub.UpdateRate = OpcObject.opcGroup_DIO.UpdateRate = OpcObject.opcGroup_AIO.UpdateRate = OpcObject.opcGroup_Rang.UpdateRate = 50;
            
            OpcObject.opcGroup_Speed.UpdateRate = 50;
            OpcObject.opcGroup_Test01.UpdateRate = 50;
            OpcObject.opcGroup_Test02.UpdateRate = 50;
            OpcObject.opcGroup_Test03.UpdateRate = 50;
            OpcObject.opcGroup_Test04.UpdateRate = 50;
            OpcObject.opcGroup_Test05.UpdateRate = 50;
            OpcObject.opcGroup_Test06.UpdateRate = 50;
            OpcObject.opcGroup_Test07.UpdateRate = 50;
            //设置组是否激活
            OpcObject.opcGroup_pub.IsActive = OpcObject.opcGroup_DIO.IsActive = OpcObject.opcGroup_AIO.IsActive = OpcObject.opcGroup_Rang.IsActive = true;
            OpcObject.opcGroup_Speed.IsActive = true;
            OpcObject.opcGroup_Test01.IsActive = true;
            OpcObject.opcGroup_Test02.IsActive = true;
            OpcObject.opcGroup_Test03.IsActive = true;
            OpcObject.opcGroup_Test04.IsActive = true;
            OpcObject.opcGroup_Test05.IsActive = true;
            OpcObject.opcGroup_Test06.IsActive = true;
            OpcObject.opcGroup_Test07.IsActive = true;
            //
            OpcObject.opcGroup_pub.IsSubscribed = OpcObject.opcGroup_DIO.IsSubscribed = OpcObject.opcGroup_AIO.IsSubscribed = OpcObject.opcGroup_Rang.IsSubscribed = true;
            OpcObject.opcGroup_Speed.IsSubscribed = true;
            OpcObject.opcGroup_Test01.IsSubscribed = true;
            OpcObject.opcGroup_Test02.IsSubscribed = true;
            OpcObject.opcGroup_Test03.IsSubscribed = true;
            OpcObject.opcGroup_Test04.IsSubscribed = true;
            OpcObject.opcGroup_Test05.IsSubscribed = true;
            OpcObject.opcGroup_Test06.IsSubscribed = true;
            OpcObject.opcGroup_Test07.IsSubscribed = true;
            */
        }

        #endregion

        #region 数据改变事件

        /// <summary> 数据改变事件</summary>
        /// <param name="TransactionID">处理ID</param>
        /// <param name="NumItems">项个数</param>
        /// <param name="ClientHandles">项客户端句柄</param>
        /// <param name="ItemValues">Tag值</param>
        /// <param name="Qualities">品质</param>
        /// <param name="TimeStamps">时间戳</param>
        void opcGroup_pub_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            Int32 k = 0;
            try
            {
                for (int i = 1; i <= NumItems; i++)
                {
                    k = Convert.ToInt32(ClientHandles.GetValue(i).ToString());
                    if (ItemValues.GetValue(i) == null)
                        return;
                    switch (k)
                    {
                        case 1:
                            //Var.pub.Alarm = (bool[])ItemValues.GetValue(i);
                            break;
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }



        #endregion

        #region 数据读取

        /// <summary>数据读取
        /// </summary>
        /// <param name="ItemObj"></param>
        /// <returns></returns>
        public string Read_String(OPCItem ItemObj)
        {
            try
            {
                object myValue = "";
                object myQuality;
                object myTimeStamp;
                ItemObj.Read(Convert.ToInt16(OPCDataSource.OPCDevice), out myValue, out myQuality, out myTimeStamp);
                return Convert.ToString(myValue);
            }
            catch (Exception)
            {
                return Convert.ToString("0");
            }

        }

        #endregion

        #region 数据写入

        /// <summary>数据写入
        /// </summary>
        /// <param name="itemObj"></param>
        /// <param name="txtWriteTagValue"></param>
        public static void Write_String(OPCItem itemObj, string txtWriteTagValue)
        {
            if (opc_connected)
            {
                try
                {
                    itemObj.Write(txtWriteTagValue);
                }
                catch (Exception)
                {

                }
            }
            else
            {

            }
        }

        #endregion
    }
}
