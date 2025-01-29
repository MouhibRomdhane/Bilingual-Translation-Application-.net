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
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form4 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-IIH7EKE2\\SQLEXPRESS;Initial Catalog=framework;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        public Form4()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (.txt)|.txt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txt_path.Text = openFileDialog.FileName;
                btn_import.Enabled = true;
            }



        }

        private void txt_path_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_import_Click(object sender, EventArgs e)
        {
            try
            {
                // Read the contents of the file
                string[] lines = File.ReadAllLines(txt_path.Text);

                con.Open();

                // Keep track of the number of rows in the table anglai  before adding the new data
                int numRowsBefore = (int)new SqlCommand("SELECT COUNT(*) FROM anglais", con).ExecuteScalar();

                // Loop through each line and add it to both of tables
                for (int i = 0; i < lines.Length; i++)
                {
                    // Split the line into its individual values
                    string[] values = lines[i].Split(',');

                    // Create a new SqlCommand object for the  table  anglais
                    SqlCommand cmd = new SqlCommand("INSERT INTO anglais (Word, word_type, Translate, exemple_ang, exemple_fr) " +
                        "VALUES (@word, @word_type, @translate, @exemple_ang, @exemple_fr)", con);

                    // Add the parameter values for the table anglais
                    cmd.Parameters.AddWithValue("@word", values[0].Trim());
                    cmd.Parameters.AddWithValue("@word_type", values[1].Trim());
                    cmd.Parameters.AddWithValue("@translate", values[2].Trim());
                    cmd.Parameters.AddWithValue("@exemple_ang", values[3].Trim());
                    cmd.Parameters.AddWithValue("@exemple_fr", values[4].Trim());

                    // Execute the command for  table anglais
                    cmd.ExecuteNonQuery();

                    // Create a new SqlCommand object for the table francais
                    cmd = new SqlCommand("INSERT INTO francais (Word, word_type, exemple_fr, exemple_ang, Translate) " +
                        "VALUES (@word, @word_type, @exemple_ang, @exemple_fr, @translate)", con);

                    // Set the parameter values for the  table francais
                    cmd.Parameters.AddWithValue("@word", values[2].Trim());

                    // Set the parameter values for the  table francais
                    string type = values[1].Trim();
                    if (type == "Noun")
                    {
                        type = "Nom";
                    }
                    else if (type == "verb")
                    {
                        type = "verbe";
                    }
                    else if (type == "adverb")
                    {
                        type = "adverbe";
                    }
                    cmd.Parameters.AddWithValue("@word_type", type);


                    cmd.Parameters.AddWithValue("@exemple_ang", values[3].Trim());
                    cmd.Parameters.AddWithValue("@exemple_fr", values[4].Trim());
                    cmd.Parameters.AddWithValue("@translate", values[0].Trim());

                    // Execute the command for the table francais
                    cmd.ExecuteNonQuery();
                }


                // Select the added data from the table anglais in descending order
                int numRowsAfter = (int)new SqlCommand("SELECT COUNT(*) FROM anglais", con).ExecuteScalar();
                int numRowsAdded = numRowsAfter - numRowsBefore;
                SqlDataAdapter da = new SqlDataAdapter("SELECT TOP " + numRowsAdded + " * FROM anglais ORDER BY id DESC", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Bind the data to the data grid
                dataGridView1.DataSource = dt;

                con.Close();

                MessageBox.Show("Data imported and saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Open writing file 
                StreamWriter writer = new StreamWriter(txt_path.Text);

                con.Open();

                // Execute a select query to retrieve all data from the table anglais
                SqlCommand cmd = new SqlCommand("SELECT * FROM anglais", con);
                SqlDataReader reader = cmd.ExecuteReader();

                // Loop through each row and link it to the file
                while (reader.Read())
                {
                    string line = String.Join(",", reader["Word"].ToString(),
                                                  reader["word_type"].ToString(),
                                                  reader["Translate"].ToString(),
                                                  reader["Exemple_ang"].ToString(),
                                                  reader["Exemple_fr"].ToString());
                    writer.WriteLine(line);
                }

                // Close the data reader
                reader.Close();

                // Fill data grid with data
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM anglais", con);
                adapter.Fill(table);
                dataGridView1.DataSource = table;

                // Close the file and database connection
                writer.Close();
                con.Close();

                MessageBox.Show("Data exported successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form3().Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
