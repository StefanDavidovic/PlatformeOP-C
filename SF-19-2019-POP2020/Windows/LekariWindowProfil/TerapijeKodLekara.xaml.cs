using SF_19_2019_POP2020.Models;
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
    /// Interaction logic for TerapijeKodLekara.xaml
    /// </summary>
    public partial class TerapijeKodLekara : Window
    {
        Lekar lekar;
        public TerapijeKodLekara(Lekar lekar)
        {
            InitializeComponent();
            this.lekar = lekar;
        }
    }
}
