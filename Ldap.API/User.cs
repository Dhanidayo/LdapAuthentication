using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ldap.API
{
    public class User
    {
        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        private string pwd;
        public string Pwd
        {
            get { return pwd; }
            set { pwd = value; }
        }
    }
}
