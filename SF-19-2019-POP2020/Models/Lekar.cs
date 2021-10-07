using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_19_2019_POP2020.Models
{
    public class Lekar : ICloneable, INotifyPropertyChanged
    {
        private int id;
        private string ime;
        private string prezime;
        private string lozinka;
        private string email;
        private string jmbg;
        private int adresaID;
        private EPol pol;
        private bool aktivan;
        private int domZdravljaID;


        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }



        public string Ime
        {
            get
            {
                return ime;
            }
            set
            {
                ime = value;
                OnPropertyChanged("Ime");
            }
        }

        public string Prezime
        {
            get
            {
                return prezime;
            }
            set
            {
                prezime = value;
                OnPropertyChanged("Prezime");
            }
        }
        public string Lozinka
        {
            get
            {
                return lozinka;
            }
            set
            {
                lozinka = value;
                OnPropertyChanged("Lozinka");
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }


        public string JMBG
        {
            get
            {
                return jmbg;
            }
            set
            {
                jmbg = value;
                OnPropertyChanged("Jmbg");
            }
        }



        public int AdresaID
        {
            get
            {
                return adresaID;
            }
            set
            {
                adresaID = value;
                OnPropertyChanged("AdresaID");
            }
        }


        public int DomZdravljaID
        {
            get
            {
                return domZdravljaID;
            }
            set
            {
                domZdravljaID = value;
                OnPropertyChanged("domZdravljaID");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public EPol Pol
        {
            get
            {
                return pol;
            }
            set
            {
                pol = value;
                OnPropertyChanged("Pol");
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



        public object Clone()
        {
            Lekar kopija = new Lekar();

            kopija.ID = ID;
            kopija.AdresaID = adresaID;
            kopija.Aktivan = Aktivan;
            kopija.Email = Email;
            kopija.Ime = Ime;
            kopija.Prezime = Prezime;
            kopija.Pol = Pol;
            kopija.Lozinka = Lozinka;
            kopija.JMBG = JMBG;
            kopija.domZdravljaID = domZdravljaID;


            return kopija;
        }
    }

}


