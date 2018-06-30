﻿using NUnit.Framework;
using SharpBucket.V2;
using SharpBucket.V2.EndPoints;
using Shouldly;

namespace SharBucketTests.V2.EndPoints
{
    [TestFixture]
    class TeamsEndPointTests
    {
        private SharpBucketV2 sharpBucket;
        private TeamsEndPoint teamsEndPoint;
        private const string TEAM_NAME = "atlassian";

        [SetUp]
        public void Init()
        {
            sharpBucket = TestHelpers.GetV2ClientAuthenticatedWithOAuth();
            teamsEndPoint = sharpBucket.TeamsEndPoint();
        }

        [Test]
        public void GetProfile_FromTeamAtlassian_ReturnsAtlassianProfile()
        {
            teamsEndPoint.ShouldNotBe(null);
            var profile = teamsEndPoint.GetProfile(TEAM_NAME);
            profile.display_name.ShouldBe("Atlassian");
        }

        [Test]
        public void ListMembers_FromTeamAtlassian_ShouldReturnManyMembers()
        {
            teamsEndPoint.ShouldNotBe(null);
            var members = teamsEndPoint.ListMembers(TEAM_NAME, 35);
            members.Count.ShouldBeGreaterThan(19);
            // This test is brittle, it should be updated since the names change
            members[0].display_name.ShouldBe("Ivan Ostafiychuk");
        }

        [Test]
        public void ListFollowers_FromTeamAtlassian_ShouldReturnManyFollowers()
        {
            teamsEndPoint.ShouldNotBe(null);
            var followers = teamsEndPoint.ListFollowers(TEAM_NAME, 8);
            followers.Count.ShouldBe(8);
            followers[0].display_name.ShouldBe("Hector Miuler Malpica Gallegos");
        }

        [Test]
        public void GetTeams_FromLoggedUser_ShouldReturnManyTeams()
        {
            teamsEndPoint.ShouldNotBe(null);
            var teams = teamsEndPoint.GetUserTeams();
            teams.Count.ShouldBeGreaterThan(0);
        }
    }
}