using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJom
{
    class Limit
    {
        public void Upper(int cap, ref int num)
        {
            if (num > cap)
            {
                num = cap;
            }
        }
        public void Lower(int limit, ref int num)
        {
            if (num < limit)
            {
                num = limit;
            }
        }
    }
}
