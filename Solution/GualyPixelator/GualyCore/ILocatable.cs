using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public interface ILocatable
    {
        public CodeLocation GetLocation();
    }
}
