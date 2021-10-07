using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_19_2019_POP2020.Services
{
    interface IPacijentService
    {
        void readPacijent();
        int savePacijent(Object obj);
        void updatePacijent(Object obj);
        void updatePacijent1(Object obj);
        void deletePacijent(int broj);
    }
}
