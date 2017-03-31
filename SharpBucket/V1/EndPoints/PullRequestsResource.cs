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
        
        public PullRequestComment DeletePullRequestComment(PullRequestComment comment, int pullRequestId)
        {
            return _repositoriesEndPoint.DeletePullRequestComment(comment, pullRequestId);
        }

        public PullRequestComment DeletePullRequestComment(int commentId, int pullRequestId)
        {
            return _repositoriesEndPoint.DeletePullRequestComment(commentId, pullRequestId);
        }
    }
}