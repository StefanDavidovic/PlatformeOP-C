using SF_19_2019_POP2020.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF19_2019_POP2020.Models
{
    public class Terapija : ICloneable, INotifyPropertyChanged
    {

        private int sifra;
        private string opis;
        private int lekarID;
        private int pacijentID;
        private bool aktivan;



        public int Sifra
        {
            get
            {
                return sifra;

            }
            set
            {
                sifra = value;
                OnPropertyChanged("Sifra");
            }

        }

        public string Opis
        {
            get
            {
                return opis;

            }
            set
            {
                opis = value;
                OnPropertyChanged("Opis");
            }

        }

        public int LekarID
        {
            get
            {
                return lekarID;

            }
            set
            {
                lekarID = value;
                OnPropertyChanged("Lekar");
            }

        }

        public Boolean Aktivan
        {
            get
            {
                return aktivan;
            }
            set
            {
                aktivan = value;
                OnPropertyChanged("Aktivan");
            }
        }


        public int PacijentID
        {
            get
            {
                return pacijentID;

            }
            set
            {
                pacijentID = value;
                OnPropertyChanged("Pacijent");
            }

        }


        public string terapijaUFajl()
        {
            return ";";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public object Clone()
        {
            Terapija kopija = new Terapija();
            kopija.Opis = Opis;
            kopija.lekarID = lekarID;
            kopija.pacijentID = pacijentID;
            kopija.Sifra = Sifra;
            kopija.Aktivan = Aktivan;
            return kopija;

        }
    }
}
