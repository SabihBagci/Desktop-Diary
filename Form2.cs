using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace diary
{
    public partial class Form2 : Form
    {

        private string user_id;

        public void setUserId(string id)
        {
            user_id = id;
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            findText();
            
            
            
        }
        private void frm_menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            findText();
        }

        private void findText()
        {
            bool dataFound = false;

            string selectedDate = monthCalendar1.SelectionStart.Day + "-" + monthCalendar1.SelectionStart.Month + "-" + monthCalendar1.SelectionStart.Year;

            lblSelectedDate.Text = "Selected Date : " + selectedDate; 

            string[] dataList = File.ReadAllLines("data.txt");
            foreach (string data in dataList)
            {
                string[] dataArray = data.Split('~');

                string user_id = dataArray[0];
                string dateOfData = dataArray[1];
                string text = dataArray[2];

                if (user_id == this.user_id && dateOfData == selectedDate)
                {
                    dataFound = true;
                    richTextBox1.Lines = text.Split(new string[] { "\\n" }, StringSplitOptions.None);
                    break;
                }
            }

            if (!dataFound)
            {
                richTextBox1.Clear();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("THIS FEATURE WILL BE ENABLE LATER");
        }

        private void aBOUTToolStripMenuItem_Click(object sender, EventArgs e)
        {

            MessageBox.Show("BAU - 2019, Check Contact For More.");

        }

        private void cONTACTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Contact to Our Designers and Developers On Instagram: @ebrualacagoz & @resullunal & @sabihbagci");

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool dataFound = false;

            string selectedDate = monthCalendar1.SelectionStart.Day + "-" + monthCalendar1.SelectionStart.Month + 
                "-" + monthCalendar1.SelectionStart.Year;

            string[] dataList = File.ReadAllLines("data.txt");

            for(int i = 0; i < dataList.Length; i++)
            {
                string[] dataArray = dataList[i].Split('~');

                string user_id = dataArray[0];
                string dateOfData = dataArray[1];
                string text = dataArray[2];                

                
                if (user_id == this.user_id && dateOfData == selectedDate)
                {
                    string line = arrayToString(richTextBox1.Lines);

                    dataList[i] = user_id + "~" + selectedDate + "~" + line;
                    dataFound = true;
                    
                    File.WriteAllLines("data.txt", dataList);
                    break;
                }
            }

            
            if (!dataFound)
            {
                string[] newDataList = new string[dataList.Length + 1];
                for(int i = 0; i < dataList.Length; i++)
                {
                    newDataList[i] = dataList[i];
                }
                newDataList[newDataList.Length - 1] = user_id + "~" + selectedDate + "~" + richTextBox1.Text;

                File.WriteAllLines("data.txt", newDataList);
            }
        }

        public string arrayToString(string[] x)
        {
            string result = "";
            for (int j = 0; j < x.Length - 1; j++)
            {
                result += x[j] + "\\n";
            }
            result += x[x.Length - 1];
            return result;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 ff = new Form1();
    
            ff.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string[] dataList = File.ReadAllLines("data.txt");
            string selectedDate = monthCalendar1.SelectionStart.Day + "-" + monthCalendar1.SelectionStart.Month + 
                "-" + monthCalendar1.SelectionStart.Year;

            int indexOfData = -1;

            for (int s = 0; s < dataList.Length; s++)
            {
                string[] data = dataList[s].Split('~');
                string user_id = data[0];
                string dateOfData = data[1];

                if (user_id == this.user_id && dateOfData == selectedDate)
                {
                    indexOfData = s;
                    break;
                }
            }

            if (indexOfData != -1)
            {
                string[] newArray = new string[dataList.Length-1];

                for (int i = 0; i < dataList.Length; i++)
                {
                    if (i == indexOfData)
                    {
                        continue;
                    }

                    if (i < indexOfData)
                    {
                        newArray[i] = dataList[i];
                    }
                    else 
                    {
                        newArray[i-1] = dataList[i];
                    }
                }

                File.WriteAllLines("data.txt", newArray);
                richTextBox1.Clear();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
} 
