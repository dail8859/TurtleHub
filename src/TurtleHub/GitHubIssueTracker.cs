using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleHub
{
    public class GitHubIssueTracker : IIssueTracker
    {
        Parameters parameters;
        GitHubClient client;

        public GitHubIssueTracker(Parameters parameters)
        {
            this.parameters = parameters;
            client = new GitHubClient(new ProductHeaderValue("TurtleHub"));
        }

        public async Task<IReadOnlyList<string>> GetAllRepositories()
        {
            var ghrepos = await client.Repository.GetAllForUser(parameters.Owner);
            var repos = new List<string>();
            foreach (var ghrepo in ghrepos)
            {
                repos.Add(ghrepo.Name);
            }
            return repos;
        }

        public async Task<IReadOnlyList<TurtleIssue>> GetAllIssuesOnRepository()
        {
            var issues = new List<TurtleIssue>();
            var pagingOptions = new ApiOptions
            {
                PageSize = 50,
                StartPage = 1,
                PageCount = 1
            };
            MiscellaneousRateLimit ratelimit;

            AssignCredentials();

#if DEBUG
            // The logging normally takes care of this but ifdef'ing this out keeps it from doing an unneeded rate limit check
            ratelimit = await client.Miscellaneous.GetRateLimits();
            Logger.LogMessage(string.Format("\tRate limit: {0}/{1}", ratelimit.Resources.Core.Remaining.ToString(), ratelimit.Resources.Core.Limit.ToString()));
#endif

            do
            {
                IReadOnlyCollection<Issue> ghissues = await client.Issue.GetAllForRepository(parameters.Owner, parameters.Repository, pagingOptions);
                Logger.LogMessage("\tGot " + ghissues.Count().ToString() + " issues");

                if (ghissues.Count() == 0)
                    break;

                foreach (var ghissue in ghissues)
                {
                    issues.Add(CreateTurtleIsssue(ghissue));
                }

                // Move to the next page
                pagingOptions.StartPage += 1;
            } while (true);

#if DEBUG
            ratelimit = await client.Miscellaneous.GetRateLimits();
            Logger.LogMessage(string.Format("\tRate limit: {0}/{1}", ratelimit.Resources.Core.Remaining.ToString(), ratelimit.Resources.Core.Limit.ToString()));
#endif

            return issues;
        }

        public async Task<RepositoryMetrics> GetRepositoryMetrics()
        {
            var repository = await client.Repository.Get(parameters.Owner, parameters.Repository);
            return new RepositoryMetrics()
            {
                HasIssues = repository.HasIssues,
                OpenIssues = repository.OpenIssuesCount
            };
        }

        private void AssignCredentials()
        {
            string token = parameters.APIToken;
            if (String.IsNullOrEmpty(token))
            {
                token = Utilities.GetStoredAPIToken(client.BaseAddress.AbsoluteUri); ;
            }
            
            if (!String.IsNullOrEmpty(token))
            {
                client.Credentials = new Credentials(token);
#if DEBUG
                // Make sure the API token is valid
                if (Utilities.CheckCurrentCredentials(client) == false)
                    throw new Exception("API Token is not valid");
                else
                    Logger.LogMessage("API Token is valid");
#endif
            }
            // else just use unauthenticated requests
        }

        protected TurtleIssue CreateTurtleIsssue(Issue issue)
        {
            return new TurtleIssue()
            {
                Number = issue.Number,
                Title = issue.Title,
                Creator = issue.User?.Login,
                Assignee = issue.Assignee?.Login,
                IsPullRequest = issue.PullRequest != null,
                HtmlUrl = issue.HtmlUrl
            };
        }
    }
}
