using SF_19_2019_POP2020.Windows.AdresaProzori;
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

namespace SF_19_2019_POP2020.Windows.DomZdravljaProzori
{
    /// <summary>
    /// Interaction logic for DomoviZdravljaAddEdit.xaml
    /// </summary>
    public partial class DomoviZdravljaAddEdit : Window
    {

        DomZdravlja domZdravlja;
        public enum Stanje { DODAVANJE, IZMENA };
        Stanje stanje;

        public DomoviZdravljaAddEdit(DomZdravlja domZdravlja, Stanje stanje = Stanje.DODAVANJE)
        {
            InitializeComponent();
            this.domZdravlja = domZdravlja;
            this.stanje = stanje;
            Random random = new Random();
          //  domZdravlja.Sifra = random.Next(1, 1000);
            domZdravlja.Aktivan = true;
            //   tbSifra.DataContext = domZdravlja;
            domZdravlja.AdresaID = 147;
            tbNaziv.DataContext = domZdravlja;
            tbAdresa.DataContext = domZdravlja;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            
            if (stanje == Stanje.DODAVANJE)
            {
                if (validacije())
                {
                    domZdravlja.Aktivan = true;
                    Util.Instance.DomoviZdravlja.Add(domZdravlja);
                    Util.Instance.SacuvajEntitet(domZdravlja);
                    this.DialogResult = true;
                    this.Close();
                }
            }
            if (stanje == Stanje.IZMENA)
            {
                if (validacije())
                {
                    //Util.Instance.Adrese.Add(adresa);
                    Util.Instance.updateDomZdravlja(domZdravlja);
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    this.DialogResult = false;
                }


            }
            
        }

        private void btnPicAdresa_Click(object sender, RoutedEventArgs e)
        {
            AdresaPick gw = new AdresaPick(AdresaPick.Stanje.PREUZIMANJE);
            if (gw.ShowDialog() == true)
            {
              //  domZdravlja.Adresa = gw.SelektovanaAdresa;
                domZdravlja.AdresaID = gw.SelektovanaAdresa.SifraAdrese;
            }
        }


        private bool validacije()
        {
            bool ok = true;
            String poruka = "DZ se nije sacuvao\nMolimo popravite sledece greske u unosu:\n";
            if (tbNaziv.Text.Equals(""))
            {
                poruka += "\n- Polje naziv ne sme biti Prazno!\n";
                ok = false;
            }
            if (Util.Instance.proveriAdresu(domZdravlja.AdresaID) == false)
            {
                {
                    poruka += "\n- Nema takve adrese!\n";
                    ok = false;
                }
            }
            if (ok == false)
            {
                MessageBox.Show(poruka, "Probajte ponovo");
            }
            return ok;




        }

    }
}
