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

namespace UI_IB120117.Korisnici_forme
{
    public partial class KorisniciIzmjna : Form
    {
        APIServices _apiService = new APIServices("Korisnici/GetKorisnik");
        APIServices _apiServiceUpdateAktivnost = new APIServices("Korisnici/UpdateAktivnost");
        APIServices _apiServiceNaselja = new APIServices("Naselja");

        esp_KorisnikByID_Result k;

        int korisnikID;
        public KorisniciIzmjna(int korisnikID)
        {
            InitializeComponent();
            this.korisnikID = korisnikID;
            this.AutoValidate = AutoValidate.Disable;
        }

        private void KorisniciIzmjna_Load(object sender, EventArgs e)
        {           
            LoadData();
        }

        private async void LoadData()
        {
            var list = await _apiServiceNaselja.GetT<List<esp_NaseljaAll_Result>>();

            if (list != null)
            {
                List<esp_NaseljaAll_Result> naselja = list;
                naselja.Insert(0, new esp_NaseljaAll_Result());
                naselja[0].Naziv = "Odaberite naselje";
                cbcNaselja.DataSource = naselja;
                cbcNaselja.DisplayMember = "Naziv";
                cbcNaselja.ValueMember = "NaseljeID";
            }
            LoadStatus();
            txtAdresa.Text = k.Adresa;
            txtIme.Text = k.Ime;
            txtMail.Text = k.Mail;
            txtPrezime.Text = k.Prezime;
            txtTelefon.Text = k.Telefon;
            cbcNaselja.SelectedValue = k.NaseljeID;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            txtAdresa.Text = txtIme.Text = txtPrezime.Text = txtMail.Text = txtTelefon.Text = "";
            cbcNaselja.SelectedValue = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                k.Adresa = txtAdresa.Text;
                k.Mail = txtMail.Text;
                k.Telefon = txtTelefon.Text;
                k.Prezime = txtPrezime.Text;
                k.Ime = txtIme.Text;
                k.KorisnickoIme = k.KorisnickoIme;
                k.LozinkaHash = k.LozinkaHash;
                k.LozinkaSalt = k.LozinkaSalt;
                k.NaseljeID = Convert.ToInt32(cbcNaselja.SelectedValue);
                k.Status = k.Status;
                k.UlogaID = k.UlogaID;


                var list = _apiService.Update<Korisnici>(k.KorisnikID,k);

                if (list != null)
                {
                    const string message = "Uspješno ste izmjenuli podatke o korisniku!";
                    const string caption = "Informacija";

                    var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInput();
                    this.Close();
                    Korisnici_forme.KorisniciLista kl = new Korisnici_forme.KorisniciLista();
                    kl.Show();

                }
                else
                {
                    const string message = "Podaci o korisniku nisu izmjenjeni!";
                    const string caption = "Informacija";

                    var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ClearInput()
        {
            txtAdresa.Text = txtIme.Text = txtPrezime.Text = txtMail.Text = txtTelefon.Text ="";
            cbcNaselja.SelectedValue = 0;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var response = _apiServiceUpdateAktivnost.GetByZahtjevOdobri<int>(k.KorisnikID, k.Status.Value);
        
            if (response!=null)
            {
                if (k.Status.Value)
                {
                    labelAktivnost.Text = "Aktivan";
                    linkLabel1.Text = "Deaktiviraj korisnika";
                }
                else
                {
                    labelAktivnost.Text = "Neaktivan";
                    linkLabel1.Text = "Aktiviraj korisnika";
                }
            }
            LoadStatus();
        }

        private async void LoadStatus()
        {
            k = new esp_KorisnikByID_Result();
            k = await _apiService.GetById<esp_KorisnikByID_Result>(korisnikID);

          
            if (k.Status.Value)
            {
                labelAktivnost.Text = "Aktivan";
                linkLabel1.Text = "Deaktiviraj korisnika";
            }
            else
            {
                labelAktivnost.Text = "Neaktivan";
                linkLabel1.Text = "Aktiviraj korisnika";
            }
        }
    }
}
