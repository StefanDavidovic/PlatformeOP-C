using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF15_2019_POP2020.Models
{
    [Serializable]
    public class Lekar : Korisnik
    {
        private string _domZdravlja;

        public string DomZdravlja
        {
            get { return _domZdravlja; }
            set { _domZdravlja = value; }
        }

        private Korisnik _korisnick;

        public Korisnik Korisnicko
        {
            get { return _korisnick; }
            set { _korisnick = value; }
        }

        public override string ToString()
        {
            return base.ToString() + "Ja sam lekar " + Korisnicko.ToString() + ". Ja radim u " + DomZdravlja;

        }

        public string LekarUpisUFajl()
        {
            return DomZdravlja + ";" + Korisnicko.KorisnickoIme;
        }

    }
}
