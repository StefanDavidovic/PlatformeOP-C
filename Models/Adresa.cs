using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF15_2019_POP2020.Models
{
    [Serializable]
    public class Adresa
    {
        private string _id;

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _ulica;

        public string Ulica
        {
            get { return _ulica; }
            set { _ulica = value; }
        }

        private string _broj;

        public string Broj
        {
            get { return _broj; }
            set { _broj = value; }
        }

        private string _drzava;

        public string Drzava
        {
            get { return _drzava; }
            set { _drzava = value; }
        }

        private string _grad;

        public string Grad
        {
            get { return _grad; }
            set { _grad = value; }
        }


        public override string ToString()
        {
            return "Ulica " + Ulica + " broj " + Broj + "Grad " + Grad + " Drzava " + Drzava;
        }

    }
}
