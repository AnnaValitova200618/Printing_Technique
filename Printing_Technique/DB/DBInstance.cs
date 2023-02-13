using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printing_Technique.DB
{
    internal static class DBInstance
    {
        static user02Context instance;
        public static user02Context GetInstance()
        {
            if (instance == null)
                instance = new user02Context();
            return instance;
        }
    }
}
