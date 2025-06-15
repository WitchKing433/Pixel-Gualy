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
            if (!programState.IsBrushColor("Transparent") && brushColor.ToArgb() != programState.canvas[x, y].ToArgb())
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
        //void Fill(int startX, int startY, Color colorToFill, ProgramState programState)
        //{
        //    Stack<(int, int)> stack = new Stack<(int, int)>();
        //    stack.Push((startX, startY));

        //    Canvas canvas = programState.canvas;
        //    Color brushColor = programState.brushColor;
        //    int width = canvas.Width;
        //    int height = canvas.Height;
        //    int targetColor = colorToFill.ToArgb();

        //    while (stack.Count > 0)
        //    {
        //        var (x, y) = stack.Pop();

        //        if (!ValidToFill(x, y, targetColor, programState))
        //            continue;

        //        canvas[x, y] = brushColor;

        //        if (ValidToFill(x, y - 1, targetColor, programState)) stack.Push((x, y - 1));
        //        if (ValidToFill(x - 1, y, targetColor, programState)) stack.Push((x - 1, y));
        //        if (ValidToFill(x, y + 1, targetColor, programState)) stack.Push((x, y + 1));
        //        if (ValidToFill(x + 1, y, targetColor, programState)) stack.Push((x + 1, y));
        //    }
        //}
        //bool ValidToFill(int x, int y, int colorToFill, ProgramState programState)
        //{
        //    Canvas canvas = programState.canvas;
        //    if (x >= 0 && y >= 0 && x < canvas.Width && y < canvas.Height)
        //    {
        //        int rgbCanvasColor = canvas[x, y].ToArgb();
        //        return rgbCanvasColor == colorToFill;
        //    }
        //    return false;
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

                if (ValidToFill(x, y - 1, targetColor, programState)) 
                    stack.Push((x, y - 1));
                if (ValidToFill(x - 1, y, targetColor, programState)) 
                    stack.Push((x - 1, y));
                if (ValidToFill(x, y + 1, targetColor, programState)) 
                    stack.Push((x, y + 1));
                if (ValidToFill(x + 1, y, targetColor, programState)) 
                    stack.Push((x + 1, y)); 
            }
        }

        bool ValidToFill(int x, int y, int targetColor, ProgramState programState)
        {
            Canvas canvas = programState.canvas;

            if (x < 0 || y < 0 || x >= canvas.Width || y >= canvas.Height)
                return false;

            return canvas[x, y].ToArgb() == targetColor;
        }


    }
}
