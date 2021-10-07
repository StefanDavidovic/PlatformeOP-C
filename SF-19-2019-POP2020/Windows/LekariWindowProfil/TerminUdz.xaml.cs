using SF_19_2019_POP2020.Models;
using SF_19_2019_POP2020.Windows.DomZdravljaProzori;
using SF_19_2019_POP2020.Windows.Pretrage;
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
    /// Interaction logic for TerminUdz.xaml
    /// </summary>
    public partial class TerminUdz : Window
    {
        int id;
        Lekar lekar;
        public TerminUdz(Lekar lekar)
        {
 
            this.lekar = lekar;

 
            InitializeComponent();
        }

        private void btnPicDomZdravlja_Click(object sender, RoutedEventArgs e)
        {
            DomZdravljaPick gw = new DomZdravljaPick(DomZdravljaPick.Stanje.PREUZIMANJE);
            if (gw.ShowDialog() == true)
            {
                //  domZdravlja.Adresa = gw.SelektovanaAdresa;
                id = gw.SelektovaniDomZdravlja.Sifra;
            }
        }







        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DateTime datum = (DateTime)dpDatum.SelectedDate;
             PrikazTerminaZaDz izd = new PrikazTerminaZaDz(id, lekar, datum);
            izd.Show();



        }

    }
}
