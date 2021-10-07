using SF_19_2019_POP2020.Windows.DoktoriProzori;
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

namespace SF_19_2019_POP2020.Windows.TerminiWindow
{
    /// <summary>
    /// Interaction logic for TerminiAddEdit.xaml
    /// </summary>
    public partial class TerminiAddEdit : Window
    {
        Termin termin;
        public enum Stanje { DODAVANJE, IZMENA };
        Stanje stanje;

        public TerminiAddEdit(Termin termin, Stanje stanje = Stanje.DODAVANJE)
        {
            InitializeComponent();

            this.termin = termin;
            this.stanje = stanje;




            tbStatus.ItemsSource = Enum.GetValues(typeof(EStatusTermina)).Cast<EStatusTermina>();

            Random random = new Random();
            termin.Status = EStatusTermina.SLOBODAN;
            termin.Aktivan = true;
            termin.Datum = DateTime.Now;
            termin.LekarID = 768;
            termin.PacijentID = 100;
            tbStatus.DataContext = termin;
          //  tbSifra.DataContext = termin;
        //    tbLekar1.DataContext = termin;
            dpDatum.DataContext = termin;
         //   tbLekar1.DataContext = termin;
         //   tbPacijent.DataContext = termin;
            






        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            

            if (stanje == Stanje.DODAVANJE)
            {
                if (validacije()) { 
                Util.Instance.Termini.Add(termin);
                Util.Instance.SacuvajEntitet(termin);
                    this.DialogResult = true;
                    this.Close();
                }
        }
            if (stanje == Stanje.IZMENA)
            {
                if (validacije()) { 
                //Util.Instance.Adrese.Add(adresa);
                Util.Instance.updateTermin(termin);
                    this.DialogResult = true;
                    this.Close();


                }
                else
                {
                    this.DialogResult = false;
                }

            }

        }

        private void btnPickLekar_Click(object sender, RoutedEventArgs e)
        {
            LekariPick gw = new LekariPick(LekariPick.Stanje.PREUZIMANJE);
            if (gw.ShowDialog() == true)
            {
                termin.LekarID = gw.SelektovaniLekar.ID;
            }
        }


        private void btnPickPacijent_Click(object sender, RoutedEventArgs e)
        {
            PacijentiPick gw = new PacijentiPick(PacijentiPick.Stanje.PREUZIMANJE);
            if (gw.ShowDialog() == true)
            {
                termin.PacijentID = gw.SelektovaniPacijent.ID;
            }
        }

        private bool validacije()
        {
            bool ok = true;
            String poruka = "Termin se nije sacuvao\nMolimo popravite sledece greske u unosu:\n";
            if (Util.Instance.proveriLekara(termin.LekarID)==false)
            {
                poruka += "\n- Ne postoji takav lekar!\n";
                ok = false;
            }
            if (Util.Instance.proveriPacijenta(termin.PacijentID) == false)
            {
                poruka += "\n- Ne postoji takav pacijent!\n";
                ok = false;
            }
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
