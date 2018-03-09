using System;
namespace TurtleHub
{
    public static class IssueTrackerFactory
    {
        public static IIssueTracker CreateIssueTracker(Parameters parameters)
        {
            return new GitHubIssueTracker(parameters);
        }
    }
}
