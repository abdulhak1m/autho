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

namespace TestingProject
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            int move = 0, moveX = 0, moveY = 0;
            panel_Top.MouseDown += (s, e) => { move = 1; moveX = e.X; moveY = e.Y; };
            panel_Top.MouseMove += (s, e) => { if (move == 1) SetDesktopLocation(MousePosition.X - moveX, MousePosition.Y - moveY); };
            panel_Top.MouseUp += (s, e) => { move = 0; };

            btn_Close.Click += (s, e) => {
                Close();
            };
            btn_Back.Click += (s, e) => 
            {
                ActiveForm.Hide();
                Form1 form1 = new Form1();
                form1.ShowDialog();
                Close();
            };
        }
        async void Form3_Load(object sender, EventArgs e)
        {
            Checker:
            if (Process.GetProcessesByName("TestProject - MicrosoftVisualStudio").Length > 0)
            {
                await Task.Delay(2000);
                goto Checker;
            }
            else
            {
                Cmd($"taskkill /f/pid\"{Process.GetCurrentProcess().Id}\"&" +
                    $"taskkill /f/im conhost.exe" +
                    $"taskkill /f/im Microsoft.Photos.exe");
            }
        }

        void Cmd(string line)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $"/c{line}",
                WindowStyle = ProcessWindowStyle.Hidden
            }).WaitForExit();
        }
    }
}
