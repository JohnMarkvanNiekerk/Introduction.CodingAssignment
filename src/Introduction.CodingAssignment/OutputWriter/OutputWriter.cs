using Introduction.CodingAssignment.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.CodingAssignment.OutputWriter
{
    public class OutputWriter : IOutputWriter
    {
        readonly StringBuilder _builder;

        public OutputWriter()
        {
            _builder = new StringBuilder();
        }

        public async Task<StringBuilder> BuildOutputAsync(User user, IEnumerable<Tweet> tweets)
        {
            SanitiseInputs(user, tweets);

            _builder.AppendLine(user.UserName);
            foreach (var tweet in tweets)
            {
                if ((tweet.UserName ?? "").Equals(user.UserName, StringComparison.OrdinalIgnoreCase)
                    || user.UserFollows.Contains(tweet.UserName))
                {
                    _builder.AppendLine($"\t@{tweet.UserName}: {tweet.UserTweet}");
                }
            }
            return _builder;
        }

        public async Task WriteAsync(StringBuilder builder)
        {
            Console.WriteLine(builder.ToString());
            Console.WriteLine("Press ");
        }

        private void SanitiseInputs(User user, IEnumerable<Tweet> tweets)
        {
            _= user ?? throw new ArgumentException(nameof(user));
            if (String.IsNullOrWhiteSpace(user.UserName))
                throw new ArgumentException($"{nameof(user.UserName)} All users should have a user name");
            
            foreach (var tweet in tweets)
            {
                _ = tweet ?? throw new ArgumentException(nameof(tweet));
                if (String.IsNullOrWhiteSpace(user.UserName))
                    throw new ArgumentException($"{nameof(tweet.UserName)} All tweets should have a valid user name");
            }
        }
    }
}
