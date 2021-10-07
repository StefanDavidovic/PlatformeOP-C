using SF_19_2019_POP2020.Models;
using SF_19_2019_POP2020.Windows.PacijentiProzori;
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

namespace SF_19_2019_POP2020.Windows.LekariWindowProfil
{
    /// <summary>
    /// Interaction logic for LekarZTerapija.xaml
    /// </summary>
    public partial class LekarZTerapija : Window
    {
        Terapija terapija;
        Lekar lekar;
        public LekarZTerapija(Terapija terapija, Lekar lekar)
        {


            InitializeComponent();
            this.lekar = lekar;
            this.terapija = terapija;

            Random random = new Random();
            //      korisnik.ID = random.Next(1, 1000);
            terapija.Aktivan = true;
            tbOpis.DataContext = terapija;
            terapija.PacijentID = 836;
            terapija.LekarID = lekar.ID;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (validacije())
            {
                Util.Instance.Terapije.Add(terapija);
                Util.Instance.SacuvajEntitet(terapija);
                this.Close();

            }

        }

        private void btnPicPacijent_Click(object sender, RoutedEventArgs e)
        {
            PacijentiPick gw = new PacijentiPick(PacijentiPick.Stanje.PREUZIMANJE);
            if (gw.ShowDialog() == true)
            {
                terapija.PacijentID = gw.SelektovaniPacijent.ID;
            }
        }

        private bool validacije()
        {
            bool ok = true;
            String poruka = "Opis se nije sacuvao\nMolimo popravite sledece greske u unosu:\n";
            if (tbOpis.Text.Equals(""))
            {
                poruka += "- Polje Opis ne sme biti Prazno!\n";
                ok = false;
            }
            if (Util.Instance.proveriPacijenta(terapija.PacijentID) == false)
            {
                poruka += "\n- Ne postoji takav pacijent!\n";
                ok = false;
            }

            if (ok == false)
            {
                MessageBox.Show(poruka, "Probajte ponovo");
            }
            return ok;




        }



    }
}
