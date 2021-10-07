using SF_19_2019_POP2020.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace SF_19_2019_POP2020.Windows
{
    /// <summary>
    /// Interaction logic for AdministratoriWindow.xaml
    /// </summary>
    public partial class AdministratoriWindow : Window
    {
        ICollectionView view;

        public ObservableCollection<Korisnik> Korisnici1 { get; set; }

        public AdministratoriWindow()
        {
            InitializeComponent();
            Korisnici1 = new ObservableCollection<Korisnik>();



            foreach (Korisnik korisnik in Aplikacija.Instance.Korisnici)
            {
                if (korisnik.TipKorisnika.Equals(ETipKorisnika.ADMINISTRATOR))
                {
                    Korisnici1.Add(korisnik);
                    view = CollectionViewSource.GetDefaultView(Korisnici1);
                }

            }
            dgAdministratori.ItemsSource = view;
            dgAdministratori.IsSynchronizedWithCurrentItem = true;


            dgAdministratori.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}