using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_19_2019_POP2020.Services
{
    interface ITerminService
    {
        void readTermin();
        int saveTermin(Object obj);
        void updateTermin(Object obj);
        void updateTermin1(Object obj);
        void deleteTermin(int broj);
    }
}
