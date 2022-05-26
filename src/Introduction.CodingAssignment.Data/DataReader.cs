using Introduction.CodingAssignment.Data.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talent.Caching;

namespace Introduction.CodingAssignment.Data
{
    public class DataReader : IDataReader
    {
        private const string tweetsPath = "tweet.txt";
        private const string usersPath = "user.txt";

        private async Task<string> ReadDataAsync(string path)
        {
            // Reads the assignment data 
            // Gets / sets the value from a sliding cache with a 5 min expiry,
            // I figure we dont need more than 5 min to run all the test etc.
            Console.WriteLine($"Reading {path}");

            return await DomainCache.FromCache_Sliding_5Min_Async(
                path, () => File.ReadAllTextAsync(path));
        }

        private async Task<string> ReadTweetFileAsync(string? path = null)
        {
            // Read Tweets 
            return await ReadDataAsync(path??tweetsPath);
        }

        private async Task<string> ReadUserFileAsync(string? path = null)
        {
            // Read Users 
            return await ReadDataAsync(path??usersPath);
        }


        public async Task<IEnumerable<User>> GetUsersAsync(string? path = null)
        {
            // get user file from the cache
            var usersFileData = await ReadUserFileAsync(path);

            //separate the lines and removed white space
            var lines = usersFileData
                .Split(Environment.NewLine)
                .Where(x => !String.IsNullOrWhiteSpace(x))
                .ToList(); // Can call cool linq queries on a List

            // new up a thread safe collection
            var bag = new ConcurrentBag<User>();

            // iterate and populate add to the collection
            lines.ForEach(async (line) =>
            {

                var user = line
                    .Split("follows")[0]
                    .Trim();

                var followers = line
                    .Split("follows")[1]
                    .Split(",")
                    .Select(x => x.Trim())
                    .ToList();

                // add or updadate users that have followers
                await AddUserWithFollowersAsync(bag, user, followers);

            });
            // Add users that dont have any followers
            lines.ForEach(async (line) =>
            {
                var user = line
                    .Split("follows")[0]
                    .Trim();

                var followers = line
                    .Split("follows")[1]
                    .Split(",")
                    .Select(x => x.Trim())
                    .ToList();

                // Add followers as users if we dont have them already
                await AddFollowersAsUsersAsync(bag, followers);

            });

            return bag;
        }

        /// <summary>
        /// Gets a Tweet model 
        /// </summary>
        /// <returns>a Tweet Model with the text and the user that posted the tweet</returns>
        public async Task<IEnumerable<Tweet>> GetTweetsAsync(string? path = null)
        {
            // read the file from cache 
            string tweetFileData = await ReadTweetFileAsync(path);

            // separate the lines and remove white space 
            var lines = tweetFileData
                .Split(Environment.NewLine)
                .Where(x => !String.IsNullOrWhiteSpace(x))
                .ToList();

            // new up a thread safe collection
            var bag = new ConcurrentBag<Tweet>();

            // Loop through the lines and build
            lines.ForEach( line  =>
            { // Alan | Ward | Alan 
                var findUser = line
                    .Split(">")[0]
                    .Trim();

                var findTweet = line
                    .Split(">")[1].Trim();

                bag.Add(new Tweet
                {
                    UserName = findUser,
                    UserTweet = findTweet,
                });
            });
            return bag;
        }

        /// <summary>
        /// Helper method that Adds Any followers that are not already a user in the collection
        /// </summary>
        /// <param name="list">The User COllction</param>
        /// <param name="user"></param>
        /// <param name="followers"></param>
        /// <returns></returns>
        private async Task AddFollowersAsUsersAsync(ConcurrentBag<User> list, List<string> followers)
        {
            foreach (var follower in followers)
            {
                if (!list.Any(x => x.UserName.Equals(follower, StringComparison.OrdinalIgnoreCase)))
                {
                    list.Add(new User
                    {
                        UserName = follower,
                        UserFollows = new List<string>(),
                    });
                }
            }
        }

        private async Task AddUserWithFollowersAsync(ConcurrentBag<User> bag, string user, List<string> findFollows)
        {
            if (bag.Any(x => x.UserName.Equals(user, StringComparison.OrdinalIgnoreCase)))
            {
                var existingUser = bag
                .SingleOrDefault(x => x.UserName.Equals(user, StringComparison.OrdinalIgnoreCase));
                foreach (var item in findFollows)
                {
                    if (!existingUser.UserFollows.Contains(item))
                    {
                        existingUser.UserFollows.Add(item);
                    }
                }

                bag.Remove(bag.SingleOrDefault(x => x.UserName.Equals(existingUser.UserName)));

                bag.Add(existingUser);
            }
            else
            {
                bag.Add(new User
                {
                    UserName = user,
                    UserFollows = findFollows,
                });
            }
        }
    }
}
