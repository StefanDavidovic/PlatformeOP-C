using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_19_2019_POP2020.Models
{
    public class Datoteke
    {

        public ObservableCollection<Korisnik> Korisnici { get; set; }

        private static Datoteke _instance = null;
        public static Datoteke Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Datoteke();
                return _instance;
            }
        }

        private Datoteke()
        {
            Korisnici = Korisnik.GetAll();

        }

    }

}