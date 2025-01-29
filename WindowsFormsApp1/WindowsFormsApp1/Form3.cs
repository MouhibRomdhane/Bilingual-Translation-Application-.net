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

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        private AutoCompleteStringCollection dataSource = new AutoCompleteStringCollection();
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-IIH7EKE2\\SQLEXPRESS;Initial Catalog=framework;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        public Form3()
        {
          
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
         
            con.Open();
          
           SqlCommand cmd = new SqlCommand();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM anglais where word='"+textBox1.Text+"' ";
            cmd.ExecuteNonQuery();
       
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();

            string login = "SELECT * FROM Francais WHERE word= @word ";


            cmd = new SqlCommand(login, con);
            cmd.Parameters.AddWithValue("@word", textBox2.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Form4().Show();
            this.Hide();
        }
        /*public void show()
        {
            con.Open();
            string login = "SELECT * FROM anglais WHERE word=@word ";
            cmd = new SqlCommand(login, con);
            cmd = con.CreateCommand() ;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM anglais  ";
            cmd.ExecuteNonQuery();
            //cmd.Parameters.AddWithValue("@word", textBox1.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }*/
    }
}
