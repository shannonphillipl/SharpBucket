using SharpBucket.V2.Pocos;
using System.Collections.Generic;
using System.Dynamic;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// The User endpoint returns user information based on the credentials of the logged in user.
    /// More info:
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/user
    /// </summary>
    public class UserEndpoint : EndPoint
    {
        public UserEndpoint(SharpBucketV2 sharpBucketV2)
            : base(sharpBucketV2, "user/")
        {
        }

        /// <summary>
        /// Returns the currently logged in user.
        /// </summary>
        public User GetUser()
        {
            return _sharpBucketV2.Get<User>(null, _baseUrl);
        }

        /// <summary>
        /// Returns an object for each repository the caller has explicit access to and their effective permission — the highest level 
        /// of permission the caller has. This does not return public repositories that the user was not granted any specific permission 
        /// in, and does not distinguish between direct and indirect privileges.
        /// Permissions can be 'admin', 'write' or 'read'
        /// Only users with admin permission for the team may access this resource.
        /// </summary>
        /// <remarks>More info: https://developer.atlassian.com/bitbucket/api/2/reference/resource/user/permissions/repositories </remarks>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        public List<RepositoryPermission> ListPermissionsForRepositories(string repositoryName = null, int max = 0)
        {
            var overrideUrl = _baseUrl + "permissions/repositories";

            dynamic parameters = new ExpandoObject();
            if (!string.IsNullOrWhiteSpace(repositoryName))
            {
                parameters.q = "repository.name=\"" + repositoryName + "\"";
            }

            return GetPaginatedValues<RepositoryPermission>(overrideUrl, max, parameters);
        }

        /// <summary>
        /// Returns an object for each team the caller is a member of, and their effective role — the highest level of privilege the caller has. 
        /// If a user is a member of multiple groups with distinct roles, only the highest level is returned
        /// Permissions can be 'admin' or 'collaborator'
        /// </summary>
        /// <remarks>More info: https://developer.atlassian.com/bitbucket/api/2/reference/resource/user/permissions/teams </remarks>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        public List<TeamPermission> ListPermissionsForTeams(int max = 0)
        {
            var overrideUrl = _baseUrl + "permissions/teams";
            return GetPaginatedValues<TeamPermission>(overrideUrl, max);
        }
    }
}