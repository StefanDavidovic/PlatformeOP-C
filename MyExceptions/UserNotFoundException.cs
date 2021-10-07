using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF15_2019_POP2020.MyExceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base() { }

        public UserNotFoundException(string message) : base(message) { }

        public UserNotFoundException(string message, Exception exception) : base(message, exception) { }
    }
}
