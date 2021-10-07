using SF_19_2019_POP2020.Windows.DoktoriProzori;
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

namespace SF_19_2019_POP2020.Windows.TerminiWindow
{
    /// <summary>
    /// Interaction logic for TerminiKodLekaraPick.xaml
    /// </summary>
    public partial class TerminiKodLekaraPick : Window
    {
        int id;
        public TerminiKodLekaraPick()
        {
            InitializeComponent();
        }

        private void btnPicLekar_Click(object sender, RoutedEventArgs e)
        {
            LekariPick gw = new LekariPick(LekariPick.Stanje.PREUZIMANJE);
            if (gw.ShowDialog() == true)
            {
                //  domZdravlja.Adresa = gw.SelektovanaAdresa;
                id = gw.SelektovaniLekar.ID;
            }


        }


        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            TerminViaLekarPrikaz izd = new TerminViaLekarPrikaz(id);
            izd.Show();
        }


    }
}
