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

namespace EntityOrnek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbOgrenciSinavEntities db = new DbOgrenciSinavEntities();
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnDersListesi_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-RP5F7ET;Initial Catalog=DbOgrenciSinav;Integrated Security=True");
            SqlCommand komut1 = new SqlCommand("select * from TBLDERSLER", baglanti);
            SqlDataAdapter da1 = new SqlDataAdapter(komut1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;
        }

        private void btnOgrenciListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.TBLOGRENCI.ToList();

            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
        }

        private void btnNotListesi_Click(object sender, EventArgs e)
        {
            var query = from item in db.TBLNOTLAR
                        select new
                        {
                            item.NOTID,
                            item.TBLOGRENCI.AD,
                            item.TBLOGRENCI.SOYAD,
                            item.TBLDERSLER.DERSAD,
                            item.SINAV1,
                            item.SINAV2,
                            item.SINAV3,
                            item.ORTALAMA,
                            item.DURUM
                        };
            dataGridView1.DataSource = query.ToList();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtbxAD.Text != "")
            {
                TBLOGRENCI t = new TBLOGRENCI();
                t.AD = txtbxAD.Text;
                t.SOYAD = txtbxSoyad.Text;
                db.TBLOGRENCI.Add(t);
                db.SaveChanges();
                MessageBox.Show("Öğrenci Listeye Eklenmiştir.");
            }

            if (txtbxDersAD.Text != "")
            {
                TBLDERSLER t1 = new TBLDERSLER();
                t1.DERSAD = txtbxDersAD.Text;
                db.TBLDERSLER.Add(t1);
                db.SaveChanges();
                MessageBox.Show("Ders Listeye Eklenmiştir.");
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (txtbxID.Text != "")
            {
                int id = Convert.ToInt32(txtbxID.Text);
                var y = db.TBLOGRENCI.Find(id);
                db.TBLOGRENCI.Remove(y);
                db.SaveChanges();
                MessageBox.Show("Öğrenci Sistemden Silinmiştir.");
            }
            if (txtbxDersID.Text != "")
            {
                int id = Convert.ToInt32(txtbxDersID.Text);
                var x = db.TBLDERSLER.Find(id);
                db.TBLDERSLER.Remove(x);
                db.SaveChanges();
                MessageBox.Show("Ders Sistemden Silinmiştir.");
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (txtbxID.Text != "")
            {
                int id = Convert.ToInt32(txtbxID.Text);
                var y = db.TBLOGRENCI.Find(id);
                y.AD = txtbxAD.Text;
                y.SOYAD = txtbxSoyad.Text;
                y.FOTOGRAF = txtbxFOTO.Text;
                db.SaveChanges();
                MessageBox.Show("Öğrenci Bilgileri Başarıyla Güncellendi.");
            }
            if (txtbxDersID.Text != "")
            {
                int id = Convert.ToInt32(txtbxDersID.Text);
                var x = db.TBLDERSLER.Find(id);
                x.DERSAD = txtbxDersAD.Text;
                db.SaveChanges();
                MessageBox.Show("Ders Bilgileri Başarıyla Güncellendi.");
            }
        }

        private void btnProcedure_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.NOTLISTESI();
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.TBLOGRENCI.Where(x => x.AD == txtbxAD.Text | x.SOYAD == txtbxSoyad.Text).ToList();
        }

        private void txtbxAD_TextChanged(object sender, EventArgs e)
        {
            string aranan = txtbxAD.Text;
            var sorgu = from item in db.TBLOGRENCI
                        where item.AD.Contains(aranan)
                        select item;
            dataGridView1.DataSource = sorgu.ToList();
        }

        private void btnLinqEntity_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                List<TBLOGRENCI> liste1 = db.TBLOGRENCI.OrderBy(p => p.AD).ToList();
                dataGridView1.DataSource = liste1;
            }
            if (radioButton2.Checked == true)
            {
                List<TBLOGRENCI> liste2 = db.TBLOGRENCI.OrderByDescending(p => p.AD).ToList();
                dataGridView1.DataSource = liste2;
            }
            if (radioButton3.Checked == true)
            {
                List<TBLOGRENCI> liste3 = db.TBLOGRENCI.OrderBy(p => p.ID).Take(3).ToList();
                dataGridView1.DataSource = liste3;
            }
            if (radioButton4.Checked == true)
            {
                List<TBLOGRENCI> liste4 = db.TBLOGRENCI.Where(x => x.ID == 5).ToList();
                dataGridView1.DataSource = liste4;
            }
            if (radioButton5.Checked == true)
            {
                List<TBLOGRENCI> liste5 = db.TBLOGRENCI.Where(x => x.AD.StartsWith("a")).ToList();
                dataGridView1.DataSource = liste5;
            }
            if (radioButton6.Checked == true)
            {
                List<TBLOGRENCI> liste6 = db.TBLOGRENCI.Where(p => p.AD.EndsWith("a")).ToList();
                dataGridView1.DataSource = liste6;
            }
            if (radioButton7.Checked == true)
            {
                bool deger = db.TBLOGRENCI.Any();
                MessageBox.Show(deger.ToString(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (radioButton8.Checked == true)
            {
                int toplam = db.TBLOGRENCI.Count();
                MessageBox.Show(toplam.ToString(), "Toplam Öğrenci Sayısı", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            if (radioButton9.Checked == true)
            {
                var toplam = db.TBLNOTLAR.Sum(p => p.SINAV1);
                MessageBox.Show(toplam.ToString(), "SINAV1 Toplamları", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            if (radioButton10.Checked == true)
            {
                var toplam = db.TBLNOTLAR.Average(x => x.SINAV1);
                MessageBox.Show(toplam.ToString(), "SINAV1 Ortalamaları", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            if (radioButton11.Checked == true)
            {
                var toplam = db.TBLNOTLAR.Average(x => x.SINAV1);
                List<TBLNOTLAR> liste11 = db.TBLNOTLAR.Where(x => x.SINAV1 > toplam).ToList();
                dataGridView1.DataSource = liste11;
            }
            if (radioButton12.Checked == true)
            {
                var deger = db.TBLNOTLAR.Max(x => x.SINAV1);
                MessageBox.Show(deger.ToString(), "En Yüsek SINAV1", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
            }
            if (radioButton13.Checked == true)
            {
                var deger = db.TBLNOTLAR.Min(p => p.SINAV1);
                MessageBox.Show(deger.ToString(), "En Düşük SINAV1", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
            }
            if (radioButton14.Checked == true)
            {
                //EN YÜKSEK PUANI YAKALAMA
                int enyuksekpuan = Convert.ToInt32(db.TBLNOTLAR.Max(x => x.SINAV1));
                //EN YÜKSEK PUAN ALAN ÖĞRENCİYİ YAKALAMA
                var enyuksekalanogrenci = db.TBLNOTLAR.Where(x => x.SINAV1 == enyuksekpuan);
                //EN YÜKSEK ALANIN ID'SİNİ YAKALAMA
                int id = Convert.ToInt32(enyuksekalanogrenci.Max(p => p.OGR));
                //EN YÜKSEK ALAN ÖĞRENCİ İÇİN NESNE OLUŞTURMA
                var enyuksekalanınadınıyakala = from item in db.TBLNOTLAR.Where(p => p.NOTID == id)
                                                select new
                                                {
                                                    item.NOTID,
                                                    item.TBLOGRENCI.AD,
                                                    item.TBLOGRENCI.SOYAD,
                                                    item.TBLDERSLER.DERSAD,
                                                    item.SINAV1,
                                                    item.SINAV2,
                                                    item.SINAV3,
                                                    item.DURUM,
                                                    item.ORTALAMA
                                                };
                //DATAGRİDVİEW'DE ÖĞRENCİSİNİN İSMİNİ GÖRÜNTÜLEME
                dataGridView1.DataSource = enyuksekalanınadınıyakala.ToList();
            }
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            var sorgu = from d1 in db.TBLNOTLAR
                        join d2 in db.TBLOGRENCI
                        on d1.OGR equals d2.ID
                        join d3 in db.TBLDERSLER
                        on d1.DERS equals d3.DERSID
                        select new
                        {
                            ÖĞRENCİ = d2.AD + " " + d2.SOYAD,
                            DERS = d3.DERSAD,
                            SINAV1 = d1.SINAV1,
                            SINAV2 = d1.SINAV2,
                            SINAV3=d1.SINAV3,
                            ORTALAMA=d1.ORTALAMA,
                            DURUM=d1.DURUM
                        };
            dataGridView1.DataSource = sorgu.ToList();
        }
    }
}
