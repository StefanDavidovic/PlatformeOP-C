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

namespace SF_19_2019_POP2020.Windows.DomZdravljaProzori
{
    /// <summary>
    /// Interaction logic for DomZdravljaPick.xaml
    /// </summary>
    public partial class DomZdravljaPick : Window
    {
        public enum Stanje { ADMINISTRACIJA, PREUZIMANJE };
        Stanje stanje;
        ICollectionView view;

        public DomZdravlja SelektovaniDomZdravlja = null;

        public DomZdravljaPick(Stanje stanje = Stanje.ADMINISTRACIJA)
        {
            InitializeComponent();
            this.stanje = stanje;

            if (stanje == Stanje.PREUZIMANJE)
            {
                btnAdd.Visibility = System.Windows.Visibility.Collapsed;
                btnDelete.Visibility = System.Windows.Visibility.Collapsed;
                btnUpdate.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                btnPick.Visibility = System.Windows.Visibility.Hidden;
            }
            view = CollectionViewSource.GetDefaultView(Util.Instance.DomoviZdravlja);
            view.Filter = CustomFilter;
            dgDomoviZdravlja.ItemsSource = view;

            dgDomoviZdravlja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
        private void DGL_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan"))
                e.Column.Visibility = Visibility.Collapsed;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private bool CustomFilter(object obj)
        {
            DomZdravlja korisnik = obj as DomZdravlja;
            // Korisnik korisnik1 = (Korisnik)obj;

            if (korisnik.Aktivan)
            {
                if (TxtPretraga.Text != "")
                {
                    if (korisnik.Naziv.Contains(TxtPretraga.Text))
                    {
                        return korisnik.Naziv.Contains(TxtPretraga.Text);
                    }

                }
                else
                    return true;

            }
            return false;
        }

        private void TxtPretraga_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }
        private void btnPick_Click(object sender, RoutedEventArgs e)
        {
            SelektovaniDomZdravlja = dgDomoviZdravlja.SelectedItem as DomZdravlja;
            this.DialogResult = true;
            this.Close();
        }
    }
}
