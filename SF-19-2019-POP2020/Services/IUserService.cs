using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_19_2019_POP2020.Services
{
    public interface IUserService
    {
        void readUsers();
        int saveUser(Object obj);
        void updateUser(Object obj);

        void updateUser1(Object obj);
        void deleteUser(string username);
    }
}
