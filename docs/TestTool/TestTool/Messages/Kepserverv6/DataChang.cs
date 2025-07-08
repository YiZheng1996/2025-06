using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTool.Messages.Kepserverv6
{
    internal class DataChang
    {
        /// <summary> 数据改变事件——pub组</summary>
        /// <param name="TransactionID">处理ID</param>
        /// <param name="NumItems">项个数</param>
        /// <param name="ClientHandles">项客户端句柄</param>
        /// <param name="ItemValues">Tag值</param>
        /// <param name="Qualities">品质</param>
        /// <param name="TimeStamps">时间戳</param>
        public static void OPCGroup_pub_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
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
                        case 0:
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary> 数据改变事件——DIO组</summary>
        /// <param name="TransactionID">处理ID</param>
        /// <param name="NumItems">项个数</param>
        /// <param name="ClientHandles">项客户端句柄</param>
        /// <param name="ItemValues">Tag值</param>
        /// <param name="Qualities">品质</param>
        /// <param name="TimeStamps">时间戳</param>
        public static void OPCGroup_DIO_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
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
                            //Var.DI0.IB0 = ConVer.ConverBoolArr(int.Parse(ItemValues.GetValue(i).ToString()));
                            break;
                        case 2:

                            break;

                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary> 数据改变事件——AIO组</summary>
        /// <param name="TransactionID">处理ID</param>
        /// <param name="NumItems">项个数</param>
        /// <param name="ClientHandles">项客户端句柄</param>
        /// <param name="ItemValues">Tag值</param>
        /// <param name="Qualities">品质</param>
        /// <param name="TimeStamps">时间戳</param>
        public static void OPCGroup_AIO_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
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

                            break;
                        case 2:

                            break;
                        case 3:

                            break;
                        case 4:

                            break;
                        case 5:

                            break;
                        case 6:

                            break;
                        case 7:

                            break;
                        case 8:

                            break;


                        case 9:

                            break;
                        case 10:

                            break;




                    }
                }
            }
            catch (Exception)
            {
                return;
            }

        }

        /// <summary> 数据改变事件——Rang组</summary>
        /// <param name="TransactionID">处理ID</param>
        /// <param name="NumItems">项个数</param>
        /// <param name="ClientHandles">项客户端句柄</param>
        /// <param name="ItemValues">Tag值</param>
        /// <param name="Qualities">品质</param>
        /// <param name="TimeStamps">时间戳</param>
        public static void OPCGroup_Rang_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {

        }

        /// <summary> 数据改变事件——CheckVar组</summary>
        /// <param name="TransactionID">处理ID</param>
        /// <param name="NumItems">项个数</param>
        /// <param name="ClientHandles">项客户端句柄</param>
        /// <param name="ItemValues">Tag值</param>
        /// <param name="Qualities">品质</param>
        /// <param name="TimeStamps">时间戳</param>
        public static void OPCGroup_Speed_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
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

                            //RWAirplanePunch.Var.Test00.TestC = ConVer.ConverBoolArr(int.Parse(ItemValues.GetValue(i).ToString()));
                            break;
                        case 2:
                            //RWAirplanePunch.Var.Test00.TestS = ConVer.ConverBoolArr(int.Parse(ItemValues.GetValue(i).ToString()));
                            break;
                        case 3:
                            //RWAirplanePunch.Var.Test00.TestNo =int.Parse(ItemValues.GetValue(i).ToString());
                            break;
                        case 4:
                            //RWAirplanePunch.Var.Test00.TestModel = int.Parse(ItemValues.GetValue(i).ToString());
                            break;


                    }
                }
            }
            catch (Exception)
            {
                return;

            }

        }

        /// <summary> 数据改变事件——Test01组</summary>
        /// <param name="TransactionID">处理ID</param>
        /// <param name="NumItems">项个数</param>
        /// <param name="ClientHandles">项客户端句柄</param>
        /// <param name="ItemValues">Tag值</param>
        /// <param name="Qualities">品质</param>
        /// <param name="TimeStamps">时间戳</param>
        public static void OPCGroup_Test01_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
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

                            break;
                        case 2:

                            break;

                    }
                }
            }
            catch (Exception)
            {
                return;

            }

        }
    }
}
