using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitVersionInfo.Model
{
    public class Version
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int GitRevCount { get; set; }
    }
}
