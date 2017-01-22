using System.Collections.Generic;
using SharpBucket.V1.Pocos;
using System.Dynamic;

namespace SharpBucket.V1.EndPoints{
    /// <summary>
    /// This is a resource for posting, deleting, and listing out comments on a Pull Request.
    /// </summary>
    public class PullRequestsResource
    {
        private readonly RepositoriesEndPoint _repositoriesEndPoint;

        public PullRequestsResource(RepositoriesEndPoint repositoriesEndPoint){
            _repositoriesEndPoint = repositoriesEndPoint;
        }

        public PullRequestComment PostPullRequestComment(PullRequestComment comment, int pullRequestId)
        {
            return _repositoriesEndPoint.PostPullRequestComment(comment, pullRequestId);
        }

    }
}