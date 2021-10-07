using SF15_2019_POP2020.Models;
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

namespace SF15_2019_POP2020.Windows
{
    public partial class AllDoctors : Window
    {
        public AllDoctors()
        {

            InitializeComponent();

            UpdateView();
        }

        private void UpdateView()
        {
            DGLekari.ItemsSource = null;
            DGLekari.ItemsSource = Util.Instance.Lekari;
            DGLekari.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGLekari_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            /*if (e.PropertyName.Equals("Aktivan"))
                e.Column.Visibility = Visibility.Collapsed;*/
        }

        private void MIDodajLekara_Click(object sender, RoutedEventArgs e)
        {
            AddEditDoctor add = new AddEditDoctor(null);

            this.Hide();
            if(!(bool)add.ShowDialog())
            {

            }
            this.Show();
        }

        private void MIIzmeniLekara_Click(object sender, RoutedEventArgs e)
        {
            Lekar stariLekar = (Lekar)DGLekari.SelectedItem;
            AddEditDoctor add = new AddEditDoctor(stariLekar, EStatus.Izmeni);

            this.Hide();
            if (!(bool)add.ShowDialog())
            {

            }
            this.Show();
        }

        private void ObrisiLekaraMI_Click(object sender, RoutedEventArgs e)
        {
            Lekar obrisiLekar = (Lekar)DGLekari.SelectedItem;
            Util.Instance.DeleteUser(obrisiLekar.KorisnickoIme);

            int index = Util.Instance.Lekari.ToList().FindIndex(u => u.KorisnickoIme.Equals(obrisiLekar.KorisnickoIme));
            Util.Instance.Lekari[index].Aktivan = false;

            UpdateView();
        }

        private void ObrisiAdminaMI_Click(object sender, RoutedEventArgs e)
        {
            Korisnik obrisiAdmin = (Korisnik)DGLekari.SelectedItem;
            Util.Instance.DeleteUser(obrisiAdmin.KorisnickoIme);

            int index = Util.Instance.Lekari.ToList().FindIndex(u => u.KorisnickoIme.Equals(obrisiAdmin.KorisnickoIme));
            Util.Instance.Lekari[index].Aktivan = false;

            UpdateView();
        }

        private void MIIzmeniAdmina_Click(object sender, RoutedEventArgs e)
        {
            Korisnik stariAdmin = (Korisnik)DGLekari.SelectedItem;
            AddEditAdmin add = new AddEditAdmin(stariAdmin, EStatus.Izmeni);

            this.Hide();
            if (!(bool)add.ShowDialog())
            {

            }
            this.Show();
        }

        private void MIDodajAdmina_Click(object sender, RoutedEventArgs e)
        {
            AddEditAdmin add = new AddEditAdmin(null);

            this.Hide();
            if (!(bool)add.ShowDialog())
            {

            }
            this.Show();
        }
    }
}
