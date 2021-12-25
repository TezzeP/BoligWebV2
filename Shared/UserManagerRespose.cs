using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class UserManagerRespose
    {
        public string Message { get; set; }
        public bool IsSucccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
