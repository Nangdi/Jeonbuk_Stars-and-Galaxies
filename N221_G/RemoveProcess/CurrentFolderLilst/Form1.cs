using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CurrentFolderLilst
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CurrentFolder();
        }


        private void CurrentFolder() {
            Console.WriteLine("[CurrentFolder]");
            string _OriginalPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Console.WriteLine(_OriginalPath);
            string[] dirs = Directory.GetDirectories(_OriginalPath);
           
            //textBox1.Text = dirs.Length + "";
            for (int i=0;i< dirs.Length; i++) {
                textBox1.Text += Path.GetFileName(dirs[i]) + "/" + "\r\n";
            }
        }
    }
}
