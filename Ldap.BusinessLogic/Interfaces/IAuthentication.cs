using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ldap.Models;

namespace Ldap.BusinessLogic.Interfaces
{
    public interface IAuthentication
    {
        string IsAuthenticated(User user);
    }
}
