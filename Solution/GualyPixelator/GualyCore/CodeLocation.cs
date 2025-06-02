using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GualyCore
{
    public class CodeLocation
    {
        public CodeLocation(int line, int begin,int end)
        {
            Line = line;
            Begin = begin;
            End = end;
        }

        public int Line {  get; set; }
        public int Begin {  get; set; }
        public int End {  get; set; }
    }
}
