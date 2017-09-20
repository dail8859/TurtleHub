// This file is part of TurtleHub.
// 
// Copyright (C)2013 Justin Dailey <dail8859@yahoo.com>
// 
// TurtleHub is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either
// version 2 of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA.

using System;
using System.Collections.Generic;
using System.Linq;

namespace TurtleHub
{
    public class Parameters : Dictionary<string, string>
    {
        public string Owner
        {
            get { return this.ContainsKey("owner") ? this["owner"] : ""; }
            set { this["owner"] = value; }
        }
        
        public string Repository
        {
            get { return this.ContainsKey("repository") ? this["repository"] : ""; }
            set { this["repository"] = value; }
        }

        public bool Debug
        {
            get { return this.ContainsKey("debug") ? Convert.ToBoolean(this["debug"]) : false; }
            set { this["debug"] = Convert.ToString(value); }
        }

        public string Keyword
        {
            get { return this.ContainsKey("keyword") ? this["keyword"] : "Closes"; }
            set { this["keyword"] = value; }
        }

        public bool RefFullRepo
        {
            get { return this.ContainsKey("reffullrepo") ? Convert.ToBoolean(this["reffullrepo"]) : false; }
            set { this["reffullrepo"] = Convert.ToString(value); }
        }

        public bool ShowPrsByDefault
        {
            get { return this.ContainsKey("showprsbydefault") ? Convert.ToBoolean(this["showprsbydefault"]) : false; }
            set { this["showprsbydefault"] = Convert.ToString(value); }
        }

        public Parameters() : base() {}
        public Parameters(string parameters) : base()
        {
            Dictionary<string, string> dict;

            try
            {
                dict = parameters.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(part => part.Split('='))
                    .ToDictionary(split => split[0], split => split[1]);
            }
            catch
            {
                throw new ArgumentException("Invalid parameters string.");
            }

            // Is there an easier way?
            foreach(var item in dict)
                this[item.Key] = item.Value;
        }

        private string CreateIssueReference(int issue)
        {
            if (RefFullRepo)
            {
                return String.Format("{0}/{1}#{2}", Owner, Repository, issue);
            }
            else
            {
                return String.Format("#{0}", issue);
            }
        }

        public string CreateReferenceMessage(IList<int> issues)
        {
            if (this.Keyword == "<None>" || this.Keyword.Length == 0)
            {
                return String.Join(", ", issues.Select(issue => CreateIssueReference(issue)));
            }
            else
            {
                return String.Join(", ", issues.Select(issue => String.Format("{0} {1}", this.Keyword, CreateIssueReference(issue))));
            }
        }

        public override string ToString()
        {
            // NOTE: make sure this string is formatted in a way that is acceptable to the constructor!
            return string.Join(";", this.Select(x => x.Key + "=" + x.Value));
        }
    }
}
