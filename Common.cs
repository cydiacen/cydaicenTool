using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Data;
using System.Web.SessionState;

/// <summary>
///Common 主要用来进行数据处理、类型转换
/// </summary>
public static class Common
{
    /// <summary>
    /// 获取WebConfig中的参数
    /// </summary>
    /// <param name="key">键名</param>
    /// <returns></returns>
    public static string GetAppSettings(string key)
    {
        return System.Configuration.ConfigurationManager.AppSettings[key].ToString();
    }

    /// <summary>
    /// 获取Cach前缀
    /// </summary>
    public static string SessionPrefix
    {
        get
        {
            return GetAppSettings("SessionPrefix");
        }
    }

    #region 数据转换
    /// <summary>
    /// 获取用户提交过来的参数POS，并将其转成整形
    /// </summary>
    /// <param name="name">键名</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns></returns>
    public static int F_Int(string name, int defaultValue)
    {
        return C_Int_Array(HttpContext.Current.Request.Form[name], defaultValue);
    }

    /// <summary>
    /// 获取用户提交过来的参数GET，并将其转成整形（大于0）
    /// </summary>
    /// <param name="name">键名</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns></returns>
    public static int Q_Int(string name, int defaultValue)
    {
        return C_Int_Array(HttpContext.Current.Request.QueryString[name], defaultValue);
    }

    /// <summary>
    /// 获取用户提交过来的参数POST/GET，并将其转成整形
    /// </summary>
    /// <param name="name">键名</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns></returns>
    public static int Q_IntAll(string name, int defaultValue)
    {
        return C_Int(HttpContext.Current.Request.QueryString[name], defaultValue);
    }

    /// <summary>
    /// 将指定值转成整形
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns></returns>
    public static int C_Int(string str, int defaultValue)
    {
        int result = 0;
        if (int.TryParse(str, out result))
            return str == "0" ? 0 : (result == 0 ? defaultValue : result);
        else
            return defaultValue;
    }

    /// <summary>
    /// 将字符串转成Decimal类型
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns></returns>
    public static decimal C_Decimal(string str)
    {
        decimal result = 0;
        if (decimal.TryParse(str, out result))
            return result;
        else
            return 0;
    }

    /// <summary>
    /// 将指定值转成Decimal
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns></returns>
    public static decimal C_Decimal(string str, decimal defaultValue)
    {
        decimal result = 0;
        if (decimal.TryParse(str, out result))
            return str == "0" ? 0 : (result == 0 ? defaultValue : result);
        else
            return defaultValue;
    }

    /// <summary>
    /// 将指定值转成整形
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns></returns>
    public static int S_Int(string str)
    {
        int result = 0;
        if (int.TryParse(str, out result))
            return result;
        else
            return 0;
    }

    /// <summary>
    /// 将指定值转成整形
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns></returns>
    public static int C_Int_Array(string str, int defaultValue)
    {
        int result = 0;
        if (int.TryParse(str, out result))
            return result > -1 ? result : defaultValue;
        else
            return defaultValue;
    }

    /// <summary>
    /// 将数据中BIT类型True/False转成1/0
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns></returns>
    public static string SqlBitToByte(string str)
    {
        return str.ToLower() == "true" ? "1" : "0";
    }

    /// <summary>
    /// 将数据中BIT类型True/False转成是/否
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns></returns>
    public static string SqlBitToCHS(string str)
    {
        return str.ToLower() == "true" ? "是" : "否";
    }
    #endregion

    #region 字符编码

