// This file is part of TurtleLab.
// 
// Copyright (C)2018 Justin Dailey <dail8859@yahoo.com>
// 
// TurtleLab is free software; you can redistribute it and/or
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

using GitLabApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace TurtleHub
{
    [ComVisible(true)]
#if WIN64
    [Guid("2F6F1277-D268-4AA5-8C6E-42B41914726E")]
#else
    [Guid("2F6F1277-D268-4AA5-8C6E-42B41914726F")]
#endif

    public static class IssueTrackerFactory
    {
        public static IIssueTracker CreateIssueTracker(Parameters parameters)
        {
            return new GitLabIssueTracker(parameters);
        }
    }

    public class GitLabIssueTracker : IIssueTracker
    {
        Parameters parameters;
        GitLabClient client;

        public GitLabIssueTracker(Parameters parameters)
        {
            this.parameters = parameters;
            client = new GitLabClient(parameters.Tracker, parameters.APIToken);
        }

        public async Task<IReadOnlyList<TurtleIssue>> GetAllIssuesOnRepository()
        {
            var issues = new List<TurtleIssue>();
            int projectIdNumber = 0;
            string projectId = String.Format("{0}/{1}", parameters.Owner, parameters.Repository);
            try
            {
                // Try to find the internal id because getting issues with project name group/project looks bugged
                var projects = await client.Projects.GetAsync(o => o.UserId = parameters.Owner);
                var project = projects.Where(p => p.Name == parameters.Repository).FirstOrDefault();
                if (project != null)
                {
                    projectIdNumber = project.Id;
                    projectId = project.Id.ToString();
                }
            }
            catch(GitLabException)
            { }

            var glissues = await client.Issues.GetAsync(projectId);
            foreach (var glissue in glissues)
            {
                issues.Add(CreateTurtleIssue(glissue));
            }

            if (projectIdNumber > 0)
            {
                var glprs = await client.MergeRequests.GetAsync(projectIdNumber);
                foreach (var glpr in glprs)
                {
                    issues.Add(CreateTurtleIssue(glpr));
                }
            }
            return issues;
        }

        public async Task<IReadOnlyList<string>> GetAllRepositories()
        {
            var repos = new List<string>();
            var projects = await client.Projects.GetAsync();
            foreach(var project in projects)
            {
                repos.Add(project.Name);
            }
            return repos;
        }

        public Task<RepositoryMetrics> GetRepositoryMetrics()
        {
            return null;
        }

        private TurtleIssue CreateTurtleIssue(GitLabApiClient.Models.Issues.Responses.Issue issue)
        {
            return new TurtleIssue()
            {
                Number = issue.Id,
                Title = issue.Title,
                Creator = issue.Author?.Username,
                Assignee = issue.Assignee?.Username,
                IsPullRequest = false,
                HtmlUrl = issue.WebUrl
            };
        }

        private TurtleIssue CreateTurtleIssue(GitLabApiClient.Models.MergeRequests.Responses.MergeRequest pr)
        {
            return new TurtleIssue()
            {
                Number = pr.Id,
                Title = pr.Title,
                Creator = pr.Author?.Username,
                Assignee = pr.Assignee?.Username,
                IsPullRequest = true,
                HtmlUrl = pr.WebUrl
            };
        }
    }
}
