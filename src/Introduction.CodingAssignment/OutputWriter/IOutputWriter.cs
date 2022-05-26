using Introduction.CodingAssignment.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.CodingAssignment.OutputWriter
{
    /// <summary>
    /// Provides for implementations to build the output and write it
    /// The Interface allows for asynchronous processing
    /// </summary>
    public interface IOutputWriter
    {
        Task<StringBuilder> BuildOutputAsync(User user, IEnumerable<Tweet> tweets);
        Task WriteAsync(StringBuilder builder);
    }
}
