using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_19_2019_POP2020.Models
{
    public class Adresa: ICloneable, INotifyPropertyChanged
    {
        private int sifraAdrese;
        private string broj;
        private string ulica;
        private string drzava;
        private string grad;
        private Boolean aktivan;


        public int SifraAdrese
        {
            get
            {
                return sifraAdrese;
            }
            set
            {
                sifraAdrese = value;
                OnPropertyChanged("SifraAdrese");
            }
        }

        public string Ulica
        {
            get
            {
                return ulica;
            }
            set
            {
                ulica = value;
                OnPropertyChanged("Ulica");
            }
        }


        public string Broj
        {
            get
            {
                return broj;
            }
            set
            {
                broj = value;
                OnPropertyChanged("Broj");
            }
        }

        public string Drzava
        {
            get
            {
                return drzava;
            }
            set
            {
                drzava = value;
                OnPropertyChanged("Drzava");
            }
        }

        public string Grad
        {
            get
            {
                return grad;
            }
            set
            {
                grad = value;
                OnPropertyChanged("Grad");
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



        public override string ToString()
        {
            return SifraAdrese +"-" + Grad;
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
            Adresa kopija= new Adresa();
            kopija.SifraAdrese = SifraAdrese;
            kopija.Drzava = Drzava;
            kopija.Broj = Broj;
            kopija.Aktivan = Aktivan;
            kopija.Grad = Grad;
            kopija.Ulica = Ulica;
            return kopija;

        }
    }
}
