using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class Label : ILineNode
    {
        private string labelName;
        private int codeLine;
        public string LabelName { get { return labelName; } set { labelName = value; } } 

        public Label(string name, int i)
        {
            labelName = name;
            codeLine = i;
        }
    }
}