    #region 地址字符串解码
    /// <summary>
    /// 将编码的地址字符串转换为解码的字符串
    /// </summary>
    /// <param name="name">参数名称</param>
    /// <returns></returns>
    public static string UrlDecode(string name)
    {
        return HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString[name]);
    }
    #endregion

    #region 地址字符串编码
    /// <summary>
    /// 将指定地址参数进行字符编码
    /// </summary>
    /// <param name="name">参数名称</param>
    /// <returns></returns>
    public static string UrlEncode(string name)
    {
        return HttpUtility.UrlEncode(HttpContext.Current.Request.QueryString[name]);
    }
    #endregion

    #region 不可逆MD5加密
    /// <summary>
    /// MD5加密
    /// </summary>
    /// <param name="str">加密字符</param>
    /// <param name="code">加密位数16/32</param>
    /// <returns></returns>
    public static string md5(string str)
    {
        return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToUpper();
    }
    #endregion

    #region 生成GUID
    /// <summary>
    /// 生成GUID
    /// </summary>
    public static string getGUID
    {
        get
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
    #endregion

    #region 截取字符串到指定长度
    /// <summary>
    /// 截取字符串到指定长度
    /// </summary>
    /// <param name="str"></param>
    /// <param name="len"></param>
    /// <param name="defaultStr"></param>
    /// <returns></returns>
    public static string splitString(string str, int len, string defaultStr)
    {
        string result = "";
        if (!string.IsNullOrEmpty(str))
        {
            if (str.Length > len)
            {
                result = str.Substring(0, len) + "...";
            }
            else
            {
                result = str;
            }
        }
        else
        {
            result = defaultStr;
        }
        return result;
    }
    #endregion

    #region 获取字符长度
    /// <summary>
    /// 获取字符长度，英文长度为1，中文为2
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int GetLength(string str)
    {
        if (str.Length == 0) return 0;
        ASCIIEncoding ascii = new ASCIIEncoding();
        int tempLen = 0; byte[] s = ascii.GetBytes(str);
        for (int i = 0, len = s.Length; i < len; i++)
        {
            if ((int)s[i] == 63)
            {
                tempLen += 2;
            }
            else
            {
                tempLen += 1;
            }
        }
        return tempLen;
    }
    #endregion

    #endregion

    #region 获取客户端信息

    #region 获取用户IP地址
    public static string GetRemoteIp()
    {
        string Ip = string.Empty;
        if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
        {
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
            {
                if (HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"] != null)
                    Ip = HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"].ToString();
                else
                    if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                        Ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    else
                        Ip = "127.0.0.1";
            }
            else
                Ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
        }
        else if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
        {
            Ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
        }
        else
        {
            Ip = "127.0.0.1";
        }
        return Ip;
    }
    #endregion

    #region 获取操作系统版本
    public static string GetSystem
    {
        get
        {
            string s = HttpContext.Current.Request.UserAgent.Trim().Replace("(", "").Replace(")", "");
            string[] sArray = s.Split(';');
            switch (sArray[2].Trim())
            {
                case "Windows 4.10":
                    s = "Windows 98";
                    break;
                case "Windows 4.9":
                    s = "Windows Me";
                    break;
                case "Windows NT 5.0":
                    s = "Windows 2000";
                    break;
                case "Windows NT 5.1":
                    s = "Windows XP";
                    break;
                case "Windows NT 5.2":
                    s = "Windows 2003";
                    break;
                case "Windows NT 6.0":
                    s = "Windows Vista";
                    break;
                case "Windows NT 6.1":
                    s = " Windows 7";
                    break;
                case "Windows NT 6.2":
                    s = " Windows 8";
                    break;
                default:
                    s = "Other";
                    break;
            }
            return s;
        }
    }
    #endregion

    #region  获得浏览器信息
    /// <summary>
    /// 获得浏览器信息
    /// </summary>
    /// <returns></returns>
    public static string GetClientBrower()
    {
        string[] browerNames = { "MSIE", "Firefox", "Opera", "Netscape", "Safari", "Lynx", "Konqueror", "Mozilla" };
        string agent = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
        if (!string.IsNullOrEmpty(agent))
        {
            foreach (string name in browerNames)
            {
                if (agent.Contains(name))
                    return name;
            }
        }
        return "Other";
    }
    #endregion

    #endregion

    #region 过滤特殊符号
    /// <summary>
    /// 过滤特殊特号(完全过滤)
    /// </summary>
    /// <param name="Str"></param>
    /// <returns></returns>
    public static string FilterSql(string Str)
    {
        Str = Str.Trim();
        string[] aryReg = { "'", "\"", "\r", "\n", "<", ">", "%", "?", ",", "=", "-", "_", ";", "|", "[", "]", "&", "/", "go", "update", "insert" };
        if (!string.IsNullOrEmpty(Str))
        {
            foreach (string str in aryReg)
            {
                Str = Str.Replace(str, string.Empty);
            }
            return Str;
        }
        else
        {
            return "";
        }
    }
    /// <summary>
    /// 过滤特殊符号(简单过滤)
    /// </summary>
    /// <param name="Str"></param>
    /// <returns></returns>
    public static string FilterSql1(string Str)
    {
        string[] aryReg = { "'", ";", "\"", "\r", "\n", "<", ">" };
        foreach (string str in aryReg)
        {
            Str = Str.Replace(str, string.Empty);
        }
        return Str;
    }


    /// <summary>
    /// 过滤特殊符号(样式管理)
    /// </summary>
    /// <param name="Str"></param>
    /// <returns></returns>
    public static string FilterStr(string Str)
    {
        if (!string.IsNullOrEmpty(Str))
        {
            Str = Str.Replace("\"", "'");
            Str = Str.Replace("\r", "");
            Str = Str.Replace("\n", "");
        }
        return Str;
    }

    /// <summary>
    /// 过滤HTML代码
    /// </summary>
    /// <param name="html"></param>
    /// <returns></returns>
    public static string FilterHTML(string Str)
    {
        if (!string.IsNullOrEmpty(Str))
        {
            string Pattern = @"<\/*[^<>]*>";
            return System.Text.RegularExpressions.Regex.Replace(Str, Pattern, "");
        }
        else
        {
            return "";
        }
    }
    #endregion

    #region 生成随机数字
    /// <summary>
    /// 生成随机数字
    /// </summary>
    /// <param name="Length">生成长度</param>
    /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
    /// <returns></returns>
    public static string Number(int Length, bool Sleep)
    {
        if (Sleep) System.Threading.Thread.Sleep(3);
        string result = "";
        System.Random random = new Random();
        for (int i = 0; i < Length; i++)
        {
            result += random.Next(10).ToString();
        }
        return result;
    }


    #endregion

    #region 判断指定数组是否包含指定字符串
    /// <summary>
    /// 判断指定数组是否包含指定字符串
    /// </summary>
    /// <param name="arr">数组</param>
    /// <param name="str">字符</param>
    /// <returns></returns>
    public static bool ArrayIsContains(string[] arr, string str)
    {
        bool containt = false;
        int len = arr.Length;
        if (len > 0)
        {
            for (int i = 0; i < len; i++)
            {
                if (arr[i] == str)
                {
                    containt = true;
                    break;
                }
            }
        }
        return containt;
    }

    /// <summary>
    /// 判断指定数组是否包含指定字符串
    /// </summary>
    /// <param name="arr">数组</param>
    /// <param name="str">字符</param>
    /// <returns></returns>
    public static bool ArrayIsContains(object[] arr, string str)
    {
        return ArrayIsContains(ToArray(arr), str);
    }


    /// <summary>
    /// 判断指定数组是否包含指定字符串
    /// </summary>
    /// <param name="strArray">需分隔字符串</param>
    /// <param name="str">包含字符串</param>
    /// <param name="separator">分隔符</param>
    /// <returns></returns>
    public static bool ArrayIsContains(string strArray, string str, char separator)
    {
        return ArrayIsContains(strArray.Split(separator), str);
    }

    /// <summary>
    /// 将指定的Object[]数组转成以指定字符串分隔的字符串
    /// </summary>
    /// <param name="oArray">数组</param>
    /// <param name="splitchar">分隔符</param>
    /// <returns></returns>
    public static string Join(object[] oArray, char splitchar)
    {
        string str = "";
        for (int i = 0, len = oArray.Length; i < len; i++)
            str += oArray[i].ToString() + splitchar;
        str.TrimEnd(splitchar);
        return str;
    }

    /// <summary>
    /// 将指定的Object[]数组转成String[]数组
    /// </summary>
    /// <param name="oArray">数组</param>
    /// <returns></returns>
    public static string[] ToArray(object[] oArray)
    {
        int len = oArray.Length;
        string[] sArray = new string[len];
        for (int i = 0; i < len; i++)
            sArray[i] = oArray[i] == null ? "" : oArray[i].ToString();
        return sArray;
    }
    #endregion

    #region 错误跳转
    /// <summary>
    /// 系统错误跳转
    /// </summary>
    /// <param name="errorCode">错误代码</param>
    public static void _Redirect(string errorCode)
    {
        if (errorCode != "000")
            HttpContext.Current.Response.Redirect("~/Admin/sMessage.aspx?error=" + errorCode);
    }
    #endregion

    #region 将指定虚拟路径转成绝对路径
    /// <summary>
    /// 将指定虚拟路径转成绝对路径
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns></returns>
    public static string getAbsolutePath(string path)
    {
        if (path.IndexOf("F:/") > -1 || path.IndexOf("F:\\") > -1)
            return path;
        if (path.IndexOf("://") < 0)
            return HttpContext.Current.Server.MapPath(path);
        else
            return path;
    }
    #endregion

    #region 通用Cookie操作
    /// <summary>
    /// 清除指定Cookie
    /// </summary>
    /// <param name="cookiename">cookiename</param>
    public static void ClearCookie(string cookiename)
    {
        HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
        if (cookie != null)
        {
            cookie.Expires = DateTime.Now.AddYears(-3);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
    /// <summary>
    /// 获取指定Cookie值
    /// </summary>
    /// <param name="cookiename">cookiename</param>
    /// <returns></returns>
    public static string GetCookieValue(string cookiename)
    {
        HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
        string str = string.Empty;
        if (cookie != null)
        {
            str = cookie.Value;
        }
        return str;
    }
    /// <summary>
    /// 添加一个Cookie（24小时过期）
    /// </summary>
    /// <param name="cookiename"></param>
    /// <param name="cookievalue"></param>
    public static void SetCookie(string cookiename, string cookievalue)
    {
        SetCookie(cookiename, cookievalue, DateTime.Now.AddDays(1.0));
    }
    /// <summary>
    /// 添加一个Cookie
    /// </summary>
    /// <param name="cookiename">cookie名</param>
    /// <param name="cookievalue">cookie值</param>
    /// <param name="expires">过期时间 DateTime</param>
    public static void SetCookie(string cookiename, string cookievalue, DateTime expires)
    {
        HttpCookie cookie = new HttpCookie(cookiename)
        {
            Value = cookievalue,
            Expires = expires
        };
        HttpContext.Current.Response.Cookies.Add(cookie);
    }
    #endregion

    #region 数据表通用处理方法

    #region 将指定对象转成JSON

    #region DataReader转换为Json
    /// <summary> 
    /// DataReader转换为Json 
    /// </summary> 
    /// <param name="dataReader">DataReader对象</param> 
    /// <returns>Json字符串</returns> 
    public static string ToJson(IDataReader dataReader, ref string result)
    {
        StringBuilder jsonString = new StringBuilder();
        jsonString.Append(" \"list\" : [");
        bool index = false;
        while (dataReader.Read())
        {
            index = true;
            jsonString.Append("{");
            for (int i = 0, len = dataReader.FieldCount; i < len; i++)
            {
                Type type = dataReader.GetFieldType(i);
                string strValue = dataReader[i].ToString();
                jsonString.Append("\"" + "data" + i.ToString() + "\":");
                strValue = StringFormat(strValue, type);
                if (i < len - 1)
                    jsonString.Append(strValue + ",");
                else
                    jsonString.Append(strValue);
            }
            jsonString.Append("},");
        }
        result = index ? "1000" : "1001";
        dataReader.Close();
        if (index)
            jsonString.Remove(jsonString.Length - 1, 1);
        jsonString.Append("] , \"status\" : \" " + result + "\" ");
        return jsonString.ToString();
    }
    #endregion

    #region 数组转换为Json
    /// <summary> 
    /// 一维数组转换为Json 
    /// </summary> 
    /// <returns>Json字符串</returns> 
    public static string ToJson(string[] sArray)
    {
        StringBuilder jsonString = new StringBuilder();
        jsonString.Append("{");
        for (int j = 0, len = sArray.Length; j < len; j++)
        {
            string strKey = "data" + j.ToString();
            string strValue = StringJson(sArray[j].ToString());
            jsonString.Append("\"" + strKey + "\":");
            jsonString.Append("\"" + strValue + "\"" + ",");
        }
        //jsonString.Remove(jsonString.Length - 1, 1);
        //jsonString.Append("}");
        return jsonString.ToString().TrimEnd(',') + "}";
    }

    /// <summary>
    /// 对象数组转JOSN
    /// </summary>
    /// <param name="oArray">数组</param>
    /// <returns></returns>
    public static string ToJson(object[] oArray)
    {
        return ToJson(ToArray(oArray));
    }
    #endregion

    #region Datatable转换为Json
    /// <summary> 
    /// Datatable转换为Json 
    /// </summary> 
    /// <param name="dt">Datatable对象</param> 
    /// <returns>Json字符串</returns> 
    public static string ToJson(DataTable dt)
    {
        return ToJson(dt, "list");
    }

    /// <summary>
    /// DataTable转换为Json 
    /// </summary>
    public static string ToJson(DataTable dt, string jsonName)
    {
        StringBuilder jsonString = new StringBuilder();
        jsonString.Append("{ \"" + jsonName + "\" : [");
        DataRowCollection drc = dt.Rows;
        int len = drc.Count;
        for (int i = 0; i < len; i++)
        {
            jsonString.Append("{");
            for (int j = 0, k = dt.Columns.Count; j < k; j++)
            {
                string strKey = "data" + j.ToString();
                string strValue = drc[i][j].ToString();
                Type type = dt.Columns[j].DataType;
                jsonString.Append("\"" + strKey + "\":");
                strValue = StringFormat(strValue, type);
                if (j < k - 1)
                    jsonString.Append(strValue + ",");
                else
                    jsonString.Append(strValue);
            }
            jsonString.Append("},");
        }
        jsonString.Remove(jsonString.Length - 1, 1);

        jsonString.Append("] , \"status\" : \" " + (len > 0 ? "1000" : "1001") + "\"  }");

        return jsonString.ToString();
    }
    #endregion


    /// <summary>
    /// 过滤Json对象内的特殊字符
    /// </summary>
    /// <param name="s">处理字符串</param>
    /// <returns></returns>
    public static string StringJson(String s)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0, len = s.Length; i < len; i++)
        {
            char c = s.ToCharArray()[i];
            switch (c)
            {
                case '\"':
                    sb.Append("\\\""); break;
                case '\\':
                    sb.Append("\\\\"); break;
                case '/':
                    sb.Append("\\/"); break;
                case '\b':
                    sb.Append("\\b"); break;
                case '\f':
                    sb.Append("\\f"); break;
                case '\n':
                    sb.Append("\\n"); break;
                case '\r':
                    sb.Append("\\r"); break;
                case '\t':
                    sb.Append("\\t"); break;
                default:
                    sb.Append(c); break;
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// 格式化字符型、日期型、布尔型
    /// </summary>
    /// <param name="str"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string StringFormat(string str, Type type)
    {
        if (type == typeof(string))
        {
            str = StringJson(str);
            str = "\"" + str + "\"";
        }
        else if (type == typeof(DateTime))
        {
            str = "\"" + str + "\"";
        }
        else if (type == typeof(bool))
        {
            str = str.ToLower();
        }
        else if (type != typeof(string) && string.IsNullOrEmpty(str))
        {
            str = "\"" + str + "\"";
        }
        return str;
    }
    #endregion

    #region DatatTable数据查询
    /// <summary>
    /// DataTable内数据查询
    /// </summary>
    /// <param name="d">数据DataTable</param>
    /// <param name="swhere">查询条件</param>
    /// <returns>数据DataTable</returns>
    public static DataTable SelectDataTable(DataTable dt, string swhere)
    {
        //DataTable dt = new DataTable();
        //dt = d;
        DataTable dtNew = new DataTable();
        dtNew = dt.Clone();
        DataRow[] dr = dt.Select(swhere);
        for (int i = 0, len = dr.Length; i < len; i++)
        {
            dtNew.ImportRow(dr[i]);
        }
        dt.Dispose(); dtNew.Dispose();
        return dtNew;
    }

    /// <summary>
    /// DataTable内数据查询
    /// </summary>
    /// <param name="d">数据DataTable</param>
    /// <param name="swhere">查询条件</param>
    /// <param name="sort">排序条件</param>
    /// <returns></returns>
    public static DataTable SelectDataTable(DataTable d, string swhere, string sort)
    {
        DataTable dt = new DataTable();
        dt = d;
        DataRow[] dr = dt.Select(swhere, sort);
        DataTable dt2 = new DataTable();
        dt2 = dt.Clone();
        for (int i = 0, len = dr.Length; i < len; i++)
        {
            dt2.ImportRow(dr[i]);
        }
        dt.Dispose(); dt2.Dispose();
        return dt2;
    }
    #endregion

    #region 判断DataTable是否为空
    /// <summary>
    /// 判断DataTable是否为空
    /// </summary>
    /// <param name="dt">数据库表</param>
    /// <returns></returns>
    public static bool DataTableIsNull(DataTable dt)
    {
        return (dt != null && dt.Rows.Count > 0);
    }
    #endregion

    #region DataTable分页
    /// <summary>
    /// 根据索引和pagesize返回记录
    /// </summary>
    /// <param name="dt">记录集 DataTable</param>
    /// <param name="PageIndex">当前页</param>
    /// <param name="pagesize">一页的记录数</param>
    /// <returns></returns>
    public static DataTable SplitDataTable(DataTable dt, int PageIndex, int PageSize)
    {
        if (PageIndex == 0)
            return dt;
        DataTable newdt = dt.Clone();
        //newdt.Clear();
        int rowbegin = (PageIndex - 1) * PageSize;
        int rowend = PageIndex * PageSize;

        if (rowbegin >= dt.Rows.Count)
            return newdt;

        if (rowend > dt.Rows.Count)
            rowend = dt.Rows.Count;
        for (int i = rowbegin; i <= rowend - 1; i++)
        {
            DataRow newdr = newdt.NewRow();
            DataRow dr = dt.Rows[i];
            foreach (DataColumn column in dt.Columns)
            {
                newdr[column.ColumnName] = dr[column.ColumnName];
            }
            newdt.Rows.Add(newdr);
        }

        return newdt;
    }
    #endregion

    #endregion

    #region 检测用户提交页面，是否为外部提交
    /// <summary>
    /// 检测用户提交页面
    /// </summary>
    /// <param name="rq"></param>
    public static bool Check_Post_Url(HttpContext context)
    {
        string WebHost = string.Empty, From_Url = string.Empty;
        bool IsRisk = true;
        if (context.Request.ServerVariables["SERVER_NAME"] != null)
        {
            WebHost = context.Request.ServerVariables["SERVER_NAME"].ToString();
        }
        if (context.Request.UrlReferrer != null)
        {
            From_Url = context.Request.UrlReferrer.ToString();
        }

        if (From_Url == string.Empty || WebHost == string.Empty)
        {
            IsRisk = false;
        }
        else
        {
            WebHost = "HTTP://" + WebHost.ToUpper();
            From_Url = From_Url.ToUpper();
            int a = From_Url.IndexOf(WebHost);
            if (From_Url.IndexOf(WebHost) < 0)
            {
                IsRisk = false;
            }
        }
        return IsRisk;
    }
    #endregion

    #region 防刷新检测
    /// <summary>
    /// 防刷新检测
    /// </summary>
    /// <param name="Second">访问间隔秒</param>
    /// <param name="UserSession"></param>
    public static bool CheckRefurbish(int Second, HttpSessionState UserSession)
    {

        bool i = true;
        if (UserSession["RefTime"] != null)
        {
            DateTime d1 = Convert.ToDateTime(UserSession["RefTime"]);
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));
            TimeSpan d3 = d2.Subtract(d1);
            if (d3.Seconds < Second)
            {
                i = false;
            }
            else
            {
                UserSession["RefTime"] = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            }
        }
        else
        {
            UserSession["RefTime"] = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
        }

        return i;
    }
    #endregion

    #region 设置页面不被缓存
    /// <summary>
    /// 设置页面不被缓存
    /// </summary>
    public static void SetPageNoCache()
    {

        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
        HttpContext.Current.Response.Expires = 0;
        HttpContext.Current.Response.CacheControl = "no-cache";
        HttpContext.Current.Response.AddHeader("Pragma", "No-Cache");
    }
    #endregion

    #region 格式化TextArea输入内容为html显示
    /// <summary>
    /// 格式化TextArea输入内容为html显示
    /// </summary>
    /// <param name="s">要格式化内容</param>
    /// <returns>完成内容</returns>
    public static string FormatTextArea(string s)
    {
        s = s.Replace("\n", "<br>");
        s = s.Replace("\x20", "&nbsp;");
        return s;
    }
    #endregion
    #region 数组相加
    /// <summary>
    /// 数组相加
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    public static int ArraySumInt(string[] array)
    {
        int len = array.Length;
        if (len > 0)
        {
            int sum = 0;
            for (int i = 0; i < len; i++)
            {
                sum += Common.S_Int(array[i]);
            }

            return sum;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// 数组排序
    /// </summary>
    /// <param name="array">数组</param>
    /// <param name="sortType">0取最小值，1取最大值</param>
    /// <param name="dotLen">小数点位数</param>
    /// <returns></returns>
    public static string ArraySort(string[] array, byte sortType, byte dotLen)
    {
        int len = array.Length;
        if (len > 0)
        {
            decimal[] iArray = new decimal[len];
            for (int i = 0; i < len; i++)
            {
                iArray[i] = Common.C_Decimal(array[i]);
            }

            Array.Sort(iArray);

            if (sortType == 0)
                return iArray[0].ToString("F" + dotLen);
            else
                return iArray[len - 1].ToString("F" + dotLen);
        }
        else
        {
            return "0";
        }
    }
    #endregion
}