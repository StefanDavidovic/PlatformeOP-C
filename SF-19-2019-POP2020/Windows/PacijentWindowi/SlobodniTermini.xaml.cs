using SF_19_2019_POP2020.Windows.TerminiWindow;
using SF19_2019_POP2020.Models;
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

namespace SF_19_2019_POP2020.Windows.PacijentWindowi
{
    /// <summary>
    /// Interaction logic for SlobodniTermini.xaml
    /// </summary>
    public partial class SlobodniTermini : Window
    {
        ICollectionView view;
        Pacijent pacijent;
        public SlobodniTermini(Pacijent pacijent)
        {
            this.pacijent = pacijent;
            InitializeComponent();
            ObservableCollection<Termin> t2 = nadjiTermin();
            view = CollectionViewSource.GetDefaultView(t2);

            dgTermini.ItemsSource = view;
            dgTermini.IsSynchronizedWithCurrentItem = true;

            dgTermini.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }






        public ObservableCollection<Termin> nadjiTermin()
        {
            ObservableCollection<Termin> pac = new ObservableCollection<Termin>();

            foreach (Termin t in Util.Instance.Termini)
            {
                if (t.Status.Equals(EStatusTermina.SLOBODAN) && t.Aktivan == true)
                {

                    pac.Add(t);
                }

            }
            return pac;

        }

        private void ZakaziTermin(object sender, RoutedEventArgs e)
        {
            Termin selektovaniTermin = view.CurrentItem as Termin;

            if (selektovaniTermin != null)
            {

                Termin old = (Termin)selektovaniTermin.Clone();
                PacijentZakazivajeTermina few = new PacijentZakazivajeTermina(selektovaniTermin,pacijent,
                    PacijentZakazivajeTermina.Stanje.IZMENA);
                if (few.ShowDialog() != true)
                {

                    int index = Aplikacija.Instance.Termini.IndexOf(
                        selektovaniTermin);
                    //   Aplikacija.Instance.Termini[index] = old;
                }
            }
            view.Refresh();
        }


        private void TerminTermin(object sender, RoutedEventArgs e)
        {
            TerminiKodLekaraPick tklp = new TerminiKodLekaraPick();
            tklp.Show();
        }





    }
}
