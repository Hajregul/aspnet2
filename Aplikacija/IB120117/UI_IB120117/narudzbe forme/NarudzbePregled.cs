using API_IB120117.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_IB120117.Util;

namespace UI_IB120117.narudzbe_forme
{
    public partial class NarudzbePregled : Form
    {
        APIServices _apiService = new APIServices("Narudzbe");        
        APIServices _apiServiceDatumi = new APIServices("Narudzbe/NarudzbeDateOdDateDo");
        APIServices _apiServiceNstavke = new APIServices("Narudzbe/AllIsporuceni");
        APIServices _apiServiceIsporuci = new APIServices("Narudzbe/Isporuci");

        int narudzbaID;
        public NarudzbePregled()
        {
            InitializeComponent();
        }

        private void NarudzbePregled_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            var response = _apiService.GetT<List<esp_AllNarudzbe_Result>>();

                if (response!=null)
                {                     
                dgvJela.DataSource = response;
                dgvJela.AutoGenerateColumns = false;
                dgvJela.Columns[1].Visible = false;
                dgvJela.Columns[0].DisplayIndex = 5;
            }            
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            DateTime d1 = dateTimePicker1.Value;
            string datumOd = d1.ToString("MM-dd-yyy");
            DateTime d2 = dateTimePicker2.Value;
            string datumDo = d2.ToString("MM-dd-yyy");

            var response = await _apiServiceDatumi.GetByDatumi<List<esp_AllNarudzbeDatumi_Result>>(datumOd,datumDo);
            if (response!=null)
            {
                List<esp_AllNarudzbeDatumi_Result> narudzbe = response;
                dgvJela.DataSource = narudzbe;
                dgvJela.AutoGenerateColumns = false;
                dgvJela.Columns[1].Visible = false;
                dgvJela.Columns[0].DisplayIndex = 5;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string status = dgvJela.SelectedCells[5].Value.ToString();
            if (status == "True")
            {
                string s = dgvJela.SelectedCells[1].Value.ToString();
                int naruzbaID = Convert.ToInt32(s);
                var response = await _apiServiceIsporuci.GetById<int>(narudzbaID);
                if (response!=0)
                {
                    int narudzbaSetovana = response;
                    MessageBox.Show("Isporučeno","Uspjeh");
                    BindData();
                }
            }
            else {
                MessageBox.Show("Narudžba je već isporućena!", "Informacija");
                status = null;
            }
        }

        private async void checkBox1_Click(object sender, EventArgs e)
        {

            bool status = false;
            if (checkBox1.Checked)
            {
                status = true;

                var response = await _apiServiceNstavke.Getisporuci<List<esp_AllNarudzbeStatus_Result>>(status);
                if (response != null)
                {
                    List<esp_AllNarudzbeStatus_Result> narudzbe = response;
                    dgvJela.DataSource = narudzbe;
                    dgvJela.Columns[0].DisplayIndex = 5;
                }
            }
            else
            {
                BindData();
            }
        }

        private void dgvJela_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                string k = dgvJela.SelectedCells[0].Value.ToString();
            
                var senderGrid = (DataGridView)sender;

                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (dgvJela.SelectedCells[e.ColumnIndex].Value.ToString() == "Detalji")
                    {
                        narudzbaID = Convert.ToInt32(k);
                    narudzbe_forme.NarudzbeDetalji jIzmjena = new narudzbe_forme.NarudzbeDetalji(narudzbaID);
                        jIzmjena.Show();
                        this.Close();
                    }
                }            
        }
    }
}
