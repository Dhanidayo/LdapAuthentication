using System;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Threading.Tasks;
using Ldap.BusinessLogic.Interfaces;
using Ldap.BusinessLogic.Implementations;
using Ldap.Models;
using Ldap.Models.DTOs;

namespace Ldap.BusinessLogic.Implementations
{
    public class Authentication : IAuthentication
    {
        private static string _path = "LDAP://domain/DC=domain,DC=com"; //your domain address goes in here. e.g. domain.com
        public static string _filterAttribute;

        public string IsAuthenticated(User user)
        {
            string domainAndUsername = "Domain" + @"\" + user.Username;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, user.Password);

            try
            {
                DirectorySearcher search = new DirectorySearcher(entry) { Filter = "(SAMAccountName=" + user.Username + ")" };

                search.PropertiesToLoad.Add("cn");
                SearchResult result =  search.FindOne();

                if (result != null)
                {
                    //Update the new path to the user in the directory.
                    _path = result.Path;
                    _filterAttribute = (string)result.Properties["cn"][0];
                    return _path;
                }
                throw new AccessViolationException("Invalid credentials");               
            }
            catch (LdapException e)
            {

                Console.WriteLine("\r\nUnable to login:\r\n\t" + e.Message);
            }

            throw new AccessViolationException("Invalid credentials");
        }

    }
}