using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-IIH7EKE2\SQLEXPRESS;Initial Catalog=framework;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();

        private void button_login_Click(object sender, EventArgs e)
        {
            
            
           
            
                conn.Open();
                string login = "SELECT * FROM login_new WHERE username= @username and password= @password";
                cmd = new SqlCommand(login, conn);
                cmd.Parameters.AddWithValue("@username", txt_username.Text);
                cmd.Parameters.AddWithValue("@password", txt_password.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                int role = -1;
                if (dr.Read() == true)
                {
                    role = Convert.ToInt32(dr["switch"]);
                }

                if (role == 1)
                {
                    new Form2().Show();
                    this.Hide();
                }
                else if (role == 0)
                {
                    new Form3().Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password, please try again", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_username.Text = "";
                    txt_password.Text = "";
                    txt_username.Focus();
                }



            conn.Close();
                





        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_clear_Click(object sender, EventArgs e)
        {

            txt_username.Clear();
            txt_password.Clear();
            txt_username.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txt_username_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txt_username.ForeColor = Color.White;
            }
            catch { }
        }

        private void txt_password_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txt_password.ForeColor = Color.White;
            }
            catch { }
        }

        private void button_login_MouseEnter(object sender, EventArgs e)
        {
            button_login.ForeColor = Color.Black;
        }

        private void button_login_MouseLeave(object sender, EventArgs e)
        {
            button_login.ForeColor = Color.White;
        }

        private void pnl_dic_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
