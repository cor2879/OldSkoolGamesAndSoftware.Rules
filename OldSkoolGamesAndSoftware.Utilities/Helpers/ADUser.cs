using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.Text.RegularExpressions;
using System.Globalization;

namespace OldSkoolGamesAndSoftware.Utilities
{
    /// <summary>
    /// Class that will enumerate the following properties for a user:
    /// mailnickname, title, memberof, telephonenumber, name, msexchuserculture,
    /// physicaldeliveryofficename, msrtcsip-primaryuseraddress, department
    /// </summary>
    public class ADUser
    {
        #region Fields

        private static string[] ADProperties = new string[] { "mailnickname", "title", "memberof", 
            "telephonenumber", "name", "msexchuserculture", "physicaldeliveryofficename", "msrtcsip-primaryuseraddress",
            "department"};
        private static string[] ADOriginProperties = { "manager" };
        private static DirectorySearcher ds = new DirectorySearcher(new DirectoryEntry("GC://your.corporate.directory.com")); // TODO: Be sure to change this to the proper directory

        #endregion

        #region Properties
        
        /// <summary>
        /// Alias
        /// </summary>
        public string Alias { get; private set; }
        
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; private set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ManagerDN { get; private set; }

        //public image Picture {get; set;}
        
        /// <summary>
        /// Phone Number
        /// </summary>
        public string Telephonenumber { get; private set; }

        /// <summary>
        /// Full Name
        /// </summary>
        public string FullName { get; private set; }

        /// <summary>
        /// Culture
        /// </summary>
        public string Culture { get; private set; }

        /// <summary>
        /// Office Location
        /// </summary>
        public string Office { get; private set; }
        
        /// <summary>
        /// SipAddress
        /// </summary>
        public string SipAddress { get; private set; }
        
        /// <summary>
        /// List of group memberships
        /// </summary>
        public List<string> Memberships { get; private set; }

        #endregion        


        #region Constructors

        /// <summary>
        /// Construct a new ADUser based on a SearchResult
        /// </summary>
        /// <param name="person">the searchresult</param>
        public ADUser(SearchResult person)
        {
            FillUserProperties(person);
        }

        /// <summary>
        /// Construct a new ADUser based on their alias
        /// </summary>
        /// <param name="alias">the alias</param>
        public ADUser(string alias)
        {
            //bail out if init failed
            if (ds == null)
                return;

            SearchResult person = FindOriginByAlias(alias);
            FillUserProperties(person);

            //fill the group memberships
            this.Memberships = GetMultiResultProperty(person, "memberof");

        }

        #endregion

        #region Methods

        /// <summary>
        /// Cleans the domain.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns></returns>
        public static string CleanDomain(string domain)
        {
            string ret = string.Empty;
            const string DCToken = "DC=";
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            if (!string.IsNullOrEmpty(domain))
            {
                int startindex = domain.IndexOf(DCToken) + DCToken.Length;
                int commaIndex = domain.IndexOf(",");
                if (startindex > 0 && commaIndex > startindex)
                {
                    ret = textInfo.ToTitleCase(domain.Substring(startindex, commaIndex - startindex) ?? string.Empty);
                }
                else
                {
                    ret = domain;
                }
            }
            return ret;
        }

        /// <summary>
        /// Fills the user properties.
        /// </summary>
        /// <param name="person">The person.</param>
        private void FillUserProperties(SearchResult person)
        {
            if (person == null)
                return;

            this.Title = GetResultProperty(person, "title") + ", " + GetResultProperty(person, "department");
            this.Alias = GetResultProperty(person, "mailnickname");
            //  this.Picture = GetResultProperty(person, "thumbnailphoto");
            this.Telephonenumber = GetResultProperty(person, "telephonenumber");
            this.FullName = GetResultProperty(person, "name");
            this.Culture = GetResultProperty(person, "msexchuserculture");
            this.Office = GetResultProperty(person, "physicaldeliveryofficename");
            this.SipAddress = GetResultProperty(person, "msrtcsip-primaryuseraddress");
            this.ManagerDN = GetResultProperty(person, "manager");
        }

        /// <summary>
        /// Gets the common Distribution lists.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="res">The resource.</param>
        /// <returns>
        /// A list populated with the names of the common distribution lists.
        /// </returns>
        private List<string> GetCommonDLs(ADUser user, SearchResult res)
        {
            List<string> resList = GetMultiResultProperty(res, "memberof");

            if (resList != null && resList.Count > 0)
            {
                var v = resList.OfType<string>();

                var commonDLs = Enumerable.Join(user.Memberships, resList, a => a, b => b, (a, b) => a);
                return commonDLs.ToList();
            }
            else
                return null;
        }

        private static SearchResult FindEmployeeByDistinguishedName(string name, string[] additionalFields)
        {
            ADUser.ds.Filter = "(&(distinguishedname=" + name /*EscapeLdap(name)*/ + ")(objectcategory=Person))";
            ADUser.ds.PropertiesToLoad.Clear();
            ADUser.ds.PropertiesToLoad.AddRange(ADUser.ADProperties);

            if (additionalFields != null && additionalFields.Length > 0)
            {
                ADUser.ds.PropertiesToLoad.AddRange(additionalFields);
            }

            return ADUser.ds.FindOne();
        }

        private SearchResult FindOriginByAlias(string alias)
        {
            if (String.IsNullOrEmpty(alias))
                return null;

            ADUser.ds.Filter = "(&(mailnickname=" + alias /*EscapeLdap(alias)*/ + ")(objectcategory=Person))";
            ADUser.ds.PropertiesToLoad.Clear();
            ADUser.ds.PropertiesToLoad.AddRange(ADUser.ADProperties);
            ADUser.ds.PropertiesToLoad.AddRange(ADUser.ADOriginProperties);
            SearchResult sr;
            try
            {
                sr = ADUser.ds.FindOne();
            }
            catch (Exception)
            {
                sr = null;
            }
            return sr;
        }

        private List<string> GetMultiResultProperty(SearchResult res, string prop)
        {
            ResultPropertyValueCollection resList = res.Properties[prop];
            if (resList != null && resList.Count > 0)
            {
                Regex rx = new Regex(@"^CN=(.*?)(?<!\\),OU=[Distribution|User].*$");
                List<string> propList = new List<string>(resList.Count);
                foreach (var p in resList)
                {
                    Match m = rx.Match(p as string);
                    if (m.Groups.Count > 1)
                    {
                        string s = m.Groups[1].Value;
                        propList.Add(s);
                    }
                }
                return propList;
            }
            else
                return null;
        }

        private static string GetResultProperty(SearchResult res, string prop)
        {
            ResultPropertyValueCollection resList = res.Properties[prop];
            if (resList != null && resList.Count > 0)
            {
                return (string)res.Properties[prop][0];
            }

            return null;
        }

        private static string EscapeLdap(string text)
        {
            StringBuilder output = new StringBuilder((int)(text.Length * 1.5));     // 1.5 is pretty much a guess... just somewhat bigger than the existing string.  Not empirically tested.

            foreach (char ch in text)
            {
                switch (ch)
                {
                    case '*':
                        output.Append("\\2a");
                        break;
                    case '(':
                        output.Append("\\28");
                        break;
                    case ')':
                        output.Append("\\29");
                        break;
                    case '\\':
                        output.Append("\\5c");
                        break;
                    default:
                        output.Append(ch);
                        break;
                }
            }

            return output.ToString();
        }

        #endregion
    }
}
