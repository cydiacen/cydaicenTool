using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Diagnostics;
using System.Text;
using System.Security.Cryptography;
using System.IO;   
namespace cydiacenTool
{
    public static class MyExtensions
    {



        //字符串转整型
        public static int S_Int(this System.String str)
        {
            int result = 0;
            int.TryParse(str, out result);
            return result;
        }


        #region 字符串添加元素
        public static string[] Mypush(this System.String[] target, string item)
        {
            string[] _arr = new string[target.Length + 1];
            target.CopyTo(_arr, 0);
            _arr[target.Length] = item;
            target = _arr;
            return target;
        }
        #endregion

        #region  数组join
        public static string MyJoin(this System.String[] target, string item)
        {
            return string.Join(item, target);
        }
        #endregion

        #region 超出长度隐藏
        public static string MySplitStr(this System.String target, int len, string defaultStr)
        {
            string result = "";
            if (!string.IsNullOrEmpty(target))
            {
                if (target.Length > len)
                {
                    result = target.Substring(0, len) + "...";
                }
                else
                {
                    result = target;
                }
            }
            else
            {
                result = defaultStr;
            }
            return result;
        }
        #endregion

        #region 过滤非法字符
        public static string MyfilterSql(this System.String target)
        {
            target = target.Trim();
            string[] aryReg = { "'", "\"", "\r", "\n", "<", ">", "%", "?", ",", "=", "-", "_", ";", "|", "[", "]", "&", "/", "go", "update", "insert" };
            if (!string.IsNullOrEmpty(target))
            {
                foreach (string str in aryReg)
                {
                    target = target.Replace(str, string.Empty);
                }
                return target;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 不可逆MD5加密
        /// <summary>
        /// 获取MD5得值，没有转换成Base64的
        /// </summary>
        /// <param name="sDataIn">需要加密的字符串</param>
        /// <param name="move">偏移量</param>
        /// <returns>sDataIn加密后的字符串</returns>
        public static string gggggg(string sDataIn, string move ="")
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] byt, bytHash;
            byt = System.Text.Encoding.UTF8.GetBytes(move + sDataIn);
            bytHash = md5.ComputeHash(byt);
            md5.Clear();
            string sTemp = "";
            for (int i = 0; i < bytHash.Length; i++)
            {
                sTemp += bytHash[i].ToString("x").PadLeft(2, '0');
            }
            return sTemp;
        }
        #endregion
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }

}
