using SF_19_2019_POP2020.Windows.DomZdravljaProzori;
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
    /// Interaction logic for LekaraViaDomZdravlja.xaml
    /// </summary>
    public partial class LekaraViaDomZdravlja : Window
    {
        int id;
        public LekaraViaDomZdravlja()
        {
            int id;
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
            PrikazLekaraDz izd = new PrikazLekaraDz(id);
            izd.Show();
        }
    }
}
