
namespace Introduction.CodingAssignment
{
    public interface IProcessor
    {
        Task ProcessAsync(string? userFile = null, string? tweetFile = null);
    }
}