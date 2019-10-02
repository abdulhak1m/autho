using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestingProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // передвижение формы
            #region Передвижение формы
            int move = 0, moveX = 0, moveY = 0;
            panel_Top.MouseDown += (s, e) => { move = 1; moveX = e.X; moveY = e.Y; };
            panel_Top.MouseMove += (s, e) => { if (move == 1) SetDesktopLocation(MousePosition.X - moveX, MousePosition.Y - moveY); };
            panel_Top.MouseUp += (s, e) => { move = 0; };
            #endregion

            btn_Close.Click += (s, e) => { Close(); };
            txtPassword.TextChanged += (s, e) => { txtPassword.UseSystemPasswordChar = true; };
            btn_showPass.MouseDown += (s, e) => { txtPassword.UseSystemPasswordChar = false; };
            btn_showPass.MouseUp += (s, e) => { txtPassword.UseSystemPasswordChar = true; };

            linkLabel1.Click += (s, e) =>
            {
                 ActiveForm.Hide();
                 Form2 MyForm2 = new Form2();
                 MyForm2.ShowDialog();
                 Close();
            };
            btn_login.Click += (s, e) =>
            {
                ActiveForm.Hide();
                Form3 form3 = new Form3();
                form3.ShowDialog();
                Close();
            };
        }
    }
}
