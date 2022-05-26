using Introduction.CodingAssignment.Data;
using Introduction.CodingAssignment.Data.Models;
using Introduction.CodingAssignment.OutputWriter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Introduction.CodingAssignment.Tests
{
    [TestClass]
    public class ProcessorTests
    {
        private IDataReader _dataReader;
        private IOutputWriter _writer;


        [TestInitialize]
        public async Task Initialize()
        {
            var reader = new Mock<IDataReader>();
            var writer = new Mock<IOutputWriter>();

            reader.Setup(x => x.GetUsersAsync(It.IsAny<string?>()))
                .Returns<string?>((file) =>
                {
                    var item = new List<User>
                    {
                            new User
                            {
                                UserFollows = new List<string> { "Harry" },
                                UserName = "Simon"
                            },
                            new User
                            {
                                UserFollows = new List<string> { "Simon", "Bill" },
                                UserName = "Harry"
                            },
                            new User
                            {
                                UserFollows = new List<string>(),
                                UserName = "Bill"
                            }
                    };
                    return Task.FromResult(item as IEnumerable<User>);
                });
            reader.Setup(x => x.GetTweetsAsync(It.IsAny<string?>()))
                .Returns<string?>((file) =>
                {
                    var item = new List<Tweet>
                    {
                            new Tweet
                            {
                                UserTweet = "Man I hate mondays",
                                UserName = "Simon"
                            },

                            new Tweet
                            {

                                UserTweet = "Always look on the bright side of life!",
                                UserName = "Bill"
                            },
                            new Tweet
                            {
                                UserTweet = "Zen is in the weekend..",
                                UserName = "Simon"
                            },

                    };
                    return Task.FromResult(item as IEnumerable<Tweet>);
                });
            _dataReader = reader.Object;
            _writer = writer.Object;
        }

        [TestMethod]
        public async Task ProcessModel_FixedDataTestAsync()
        {

            var processor = new Processor(_dataReader, _writer);
            try
            {
                await processor.ProcessAsync();
                Assert.IsTrue(true);

            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Assert.IsTrue(false);
            }
        }
    }
}