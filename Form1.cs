using App_Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace cydiacenTool
{
    public partial class Form1 : Form
    {
        int flag = 0;
        int flag2 = 0;
        public Form1()
        {
            
            InitializeComponent();
        }

       

        private void ReleaseCapture()
        {
            throw new NotImplementedException();
        }

        #region 将对应的数据库数据类型转换为相应格式的变量

        public string toSql(string x)
        {
            string cc = "";
            x = x.ToLower();
            switch (x)
            {
                case "bigint": cc = "BigInt "; break;
                case "binary": cc = "Binary "; break;
                case "bit": cc = "Bit "; break;
                case "char": cc = "Char "; break;
                case "datetime": cc = "DateTime "; break;
                case "decimal": cc = "Decimal "; break;
                case "float": cc = "Float "; break;
                case "image": cc = "Image "; break;
                case "int": cc = "Int,8 "; break;
                case "money": cc = "Money "; break;
                case "nchar": cc = "NChar "; break;
                case "ntext": cc = "NText "; break;
                case "nvarchar": cc = "NVarChar "; break;
                case "uniqueidentifier": cc = "UniqueIdentifier "; break;
                case "smalldatetime": cc = "SmallDateTime "; break;
                case "smallint": cc = "SmallInt "; break;
                case "smallmoney": cc = "SmallMoney "; break;
                case "text": cc = "Text "; break;
                case "timestamp": cc = "Timestamp "; break;
                case "tinyint": cc = "TinyInt "; break;
                case "varbinary": cc = "VarBinary "; break;
                case "varchar": cc = "VarChar "; break;
                case "variant": cc = "Variant "; break;
                case "xml": cc = "Xml "; break;
                case "udt": cc = "Udt "; break;
                case "structured": cc = "Structured "; break;
                case "date": cc = "Date "; break;
                case "time": cc = "Time "; break;
                case "datetime2": cc = "DateTime2 "; break;
                case "datetimeoffset": cc = "DateTimeOffset "; break;
            }
            return cc;
        }

        #endregion

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                
                button5.Show();
            }
            else {
                button5.Hide();
            }
        }
        #region 设置置顶
        private void button2_Click(object sender, EventArgs e)
        {
            if (flag == 0)
            {
                this.TopMost = true;
                button2.Text = "已置顶(&`)";
                flag = 1;
            }
            else {
                this.TopMost = false;
                button2.Text = "已取消置顶(&`)";
                flag = 0;
            }
        }
        #endregion
        #region 移动窗体
        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标签是否为左键
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                this.Location = mouseSet;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }
        #endregion
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //首尾加字符
        private void button3_Click(object sender, EventArgs e)
        {
            
            string ss,str="",cc="";
            ss=textBox1.Text;
            while (ss.Trim() != "") {
                ss = ss.Trim();
                Regex reg =new Regex("[^\r\n]*");
                cc=reg.Match(ss).ToString();
                if (checkBox1.Checked) {
                    cc = cc.Replace("\'", "\"");
                }
                if (checkBox2.Checked)
                {
                    cc = cc.Replace("\"", "\'");
                }
                //是否将内部的单引号转双引号。
                int count = cc.Length;
                ss = ss.Substring(count);
                str += textBox3.Text + cc + textBox4.Text + "\r\n";
            }
            textBox2.Text = str;
            Clipboard.SetText(textBox2.Text);
            textBox2.Copy();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                button6.Show();
            }
            else { button6.Hide(); }
        }

        //透明度拉条
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.Opacity = ((double)(100 - trackBar1.Value)) / 100;
        }

        //去换行
        private void button7_Click(object sender, EventArgs e)
        {
            string ss, str = "", cc = "";
            ss = textBox1.Text;
            while (ss.Trim() != "")
            {
                ss = ss.Trim();
                Regex reg = new Regex("[^\r\n]*");
                cc = reg.Match(ss).ToString();
                if (checkBox1.Checked)
                {
                    cc = cc.Replace("\'", "\"");
                }
                if (checkBox2.Checked)
                {
                    cc = cc.Replace("\"", "\'");
                }
                //是否将内部的单引号转双引号。
                int count = cc.Length;
                ss = ss.Substring(count);
                str += cc ;
            }
            textBox2.Text = str;
            Clipboard.SetText(textBox2.Text);
            textBox2.Copy();
        }

        

        #region CMD功能界面
        public static bool CloseProcess(string ProcName)
        {
            bool result = false;
            System.Collections.ArrayList procList = new System.Collections.ArrayList();
            string tempName = "";
            int begpos;
            int endpos;
            foreach (System.Diagnostics.Process thisProc in System.Diagnostics.Process.GetProcesses())
            {
                tempName = thisProc.ToString();
                begpos = tempName.IndexOf("(") + 1;
                endpos = tempName.IndexOf(")");
                tempName = tempName.Substring(begpos, endpos - begpos);
                procList.Add(tempName);
                if (tempName == ProcName)
                {
                    if (!thisProc.CloseMainWindow())
                        thisProc.Kill(); // 当发送关闭窗口命令无效时强行结束进程
                    result = true;
                }
            }
            return result;
        }

        public static string Cmd(string[] cmd)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.AutoFlush = true;
            for (int i = 0; i < cmd.Length; i++)
            {
                p.StandardInput.WriteLine(cmd[i].ToString());
            }
            p.StandardInput.WriteLine("exit");
            string strRst = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            p.Close();
            return strRst;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }
        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Show();
            textBox2.Show();
            textBox3.Show();
            textBox4.Show();
            //button1.Show();
            button3.Show();
            //button5.Show();
            //button6.Show();
            button7.Show();
            checkBox1.Show();
            button10.Hide();
            button12.Hide();
            button13.Hide();
            //button14.Show();
            comboBox1.Show();
            button1.Hide();
            button14.Hide();
            textBox5.Hide();
            label1.Hide();
        }
        //公司IP

        private void button10_Click(object sender, EventArgs e)
        {
            string[] cmd = new string[] { "netsh interface ip set address name=\"WLAN\" static 192.168.0.66 255.255.255.0 192.168.0.1","netsh interface ip set address name=\"WLAN 2\" static 192.168.0.66 255.255.255.0 192.168.0.1","netsh interface ip set dnsservers name=\"WLAN\" static 101.226.4.6","netsh interface ip set dnsservers name=\"WLAN 2\" static 101.226.4.6","netsh interface ip add dnsservers name=\"WLAN\" 101.226.4.6","netsh interface ip add dnsservers name=\"WLAN 2\" 101.226.4.6" };
            textBox2.Text = Cmd(cmd);
            CloseProcess("cmd.exe");


        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Hide();
            textBox3.Hide();
            textBox4.Hide();
            //button1.Hide();
            button3.Hide();
            button5.Hide();
            button6.Hide();
            button7.Hide();
            checkBox1.Hide();
            button10.Show();
            button12.Show();
            button13.Show();
            //button14.Hide();
            comboBox1.Hide();
            button1.Show();
            button14.Show();
            textBox5.Show();
            label1.Show();
            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string[] cmd = new string[] { "ipconfig" };
            Regex reg = new Regex("(IPv4 )([^\\W]|[\\D])+");
            string tt = Cmd(cmd);
            textBox2.Text = "\r\n\r\n\r\n\r\n\r\n\r\n   " + reg.Match(tt).ToString().Substring(0, 148).Replace(". ", "");
            //textBox2.Text = Cmd(cmd).Substring(601, 300);
            CloseProcess("cmd.exe");
        }
        //自动IP
        private void button13_Click(object sender, EventArgs e)
        {
            string[] cmd = new string[] { "netsh interface ip set address name=\"WLAN\" dhcp","netsh interface ip set address name=\"WLAN 2\" dhcp","netsh interface ip set dnsservers name=\"WLAN\" dhcp","netsh interface ip set dnsservers name=\"WLAN 2\" dhcp "};
            textBox2.Text = Cmd(cmd);
            CloseProcess("cmd.exe");
        }

        //家里IP
        private void button1_Click(object sender, EventArgs e)
        {
            string[] cmd = new string[] { "netsh interface ip set address name=\"WLAN\" static 192.168.1.166 255.255.255.0 192.168.1.1", "netsh interface ip set address name=\"WLAN 2\" static 192.168.1.166 255.255.255.0 192.168.1.1", "netsh interface ip set dnsservers name=\"WLAN\" static 101.226.4.6", "netsh interface ip set dnsservers name=\"WLAN 2\" static 101.226.4.6", "netsh interface ip add dnsservers name=\"WLAN\" 101.226.4.6", "netsh interface ip add dnsservers name=\"WLAN 2\" 101.226.4.6" };
            textBox2.Text = Cmd(cmd);
            CloseProcess("cmd.exe");
        }
        #endregion

        //截图界面唤醒
        private void button15_Click(object sender, EventArgs e)
        {
            new Form2().Show();
        }

        #region SQL
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string aa, bb, cc, dd, ee, str="", ss;
             ss = textBox1.Text;
            switch (comboBox1.Text) { 
                case "linq转sql":

                    break;
                case "显示截图":
                    if (!Clipboard.ContainsImage())
                    {
                        MessageBox.Show("请先截取图片！");
                        break;
                    }
                    new Form3().Show();
                    break;

                    /*
                     
                     */
                case "插入转换":
                    cc="";dd="";
                    while (ss.Trim() != "")
                    {
                        ss = ss.Trim();
                        Regex rx = new Regex("[^\n]*");
                        aa = rx.Match(ss).ToString();
                        rx = new Regex("(^|[@])([^ ]*)");
                        bb = rx.Match(aa).ToString();
                        dd += bb + ",";
                        bb = bb.Substring(1);
                        int count = aa.Length;
                        ss = ss.Substring(count);
                        cc += bb+",";
                        //str += "insert into (,E_Name,E_Host,E_UnderTake,E_StartTime,E_OverTime,E_Rules,E_RuleAddr,E_RegistBegin	,E_RegistOver,E_Poster,E_Browse,E_EventState,E_Cast,E_Remark)	values(@a,@E_Name,@E_Host,@E_UnderTake,@E_StartTime,@E_OverTime,@E_Rules,@E_RuleAddr,@E_RegistBegin)";
                    }
                    str = "insert into XXXXXX(" + cc.Substring(0,cc.Length-1) + ")   values(" + dd.Substring(0,dd.Length-1) + ")";
                    textBox2.Text = str;
                    Clipboard.SetText(textBox2.Text);
                    textBox2.Copy();
                    break;
                case "更新转换":
                    cc="";dd="";
                    while (ss.Trim() != "")
                    {
                        ss = ss.Trim();
                        Regex rx = new Regex("[^\n]*");
                        aa = rx.Match(ss).ToString();
                        rx = new Regex("(^|[@])([^ ]*)");
                        bb = rx.Match(aa).ToString();
                        dd = bb;
                        bb = bb.Substring(1);
                        int count = aa.Length;
                        ss = ss.Substring(count);
                        cc += bb + "=" + dd+",";
                    }
                    str = "update set XXXXXX  " + cc.Substring(0,cc.Length-1) + "";
                    textBox2.Text = str;
                    Clipboard.SetText(textBox2.Text);
                    textBox2.Copy();
                    break;

                /*
                 用法：复制如下规格内容进行转换，TABLE大写
                 * CREATE TABLE M_MerchantInfo(
                    ID INT IDENTITY(1,1) NOT NULL,--打发似的
                    。。。。
                 */
                case "添加表备注1":
                    cc="";dd="";str="";bb="";string mark="";
                    bool flag = false;
                    while (ss.Trim() != "")
                    {
                        ss = ss.Trim();
                        Regex rx = new Regex("[^\n]*");
                        Regex re = new Regex("--.+");
                        aa = rx.Match(ss).ToString();
                        if (!flag)
                        {
                            rx = new Regex("(TABLE)[^(]+");
                            bb = rx.Match(aa).ToString();
                            mark = re.Match(aa).ToString().Substring(2, mark.Length - 3);
                            bb=bb.Substring(6);
                            flag = true;
                            str += "execute sp_addextendedproperty N'MS_Description', N'"+mark+"', N'user', N'dbo', N'table', N'" + bb + "', null,null \r\n go\r\n";
                        }
                        else {
                            rx = new Regex("(^)([^ |^\t]*)");
                            dd = rx.Match(aa).ToString();
                            mark = re.Match(aa).ToString();
                            if (mark.Substring(mark.Length - 1) == "\r")
                            {
                                mark = mark.Substring(2, mark.Length - 3);
                            }
                            else
                            {
                                mark = mark.Substring(2, mark.Length - 2);
                            }
                            str += "execute sp_addextendedproperty N'MS_Description', N'"+mark+"', N'user', N'dbo', N'table', N'" + bb + "', N'column',N'" + dd + "' \r\n go\r\n";
                        }
                        int count = aa.Length;
                        ss = ss.Substring(count);
                    }
                    str = "constraint PK_CompetitionInfo_ID primary key clustered (ID)\r\n ) \r\n go\r\n" + str;
                    textBox2.Text = str;
                    Clipboard.SetText(textBox2.Text);
                    textBox2.Copy();
                    break;
                    /*
                     用法：
                     * 将数据词典中的备注信息 整列复制到左文本框
                     */
                case "添加表备注2":
                    cc = ""; dd = "";string aa1="";
                    ss = textBox2.Text;
                    string ss1 = textBox1.Text;
                    while (ss.Trim() != "")
                    {
                        ss = ss.Trim();
                        ss1 = ss1.Trim();
                        Regex rx = new Regex("[^\r\n]*");
                        aa = rx.Match(ss).ToString();
                        int count = aa.Length;
                        aa1 = rx.Match(ss1).ToString();
                        rx = new Regex("XXXX");
                        if (rx.IsMatch(aa))
                        {
                            aa=aa.Replace("XXXX", aa1);
                            ss1 = ss1.Substring(aa1.Length);
                        }
                        ss = ss.Substring(count);
                        str += aa+"\r\n";
                    }
                    textBox2.Text = str;
                    Clipboard.SetText(textBox2.Text);
                    textBox2.Copy();
                    break;
                    /*
                     用法：
                     * 将存储过程中所需要的参数复制到左边文本框
                     */
                case "参数转换":
                    int nn = 0;
                     aa=""; bb=""; cc=""; dd=""; ee=""; str = "";
                    while (ss.Trim() != "")
                    {
                        ss = ss.Trim();
                        Regex rx = new Regex("[^\n]*");
                        aa = rx.Match(ss).ToString();
                        rx = new Regex("(^|[@])([^ ]*)");
                        bb = rx.Match(aa).ToString();
                        rx = new Regex("( )([^-]*)");
                        cc = rx.Match(aa).ToString();
                        rx = new Regex("(output)|(,output)");
                        ee = rx.Match(aa.ToLower()).ToString();
                        if (ee == "output")
                        {
                            cc = cc.Trim().Substring(0, cc.Length - 7);
                            bb = "MakeOutParam(\"" + bb;
                        }
                        else if (ee == ",output")
                        {
                            cc = cc.Trim().Substring(0, cc.Length - 7);
                            bb = "MakeOutParam(\"" + bb;
                        }
                        else
                        {
                            bb = "MakeInParam(\"" + bb;
                        }
                        string text = "";
                        text = cc.Trim();
                        text = text.Substring(text.Length - 2, 2);
                        if (text != ")," && cc.Trim().Substring(cc.Trim().Length - 1, 1) != ")")
                        {
                            dd = "";
                        }
                        else
                        {
                            rx = new Regex("[(]+[^,|)]+");
                            dd = rx.Match(cc).ToString();
                            dd = dd.Substring(1);
                            if (dd.ToLower() == "max") { dd = "-1"; };
                            dd += ",";
                        }
                        rx = new Regex("[A-Za-z]+");
                        cc = rx.Match(cc).ToString();
                        int count = aa.Length;
                        ss = ss.Substring(count);
                        str += bb + "\",SqlDbType." + toSql(cc.ToLower()) + "," + dd + "cmdParms[" + nn + "]),\r\n";
                        nn++;
                    }
                    textBox2.Text = str.Substring(0, str.Length - 3);
                    Clipboard.SetText(textBox2.Text);
                    textBox2.Copy();
                    break;
                case "输出request":
                    cc = ""; dd = ""; aa1="";
                    ss = textBox1.Text+",";
                    str = "";
                    int i=0;
                     ss1 = textBox1.Text;
                    while (ss.Trim() != "")
                    {
                        ss = ss.Trim();
                        ss1 = ss1.Trim();
                        Regex rx = new Regex("[^,]+");
                        aa = rx.Match(ss).ToString();
                        str += "string a" + i + " =" + aa +";\r\n";
                        int count = aa.Length;
                        ss = ss.Substring(1);
                        ss = ss.Substring(count);
                        i++;
                    }
                    textBox2.Text = str;
                    Clipboard.SetText(textBox2.Text);
                    textBox2.Copy();
                    break;
                case "获取mac地址":
                    MacPhysicalAddress mac = new MacPhysicalAddress();
                    for (int j = 0; j < mac.GetAllNic().Count; j++)
                    {
                        textBox2.Text += mac.GetAllNic()[j];
                    }
                    break;
            }
        }
        #endregion
        #region 定时关机
        private void button14_Click(object sender, EventArgs e)
        {
            
            if (flag2 == 0)
            {
                string[] cmd = new string[] { "shutdown -s -t " + int.Parse(textBox5.Text) * 60 + "" };
                textBox2.Text = Cmd(cmd);
                CloseProcess("cmd.exe");
                this.button14.Text = "取消关机";
                flag2 = 1;
            }
            else {
                string[] cmd = new string[] { "shutdown -a" };
                textBox2.Text = Cmd(cmd);
                CloseProcess("cmd.exe");
                this.button14.Text = "定时关机";
                flag2 = 0;
            }
        }
        #endregion
        #region 前端通用
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = "",rechange="";
            int num = 0; Regex reg = new Regex("(select).+(from)");
            string[] sArray = new  string[]{};
            switch (comboBox2.Text)
            {
                    /*
                     用法：输入含有select from where order by 的sql语句
                     * 作用：根据简单的查询语句生成通用的C#分页方法
                     * 
                     */
                case "C_CommonList":
                    string text= textBox1.Text.ToLower();
                    string table = "", field = "", order = "", where = "";
                    reg = new Regex("(select).+(from)");

                    field = reg.Match(text).ToString();
                    text=text.Substring(field.Length-4);//
                    field = field.Substring(6, field.Length - 10);

                    reg = new Regex("(from).+(where)");

                    table = reg.Match(text).ToString();
                    text=text.Substring(table.Length-5);//
                    table = table.Substring(4, table.Length - 9);

                    reg = new Regex("(where).+(order)");

                    where = reg.Match(text).ToString();
                    text=text.Substring(where.Length-5);//
                    where = where.Substring(5, where.Length - 10);

                    reg = new Regex("(order).+");

                    order = reg.Match(text).ToString();
                    text=text.Substring(order.Length-5);//
                    order = order.Substring(5);

                    str += "string pagecount = \"\", recordCount = \"\";\r\n";
                    str += "\r\nif (dp.C_CommonList(new string[] { page, \"6\", \"" + field + "\", \"" + table + "\", \"" + where + "\", \""+order+"\" }, ref pagecount, ref recordCount, ref list) == \"1000\")\r\n";
                    str += "{\r\n";
                    str += "}\r\n";
                    textBox2.Text = str;
                    Clipboard.SetText(textBox2.Text);
                    textBox2.Copy();
                    break;
                case "Request.Form":
                    rechange = textBox1.Text;
                    str += "string ";
                    sArray = rechange.Split(',');
                    num=sArray.Length;
                    for (int i = 0; i < num; i++)
                    {
                        str += sArray[i] + " = Request.Form[\""+sArray[i]+"\"],";
                    }
                    str=str.Substring(0,str.Length - 1);
                    textBox2.Text = str+";";
                    Clipboard.SetText(textBox2.Text);
                    textBox2.Copy();
                    break;
                case "服务端html":
                    string ss,cc="";
                    ss=textBox1.Text;
                    while (ss.Trim() != "") {
                        ss = ss.Trim();
                        reg = new Regex("[^\r\n]*");
                        cc=reg.Match(ss).ToString();
                        cc = cc.Replace("\"", "\'");
                        int count = cc.Length;
                        ss = ss.Substring(count);
                        str += "html+=\"" + cc + "\";" + "\r\n";
                    }
                    textBox2.Text = str;
                    Clipboard.SetText(textBox2.Text);
                    textBox2.Copy();
                    break;
                case "DataTable":
                    str = "";
                    string[] array = new string[] {};
                    ss = textBox1.Text;
                    while (ss.Trim() != "")
                    {
                        ss = ss.Trim();
                        reg = new Regex("[^\r\n]*");
                        cc = reg.Match(ss).ToString();
                        int count = cc.Length;
                        ss = ss.Substring(count);
                        array= array.Mypush(cc);
                        
                    }
                    str+="DateTime time = Convert.ToDateTime(dp.C_ExecuteScalar(\"select getdate()\"));\r\n";
                    str+="string[] DataInfo = GroupArray[i].Split('|');\r\n";
                    str+="if (GroupInfo[0].ToString() == \"0\")\r\n";
                    str+="{\r\n";
                    str+="DataRow dr = dt.NewRow();\r\n";
                    
                    num = array.Length - 3;
                    for (int i = 0; i < num ; i++)
                    {
                        str += "dr[\"" + array[i] + "\"] = DataInfo[" + i + "];\r\n";
                    }
                    str+="dr[\"AddDay\"] = time.ToString(\"yyyyMMdd\");\r\n";
                    str+="dr[\"AddTime\"] = time.ToString();\r\n";
                    str+="dr[\"Mark\"] = \"0\";\r\n";
                    str+="dt.Rows.Add(dr);\r\n";
                    str+="}\r\n";
                    str+="else\r\n";
                    str+="{\r\n";

                    for (int i = 0; i < num; i++)
                    {
                        str += "dt.Rows[i][\""+array[i]+"\"] = GroupInfo["+i+"];\r\n";
                    }
                    str+="dt.Rows[i][\"AddDay\"] = time.ToString(\"yyyyMMdd\");\r\n";
                    str+="dt.Rows[i][\"AddTime\"] = time.ToString();\r\n";
                    str+="dt.Rows[i][\"Mark\"] = \"0\";\r\n";
                    str+="}\r\n";
                    textBox2.Text = str;
                    Clipboard.SetText(textBox2.Text);
                    textBox2.Copy();
                    break;
                case "html还原":
                    ss=textBox1.Text;
                    while (ss.Trim() != "") {
                        ss = ss.Trim();
                        reg = new Regex("[^\r\n]*");
                        cc=reg.Match(ss).ToString();
                        int count = cc.Length;
                        ss = ss.Substring(count);
                        cc = cc.Replace("html+=\"", "");
                        cc = cc.Replace("html += \"", "");
                        cc = cc.Replace("html+=\'", "");
                        cc = cc.Replace("html += \'", "");
                        cc = cc.Replace("\";", "");
                        cc = cc.Replace("\';", "");
                        
                        str += "" + cc + "\r\n";
                    }
                    textBox2.Text = str;
                    Clipboard.SetText(textBox2.Text);
                    textBox2.Copy();
                    break;
                case "生成数组":
                    ss = textBox1.Text;
                    ss = ss.Trim();
                    str = "//" + ss + "\r\n";
                    str += "var arr = ['";
                    ss = ss.Replace(",","','");
                    reg = new Regex(@"\d*");
                    ss = reg.Replace(ss,"");
                    str +=ss+ "'];\r\n";
                     textBox2.Text = str;
                    Clipboard.SetText(textBox2.Text);
                    textBox2.Copy();
                    break;
                case "MD5":
                    ss = textBox1.Text;
                    ss = ss.Trim();
                    MD5 md5hash = MD5.Create();
                    // Convert the input string to a byte array and compute the hash.
                    byte[] data = md5hash.ComputeHash(Encoding.UTF8.GetBytes(ss));

                    // Create a new Stringbuilder to collect the bytes
                    // and create a string.
                    StringBuilder sBuilder = new StringBuilder();

                    // Loop through each byte of the hashed data 
                    // and format each one as a hexadecimal string.
                    for (int i = 0; i < data.Length; i++)
                    {
                        sBuilder.Append(data[i].ToString("x2"));
                    }
                    textBox2.Text = sBuilder.ToString();
                    Clipboard.SetText(textBox2.Text);
                    textBox2.Copy();
                    break;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            new planTime().Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            new CreateFile().Show();
        }
    }
        #endregion
    

}
