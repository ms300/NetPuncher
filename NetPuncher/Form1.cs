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
            progressBar1.Maximum = Program.totalCount = num2 - num1+1;
            Program.scannedCount = 0;
            var lst = new TaskList();
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
            lvi.Font = new Font("微软雅黑",11,FontStyle.Regular);
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
            //添加其他栏目
            lvi.SubItems.Add(retPing);
            lvi.SubItems.Add("");
            lvi.SubItems.Add("");
            lvi.SubItems.Add("");
            lvi.SubItems.Add("");
            lvi.SubItems.Add("");
            lvi.SubItems.Add("");

            Program.scannedCount++;
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
                //this.listView1.Items.Add(obj);
                this.Invoke(new Action<ListViewItem>(this.AddListItem), obj);
            }

        }

        private void AddListItem(ListViewItem str)
        {
            this.listView1.Items.Add(str);
            ip ipEnum = new ip();
            //int barMax = Math.Abs(ipEnum.IPToNumber(textBox2.Text) - ipEnum.IPToNumber(textBox1.Text));
            int barMax = Program.totalCount;
            if (Program.scannedCount == barMax)
            {
                button1.Enabled = true;
                TaskbarProgress.SetState(this.Handle, TaskbarProgress.TaskbarStates.NoProgress);
                button2.Enabled = true;
                button3.Enabled = true;
            }
            else
            {
                TaskbarProgress.SetValue(this.Handle, Program.scannedCount, barMax);
            }
            progressBar1.Value = Program.scannedCount;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            listView1.BeginUpdate();
            foreach ( ListViewItem Item in this.listView1.Items)
            {
                if (Item.SubItems[2].Text == "超时")
                {
                    Item.Remove();
                }
            }
            listView1.EndUpdate();
            button3.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = listView1.Items.Count*6;
            progressBar1.Value = 0;
            Program.scannedCount = 0;
            Program.totalCount = listView1.Items.Count*6;
            var lst = new TaskList();
            foreach (ListViewItem Item in listView1.Items)
            {
                string strIP = Item.Text;
                int ItemIndex = Item.Index;
                ListViewItem obj = new ListViewItem();
                obj.Text = Item.Text;
                obj.SubItems.Add(Item.SubItems[0].Text);
                obj.SubItems.Add(Item.SubItems[1].Text);
                obj.SubItems.Add(Item.SubItems[2].Text);
                int[] arrPort = new int[6] { 445, 3306, 80, 443, 22, 21 };
                foreach (int Port in arrPort) {
                    var NewTask = new Action(() =>
                    {
                            //Console.WriteLine(string.Format("第{0}个任务（用时{1}秒）已经开始", j, s));
                            //Thread.Sleep(s * 1000);
                            //Console.WriteLine(string.Format("第{0}个任务（用时{1}秒）已经结束", j, s));
                            scanPort(ItemIndex, strIP, Port);
                    });
                    lst.Tasks.Add(NewTask);
                }
                //progressBar1.Value = i-num1;

            }
            lst.Start();

        }

        private void scanPort(int ItemIndex,string strIP,int strPort)
        {
            Program.scannedCount++;
            Scanner scr = new Scanner(strIP,strPort);
            bool ret = scr.Scan();
            //Scanner scr2 = new Scanner(strIP, 3306);
            //bool ret2 = scr2.Scan();
            //Scanner scr3 = new Scanner(strIP, 80);
            //bool ret3 = scr3.Scan();
            //Scanner scr4 = new Scanner(strIP, 443);
            //bool ret4 = scr4.Scan();
            //Scanner scr5 = new Scanner(strIP, 22);
            //bool ret5 = scr5.Scan();
            //Scanner scr6 = new Scanner(strIP, 21);
            //bool ret6 = scr6.Scan();
            //obj.SubItems.Add(ret1 ? "〇" : "×");
            //obj.SubItems.Add(ret2 ? "〇" : "×");
            //obj.SubItems.Add(ret3 ? "〇" : "×");
            //obj.SubItems.Add(ret4 ? "〇" : "×");
            //obj.SubItems.Add(ret5 ? "〇" : "×");
            //obj.SubItems.Add(ret6 ? "〇" : "×");
            string strRet = ret ? "〇" : "×";
            int SubIndex = int.Parse(Program.PortToIndex[strPort.ToString()]);
            this.Invoke(new Action<int,int,string>(this.ChangeListItem),ItemIndex,SubIndex,strRet);


        }

        private void ChangeListItem(int ItemIndex,int SubIndex,string str)
        {
            int barMax = Program.totalCount;
            if (Program.scannedCount == barMax)
            {
                button1.Enabled = true;
                TaskbarProgress.SetState(this.Handle, TaskbarProgress.TaskbarStates.NoProgress);
                button2.Enabled = true;
                button3.Enabled = true;
            }
            else
            {
                TaskbarProgress.SetValue(this.Handle, Program.scannedCount, barMax);
            }
            progressBar1.Value = Program.scannedCount;
            listView1.Items[ItemIndex].SubItems[SubIndex].Text = str;


        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://" + listView1.SelectedItems[0].Text);
        }

        private void 加入SSHHToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
