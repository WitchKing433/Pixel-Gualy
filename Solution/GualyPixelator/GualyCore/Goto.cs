using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class Goto : ILineNode, IExecutable
    {
        string destinLabel;

        public Goto(string a) 
        {
            destinLabel = a;
        }
        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
