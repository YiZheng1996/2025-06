using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTool.Messages.Kepserverv6
{
    internal class ConVer
    {
        /// <summary>
        /// 将十进数转换为bool[]
        /// </summary>
        /// <param name="valueI">要进行转换的十进制数</param>
        /// <returns>返回bool[]</returns>
        public static bool[] ConverBoolArr(int valueI)
        {
            bool[] b = new bool[8];
            string str2 = Convert.ToString(valueI, 2).PadLeft(8, '0');

            b[0] = Convert.ToBoolean(int.Parse(str2.Substring(7, 1)));
            b[1] = Convert.ToBoolean(int.Parse(str2.Substring(6, 1)));
            b[2] = Convert.ToBoolean(int.Parse(str2.Substring(5, 1)));
            b[3] = Convert.ToBoolean(int.Parse(str2.Substring(4, 1)));
            b[4] = Convert.ToBoolean(int.Parse(str2.Substring(3, 1)));
            b[5] = Convert.ToBoolean(int.Parse(str2.Substring(2, 1)));
            b[6] = Convert.ToBoolean(int.Parse(str2.Substring(1, 1)));
            b[7] = Convert.ToBoolean(int.Parse(str2.Substring(0, 1)));
            return b;
        }

        /// <summary>
        /// 将bool[]转换为十进数
        /// </summary>
        /// <param name="valueI">要进行转换的bool[]</param>
        /// <returns>返回十进制数</returns>
        public static int ConverInt(bool[] valueBoolArr)
        {
            StringBuilder str = new StringBuilder();
            str.Append(valueBoolArr[7] == true ? "1" : "0");
            str.Append(valueBoolArr[6] == true ? "1" : "0");
            str.Append(valueBoolArr[5] == true ? "1" : "0");
            str.Append(valueBoolArr[4] == true ? "1" : "0");
            str.Append(valueBoolArr[3] == true ? "1" : "0");
            str.Append(valueBoolArr[2] == true ? "1" : "0");
            str.Append(valueBoolArr[1] == true ? "1" : "0");
            str.Append(valueBoolArr[0] == true ? "1" : "0");
            return Convert.ToInt32(str.ToString(), 2);
        }

        /// <summary>
        /// 将字符型数字转换为指定长度的数字字符串
        /// </summary>
        /// <param name="strfloat">要进行转换的字符</param>
        /// <param name="len">需要保留的小数位数</param>
        /// <returns>转换为字符</returns>
        public static string strFloat(string strfloat, int len)
        {
            try
            {
                float f = float.Parse(strfloat);
                return f.ToString("f" + len.ToString());
            }
            catch (Exception)
            {
                return "---";
            }
        }

        /// <summary>
        /// 字符串转换为Bool
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool ConvertBool(object value)
        {
            string strtemp;
            try
            {
                strtemp = value.ToString();
            }
            catch (Exception)
            {
                strtemp = "0";
            }
            if (strtemp == "1" || strtemp == "True" || strtemp == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
