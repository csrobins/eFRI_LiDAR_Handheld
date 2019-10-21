using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteSample
{
    public class Utils
    {
        public Guid getGUID()
        {
            // Create and display the value of two GUIDs.
            return Guid.NewGuid();
        }
    }
}
