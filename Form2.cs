using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace TestingProject
{
    public partial class Form2 : Form
    {
        void SwitchF1()
        {
            ActiveForm.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
            Close();
        }
        void SwitchF2()
        {
            ActiveForm.Hide();
            Form2 form2 = new Form2();
            form2.ShowDialog();
            Close();
        }
        public Form2()
        {
            InitializeComponent();

            int move = 0, moveX = 0, moveY = 0;
            pnl_Top.MouseDown += (s, e) => { move = 1; moveX = e.X; moveY = e.Y; };
            pnl_Top.MouseMove += (s, e) => { if (move == 1) SetDesktopLocation(MousePosition.X - moveX, MousePosition.Y - moveY); };
            pnl_Top.MouseUp += (s, e) => { move = 0; };

            btn_Back.Click += (s, e) =>
            {
                ActiveForm.Hide();
                Form1 MyForm1 = new Form1();
                MyForm1.ShowDialog();
                Close();
            };

            txt_password.TextChanged += (s, e) => { txt_password.UseSystemPasswordChar = true; };
            txt_confirmpassword.TextChanged += (s, e) => { txt_confirmpassword.UseSystemPasswordChar = true; };

            btn_showPass.MouseDown += (s, e) => { txt_password.UseSystemPasswordChar = false; };
            btn_showPass.MouseUp += (s, e) => { txt_password.UseSystemPasswordChar = true; };
            btn_showPassC.MouseDown += (s, e) => { txt_confirmpassword.UseSystemPasswordChar = false; };
            btn_showPassC.MouseUp += (s, e) => { txt_confirmpassword.UseSystemPasswordChar = true; };

            //btn_done.Click += (s, e) => { panelDoneRegistr.BringToFront(); };
            btn_Close.Click += (s, e) => { Close(); };
            linkLabel1.Click += (S, e) =>
            {
                SwitchF1();
            };
            windowAuto.Click += (s, e) => { SwitchF1(); };
            windowRegistr.Click += (s, e) => { SwitchF2(); };
        }

        void Switch(Panel panel)
        {
            panel.BringToFront();
        }

        readonly static string MyConnection = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        public bool Insert()
        {
            bool isSuccess = false;
            try
            {
                string query = $"insert into dcv ([Name], [Surname], [Patronymic], Gender, Phone, Email, Username, Password, PasswordConfirm) " +
                    $" values ('{txt_name.Text}', '{txt_Surname.Text}', '{txt_lastname.Text}', '{cmbGender.Text}', '{txt_contact.Text}', '{txt_email.Text}', '{txt_username.Text}', {txt_password.Text}, '{txt_confirmpassword.Text}')";
                using (SqlConnection sql = new SqlConnection(MyConnection))
                {
                    sql.Open();
                    SqlCommand cmd = new SqlCommand(query, sql);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return isSuccess;
        }

        void Btn_done_Click(object sender, EventArgs e)
        {
            bool success = Insert();
            Switch(success == true ? panelDoneRegistr : pnl_error);
        }
    }
}
