using Introduction.CodingAssignment.Data.Models;

namespace Introduction.CodingAssignment.Data
{
    public interface IDataReader
    {
        Task<IEnumerable<Tweet>> GetTweetsAsync(string? path = null);
        Task<IEnumerable<User>> GetUsersAsync(string? path = null);
    }
}