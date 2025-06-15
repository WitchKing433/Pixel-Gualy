using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GualyCore
{
    public class FillInstruction : Instruction
    {
        public FillInstruction(CodeLocation location, List<Expression> parameters) : base(location, parameters)
        {
        }

        public override void Execute(ProgramState programState)
        {
            Color brushColor = programState.brushColor;
            int x = programState.wallePosition.Item1;
            int y = programState.wallePosition.Item2;
            if (!programState.IsBrushColor("Transparent"))
            {
                Fill(x,y, programState.canvas[x,y], programState);
            }
        }
        //void Fill(int x, int y, Color colorToFill, ProgramState programState)
        //{
        //    (int, int)[] directions = { (0, -1), (-1, 0), (0, 1), (1, 0) };
        //    programState.canvas[x,y] = programState.brushColor;

        //    for (int i = 0; i < directions.Length; i++)
        //    {
        //        int newX = x + directions[i].Item1;
        //        int newY = y + directions[i].Item2;
        //        if (ValidToFill(newX, newY, colorToFill, programState))
        //        {
        //            Fill(newX,newY, colorToFill, programState);
        //        }
        //    }
        //}
        void Fill(int startX, int startY, Color colorToFill, ProgramState programState)
        {
            Stack<(int, int)> stack = new Stack<(int, int)>();
            stack.Push((startX, startY));

            Canvas canvas = programState.canvas;
            Color brushColor = programState.brushColor;
            int width = canvas.Width;
            int height = canvas.Height;
            int targetColor = colorToFill.ToArgb();

            while (stack.Count > 0)
            {
                var (x, y) = stack.Pop();

                if (!ValidToFill(x, y, targetColor, programState))
                    continue;

                canvas[x, y] = brushColor;

                stack.Push((x, y - 1));
                stack.Push((x - 1, y));
                stack.Push((x, y + 1));
                stack.Push((x + 1, y));
            }
        }
        bool ValidToFill(int x, int y, int colorToFill, ProgramState programState)
        {
            Canvas canvas = programState.canvas;
            int rgbCanvasColor = canvas[x, y].ToArgb();
            return x >= 0 && y >= 0 && x < canvas.Width && y < canvas.Height && rgbCanvasColor == colorToFill;
        }
    }
}
