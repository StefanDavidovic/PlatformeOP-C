using SF_19_2019_POP2020.Models;
using SF_19_2019_POP2020.Windows.AdresaProzori;
using SF_19_2019_POP2020.Windows.PacijentWindowi;
using SF19_2019_POP2020;
using SF19_2019_POP2020.Models;
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

namespace SF_19_2019_POP2020.Windows
{
    /// <summary>
    /// Interaction logic for RegistracijaPacijent.xaml
    /// </summary>
    public partial class RegistracijaPacijent : Window
    {
        Pacijent korisnik = new Pacijent();



        public RegistracijaPacijent()
        {
            InitializeComponent();

            tbPol.ItemsSource = Enum.GetValues(typeof(EPol)).Cast<EPol>();
            Random random = new Random();
            //    korisnik.ID = random.Next(1, 1000);
            korisnik.Aktivan = true;
            korisnik.AdresaID = 836;
            tbLozinka.DataContext = korisnik;
            tbIme.DataContext = korisnik;
            tbEmail.DataContext = korisnik;
            tbIme.DataContext = korisnik;
            tbJmbg.DataContext = korisnik;
            tbPol.DataContext = korisnik;
            tbPrezime.DataContext = korisnik;
            tbPol.DataContext = korisnik;
            tbAdresa.DataContext = korisnik;
        }
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {


            if (validacije())
            {

                //   this.DialogResult = true;
                Util.Instance.Pacijenti.Add(korisnik);
                Util.Instance.SacuvajEntitet(korisnik);
                PacijentGlavna hw = new PacijentGlavna(tbJmbg.Text);
                hw.Show();
                this.Close();
            }
        }



        private void btnPicAdresa_Click(object sender, RoutedEventArgs e)
        {
            // Util.Instance.CitanjeEntiteta();
            AdresaPick gw = new AdresaPick(AdresaPick.Stanje.PREUZIMANJE);
            if (gw.ShowDialog() == true)
            {
                korisnik.AdresaID = gw.SelektovanaAdresa.SifraAdrese;
            }
        }

        private bool validacije()
        {
            bool ok = true;
            String poruka = "Korisnik se nije sacuvao\nMolimo popravite sledece greske u unosu:\n";
            if (tbIme.Text.Equals(""))
            {
                poruka += "- Polje Ime ne sme biti Prazno!\n";
                ok = false;
            }

            if (Util.Instance.proveriAdresu(korisnik.AdresaID) == false)
            {
                poruka += "- Nema takve adrese!\n";
                ok = false;
            }
            if (tbPrezime.Text.Equals(""))
            {
                poruka += "- Polje Prezime ne sme biti Prazno!\n";
                ok = false;
            }

            if (tbLozinka.Text.Equals(""))
            {
                poruka += "- Polje Lozinka ne sme biti Prazno!\n";
                ok = false;
            }
            if (!tbEmail.Text.Contains("@"))
            {
                poruka += "- Polje email nije u odgovarajucem formatu!\n";
                ok = false;
            }

            if (!tbJmbg.Text.All(char.IsDigit))
            {
                poruka += "- jmbg ne valj!\n";
                ok = false;
            }

            if (jelUnikat(tbJmbg.Text))
            {
                poruka += "- jmbg je zauzet!\n";
                ok = false;
            }

            if(tbJmbg.Text.Length != 13)
            {
                poruka += "- jmbg mora imati 13 cifara!\n";
                ok = false;
            }



            if (tbLozinka.Text.Equals(""))
            {
                poruka += "- Jmbg mora imati 13 cifara!\n";
                ok = false;
            }
            if (ok == false)
            {
                MessageBox.Show(poruka, "Probajte ponovo");
            }
            return ok;




        }

        private bool jelUnikat(string jmbg)
        {

            foreach (Pacijent pacijent in Util.Instance.Pacijenti)
            {
                if (jmbg.Equals(pacijent.JMBG))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
