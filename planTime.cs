using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms.VisualStyles;

namespace cydiacenTool
{
    public partial class planTime : Form
    {
        private Time mTime = null;
        private Thread mDisplayThread = null;
        public delegate void UpdateLabel();//声明一个委托
        public UpdateLabel updateLabel;//定义一个委托
        private int nowListIndex = 0;
        public List<object[]> FilterItem = new List<object[]>();
        public planTime()
        {
            InitializeComponent();
            mTime = new Time();
            updateLabel = new UpdateLabel(UpdateTime);//实例化一个委托对象
        }
        

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 )
            {
                var item = dataGridView1.Rows[e.RowIndex].Cells;
                //nowListIndex = e.RowIndex;
                //content.Text =Convert.ToString(item[1].Value);
            }
        }
        //暂停继续按钮
        private void button1_Click(object sender, EventArgs e)
        {
            switch (button1.Text)
            {
                case "暂停":
                    mTime._flag = false;
                    button1.Text = "继续";
                    break;
                case "继续":
                    mTime._flag = true;
                    button1.Text = "暂停";
                    break;
            }
        }
        //开始暂停按钮
        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "开始")
            {
                if (content.Text.Trim() == "")
                {
                    MessageBox.Show("计划内容不能为空！");
                    return;
                }
                var obj = new object[6];
                obj[0] = nowListIndex;
                obj[1] = DateTime.MinValue.ToLongTimeString();
                obj[2] = content.Text;
                obj[3] = DateTime.Now;
                obj[4] = "";
                obj[5] = true;
                FilterItem.Add(obj);
                dataGridView1.Rows.Add();
                dataGridView1.Rows[nowListIndex].Cells[0].Value = obj[1];
                dataGridView1.Rows[nowListIndex].Cells[1].Value = obj[2];
                dataGridView1.Rows[nowListIndex].Cells[2].Value = obj[3];
                dataGridView1.Rows[nowListIndex].Cells[3].Value = obj[4];
                mTime.Start();
                mDisplayThread = new Thread(new ThreadStart(DisplayTimeFunc));
                mDisplayThread.Start();
                button2.Text = "完成";
            }
            else if (button2.Text == "完成")
            {
                mDisplayThread.Abort();
                mTime.Stop();
                mTime.Clear();
                FilterItem.Clear();
                dataGridView1.Rows[nowListIndex].Cells[3].Value = DateTime.Now;
                content.Text = "";
                button2.Text = "开始";
                nowListIndex++;
            }
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
        }
        public void DisplayTimeFunc()
        {
            while (true)
            {
                Thread.Sleep(1000);
                if (mTime._flag)
                {
                    this.Invoke(this.updateLabel);
                }
                Console.WriteLine("{0}", mTime.FormatTimeResult());
            }
        }
        public void UpdateTime() { dataGridView1.Rows[nowListIndex].Cells[0].Value = mTime.FormatTimeResult(); }
        private void planTime_FormClosing(object sender, FormClosingEventArgs e) { mDisplayThread.Abort(); }
        public class Time
        {
            private int _minute;
            private int _second;
            private int _hour;
            public bool _flag; //线程标识
            private Thread _TimingThread = null;

            public Time()
            {
                this._minute = 0;
                this._second = 0;
                this._hour = 0;
                this._flag = true;
            }
            /// <summary>
            /// 清零计时
            /// </summary>
            public void Clear()
            {
                this._minute = 0;
                this._second = 0;
                this._hour = 0;
                this._TimingThread.Abort();
            }
            /// <summary>
            /// 开始计时
            ///     ///
            ///  </summary> 
            public void Start()
            {
                this._flag = true;
                _TimingThread = new Thread(new ThreadStart(AddSecond));
                _TimingThread.Start();
            }

            /// <summary>    /// 线程执行方法    /// </summary>  
            private void AddSecond()
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    if (!this._flag)continue;
                    if (this._second == 59)
                    {
                        this._minute++;
                        this._second = 0;
                    }
                    else
                    {
                        this._second++;
                    }
                    if (this._minute == 60)
                    {
                        this._hour++;
                        this._minute = 0;
                    }
                    Console.WriteLine(this._flag);
                }

            }

            /// <summary>    /// 格式化显示计时结果    /// </summary>    /// <returns></returns>  
            public string FormatTimeResult()
            {
                string minute = string.Empty;
                string second = string.Empty;
                string hour = string.Empty;
                if (this._minute < 10)
                {
                    minute = "0" + this._minute;
                }
                else
                {
                    minute = this._minute.ToString();
                }
                if (this._second < 10)
                {
                    second = "0" + this._second.ToString();
                }
                else
                {
                    second = this._second.ToString();
                }
                hour = this._hour.ToString();
                return this._flag? hour + "小时" + minute + "分钟" + second + "秒":"";
            }

            /// <summary>    /// 停止    /// </summary> 
            public void Stop()
            {
                this._flag = false;
            }
            /// <summary>    /// 继续    /// </summary> 
            public void Continue()
            {
                this._flag = true;
            }
            /// <summary>    /// 归0操作    /// </summary>  
            public void Zero()
            {
                this._minute = 0;
                this._second = 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Clipboard.SetDataObject(dataGridView1.Text,false);
        }
    }
    
}

    
