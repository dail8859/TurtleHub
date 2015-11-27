using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleHub
{
    public class Parameters
    {
        public string Username { get; private set; }
        public string Repository { get; private set; }

        public Parameters(string parameters)
        {
            var ownerrepo = parameters.Split('/');
            if (ownerrepo.Length == 2)
            {
                this.Username = ownerrepo[0];
                this.Repository = ownerrepo[1];
            }
        }

        public Parameters(string username, string repository)
        {
            this.Username = username;
            this.Repository = repository;
        }

        public override string ToString()
        {
            // NOTE: make sure this string is formatted in a way that is acceptable to the constructor
            return Username + "/" + Repository;
        }
    }
}
