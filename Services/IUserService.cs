using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF15_2019_POP2020.Services
{
    public interface IUserService
    {
        void readUsers(string filename);
        void saveUsers(string filename);

        void deleteUser(string username);
    }
}
