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

    public partial class AddEditDoctor : Window
    {
        private EStatus odabranStatus;
        private Lekar odabranLekar;
        public AddEditDoctor(Lekar lekar, EStatus status = EStatus.Dodaj)
        {
            InitializeComponent();

            odabranLekar = lekar;
            odabranStatus = status;

            if(status.Equals(EStatus.Izmeni) && lekar != null && lekar.TipKorisnika == ETipKorisnika.LEKAR)
            {
                this.Title = "Izmeni lekara";
                TxtEmail.Text = lekar.Email;
                TxtKorisnickoIme.Text = lekar.KorisnickoIme;
                TxtName.Text = lekar.Ime;
                TxtPrezime.Text = lekar.Prezime;
                TxtKorisnickoIme.IsEnabled = false;
            } 
            else
            {
                this.Title = "Dodaj lekara";
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

            Lekar lekar = new Lekar
            {
                Ime = TxtName.Text,
                Prezime = TxtPrezime.Text,
                KorisnickoIme = TxtKorisnickoIme.Text,
                Email = TxtEmail.Text,
                TipKorisnika = tip,
                Aktivan = true,
                JMBG = "1234",
                Lozinka = "1234", 
                DomZdravlja = "Dom zdravlja 1", 
                Korisnicko = k
            };

            if(odabranStatus.Equals(EStatus.Dodaj))
            {
                Util.Instance.Korisnici.Add(k);
                Util.Instance.Lekari.Add(lekar);
            }
            else
            {
                int izmenaLekar = Util.Instance.Lekari.ToList().FindIndex(u => u.KorisnickoIme.Equals(TxtKorisnickoIme.Text));
                int izmenaKorisnik = Util.Instance.Korisnici.ToList().FindIndex(u => u.KorisnickoIme.Equals(TxtKorisnickoIme.Text));

                Util.Instance.Korisnici[izmenaKorisnik] = k;
                Util.Instance.Lekari[izmenaLekar] = lekar;
            }

            Util.Instance.SacuvajEntite("korisnici.txt");
            Util.Instance.SacuvajEntite("lekari.txt");

            this.DialogResult = true;
            this.Close();
        }
    }
}
