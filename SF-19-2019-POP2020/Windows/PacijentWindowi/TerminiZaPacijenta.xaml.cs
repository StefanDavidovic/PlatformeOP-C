using SF19_2019_POP2020.Models;
using System;
using System.Collections.Generic;
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

namespace SF_19_2019_POP2020.Windows.PacijentWindowi
{
    /// <summary>
    /// Interaction logic for TerminiZaPacijenta.xaml
    /// </summary>
    public partial class TerminiZaPacijenta : Window
    {
        ICollectionView view;
        Pacijent pacijent;
        public TerminiZaPacijenta(Pacijent pacijent)
        {
            this.pacijent = pacijent;
            InitializeComponent();
            view = CollectionViewSource.GetDefaultView(Util.Instance.Termini);
            view.Filter = PrikazFiltera;

            dgTermini.ItemsSource = view;
            dgTermini.IsSynchronizedWithCurrentItem = true;


            dgTermini.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private bool PrikazFiltera(object obj)
        {
            return ((Termin)obj).Aktivan;
        }

        private void btnZakazi(object sender, RoutedEventArgs e)
        {

            SlobodniTermini odzm = new SlobodniTermini(pacijent);
            odzm.Show();
        }

    }
}
