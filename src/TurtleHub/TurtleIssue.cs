using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleHub
{
    public class TurtleIssue
    {
        public int Number { get; set; }

        public string Title { get; set; }

        public string Creator { get; set; }

        public string Assignee { get; set; }

        public string HtmlUrl { get; set; }

        public bool IsPullRequest { get; set; }
    }
}
