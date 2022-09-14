using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityOrnek
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        DbOgrenciSinavEntities db = new DbOgrenciSinavEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                List<TBLNOTLAR> liste1 = db.TBLNOTLAR.Where(x => x.SINAV1 < 50).ToList();
                dataGridView1.DataSource = liste1;
            }
            if (radioButton2.Checked == true)
            {
                List<TBLOGRENCI> liste2 = db.TBLOGRENCI.Where(x => x.AD == "Ali").ToList();
                dataGridView1.DataSource = liste2;
            }
            if (radioButton3.Checked == true)
            {
                var deger = txtbxAdveyaSoyad.Text;
                List<TBLOGRENCI> liste3 = db.TBLOGRENCI.Where(x => x.AD == deger || x.SOYAD == deger).ToList();
                dataGridView1.DataSource = liste3;
            }
            if (radioButton4.Checked == true)
            {
                var degerler = db.TBLOGRENCI.Select(x =>
                new
                {
                    soyadı = x.SOYAD
                });
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton5.Checked == true)
            {
                var degerler = db.TBLOGRENCI.Select(x =>
                new
                {
                    AD = x.AD.ToUpper(),
                    SOYAD = x.SOYAD.ToLower()
                });
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton6.Checked == true)
            {
                var deger = db.TBLOGRENCI.Select(x =>
                new
                {
                    AD = x.AD,
                    SOYAD = x.SOYAD
                }).Where(x=>x.AD != "Ali");
                dataGridView1.DataSource = deger.ToList();
            }
            if (radioButton7.Checked == true)
            {
                var deger = db.TBLNOTLAR.Select(x =>
                new
                {
                    OgrenciAd = x.OGR,
                    Ortalaması = x.ORTALAMA,
                    Durumu = x.DURUM == true ? "Geçti" : "Kaldı"
                });
                dataGridView1.DataSource = deger.ToList();
            }
            if (radioButton8.Checked == true)
            {
                var deger = db.TBLNOTLAR.SelectMany(x => db.TBLOGRENCI.Where(y => y.ID == x.OGR), (x, y) =>
                new
                {
                    y.AD,
                    x.ORTALAMA
                });
                dataGridView1.DataSource = deger.ToList();
            }
            if (radioButton9.Checked == true)
            {
                var deger = db.TBLNOTLAR.SelectMany(x => db.TBLOGRENCI.Where(y => y.ID == x.OGR), (x, y) =>
                new
                {
                    OGRENCI = y.AD + " " + y.SOYAD,
                    DURUM = x.DURUM == true ? "Geçti" : "Kaldı"
                });
                dataGridView1.DataSource = deger.ToList();
            }
            if (radioButton10.Checked == true)
            {
                var deger = db.TBLOGRENCI.OrderBy(x => x.ID).Take(3);
                dataGridView1.DataSource = deger.ToList();
            }
            if (radioButton11.Checked == true)
            {
                var deger = db.TBLOGRENCI.OrderByDescending(x => x.ID).Take(3);
                dataGridView1.DataSource = deger.ToList();
            }
            if (radioButton12.Checked == true)
            {
                var deger = db.TBLOGRENCI.OrderBy(x => x.AD);
                dataGridView1.DataSource = deger.ToList();
            }
            if (radioButton13.Checked == true)
            {
                var deger = db.TBLOGRENCI.OrderBy(x => x.ID).Skip(5);
                dataGridView1.DataSource = deger.ToList();
            }
        }
    }
}
