using Introduction.CodingAssignment.Data;
using Introduction.CodingAssignment.OutputWriter;
using System.Text;

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

            var users = (await _dataReader.GetUsersAsync(userFile));
            
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
