using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaiWebshop
{
    public partial class Form1 : Form
    {
        Database1Entities context = new Database1Entities();
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var ügyfelek = from x in context.Ugyfel where x.NEV.Contains(textBox1.Text) select x;
            ugyfelBindingSource.DataSource = ügyfelek.ToList();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            var LOGIN = ((Ugyfel)listBox1.SelectedItem).LOGIN;
            var rendelések = from x in context.Rendeles where x.LOGIN == LOGIN select x;
            listBox2.DisplayMember = "REND_DATUM";
            listBox2.DataSource = rendelések.ToList();




        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var SORSZAM = ((Rendeles)listBox2.SelectedItem).SORSZAM;
            var rendelésiTételek = from x in context.Rendeles_tetel
                                   where x.SORSZAM == SORSZAM
                                   select new
                                   {
                                       Teméknév = x.Termek.MEGNEVEZES,
                                       Kategória = x.Termek.Termekkategoria.KAT_NEV,
                                       Egységár = x.EGYSEGAR,
                                       Ár = x.EGYSEGAR * x.MENNYISEG,
                                       Megység = x.Termek.MEGYS,
                                       Mennyiség = x.MENNYISEG
                                   };
            dataGridView1.DataSource = rendelésiTételek.ToList();

            var összesen = (from x in rendelésiTételek select x.Ár).Sum();
            textBox2.Text = összesen.ToString();

        }
    }
}
