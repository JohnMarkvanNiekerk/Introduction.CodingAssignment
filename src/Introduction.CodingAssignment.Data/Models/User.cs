using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.CodingAssignment.Data.Models
{
    public class User
    {
        public string UserName { get; set; }
        public List<string> UserFollows { get; set; }
    }
}
