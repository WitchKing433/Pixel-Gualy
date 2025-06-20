using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;


namespace GualyCore
{
    public class ProgramState
    {
        static Color[] knownColors = Enum.GetValues(typeof(KnownColor))
                                      .Cast<KnownColor>()
                                      .Select(Color.FromKnownColor)
                                      .ToArray();
        static readonly Dictionary<int, KnownColor> argbToKnown = BuildLookup();
        public int index;
        public Canvas canvas;
        public int brushSize;
        public BrushShape brushShape;
        public Color brushColor;
        public (int, int) wallePosition;
        public Dictionary<string, int> labels;
        public Dictionary<string, object> variables;

        static Dictionary<int, KnownColor> BuildLookup()
        {
            var d = new Dictionary<int, KnownColor>();
            foreach (KnownColor kc in Enum.GetValues(typeof(KnownColor)))
            {
                Color c = Color.FromKnownColor(kc);
                int argb = c.ToArgb();
                if (!d.ContainsKey(argb))
                    d[argb] = kc;
            }
            return d;
        }
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

        public string CreateImage(string outputPath, string fileName)
        {
            string fullPath = Path.Combine(outputPath, fileName+ ".png");
            var image = new Bitmap(canvas.Width, canvas.Height);

            for (int y = 0; y < canvas.Height; y++)
            {
                for (int x = 0; x < canvas.Width; x++)
                {
                    image.SetPixel(x, y, canvas[x, y]);
                }
            }

            image.Save(fullPath,ImageFormat.Png);
            return fullPath;
        }

        public static string ImageToCode(string path)
        {
            using var original = new Bitmap(path);
            const int MAX = 200;
            Bitmap bmp = (original.Width > MAX || original.Height > MAX)
                ? new Bitmap(MAX, MAX, PixelFormat.Format32bppArgb)
                : new Bitmap(original);

            if (bmp != original)
            {
                using var g = Graphics.FromImage(bmp);
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.SmoothingMode = SmoothingMode.None;
                g.DrawImage(original, 0, 0, bmp.Width, bmp.Height);
            }

            int size = Math.Min(bmp.Width, bmp.Height);
            var rect = new Rectangle(0, 0, size, size);
            var bd = bmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int stride = Math.Abs(bd.Stride);
            int totalBytes = stride * size;
            byte[] pixels = new byte[totalBytes];
            Marshal.Copy(bd.Scan0, pixels, 0, totalBytes);
            bmp.UnlockBits(bd);

            var sb = new StringBuilder(size * size / 2 + 64);
            sb.AppendLine("Spawn(0,0)");

            string prevColorName = null;
            int xDir = 1;

            for (int y = 0; y < size; y++)
            {
                if (y > 0)
                {
                    sb.AppendLine("DrawLine(0,1,1)");
                    xDir = -xDir;
                }

                int xStart = xDir > 0 ? 0 : size - 1;
                for (int i = 0, x = xStart; i < size; i++, x += xDir)
                {
                    int off = y * stride + x * 4;
                    byte b = pixels[off + 0];
                    byte g = pixels[off + 1];
                    byte r = pixels[off + 2];
                    var c = Color.FromArgb(r, g, b);

                    string name;
                    if (argbToKnown.TryGetValue(c.ToArgb(), out var kc))
                        name = kc.ToString();
                    else
                        name = GetNearestKnownColor(c).ToString();

                    if (name != prevColorName)
                    {
                        sb.AppendLine($@"Color(""{name}"")");
                        prevColorName = name;
                    }

                    sb.AppendLine($"DrawLine({xDir},0,1)");
                }
            }

            return sb.ToString();
        }

        public static bool IsKnownColor(Color color)
        {
            return argbToKnown.ContainsKey(color.ToArgb());
        }
        
        static KnownColor GetNearestKnownColor(Color input)
        {
            Color closest = knownColors
                .OrderBy(c => DistanceSquared(c, input))
                .First();

            return argbToKnown[closest.ToArgb()];
        }

        static int DistanceSquared(Color c1, Color c2)
        {
            int dr = c1.R - c2.R;
            int dg = c1.G - c2.G;
            int db = c1.B - c2.B;
            return dr * dr + dg * dg + db * db;
        }

    }
    public enum BrushShape
    {
        Square,
        Circle
    }
}
