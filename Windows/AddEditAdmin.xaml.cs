using SF15_2019_POP2020.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SF15_2019_POP2020.Windows
{

    public partial class AddEditAdmin : Window
    {
        private EStatus odabranStatus;
        private Korisnik odabraniAdmin;
        public AddEditAdmin(Korisnik korisnik, EStatus status = EStatus.Dodaj)
        {
            InitializeComponent();

            odabraniAdmin = korisnik;
            odabranStatus = status;

            if (status.Equals(EStatus.Izmeni) && korisnik != null && korisnik.TipKorisnika == ETipKorisnika.ADMINISTRATOR)
            {
                this.Title = "Izmeni Admina";
                TxtEmail.Text = korisnik.Email;
                TxtKorisnickoIme.Text = korisnik.KorisnickoIme;
                TxtName.Text = korisnik.Ime;
                TxtPrezime.Text = korisnik.Prezime;
                TxtKorisnickoIme.IsEnabled = false;
            }
            else
            {
                this.Title = "Dodaj Admina";
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {

            this.DialogResult = false;
            this.Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)CmbTipKorisnika.SelectedItem;
            string value = item.Content.ToString();
            Enum.TryParse(value, out ETipKorisnika tip);

            Korisnik k = new Korisnik
            {
                Ime = TxtName.Text,
                Prezime = TxtPrezime.Text,
                KorisnickoIme = TxtKorisnickoIme.Text,
                Email = TxtEmail.Text,
                TipKorisnika = tip,
                Aktivan = true,
                JMBG = "1234",
                Lozinka = "1234"
            };


            if (odabranStatus.Equals(EStatus.Dodaj))
            {
                Util.Instance.Korisnici.Add(k);
                
            }
            else
            {
               
                int izmenaKorisnik = Util.Instance.Korisnici.ToList().FindIndex(u => u.KorisnickoIme.Equals(TxtKorisnickoIme.Text));

                Util.Instance.Korisnici[izmenaKorisnik] = k;
               
            }

            Util.Instance.SacuvajEntite("korisnici.txt");
           

            this.DialogResult = true;
            this.Close();
        }
    }
}

