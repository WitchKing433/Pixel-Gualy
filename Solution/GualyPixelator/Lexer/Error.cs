using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer
{
    public class Error
    {
        public string Text { get; private set; }
        public (int,int) Location { get; private set; }                 // (Row, Column)

        public Error(string text, (int,int) location) 
        {
            Text = text;
            Location = location;
        }

        public override string ToString() 
        {
            return $"{Location.Item1}, {Location.Item2} - {Text}";
        }
    }
}
