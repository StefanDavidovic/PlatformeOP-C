
using SF_19_2019_POP2020.Windows.DoktoriProzori;
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
    /// Interaction logic for PacijentZakazivajeTermina.xaml
    /// </summary>
    /// 
    public partial class PacijentZakazivajeTermina : Window
    {
        Termin termin;
        Pacijent pacijent;
        public enum Stanje { DODAVANJE, IZMENA };
        Stanje stanje;
        public PacijentZakazivajeTermina(Termin termin, Pacijent pacijent, Stanje stanje = Stanje.DODAVANJE)
        {
            InitializeComponent();
           
            this.termin = termin;
            this.pacijent = pacijent;
            this.stanje = stanje;


            termin.Status = EStatusTermina.ZAKAZAN;
            termin.PacijentID = pacijent.ID;

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            if (stanje == Stanje.DODAVANJE)
            {
                Util.Instance.Termini.Add(termin);
                Util.Instance.SacuvajEntitet(termin);
            }
            if (stanje == Stanje.IZMENA)
            {
                //Util.Instance.Adrese.Add(adresa);
                Util.Instance.updateTermin(termin);


            }
            this.Close();
        }

        private void btnPickLekar_Click(object sender, RoutedEventArgs e)
        {
            LekariPick gw = new LekariPick(LekariPick.Stanje.PREUZIMANJE);
            if (gw.ShowDialog() == true)
            {
                termin.LekarID = gw.SelektovaniLekar.ID;
            }
        }
    }
}
