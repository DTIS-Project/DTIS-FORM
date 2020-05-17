using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DTIS_YETKİLİ_EKLE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string fotoekle;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Title = "Resim";
            file.Filter = "jpeg (*.jpg)|*.jpg|png (*.png)|*.png";
            if (file.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(file.FileName);
                fotoekle = file.FileName.ToString();
            }
        }
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ASUS\source\repos\DTIS_YETKİLİ_EKLE\DTISDB.mdf;Integrated Security=True");
        private void button2_Click(object sender, EventArgs e)
        {
            FileStream file = new FileStream(fotoekle, FileMode.Open, FileAccess.Read);
            BinaryReader bin = new BinaryReader(file);
            byte[] img = bin.ReadBytes((int)file.Length);
            bin.Close();
            file.Close();
            conn.Open();
            SqlCommand com = new SqlCommand("insert into dbo.dtis_yetkili(id, name, degree, password, img) values (@p1, @p2, @p3, @p4, @p5)", conn);
            com.Parameters.AddWithValue("@p1", textBox1.Text);
            com.Parameters.AddWithValue("@p2", textBox2.Text);
            com.Parameters.AddWithValue("@p3", textBox3.Text);
            com.Parameters.AddWithValue("@p4", textBox4.Text);
            com.Parameters.Add("@p5", SqlDbType.Image, img.Length).Value = img;
            com.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Kayıt Eklendi");
        }
    }
}
