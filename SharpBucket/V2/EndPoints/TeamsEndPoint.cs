using System.Collections.Generic;
using System.Dynamic;
using SharpBucket.V2.Pocos;
using System.Linq;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// The Teams End Point returns all the teams that the authenticated user is associated with.
    /// More info:
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams
    /// </summary>
    public class TeamsEndPoint : EndPoint
    {
        public TeamsEndPoint(SharpBucketV2 sharpBucketV2)
            : base(sharpBucketV2, "teams/")
        {
        }

        #region /teams/ endpoint

        /// <summary>
        /// Returns a list of all the teams which the caller is a member of at least one team group or repository owned by the team.
        /// </summary>
        public List<Team> GetUserTeams(int max = 0)
        {
            dynamic parameters = new ExpandoObject();
            parameters.role = "member";
            return GetPaginatedValues<Team>(_baseUrl, max, parameters);
        }

        /// <summary>
        /// Returns a list of teams which the caller has write access to at least one repository owned by the team. This includes teams for which
        /// the user has administrator access.
        /// </summary>
        public List<Team> ListTeams(int max = 0)
        {
            var roleAdminTeams = new List<Team>();
            var allUniqueTeams = new List<Team>();

            dynamic parameters1 = new ExpandoObject();
            parameters1.role = "admin";
            roleAdminTeams.AddRange(GetPaginatedValues<Team>(_baseUrl, max, parameters1));

            dynamic parameters2 = new ExpandoObject();
            parameters2.role = "contributor";

            allUniqueTeams.AddRange(GetPaginatedValues<Team>(_baseUrl, max, parameters2));
            foreach (var ao in roleAdminTeams)
            {
                if (!allUniqueTeams.Any(r => r.username == ao.username))
                {
                    allUniqueTeams.Add(ao);
                }
            }

            return allUniqueTeams;
        }

        /// <summary>
        /// Returns a list teams which the caller has team administrator access
        /// </summary>
        public List<Team> ListAdminTeams(int max = 0)
        {
            dynamic parameters = new ExpandoObject();
            parameters.role = "admin";
            return GetPaginatedValues<Team>(_baseUrl, max, parameters);
        }

        #endregion

        #region /teams/{username} endpoint

        /// <summary>
        /// Returns the public information associated with a team.
        /// If the team's profile is private, location, website and created_on elements are omitted.
        /// </summary>
        /// <param name="username">The team's identifier.</param>
        public Team GetProfile(string username)
        {
            var overrideUrl = _baseUrl + username;
            return _sharpBucketV2.Get(new Team(), overrideUrl);
        }

        /// <summary>
        /// Returns all members of the specified team. Any member of any of the team's groups is considered a member of the team. This includes users 
        /// in groups that may not actually have access to any of the team's repositories.
        /// </summary>
        /// <param name="username">The team's identifier.</param>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        public List<Team> ListMembers(string username, int max = 0)
        {
            var overrideUrl = _baseUrl + username + "/members";
            return GetPaginatedValues<Team>(overrideUrl, max);
        }

        /// <summary>
        /// Returns the list of accounts that are following this team.
        /// </summary>
        /// <param name="username">The team's identifier.</param>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        public List<Team> ListFollowers(string username, int max = 0)
        {
            var overrideUrl = _baseUrl + username + "/followers";
            return GetPaginatedValues<Team>(overrideUrl, max);
        }

        /// <summary>
        /// Returns the list of accounts this team is following.
        /// </summary>
        /// <param name="username">The team's identifier.</param>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        public List<Team> ListFollowing(string username, int max = 0)
        {
            var overrideUrl = _baseUrl + username + "/following";
            return GetPaginatedValues<Team>(overrideUrl, max);
        }

        /// <summary>
        /// Returns an object for each repository permission for all of a team’s repositories.
        /// Permissions returned are effective permissions — the highest level of permission the user has.This does not include public repositories that 
        /// users are not granted any specific permission in, and does not distinguish between direct and indirect privileges.
        /// Permissions can be 'admin', 'write' or 'read'
        /// Only users with admin permission for the team may access this resource.
        /// </summary>
        /// <remarks>More info: https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D/permissions/repositories </remarks>
        /// <param name="username">The team's identifier.</param>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        public List<RepositoryPermission> ListPermissionsForRepositories(string username, string repositoryName = null, int max = 0)
        {
            var overrideUrl = _baseUrl + username + "/permissions/repositories";

            dynamic parameters = new ExpandoObject();
            if (!string.IsNullOrWhiteSpace(repositoryName))
            {
                parameters.q = "repository.name=\"" + repositoryName + "\"";
            }

            return GetPaginatedValues<RepositoryPermission>(overrideUrl, max, parameters);
        }

        /// <summary>
        /// Returns an object for each team permission a user on the team has.
        /// Permissions returned are effective permissions — if a user is a member of multiple groups with distinct roles, only the highest level is returned.
        /// Permissions can be 'admin' or 'collaborator'
        /// Only users with admin permission for the team may access this resource.
        /// </summary>
        /// <remarks>More info: https://developer.atlassian.com/bitbucket/api/2/reference/resource/teams/%7Busername%7D/permissions </remarks>
        /// <param name="username">The team's identifier.</param>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        public List<TeamPermission> ListPermissionsForTeams(string username, int max = 0)
        {
            var overrideUrl = _baseUrl + username + "/permissions";
            return GetPaginatedValues<TeamPermission>(overrideUrl, max);
        }

        /// <summary>
        /// Returns all repositories owned by a user/team. This includes private repositories, but filtered down to the ones that the calling user has access to.
        /// </summary>
        /// <param name="username">The team's identifier.</param>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        public List<Repository> ListRepositories(string username, int max = 0)
        {
            var overrideUrl = _baseUrl + username + "/repositories";
            return GetPaginatedValues<Repository>(overrideUrl, max);
        }

        #endregion
    }
}