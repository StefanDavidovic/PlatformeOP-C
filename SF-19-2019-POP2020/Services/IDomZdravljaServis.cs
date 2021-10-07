using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_19_2019_POP2020.Services
{
    interface IDomZdravljaServis
    {
        void readDomZdravlja();
        int saveDomZdravlja(Object obj);
        void updateDomZdravlja(Object obj);
        void updateDomZdravlja1(Object obj);
        void deleteDomZdravlja(int broj);
    }
}
