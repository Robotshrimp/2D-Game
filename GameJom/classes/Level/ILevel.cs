using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJom.classes.Level
{
    interface ILevel
    {
        void Update();
        void Draw();
        void Save();
    }
}
