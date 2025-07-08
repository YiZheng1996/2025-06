using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OPCAutomation;
namespace TestTool.Messages.Kepserverv6
{
    internal class OpcWrite
    {
        /// <summary>
        /// OPC数据写入操作
        /// </summary>
        /// <param name="opcitem">要进行写入的OPCItem</param>
        /// <param name="bvalue">要操作的bool[]</param>
        /// <param name="index">要操作数组的index</param>
        /// <param name="value">数组项值</param>
        public static void OpcW(OPCItem opcitem, bool[] bvalue, int index, bool value)
        {
            bvalue[index] = value;
            OpcClass.Write_String(opcitem, ConVer.ConverInt(bvalue).ToString());
        }

        /// <summary>
        /// OPC数据写入操作
        /// </summary>
        /// <param name="opcitem">要进行写入的OPCItem</param>
        /// <param name="value">写入数值</param>
        public static void OpcW(OPCItem opcitem, string value)
        {
            try
            {
                opcitem.Write(value);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// OPC数据写入操作
        /// </summary>
        /// <param name="opcitem">要进行写入的OPCItem</param>
        /// <param name="value">写入数值</param>
        public static void OpcW(OPCItem opcitem, double value)
        {
            try
            {
                opcitem.Write(value);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// OPC数据写入操作
        /// </summary>
        /// <param name="opcitem">要进行写入的OPCItem</param>
        /// <param name="value">写入数值</param>
        public static void OpcW(OPCItem opcitem, bool value)
        {
            try
            {
                opcitem.Write(value);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// OPC数据写入操作
        /// </summary>
        /// <param name="opcitem">要进行写入的OPCItem</param>
        /// <param name="value">写入数值</param>
        public static void OpcW(OPCItem opcitem, Int16 value)
        {
            try
            {
                opcitem.Write(value);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// OPC数据写入操作
        /// </summary>
        /// <param name="opcitem">要进行写入的OPCItem</param>
        /// <param name="value">写入数值</param>
        public static void OpcW(OPCItem opcitem, int value)
        {
            try
            {
                opcitem.Write(value);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 将Bool值写入Byte类型的opc中
        /// </summary>
        /// <param name="opcitem">要进行写入的OPCItem</param>
        /// <param name="value">写入数值</param>
        public static void opcW_ByteBool(OPCItem opcitem, bool value)
        {
            int i = (value == true) ? 1 : 0;
            try
            {
                opcitem.Write(i);
            }
            catch (Exception)
            {

            }
        }
    }
}
