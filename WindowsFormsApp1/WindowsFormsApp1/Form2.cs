using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp1
{

    public partial class Form2 : Form 
    {  
        private AutoCompleteStringCollection dataSource = new AutoCompleteStringCollection();
        public Form2()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(type_1.Text) || string.IsNullOrEmpty(type_2.Text) || string.IsNullOrEmpty(txtadd_eng.Text) || string.IsNullOrEmpty(txtadd_fr.Text) || string.IsNullOrEmpty(exp_eng.Text) || string.IsNullOrEmpty(exp_fr.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Ajout non réussi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string connectionString = @"Data Source=LAPTOP-IIH7EKE2\SQLEXPRESS;Initial Catalog=framework;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string addd = "INSERT INTO anglais(word,word_type,translate,exemple_ang,exemple_fr) Values (@word,@word_type,@translate,@exemple_ang,@exemple_fr);"
              + " INSERT INTO francais (word,type,traduction,exemple_ang,exemple_fr) Values(@translate,@type,@word, @exemple_ang, @exemple_fr); ";

                    using (SqlCommand cmd = new SqlCommand(addd, con))
                    {
                        cmd.Parameters.AddWithValue("@word_type", type_1.Text);
                        cmd.Parameters.AddWithValue("@type", type_2.Text);
                        cmd.Parameters.AddWithValue("@word", txtadd_eng.Text);
                        cmd.Parameters.AddWithValue("@translate", txtadd_fr.Text);
                        cmd.Parameters.AddWithValue("@traduction", txtadd_eng.Text);
                        cmd.Parameters.AddWithValue("@exemple_ang", exp_eng.Text);
                        cmd.Parameters.AddWithValue("@exemple_fr", exp_fr.Text);
                        cmd.ExecuteNonQuery();
                    }
                }

                type_1.Text = "";
                type_2.Text = "";
                txtadd_eng.Text = "";
                txtadd_fr.Text = "";
                exp_eng.Text = "";
                exp_fr.Text = "";

                MessageBox.Show("Le mot a été ajouté avec succès.", "Mot ajouté", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mdf_btn_Click(object sender, EventArgs e)
        {
            if (editword.Text == "" || editfr.Text == "" || editeng.Text == "" || edittypeen.Text == "" || edittypefr.Text == "" || editexen.Text == "" || editexfr.Text == "")
            {
                MessageBox.Show("Please fill all fields", "Editing Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string connectionString = @"Data Source=LAPTOP-IIH7EKE2\SQLEXPRESS;Initial Catalog=framework;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string checkExistQueryFrensh = "SELECT COUNT(*) FROM Francais WHERE word = @editword;";
                    SqlCommand checkExistCmdFrensh = new SqlCommand(checkExistQueryFrensh, con);
                    checkExistCmdFrensh.Parameters.AddWithValue("@editword", editword.Text);
                    int rowCountFrensh = (int)checkExistCmdFrensh.ExecuteScalar();
                    string checkExistQueryEnglish = "SELECT COUNT(*) FROM anglais WHERE word = @editword;";
                    SqlCommand checkExistCmdEnglish = new SqlCommand(checkExistQueryEnglish, con);
                    checkExistCmdEnglish.Parameters.AddWithValue("@editword", editword.Text);
                    int rowCountEnglish = (int)checkExistCmdEnglish.ExecuteScalar();
                    if (rowCountFrensh > 0)
                    {
                        // The value of editword.Text exists in the French table, so we can update it
                        string edit = "UPDATE francais SET word=@word, word_type=@type, translate=@translate, exemple_ang=@exemple_ang, exemple_fr=@exemple_fr WHERE word=@editword;" +
               "UPDATE anglais SET word=@translate, word_type=@word_type1, translate=@word, exemple_ang=@exemp_ang, exemple_fr=@exemple_fr WHERE word=@editword;";
                        SqlCommand cmd = new SqlCommand(edit, con);

                        cmd.Parameters.AddWithValue("@word", editfr.Text);
                        cmd.Parameters.AddWithValue("@word_type", edittypefr.Text);
                        cmd.Parameters.AddWithValue("@word_type1", edittypeen.Text);
                        cmd.Parameters.AddWithValue("@translate", editeng.Text);
                        cmd.Parameters.AddWithValue("@exemple_ang", editexen.Text);
                        cmd.Parameters.AddWithValue("@exemple_fr", editexfr.Text);
                        cmd.Parameters.AddWithValue("@editword", editword.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();

                        edittypeen.Text = "";
                        edittypefr.Text = "";
                        editeng.Text = "";
                        editfr.Text = "";
                        editexen.Text = "";
                        editexfr.Text = "";

                        MessageBox.Show("Word successfully updated", "Word Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                    else if (rowCountEnglish > 0)
                    {
                        // The value of editword.Text exists in the anglais table, so we can update it
                        string edit = "UPDATE anglais SET word=@word, word_type =@word_type1, translate = @translate, exemple_ang = @exemple_ang, exemple_fr = @exemple_fr WHERE word = @editword;" +
                        " UPDATE Francais SET word=@translate, word_type= @word_type, translate= @word, exemple_ang = @exemple_ang, exemple_fr = @exemple_fr WHERE translate= @editword;";
                        SqlCommand cmd = new SqlCommand(edit, con);

                        cmd.Parameters.AddWithValue("@translate", editfr.Text);
                        cmd.Parameters.AddWithValue("@word_type", edittypefr.Text);
                        cmd.Parameters.AddWithValue("@word_type1", edittypeen.Text);

                        cmd.Parameters.AddWithValue("@word", editeng.Text);
                        cmd.Parameters.AddWithValue("@exemple_fr", editexfr.Text);
                        cmd.Parameters.AddWithValue("@exemple_ang", editexen.Text);
                        cmd.Parameters.AddWithValue("@editword", editword.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();

                        edittypeen.Text = "";
                        edittypefr.Text = "";
                        editeng.Text = "";
                        editfr.Text = "";
                        editexen.Text = "";
                        editexfr.Text = "";
                        MessageBox.Show("Word successfully updated", "Word Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                    else
                    {
                        // The value of editW.Text does not exist in either table
                        con.Close();
                        MessageBox.Show("The word does not exist in either the French or English table", "Editing Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dlt_btn_Click(object sender, EventArgs e)
            
        { SqlCommand  cmd=new SqlCommand();
            string connectionString = @"Data Source=LAPTOP-IIH7EKE2\SQLEXPRESS;Initial Catalog=framework;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
                if (dlt_btn.Text == "")
                {
                    MessageBox.Show("Input empty", "Deleting Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    con.Open();
                    string delet = "DELETE FROM anglais WHERE word=@word OR translate=@word; " +
                                 "DELETE FROM francais WHERE word=@word OR translate  = @word;";
                    cmd = new SqlCommand(delet, con);
                    cmd.Parameters.AddWithValue("@word", dlt_btn.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    dlt_btn.Text = "";
                    MessageBox.Show("Word successfully DELETED", "Word Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form3().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Form4().Show();
            this.Hide();
        }
    }
}

