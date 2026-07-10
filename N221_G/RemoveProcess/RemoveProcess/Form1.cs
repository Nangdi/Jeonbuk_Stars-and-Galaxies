using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RemoveProcess
{
    public partial class Form1 : Form
    {

        private string choseProcessName = "Rs232_Console_Connection";
        public Form1(string[] args)
        {
            InitializeComponent();
            if (args.Length > 1) {
                choseProcessName = args[1];
                ProcessKill();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            //리스트 목록 정보
            Process[] processList= Process.GetProcesses();
            string choseProcessName = "Rs232_Console_Connection";

            for (int i=0;i< processList.Length; i++) {
                string ProcessName = processList[i].ProcessName;
                if (ProcessName.Equals(choseProcessName)) {

                    //프로세스 킬
                    textBox1.Text = ""+ choseProcessName + "\r\n";
                    //processList[i].Kill();
                    //Console.WriteLine("프로세스 내용 정보] : "+ choseProcessName);
                }
            }
        }


        private void ProcessKill() {
            Process[] processList = Process.GetProcesses();
            string choseProcessName = "Rs232_Console_Connection";

            for (int i = 0; i < processList.Length; i++)
            {
                string ProcessName = processList[i].ProcessName;
                if (ProcessName.Equals(choseProcessName))
                {
                    //프로세스킬
                    processList[i].Kill();
                    //Console.WriteLine("프로세스 내용 정보] : " + choseProcessName);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProcessKill();
        }
    }
}
