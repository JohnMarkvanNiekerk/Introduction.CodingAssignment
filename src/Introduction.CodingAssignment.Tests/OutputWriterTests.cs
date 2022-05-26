using Introduction.CodingAssignment.Data;
using Introduction.CodingAssignment.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Introduction.CodingAssignment.Tests
{
    [TestClass]
    [DeploymentItem(@"TestData\tweet.txt", "TestData")]
    [DeploymentItem(@"TestData\user.txt", "TestData")]
    [DeploymentItem(@"TestData\Output1.txt", "TestData")]
    [DeploymentItem(@"TestData\Output2.txt", "TestData")]
    [DeploymentItem(@"TestData\Output3.txt", "TestData")]
    public class OutputWriterTests
    {
        private IEnumerable<User>? _users;
        private IEnumerable<Tweet>? _tweets;
        private string? _output1;
        private string? _output2;
        private string? _output3;

        [TestInitialize]
        public async Task Initailse()
        {
            var reader = new DataReader();
            _users = await reader.GetUsersAsync();
            _tweets = await reader.GetTweetsAsync();
            _output1 = await File.ReadAllTextAsync(@"TestData\Output1.txt");
            _output2 = await File.ReadAllTextAsync(@"TestData\Output2.txt");
            _output3 = await File.ReadAllTextAsync(@"TestData\Output3.txt");

        }

        [TestMethod]
        public async Task CheckOutputs()
        {
            var writer = new OutputWriter.OutputWriter();
            for (int i = 0; i < _users.Count(); i++)
            {
                // Make sure the users are in aplphabetical order
                switch (i)
                {
                    case 0:
                        //Arrange 
                        var output1 = (await writer.BuildOutputAsync(_users.ToArray()[i], _tweets)).ToString();
                        //Assert
                        Assert.AreEqual(_output1, output1.ToString());
                        Assert.IsTrue(_users.ToArray()[i].UserName.Equals("Alan"));
                        break;
                    case 1:
                        // Arrange
                        var output2 = (await writer.BuildOutputAsync(_users.ToArray()[i], _tweets)).ToString();
                        Assert.AreEqual(_output2, output2.ToString());
                        Assert.IsTrue(_users.ToArray()[i].UserName.Equals("Martin"));
                        break;
                    case 2:
                        // Arrange
                        var output3 = (await writer.BuildOutputAsync(_users.ToArray()[i], _tweets)).ToString();
                        Assert.AreEqual(_output3, output3.ToString());
                        Assert.IsTrue(_users.ToArray()[i].UserName.Equals("Ward"));
                        break;
                    default:
                        throw new DataMisalignedException("");
                }
            }

        }
    }
}