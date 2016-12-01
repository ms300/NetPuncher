using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace NetPuncher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            ip ipEnum = new ip();
            int num1 = ipEnum.IPToNumber(textBox1.Text);
            int num2 = ipEnum.IPToNumber(textBox2.Text);
            progressBar1.Minimum =0;
            progressBar1.Maximum = num2-num1+1;
            var lst = new PingList();
            for (int i = num1; i <= num2; i++)
            {
                string strIP = ipEnum.NumberToIP(i);
                var NewTask = new Action(() =>
                {
                    //Console.WriteLine(string.Format("第{0}个任务（用时{1}秒）已经开始", j, s));
                    //Thread.Sleep(s * 1000);
                    //Console.WriteLine(string.Format("第{0}个任务（用时{1}秒）已经结束", j, s));
                    pingIP(strIP);
                });
                lst.Tasks.Add(NewTask);
                //progressBar1.Value = i-num1;

            }
            lst.Start();
            button1.Enabled = true;

        }

        private void pingIP(string strIP)
        {
            ip ipEnum = new ip();
            ListViewItem lvi = new ListViewItem();
            lvi.Text = strIP;
            string retPing = ipEnum.ping(strIP);
            lvi.SubItems.Add("");
            lvi.SubItems.Add(retPing);
            SetListItem(lvi);
        }

        private void SetListItem(ListViewItem obj)
        {
            if (this.listView1.InvokeRequired)
            {
                this.Invoke(new Action<ListViewItem>(this.AddListItem), obj);
            }
            else
            {
                this.listView1.Items.Add(obj);
            }

        }

        private void AddListItem(ListViewItem str)
        {
            this.listView1.Items.Add(str);
            progressBar1.Value = listView1.Items.Count;
        }

    }
}
