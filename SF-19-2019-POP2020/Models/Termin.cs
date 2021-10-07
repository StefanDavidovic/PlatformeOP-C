using SF_19_2019_POP2020.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF19_2019_POP2020.Models
{
    public class Termin: ICloneable, INotifyPropertyChanged
    {
        private int sifra;
        private int lekarID;
        private DateTime datum;
        private EStatusTermina status;
        private int pacijentID;
        private Boolean aktivan;



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

        public DateTime Datum
        {
            get
            {
                return datum;
            }
            set
            {
                datum = value;
                OnPropertyChanged("Datum");
            }
        }

        public EStatusTermina Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                OnPropertyChanged("Status");
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
            Termin kopija = new Termin();
            kopija.sifra = Sifra;
            kopija.lekarID = lekarID;
            kopija.datum = Datum;
            kopija.status = Status;
            kopija.pacijentID = pacijentID;
            kopija.aktivan = Aktivan;
            return kopija;

        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}