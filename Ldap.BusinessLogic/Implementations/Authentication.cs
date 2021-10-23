using System;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Threading.Tasks;
using Ldap.BusinessLogic.Interfaces;
using Ldap.Models.DTOs;

namespace Ldap.BusinessLogic.Implementations
{
    public class Authentication : IAuthentication
    {
        private static string _path = "domain.com";
        public static string _filterAttribute;
        //public string username;
        //public string pwd;

        public string IsAuthenticated(UserRequestDTO userRequestDTO)
        {
            string domainAndUsername = "Domain" + @"\" + UserRequestDTO.Username;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, UserRequestDTO.Password);

            try
            {
                DirectorySearcher search = new DirectorySearcher(entry) { Filter = "(SAMAccountName=" + UserRequestDTO.Username + ")" };

                search.PropertiesToLoad.Add("cn");
                SearchResult result =  search.FindOne();

                if (result != null)
                {
                    //Update the new path to the user in the directory.
                    _path = result.Path;
                    _filterAttribute = (string)result.Properties["cn"][0];
                    //Console.WriteLine(_path);
                    //Console.WriteLine(_filterAttribute);
                    //Console.WriteLine("Done");
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