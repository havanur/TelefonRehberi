using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TelefonRehberi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;

        private void Form1_Load(object sender, EventArgs e)
        {
            KisiGetir();
        }

        void KisiGetir()
        {
            baglanti = new SqlConnection("Server=(localdb)\\V11.0; Initial Catalog=TelefonRehberi;Integrated Security=SSPI");
            baglanti.Open();
            da = new SqlDataAdapter("Select * From Kisiler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu="Insert Into Kisiler (AD,SOYAD,TELEFON) values (@ad, @soyad, @telefon)";
            komut = new SqlCommand(sorgu,baglanti);
            komut.Parameters.AddWithValue("@ad",textBox2.Text);
            komut.Parameters.AddWithValue("@soyad",textBox3.Text);
            komut.Parameters.AddWithValue("@telefon", textBox4.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

            KisiGetir();

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "Delete From Kisiler Where ID=@id";
            komut = new SqlCommand(sorgu,baglanti);
            komut.Parameters.AddWithValue("@id",Convert.ToInt32(textBox1.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

            KisiGetir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "update Kisiler Set AD=@ad, SOYAD=@soyad, TELEFON=@telefon Where ID=@id";
            komut = new SqlCommand(sorgu,baglanti);
            komut.Parameters.AddWithValue("@id",Convert.ToInt32(textBox1.Text));
            komut.Parameters.AddWithValue("@ad", textBox2.Text);
            komut.Parameters.AddWithValue("@soyad", textBox3.Text);
            komut.Parameters.AddWithValue("@telefon", textBox4.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

            KisiGetir();

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            da = new SqlDataAdapter("SElect *from Kisiler where AD like '" + textBox5.Text + "%'", baglanti);
            DataSet ds = new DataSet();
            baglanti.Open();
            da.Fill(ds, "Kisiler");
            dataGridView1.DataSource = ds.Tables["Kisiler"];
            baglanti.Close();
        }
    }
}
