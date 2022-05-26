using Introduction.CodingAssignment.Data;
using Introduction.CodingAssignment.OutputWriter;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.CodingAssignment
{
    public class Processor : IProcessor
    {
        private readonly IDataReader _dataReader;
        private readonly IOutputWriter _outputWriter;

        public Processor(
            IDataReader dataReader,
            IOutputWriter outputWriter)
        {
            _dataReader = dataReader ?? throw new ArgumentNullException(nameof(dataReader));
            _outputWriter = outputWriter ?? throw new ArgumentNullException(nameof(outputWriter));
        }

        public async Task ProcessAsync(string? userFile = null, string? tweetFile = null)
        {

            var users = (await _dataReader.GetUsersAsync(userFile))
                .OrderBy(x => x.UserName)
                .Distinct();
            
            var tweets = (await _dataReader.GetTweetsAsync(tweetFile));

            var builder = new StringBuilder();
            foreach (var user in users)
            {
                builder = await _outputWriter.BuildOutputAsync(user, tweets);
            }

            await _outputWriter.WriteAsync(builder);
        }

    }
}
