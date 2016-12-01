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
using System.Net;

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
            button2.Enabled = false;
            button3.Enabled = false;
            listView1.Items.Clear();
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

        }

        private void pingIP(string strIP)
        {
            ip ipEnum = new ip();
            ListViewItem lvi = new ListViewItem();
            lvi.Text = strIP;
            string retPing = ipEnum.ping(strIP);
            if (retPing != "超时")
            {
                try
                {
                    IPHostEntry myscanhost = Dns.GetHostEntry(strIP);
                    string strHost = myscanhost.HostName.ToString();
                    lvi.SubItems.Add(strHost);
                }
                catch
                {
                    lvi.SubItems.Add("");
                }
            }
            else
            {
                lvi.SubItems.Add("");
            }
           
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
            ip ipEnum = new ip();
            int barMax = Math.Abs(ipEnum.IPToNumber(textBox2.Text) - ipEnum.IPToNumber(textBox1.Text));
            if (progressBar1.Value == barMax)
            {
                button1.Enabled = true;
                TaskbarProgress.SetState(this.Handle, TaskbarProgress.TaskbarStates.NoProgress);
                button2.Enabled = true;
                button3.Enabled = true;
            }
            else
            {
                TaskbarProgress.SetValue(this.Handle, listView1.Items.Count, barMax);
            }
            progressBar1.Value = listView1.Items.Count;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            foreach( ListViewItem Item in this.listView1.Items)
            {
                if (Item.SubItems[2].Text == "超时")
                {
                    Item.Remove();
                }
            }
            button3.Enabled = true;
        }
    }
}
