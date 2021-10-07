using SF_19_2019_POP2020.Models;
using SF_19_2019_POP2020.Windows.Pretrage;
using SF19_2019_POP2020.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
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

namespace SF_19_2019_POP2020.Windows.NEPRIJAVLJENIWindow
{
    /// <summary>
    /// Interaction logic for Neregistrovani.xaml
    /// </summary>
    public partial class Neregistrovani : Window

    {
        ICollectionView view3;
        public Neregistrovani()
        {
           
            InitializeComponent();
            Util.Instance.CitanjeEntiteta();
            view3 = CollectionViewSource.GetDefaultView(Util.Instance.Lekari);
            view3.Filter = CustomFilter;
            dgLekari.ItemsSource = view3;
            dgLekari.IsSynchronizedWithCurrentItem = true;
            dgLekari.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }


        private bool CustomFilter(object obj)
        {
            Lekar korisnik = obj as Lekar;
            // Korisnik korisnik1 = (Korisnik)obj;

            if (korisnik.Aktivan)
            {
                if (TxtPretraga.Text != "")
                {
                    if (korisnik.Ime.Contains(TxtPretraga.Text))
                    {
                        return korisnik.Ime.Contains(TxtPretraga.Text);
                    }
                    if (korisnik.Prezime.Contains(TxtPretraga.Text))
                    {
                        return korisnik.Prezime.Contains(TxtPretraga.Text);
                    }
                    if (korisnik.Email.Contains(TxtPretraga.Text))
                    {
                        return korisnik.Email.Contains(TxtPretraga.Text);
                    }
                }
                else
                    return true;

            }
            return false;
        }

        private void TxtPretraga_KeyUp(object sender, KeyEventArgs e)
        {
            view3.Refresh();
        }



        private void DGL_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("ID") || e.PropertyName.Equals("JMBG") || e.PropertyName.Equals("Lozinka"))
                e.Column.Visibility = Visibility.Collapsed;
        }
        private void btn_DomZdravlja(object sender, RoutedEventArgs e)
        {
            OdaberiDomZdravljaMesto izd = new OdaberiDomZdravljaMesto();
            izd.Show();
        }

        private void btn_DomZdravlja2(object sender, RoutedEventArgs e)
        {
            DomZdravljaViaAdresa izd = new DomZdravljaViaAdresa();
            izd.Show();
        }

    }
}