using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleHub
{
    public interface IIssueTracker
    {
        Task<IReadOnlyList<string>> GetAllRepositories();

        Task<IReadOnlyList<TurtleIssue>> GetAllIssuesOnRepository();

        Task<RepositoryMetrics> GetRepositoryMetrics();
    }
}
