using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ldap.Models;

namespace Ldap.BusinessLogic.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateJSONWebToken(User user);
    }
}
