using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;


namespace GualyCore
{
    public class ProgramState
    {
        public int index;
        public Canvas canvas;
        public int brushSize;
        public BrushShape brushShape;
        public Color brushColor;
        public (int, int) wallePosition;
        public Dictionary<string, int> labels;
        public Dictionary<string, object> variables;

        public ProgramState((int, int) canvasSize, Dictionary<string, int> labels)
        {
            index = 0;
            brushSize = 1;
            brushShape = BrushShape.Circle;
            wallePosition = (0, 0);
            this.labels = labels;
            variables = new Dictionary<string, object> ();
            canvas = new Canvas(canvasSize.Item1, canvasSize.Item2);
            brushColor = Color.Transparent;
        }
        public void Draw()
        {
            if (!IsBrushColor("Transparent"))
            {
                int x = wallePosition.Item1;
                int y = wallePosition.Item2;
                if (IsInRange())
                {
                    if (brushSize == 1)
                        canvas[x, y] = brushColor;
                    else
                    {
                        int i = x - brushSize / 2;
                        int j = y - brushSize / 2;
                        if (i < 0) i = 0;
                        if (j < 0) j = 0;
                        switch (brushShape) 
                        {
                            case BrushShape.Circle:
                                DrawCircle(i, j); break;
                            case BrushShape.Square:
                                DrawSquare(i, j); break;
                            default: break;
                        }
                    }
                }
            }
        }
        void DrawSquare(int i, int j)
        {
            for (int k = 0; k < brushSize && i + k < canvas.Width; k++)
            {
                for (int l = 0; l < brushSize && j + l < canvas.Height; l++)
                {
                    canvas[i + k, j + l] = brushColor;
                }
            }
        }
        
        void DrawCircle(int i, int j)
        {
            int radiusSquared = (brushSize / 2) * (brushSize / 2);
            for (int k = 0; k < brushSize && i + k < canvas.Width; k++)
            {
                for (int l = 0; l < brushSize && j + l < canvas.Height; l++)
                {
                    int dx = k - brushSize / 2;
                    int dy = l - brushSize / 2;
                    if (dx * dx + dy * dy <= radiusSquared)
                    {
                        canvas[i + k, j + l] = brushColor;
                    }
                }
            }
        }
        public void DrawAtPosition(int x, int y)
        {
            (int originalX, int originalY) = wallePosition;
            wallePosition = (x, y);
            Draw();
            wallePosition = (originalX, originalY);
        }
        public bool IsInRange(int varX = 0, int varY = 0)
        {
            int x = wallePosition.Item1 + varX;
            int y = wallePosition.Item2 + varY;
            return x >= 0 && y >= 0 && x < canvas.Width && y < canvas.Height;
        }
        public bool IsBrushColor(string name)
        {
            return EqualColor(name, brushColor);
        }
        public bool EqualColor(string name, Color color)
        {
            Color color1 = Color.FromName(name);
            return color1.IsKnownColor && color1.ToArgb() == color.ToArgb();
        }

        public void CreateImage()
        {
            string folderPath = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = Path.Combine(folderPath, "Canvas.png");
            var image = new Bitmap(canvas.Width, canvas.Height);

            for (int y = 0; y < canvas.Height; y++)
            {
                for (int x = 0; x < canvas.Width; x++)
                {
                    image.SetPixel(x, y, canvas[x, y]);
                }
            }

            image.Save(fullPath,ImageFormat.Png);
            //Console.WriteLine(fullPath);
        }
       
    }
    public enum BrushShape
    {
        Square,
        Circle
    }
}
