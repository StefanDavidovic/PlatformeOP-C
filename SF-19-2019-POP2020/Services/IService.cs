using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_19_2019_POP2020.Services
{
    interface IService
    {
        void readAdrese();
        int saveAdresa(Object obj);
        void updateAdresa(Object obj);
        void updateAdresa1(Object obj);
        void deleteAdresa(int broj);
    }
}
