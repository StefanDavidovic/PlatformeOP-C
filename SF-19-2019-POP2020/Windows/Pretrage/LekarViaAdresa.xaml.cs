using SF_19_2019_POP2020.Windows.AdresaProzori;
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

namespace SF_19_2019_POP2020.Windows.Pretrage
{
    /// <summary>
    /// Interaction logic for LekarViaAdresa.xaml
    /// </summary>
    public partial class LekarViaAdresa : Window
    {
        int id;
        public LekarViaAdresa()
        {
            int id;
            InitializeComponent();
        }

        private void btnPicDomZdravlja_Click(object sender, RoutedEventArgs e)
        {
            AdresaPick gw = new AdresaPick(AdresaPick.Stanje.PREUZIMANJE);
            if (gw.ShowDialog() == true)
            {
                //  domZdravlja.Adresa = gw.SelektovanaAdresa;
                id = gw.SelektovanaAdresa.SifraAdrese;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            PrikazLekaraAdresa izd = new PrikazLekaraAdresa(id);
            izd.Show();
        }
    }
}

