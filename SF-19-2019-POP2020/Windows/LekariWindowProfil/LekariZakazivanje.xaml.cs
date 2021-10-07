using SF_19_2019_POP2020.Models;
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
    /// Interaction logic for LekariZakazivanje.xaml
    /// </summary>
    public partial class LekariZakazivanje : Window
    {
        Termin termin;
        public LekariZakazivanje(Termin termin,Lekar lekar)
        {
            InitializeComponent();
            this.termin = termin;



            //BUG SA DATUMOM - ne radi odabir datuma tjst ne prolazi select.

 
            Random random = new Random();
            termin.Status = EStatusTermina.SLOBODAN;
            termin.Aktivan = true;
            termin.Datum = DateTime.Now;
            termin.PacijentID = 100;
            termin.LekarID = lekar.ID;
            // termin.LekarID = 14;
            //   termin.PacijentID = 323;
            //  tbSifra.DataContext = termin;
            //    tbLekar1.DataContext = termin;
            dpDatum.DataContext = termin;
            //   tbLekar1.DataContext = termin;
            //   tbPacijent.DataContext = termin;
        }


        private void btnOK_Click(object sender, RoutedEventArgs e)
        {

            if (validacije())
            {
                Util.Instance.Termini.Add(termin);
                Util.Instance.SacuvajEntitet(termin);
                this.Close();
            }

        }

        private bool validacije()
        {
            bool ok = true;
            String poruka = "Termin se nije sacuvao\nMolimo popravite sledece greske u unosu:\n";
            if (dpDatum.SelectedDate < DateTime.Now)
            {
                poruka += "\n- Izabrali ste datum u proslosti!\n";
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
