using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OPCAutomation;
namespace TestTool.Messages.Kepserverv6
{
    internal class OpcObject
    {
        #region OPCGroup定义
        /// <summary>
        /// OPCGroup--温湿度
        /// </summary>
        //public static OPCGroup opcGroup_TempHumi;
        /// <summary>
        /// OPCGroup——公共
        /// </summary>
        public static OPCGroup opcGroup_pub;
        #endregion
        #region OPCItem定义
        #region MVB专用块(无任何实际含义)
        #endregion 

        public static List<OPCItem> opcItemsList { get; set; }
        //将地址传递给opcAddress后，变量按照索引存放opcItemsList
        public static List<string> opcAddress { get; set; }
        public static void SetAddress(List<string> str)
        {
            //str 按照顺序的地址
            OpcObject.opcAddress = str;
        }
        public struct Pub
        {
            ////------1、建立变量
            //public static OPCItem X; //机器人x坐标
            //public static OPCItem Y; //机器人y坐标
            //public static OPCItem PX; //发送机器人X坐标
            //public static OPCItem PY; //发送机器人Y坐标
            //public static OPCItem Ng; //失败
            //public static OPCItem Ka; //拍照
            //public static OPCItem Rd;  //半径
            //public static OPCItem Num; //功能号 10轴承拍照 20齿轮拍照 30标定板拍照 40九点拍照
            //public static OPCItem Ok;  //成功
            //public static OPCItem Xh;  //型号
            //public static OPCItem St;  //机器人停止信号
            //public static OPCItem Rm;  //小轴承内径
            //public static OPCItem Rl;  //大轴承内径
            //public static OPCItem Rg;  //齿轮内径
            //public static OPCItem Cl;  //齿轮型号
            //public static OPCItem Rh;  //齿轮外径
           
        }
        
        
        #endregion
    }
}
