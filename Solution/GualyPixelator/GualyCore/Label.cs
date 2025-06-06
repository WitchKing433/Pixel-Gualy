using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class Label : ILineNode, ILocatable
    {
        private string labelName;
        CodeLocation location;
        public string LabelName { get { return labelName; } private set { } } 

        public Label(string name, CodeLocation location)
        {
            labelName = name;
            this.location = location;
        }

        public CodeLocation GetLocation()
        {
            return location;
        }

        public ProgramState Execute(ProgramState programState)
        {
            return programState;
        }
    }
}
