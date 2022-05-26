using Introduction.CodingAssignment.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Introduction.CodingAssignment.Tests
{
    [TestClass]
    [DeploymentItem(@"TestData\tweet.txt", "TestData")]
    [DeploymentItem(@"TestData\user.txt", "TestData")]

    public class DataReaderTests
    {
        private DataReader _dataReader;

        [TestInitialize]
        public async Task Initialize()
        {
            _dataReader = new DataReader();
        }

        [TestMethod]
        public async Task ReadUsersTest()
        {

            var users = await _dataReader.GetUsersAsync();

            var ward = users
                .Where(x => (x.UserName ?? "").Equals("Ward", System.StringComparison.OrdinalIgnoreCase));

            var wardFollows = ward.FirstOrDefault().UserFollows;

            var alan = users
                .Where(x => (x.UserName ?? "").Equals("Alan", System.StringComparison.OrdinalIgnoreCase));
            var alanFollows = alan.FirstOrDefault().UserFollows;


            var martin = users
                .Where(x => (x.UserName ?? "").Equals("Martin", System.StringComparison.OrdinalIgnoreCase));
            var martinFollows = martin.FirstOrDefault().UserFollows;

            // Assert
            // We should only have 3 users
            Assert.AreEqual(3, users.Count());

            // Of the 3 users we should only have the 1 of each
            Assert.AreEqual(1, alan.Count());
            Assert.AreEqual(1, ward.Count());
            Assert.AreEqual(1, martin.Count());

            // Make sure we have the right amount of followers
            Assert.AreEqual(2, wardFollows.Count);
            Assert.AreEqual(1, alanFollows.Count);
            Assert.AreEqual(0, martinFollows.Count);

            // Make sure the people followed are right 
            Assert.IsTrue(wardFollows.Contains("Alan"));
            Assert.IsTrue(wardFollows.Contains("Martin"));
            Assert.IsTrue(alanFollows.Contains("Martin"));


        }

        [TestMethod]
        public async Task ReadTweetsTest()
        {
            var tweets = await _dataReader.GetTweetsAsync();

            var alanTweets = tweets
                .Where(x => (x.UserName ?? "").Equals("Alan", System.StringComparison.OrdinalIgnoreCase));

            var wardTweets = tweets
                .Where(x => (x.UserName ?? "").Equals("Ward", System.StringComparison.OrdinalIgnoreCase));

            var martinTweets = tweets
            .Where(x => (x.UserName ?? "").Equals("Martin", System.StringComparison.OrdinalIgnoreCase));


            // Make sure we have the right number of tweets 
            Assert.AreEqual(3, tweets.Count());

            // Make sure that the tweets are linked to the right users
            Assert.AreEqual(2, alanTweets.Count());
            Assert.AreEqual(1, wardTweets.Count());
            Assert.IsFalse(martinTweets.Any());

        }

    }
}
