using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_19_2019_POP2020.Services
{
    interface ITerapijaService
    {
        void readTerapija();
        int saveTerapija(Object obj);
        void updateTerapija(Object obj);
        void updateTerapija1(Object obj);
        void deleteTerapija(int broj);
    }
}
