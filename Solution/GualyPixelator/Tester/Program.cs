using Lexer;
using GualyCore;
using Parser;
using System.Drawing;
using Interpreter;
namespace Tester
{
    public class Program
    {
        static void Main(string[] args)
        {
            Interpreter.Interpreter interpreter = new Interpreter.Interpreter("Spawn(0, 0)\nColor(\"Transparent\")\nSize(1)\nDrawLine(1, 0, 500)\nDrawLine(0, 1, 500)\ncanvas_size <- GetCanvasSize()\nmesa_ancho <- canvas_size - (canvas_size % 10)\nmesa_alto <- canvas_size / 5\nColor(\"White\")\nDrawRectangle(0, 0, 0, canvas_size, canvas_size)\nFill()\nColor(\"Orange\")\nDrawRectangle(0, 1, mesa_alto, mesa_ancho, mesa_alto)\nFill()\npersonas <- 12\nespaciado <- canvas_size / personas\nradio_cabeza <- 31\nColor(\"Yellow\")\ni <- 0\nloop_cabezas\nDrawCircle(0, 0, radio_cabeza)\nFill()\ni <- i + 1\nGoTo [loop_cabezas] (i < personas)\nColor(\"Blue\")\nventana_ancho <- canvas_size / 2\nventana_alto <- canvas_size / 10\nDrawRectangle(0, 0, 0, ventana_ancho, ventana_alto)\nFill()\nColor(\"Red\")\nvelas <- 5\ndistancia_velas <- canvas_size / velas\ntamano_flama <- canvas_size / 50\nj <- 0\nloop_velas\nDrawRectangle(0, 1, tamano_flama, distancia_velas, tamano_flama)\nFill()\nj <- j + 1\nGoTo [loop_velas] (j < velas)\n", 1000);
            if (interpreter.PaintAll())
                Console.WriteLine("Siiiiiiiiii");
            else
                Console.WriteLine("Noooooooooo");

        }
    }

}
