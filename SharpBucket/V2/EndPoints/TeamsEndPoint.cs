using System.Collections.Generic;
using System.Dynamic;
using SharpBucket.V2.Pocos;
using System.Linq;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// The Teams End Point gets a team's profile information.
    /// More info:
    /// https://confluence.atlassian.com/display/BITBUCKET/teams+Endpoint
    /// </summary>
    public class TeamsEndPoint : EndPoint
    {
        public TeamsEndPoint(SharpBucketV2 sharpBucketV2, string teamName)
            : base(sharpBucketV2, "teams/" + teamName + "/")
        {
        }

        public List<Team> GetUserTeams(int max = 0)
        {
            dynamic parameters = new ExpandoObject();
            parameters.role = "member";
            return GetPaginatedValues<Team>("teams/", max, parameters);
            //return _sharpBucketV2.Get<List<Team>>(null, "teams/", parameters);
        }

        /// <summary>
        /// Get teams associated with a given user
        /// </summary>
        public List<Team> ListTeams(int max = 0)
        {
            var overrideUrl = "teams/";
            var roleAdminTeams = new List<Team>();
            var allUniqueTeams = new List<Team>();

            dynamic parameters1 = new ExpandoObject();
            parameters1.role = "admin";
            roleAdminTeams.AddRange(GetPaginatedValues<Team>(overrideUrl, max, parameters1));

            dynamic parameters2 = new ExpandoObject();
            parameters2.role = "contributor";

            allUniqueTeams.AddRange(GetPaginatedValues<Team>(overrideUrl, max, parameters2));
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
        /// Get all teams of which the authorized user is an admin
        /// </summary>
        public List<Team> ListAdminTeams(int max = 0)
        {
            var overrideUrl = "teams/";
            dynamic parameters = new ExpandoObject();
            parameters.role = "admin";
            return GetPaginatedValues<Team>(overrideUrl, max, parameters);
        }

        /// <summary>
        /// Gets the public information associated with a team. 
        /// If the team's profile is private, the caller must be authenticated and authorized to view this information. 
        /// </summary>
        /// <returns></returns>
        public Team GetProfile()
        {
            return _sharpBucketV2.Get(new Team(), _baseUrl);
        }

        /// <summary>
        /// Gets the team's members.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        public List<Team> ListMembers(int max = 0)
        {
            var overrideUrl = _baseUrl + "members/";
            return GetPaginatedValues<Team>(overrideUrl, max);
        }

        /// <summary>
        /// Gets the list of accounts following the team.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        public List<Team> ListFollowers(int max = 0)
        {
            var overrideUrl = _baseUrl + "followers/";
            return GetPaginatedValues<Team>(overrideUrl, max);
        }

        /// <summary>
        /// Gets a list of accounts the team is following.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        public List<Team> ListFollowing(int max = 0)
        {
            var overrideUrl = _baseUrl + "following/";
            return GetPaginatedValues<Team>(overrideUrl, max);
        }

        /// <summary>
        /// Gets the list of the team's repositories. 
        /// Private repositories only appear on this list if the caller is authenticated and is authorized to view the repository.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        public List<Repository> ListRepositories(int max = 0)
        {
            var overrideUrl = _baseUrl + "repositories/";
            return GetPaginatedValues<Repository>(overrideUrl, max);
        }
    }
}