using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.CompilerServices;
using System.Drawing.Drawing2D;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using static ServerDrawer.Form1;
using Decoder;
using System.ComponentModel.Design;
using ServerLib;

namespace ServerDrawer
{

    public partial class Form1 : Form
    {
        //Variables
        const int port = 1984;
        public static int rotation;
        public static int width = 1000, height = 1000;
        //Classes
        public ServerProgram program = new ServerProgram();
        //Figure class to that stores information about figures to draw
        public class Figure
        {
            public string Name, text;
            public short x1, y1, x2, y2, rad, size;
            public byte[] RGB;
            public System.Drawing.Image Image;
            public Figure(string _name, short _x1, short _y1, byte[] _RGB)
            {
                Name = _name;
                this.x1 = _x1;
                this.y1 = _y1;
                RGB = _RGB;
            }

            public Figure(string _name, short _x1, short _y1, short _x2, short _y2, byte[] _RGB)
                : this(_name, _x1, _y1, _RGB)
            {
                this.x2 = _x2;
                this.y2 = _y2;
            }

            public Figure(string _name, short _x1, short _y1, short _x2, byte[] _RGB)
                : this(_name, _x1, _y1, _RGB)
            {
                this.x2 = _x2;
            }

            public Figure(string _name, short _x1, short _y1, short _x2, short _y2, short _rad, byte[] _RGB)
    : this(_name, _x1, _y1, _RGB)
            {
                this.x2 = _x2;
                this.y2 = _y2;
                this.rad = _rad;
            }

            public Figure(string _name, short _x1, short _y1, short _size, string _text, byte[] _RGB)
                    : this(_name, _x1, _y1, _RGB)
            {
                this.text = _text;
                this.size = _size;
            }

            public Figure(string _name, short _x1, short _y1, System.Drawing.Image _image)
            {
                Name = _name;
                this.x1 = _x1;
                this.y1 = _y1;
                this.Image = _image;
            }




        }
        //TextAsSymbols class that breaks down string messages into symbols and graphically draws them
        public class TextAsSymbols
        {

            public int size, x0, y0;
            public float[,] coords;
            public byte[] RGB;
            public List<float[,]> symbols = new List<float[,]>();

            public TextAsSymbols(int _x0, int _y0, int _size, Byte[] _RGB)
            {
                this.x0 = _x0;
                this.y0 = _y0;
                this.RGB = _RGB;
                this.size = _size;

            }


            public void SymbolA(int _x0, int _y0)
            {
                coords = new float[3, 4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0;
                coords[0, 2] = (_x0 + (1 * size));
                coords[0, 3] = (_y0 - (2 * size));
                coords[1, 0] = coords[0, 2];
                coords[1, 1] = coords[0, 3];
                coords[1, 2] = (_x0 + (2 * size));
                coords[1, 3] = _y0;
                coords[2, 0] = ((_x0 + coords[1, 0]) / 2);
                coords[2, 1] = ((_y0 + coords[1, 1]) / 2);
                coords[2, 2] = ((coords[1, 0] + coords[1, 2]) / 2);
                coords[2, 3] = ((coords[1, 1] + coords[1, 3]) / 2);
                symbols.Add(coords);
            }

            public void SymbolB(int _x0, int _y0)
            {
                coords = new float[10, 4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0;
                coords[0, 2] = _x0;
                coords[0, 3] = _y0 - size * 2;
                coords[1, 0] = _x0;
                coords[1, 1] = _y0;
                coords[1, 2] = _x0 + size * 1.75f;
                coords[1, 3] = _y0;
                coords[2, 0] = _x0;
                coords[2, 1] = _y0 - size;
                coords[2, 2] = _x0 + size * 1.75f;
                coords[2, 3] = _y0 - size;
                coords[3, 0] = _x0;
                coords[3, 1] = _y0 - size * 2;
                coords[3, 2] = _x0 + size * 1.75f;
                coords[3, 3] = _y0 - size * 2;
                coords[4, 0] = coords[1, 2];
                coords[4, 1] = coords[1, 3];
                coords[4, 2] = _x0 + size * 2;
                coords[4, 3] = _y0 - size * 0.25f;
                coords[5, 0] = coords[2, 2];
                coords[5, 1] = coords[2, 3];
                coords[5, 2] = _x0 + size * 2;
                coords[5, 3] = _y0 - size * 0.75f;
                coords[6, 0] = coords[5, 2];
                coords[6, 1] = coords[5, 3];
                coords[6, 2] = coords[4, 2];
                coords[6, 3] = coords[4, 3];
                coords[7, 0] = coords[3, 2];
                coords[7, 1] = coords[3, 3];
                coords[7, 2] = _x0 + size * 2;
                coords[7, 3] = _y0 - size * 1.75f;
                coords[8, 0] = coords[2, 2];
                coords[8, 1] = coords[2, 3];
                coords[8, 2] = _x0 + size * 2;
                coords[8, 3] = _y0 - size * 1.25f;
                coords[9, 0] = coords[8, 2];
                coords[9, 1] = coords[8, 3];
                coords[9, 2] = coords[7, 2];
                coords[9, 3] = coords[7, 3];
                symbols.Add(coords);
            }

            public void SymbolC(int _x0, int _y0)
            {
                coords = new float[8, 4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0 - (size * 0.33f);
                coords[0, 2] = _x0;
                coords[0, 3] = _y0 - size * 1.66f;
                coords[1, 0] = coords[0, 0];
                coords[1, 1] = coords[0, 1];
                coords[1, 2] = coords[0, 0] + (size * 0.33f);
                coords[1, 3] = _y0;
                coords[2, 0] = coords[1, 2];
                coords[2, 1] = coords[1, 3];
                coords[2, 2] = _x0 + size * 1.66f;
                coords[2, 3] = _y0;
                coords[3, 0] = coords[0, 2];
                coords[3, 1] = coords[0, 3];
                coords[3, 2] = coords[1, 2];
                coords[3, 3] = _y0 - (size * 2f);
                coords[4, 0] = coords[3, 2];
                coords[4, 1] = coords[3, 3];
                coords[4, 2] = coords[2, 2];
                coords[4, 3] = coords[4, 1];
                coords[5, 0] = coords[4, 2];
                coords[5, 1] = coords[4, 3];
                coords[5, 2] = coords[5, 0] + (size * 0.33f);
                coords[5, 3] = coords[0, 3];
                coords[6, 0] = coords[2, 2];
                coords[6, 1] = coords[2, 3];
                coords[6, 2] = coords[6, 0] + (size * 0.33f);
                coords[6, 3] = coords[0, 1];
                symbols.Add(coords);
            }
            public void SymbolD(int _x0, int _y0)
            {
                coords = new float[6, 4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0;
                coords[0, 2] = _x0;
                coords[0, 3] = _y0 - (2 * size);
                coords[1, 0] = _x0;
                coords[1, 1] = coords[0, 3];
                coords[1, 2] = _x0 + size;
                coords[1, 3] = coords[0, 3];
                coords[2, 0] = coords[1, 2];
                coords[2, 1] = coords[1, 3];
                coords[2, 2] = coords[1, 2] + size;
                coords[2, 3] = coords[1, 3] + (size * 0.33f);
                coords[3, 0] = _x0;
                coords[3, 1] = _y0;
                coords[3, 2] = coords[1, 2];
                coords[3, 3] = coords[3, 1];
                coords[4, 0] = coords[3, 2];
                coords[4, 1] = coords[3, 3];
                coords[4, 2] = coords[3, 2] + size;
                coords[4, 3] = coords[3, 3] - (size * 0.33f);
                coords[5, 0] = coords[4, 2];
                coords[5, 1] = coords[4, 3];
                coords[5, 2] = coords[2, 2];
                coords[5, 3] = coords[2, 3];

                symbols.Add(coords);
            }

            public void SymbolE(int _x0, int _y0)
            {
                coords = new float[4, 4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0;
                coords[0, 2] = _x0 + (size * 2);
                coords[0, 3] = _y0;
                coords[1, 0] = coords[0, 0];
                coords[1, 1] = coords[0, 1];
                coords[1, 2] = coords[0, 0];
                coords[1, 3] = coords[0, 1] - (size * 2);
                coords[2, 0] = coords[0, 0];
                coords[2, 1] = coords[1, 3];
                coords[2, 2] = coords[0, 2];
                coords[2, 3] = coords[1, 3];
                coords[3, 0] = coords[0, 0];
                coords[3, 1] = coords[0, 1] - size;
                coords[3, 2] = coords[0, 0] + size;
                coords[3, 3] = coords[3, 1];
                symbols.Add(coords);
            }
            public void SymbolF(int _x0, int _y0)
            {
                coords = new float[3, 4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0;
                coords[0, 2] = coords[0, 0];
                coords[0, 3] = coords[0, 1] - (size * 2);
                coords[1, 0] = coords[0, 0];
                coords[1, 1] = coords[0, 3];
                coords[1, 2] = coords[0, 0] + (size * 2);
                coords[1, 3] = coords[0, 3];
                coords[2, 0] = coords[0, 0];
                coords[2, 1] = coords[0, 1] - size;
                coords[2, 2] = coords[0, 0] + size;
                coords[2, 3] = coords[2, 1];
                symbols.Add(coords);
            }
            public void SymbolG(int _x0, int _y0)
            {
                coords = new float[9, 4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0 - (size * 0.33f);
                coords[0, 2] = _x0;
                coords[0, 3] = _y0 - size * 1.66f;
                coords[1, 0] = coords[0, 0];
                coords[1, 1] = coords[0, 1];
                coords[1, 2] = coords[0, 0] + (size * 0.33f);
                coords[1, 3] = _y0;
                coords[2, 0] = coords[1, 2];
                coords[2, 1] = coords[1, 3];
                coords[2, 2] = _x0 + size * 1.66f;
                coords[2, 3] = _y0;
                coords[3, 0] = coords[0, 2];
                coords[3, 1] = coords[0, 3];
                coords[3, 2] = coords[1, 2];
                coords[3, 3] = _y0 - (size * 2f);
                coords[4, 0] = coords[3, 2];
                coords[4, 1] = coords[3, 3];
                coords[4, 2] = coords[2, 2];
                coords[4, 3] = coords[4, 1];
                coords[5, 0] = coords[4, 2];
                coords[5, 1] = coords[4, 3];
                coords[5, 2] = coords[5, 0] + (size * 0.33f);
                coords[5, 3] = coords[0, 3];
                coords[6, 0] = coords[2, 2];
                coords[6, 1] = coords[2, 3];
                coords[6, 2] = coords[6, 0] + (size * 0.33f);
                coords[6, 3] = coords[0, 1];
                coords[7, 0] = coords[6, 2];
                coords[7, 1] = coords[6, 3];
                coords[7, 2] = coords[7, 0];
                coords[7, 3] = coords[7, 1] - (size * 0.5f);
                coords[8, 0] = coords[7, 2];
                coords[8, 1] = coords[7, 3];
                coords[8, 2] = coords[8, 0] - size;
                coords[8, 3] = coords[8, 1];
                symbols.Add(coords);
            }
            public void SymbolH(int _x0, int _y0)
            {
                coords = new float[3, 4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0;
                coords[0, 2] = _x0;
                coords[0, 3] = _y0 - size * 2;
                coords[1, 0] = _x0 + size * 2;
                coords[1, 1] = _y0;
                coords[1, 2] = coords[1, 0];
                coords[1, 3] = coords[0, 3];
                coords[2, 0] = _x0;
                coords[2, 1] = _y0 - size;
                coords[2, 2] = coords[1, 0];
                coords[2, 3] = coords[2, 1];
                symbols.Add(coords);
            }
            public void SymbolI(int _x0, int _y0)
            {
                coords = new float[3, 4];
                coords[0, 0] = _x0 + size;
                coords[0, 1] = _y0;
                coords[0, 2] = coords[0, 0];
                coords[0, 3] = _y0 - size * 2;
                coords[1, 0] = _x0;
                coords[1, 1] = coords[0, 3];
                coords[1, 2] = _x0 + size * 2;
                coords[1, 3] = coords[0, 3];
                coords[2, 0] = coords[1, 0];
                coords[2, 1] = _y0;
                coords[2, 2] = coords[1, 2];
                coords[2, 3] = _y0;
                symbols.Add(coords);
            }
            public void SymbolJ(int _x0, int _y0)
            {
                coords = new float[5, 4];
                coords[0, 0] = _x0 + size * 1.66f;
                coords[0, 1] = _y0 - size * 0.33f;
                coords[0, 2] = coords[0, 0];
                coords[0, 3] = _y0 - size * 2;
                coords[1, 0] = _x0;
                coords[1, 1] = coords[0, 3];
                coords[1, 2] = _x0 + size * 1.66f;
                coords[1, 3] = coords[0, 3];
                coords[2, 0] = coords[0, 0];
                coords[2, 1] = coords[0, 1];
                coords[2, 2] = coords[2, 0] - size * 0.5f;
                coords[2, 3] = _y0;
                coords[3, 0] = coords[2, 2];
                coords[3, 1] = coords[2, 3];
                coords[3, 2] = _x0 + size * 0.5f;
                coords[3, 3] = _y0;
                coords[4, 0] = coords[3, 2];
                coords[4, 1] = _y0;
                coords[4, 2] = _x0;
                coords[4, 3] = _y0 - size * 0.5f;

                symbols.Add(coords);
            }
            public void SymbolK(int _x0, int _y0)
            {
                coords = new float[3, 4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0;
                coords[0, 2] = _x0;
                coords[0, 3] = _y0 - size * 2;
                coords[1, 0] = _x0;
                coords[1, 1] = _y0 - size;
                coords[1, 2] = _x0 + size * 2;
                coords[1, 3] = _y0;
                coords[2, 0] = _x0;
                coords[2, 1] = coords[1, 1];
                coords[2, 2] = coords[1, 2];
                coords[2, 3] = y0 - size * 2;
                symbols.Add(coords);
            }
            public void SymbolL(int _x0, int _y0)
            {
                coords = new float[2, 4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0;
                coords[0, 2] = _x0 + size * 2;
                coords[0, 3] = _y0;
                coords[1, 0] = _x0;
                coords[1, 1] = _y0;
                coords[1, 2] = _x0;
                coords[1, 3] = _y0 - size * 2;
                symbols.Add(coords);
            }
            public void SymbolM(int _x0, int _y0)
            {
                coords = new float[4, 4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0;
                coords[0, 2] = _x0;
                coords[0, 3] = _y0 - size * 2;
                coords[1, 0] = _x0 + size * 2;
                coords[1, 1] = _y0;
                coords[1, 2] = coords[1, 0];
                coords[1, 3] = coords[0, 3];
                coords[2, 0] = _x0;
                coords[2, 1] = coords[0, 3];
                coords[2, 2] = _x0 + size;
                coords[2, 3] = _y0 - size;
                coords[3, 0] = coords[1, 0];
                coords[3, 1] = coords[0, 3];
                coords[3, 2] = coords[2, 2];
                coords[3, 3] = coords[2, 3];

                symbols.Add(coords);
            }
            public void SymbolN(int _x0, int _y0)
            {
                coords = new float[3, 4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0;
                coords[0, 2] = _x0;
                coords[0, 3] = _y0 - size * 2;
                coords[1, 0] = _x0 + size * 2;
                coords[1, 1] = _y0;
                coords[1, 2] = coords[1, 0];
                coords[1, 3] = coords[0, 3];
                coords[2, 0] = _x0;
                coords[2, 1] = coords[0, 3];
                coords[2, 2] = coords[1, 0];
                coords[2, 3] = _y0;

                symbols.Add(coords);
            }
            public void SymbolO(int _x0, int _y0)
            {
                coords = new float[8, 4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0 - (size * 0.33f);
                coords[0, 2] = _x0;
                coords[0, 3] = _y0 - size * 1.66f;
                coords[1, 0] = coords[0, 0];
                coords[1, 1] = coords[0, 1];
                coords[1, 2] = coords[0, 0] + (size * 0.33f);
                coords[1, 3] = _y0;
                coords[2, 0] = coords[1, 2];
                coords[2, 1] = coords[1, 3];
                coords[2, 2] = _x0 + size * 1.66f;
                coords[2, 3] = _y0;
                coords[3, 0] = coords[0, 2];
                coords[3, 1] = coords[0, 3];
                coords[3, 2] = coords[1, 2];
                coords[3, 3] = _y0 - (size * 2f);
                coords[4, 0] = coords[3, 2];
                coords[4, 1] = coords[3, 3];
                coords[4, 2] = coords[2, 2];
                coords[4, 3] = coords[4, 1];
                coords[5, 0] = coords[4, 2];
                coords[5, 1] = coords[4, 3];
                coords[5, 2] = _x0 + size * 2;
                coords[5, 3] = coords[0, 3];
                coords[6, 0] = coords[2, 2];
                coords[6, 1] = coords[2, 3];
                coords[6, 2] = coords[6, 0] + (size * 0.33f);
                coords[6, 3] = coords[0, 1];
                coords[7, 0] = _x0 + size * 2;
                coords[7, 1] = coords[0, 1];
                coords[7, 2] = coords[7, 0];
                coords[7, 3] = coords[0, 3];
                symbols.Add(coords);
            }
            public void SymbolP(int _x0, int _y0)
            {
                coords = new float[6, 4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0;
                coords[0, 2] = _x0;
                coords[0, 3] = _y0 - size * 2;
                coords[1, 0] = _x0;
                coords[1, 1] = coords[0, 3];
                coords[1, 2] = _x0 + size * 1.66f;
                coords[1, 3] = coords[0, 3];
                coords[2, 0] = _x0;
                coords[2, 1] = _y0 - size;
                coords[2, 2] = coords[1, 2];
                coords[2, 3] = coords[2, 1];
                coords[3, 0] = coords[1, 2];
                coords[3, 1] = coords[1, 3];
                coords[3, 2] = _x0 + size * 2;
                coords[3, 3] = _y0 - size * 1.67f;
                coords[4, 0] = coords[1, 2];
                coords[4, 1] = coords[2, 1];
                coords[4, 2] = _x0 + size * 2;
                coords[4, 3] = _y0 - size * 1.33f;
                coords[5, 0] = coords[4, 2];
                coords[5, 1] = coords[4, 3];
                coords[5, 2] = coords[3, 2];
                coords[5, 3] = coords[3, 3];

                symbols.Add(coords);
            }

            public void SymbolQ(int _x0, int _y0)
            {
                coords = new float[9, 4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0 - (size * 0.33f);
                coords[0, 2] = _x0;
                coords[0, 3] = _y0 - size * 1.66f;
                coords[1, 0] = coords[0, 0];
                coords[1, 1] = coords[0, 1];
                coords[1, 2] = coords[0, 0] + (size * 0.33f);
                coords[1, 3] = _y0;
                coords[2, 0] = coords[1, 2];
                coords[2, 1] = coords[1, 3];
                coords[2, 2] = _x0 + size * 1.66f;
                coords[2, 3] = _y0;
                coords[3, 0] = coords[0, 2];
                coords[3, 1] = coords[0, 3];
                coords[3, 2] = coords[1, 2];
                coords[3, 3] = _y0 - (size * 2f);
                coords[4, 0] = coords[3, 2];
                coords[4, 1] = coords[3, 3];
                coords[4, 2] = coords[2, 2];
                coords[4, 3] = coords[4, 1];
                coords[5, 0] = coords[4, 2];
                coords[5, 1] = coords[4, 3];
                coords[5, 2] = _x0 + size * 2;
                coords[5, 3] = coords[0, 3];
                coords[6, 0] = coords[2, 2];
                coords[6, 1] = coords[2, 3];
                coords[6, 2] = coords[6, 0] + (size * 0.33f);
                coords[6, 3] = coords[0, 1];
                coords[7, 0] = _x0 + size * 2;
                coords[7, 1] = coords[0, 1];
                coords[7, 2] = coords[7, 0];
                coords[7, 3] = coords[0, 3];
                coords[8, 0] = _x0 + size * 2;
                coords[8, 1] = _y0;
                coords[8, 2] = _x0 + size * 1.66f;
                coords[8, 3] = _y0 - size * 0.5f;
                symbols.Add(coords);
            }
            public void SymbolR(int _x0, int _y0)
            {
                coords = new float[7, 4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0;
                coords[0, 2] = _x0;
                coords[0, 3] = _y0 - size * 2;
                coords[1, 0] = _x0;
                coords[1, 1] = coords[0, 3];
                coords[1, 2] = _x0 + size * 1.66f;
                coords[1, 3] = coords[0, 3];
                coords[2, 0] = _x0;
                coords[2, 1] = _y0 - size;
                coords[2, 2] = coords[1, 2];
                coords[2, 3] = coords[2, 1];
                coords[3, 0] = coords[1, 2];
                coords[3, 1] = coords[1, 3];
                coords[3, 2] = _x0 + size * 2;
                coords[3, 3] = _y0 - size * 1.67f;
                coords[4, 0] = coords[1, 2];
                coords[4, 1] = coords[2, 1];
                coords[4, 2] = _x0 + size * 2;
                coords[4, 3] = _y0 - size * 1.33f;
                coords[5, 0] = coords[4, 2];
                coords[5, 1] = coords[4, 3];
                coords[5, 2] = coords[3, 2];
                coords[5, 3] = coords[3, 3];
                coords[6, 0] = coords[1, 2] - size * 0.66f;
                coords[6, 1] = coords[2, 1];
                coords[6, 2] = _x0 + size * 2;
                coords[6, 3] = _y0;

                symbols.Add(coords);
            }

            public void SymbolS(int _x0, int _y0)
            {
                coords = new float[11, 4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0 - size * 0.25f;
                coords[0, 2] = _x0 + size * 0.5f;
                coords[0, 3] = _y0;
                coords[1, 0] = coords[0, 2];
                coords[1, 1] = coords[0, 3];
                coords[1, 2] = _x0 + size * 1.5f;
                coords[1, 3] = _y0;
                coords[2, 0] = coords[1, 2];
                coords[2, 1] = _y0;
                coords[2, 2] = _x0 + size * 2;
                coords[2, 3] = coords[0, 1];
                coords[3, 0] = coords[2, 2];
                coords[3, 1] = coords[2, 3];
                coords[3, 2] = coords[2, 2];
                coords[3, 3] = _y0 - size * 0.75f;
                coords[4, 0] = coords[2, 2];
                coords[4, 1] = coords[3, 3];
                coords[4, 2] = coords[1, 2];
                coords[4, 3] = _y0 - size;

                coords[5, 0] = coords[0, 2];
                coords[5, 1] = _y0 - size;
                coords[5, 2] = coords[1, 2];
                coords[5, 3] = coords[5, 1];

                coords[6, 0] = coords[0, 2];
                coords[6, 1] = coords[5, 3];
                coords[6, 2] = _x0;
                coords[6, 3] = _y0 - size * 1.25f;
                coords[7, 0] = _x0;
                coords[7, 1] = _y0 - size * 1.25f;
                coords[7, 2] = _x0;
                coords[7, 3] = _y0 - size * 1.75f;

                coords[8, 0] = _x0;
                coords[8, 1] = coords[7, 3];
                coords[8, 2] = coords[0, 2];
                coords[8, 3] = _y0 - size * 2;

                coords[9, 0] = coords[8, 2];
                coords[9, 1] = coords[8, 3];
                coords[9, 2] = coords[1, 2];
                coords[9, 3] = coords[9, 1];

                coords[10, 0] = coords[9, 2];
                coords[10, 1] = coords[9, 3];
                coords[10, 2] = _x0 + size * 2;
                coords[10, 3] = coords[7, 3];


                symbols.Add(coords);
            }

            public void SymbolT(int _x0, int _y0)
            {
                coords = new float[2, 4];
                coords[0, 0] = _x0 + size;
                coords[0, 1] = _y0;
                coords[0, 2] = coords[0, 0];
                coords[0, 3] = _y0 - size * 2;
                coords[1, 0] = _x0;
                coords[1, 1] = coords[0, 3];
                coords[1, 2] = _x0 + size * 2;
                coords[1, 3] = coords[0, 3];
                symbols.Add(coords);
            }
            public void SymbolU(int _x0, int _y0)
            {
                coords = new float[5, 4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0 - (size * 0.33f);
                coords[0, 2] = _x0;
                coords[0, 3] = _y0 - size * 2;
                coords[1, 0] = coords[0, 0];
                coords[1, 1] = coords[0, 1];
                coords[1, 2] = coords[0, 0] + (size * 0.33f);
                coords[1, 3] = _y0;
                coords[2, 0] = coords[1, 2];
                coords[2, 1] = coords[1, 3];
                coords[2, 2] = _x0 + size * 1.66f;
                coords[2, 3] = _y0;

                coords[3, 0] = coords[2, 2];
                coords[3, 1] = coords[2, 3];
                coords[3, 2] = _x0 + size * 2;
                coords[3, 3] = coords[0, 1];
                coords[4, 0] = _x0 + size * 2;
                coords[4, 1] = coords[0, 1];
                coords[4, 2] = coords[4, 0];
                coords[4, 3] = _y0 - size * 2;
                symbols.Add(coords);
            }
            public void SymbolV(int _x0, int _y0)
            {
                coords = new float[2, 4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0 - size * 2;
                coords[0, 2] = _x0 + size;
                coords[0, 3] = _y0;
                coords[1, 0] = _x0 + size;
                coords[1, 1] = _y0;
                coords[1, 2] = _x0 + size * 2;
                coords[1, 3] = _y0 - size * 2;
                symbols.Add(coords);
            }
            public void SymbolW(int _x0, int _y0)
            {
                coords = new float[4, 4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0 - size * 2;
                coords[0, 2] = _x0 + size * 0.5f;
                coords[0, 3] = _y0;
                coords[1, 0] = coords[0, 2];
                coords[1, 1] = _y0;
                coords[1, 2] = _x0 + size;
                coords[1, 3] = _y0 - size;
                coords[2, 0] = coords[1, 2];
                coords[2, 1] = coords[1, 3];
                coords[2, 2] = _x0 + size * 1.5f;
                coords[2, 3] = _y0;
                coords[3, 0] = _x0 + size * 1.5f;
                coords[3, 1] = _y0;
                coords[3, 2] = _x0 + size * 2f;
                coords[3, 3] = _y0 - size * 2;
                symbols.Add(coords);
            }

            public void SymbolX(int _x0, int _y0)
            {
                coords = new float[2, 4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0 - size * 2;
                coords[0, 2] = _x0 + size * 2;
                coords[0, 3] = _y0;
                coords[1, 0] = _x0;
                coords[1, 1] = _y0;
                coords[1, 2] = _x0 + size * 2;
                coords[1, 3] = _y0 - size * 2;
                symbols.Add(coords);
            }
            public void SymbolY(int _x0, int _y0)
            {
                coords = new float[3, 4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0 - size * 2;
                coords[0, 2] = _x0 + size;
                coords[0, 3] = _y0 - size;
                coords[1, 0] = _x0 + size * 2;
                coords[1, 1] = _y0 - size * 2;
                coords[1, 2] = _x0 + size;
                coords[1, 3] = _y0 - size;
                coords[2, 0] = _x0 + size;
                coords[2, 1] = _y0 - size;
                coords[2, 2] = _x0 + size;
                coords[2, 3] = _y0;
                symbols.Add(coords);
            }
            public void SymbolZ(int _x0, int _y0)
            {
                coords = new float[3, 4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0 - size * 2;
                coords[0, 2] = _x0 + size * 2;
                coords[0, 3] = _y0 - size * 2;
                coords[1, 0] = coords[0, 2];
                coords[1, 1] = coords[0, 3];
                coords[1, 2] = _x0;
                coords[1, 3] = _y0;
                coords[2, 0] = _x0;
                coords[2, 1] = _y0;
                coords[2, 2] = _x0 + size * 2;
                symbols.Add(coords);
            }



            public void TextProcessor(string text)
            {
                foreach (char c in text)
                {
                    switch (c)
                    {
                        case 'A':
                        case 'a':
                            SymbolA(x0, y0);
                            break;
                        case 'B':
                        case 'b':
                            SymbolB(x0, y0);
                            break;
                        case 'C':
                        case 'c':
                            SymbolC(x0, y0);
                            break;
                        case 'D':
                        case 'd':
                            SymbolD(x0, y0);
                            break;
                        case 'E':
                        case 'e':
                            SymbolE(x0, y0);
                            break;
                        case 'F':
                        case 'f':
                            SymbolF(x0, y0);
                            break;
                        case 'G':
                        case 'g':
                            SymbolG(x0, y0);
                            break;
                        case 'H':
                        case 'h':
                            SymbolH(x0, y0);
                            break;
                        case 'I':
                        case 'i':
                            SymbolI(x0, y0);
                            break;
                        case 'J':
                        case 'j':
                            SymbolJ(x0, y0);
                            break;
                        case 'K':
                        case 'k':
                            SymbolK(x0, y0);
                            break;
                        case 'L':
                        case 'l':
                            SymbolL(x0, y0);
                            break;
                        case 'M':
                        case 'm':
                            SymbolM(x0, y0);
                            break;
                        case 'N':
                        case 'n':
                            SymbolN(x0, y0);
                            break;
                        case 'O':
                        case 'o':
                            SymbolO(x0, y0);
                            break;
                        case 'P':
                        case 'p':
                            SymbolP(x0, y0);
                            break;
                        case 'Q':
                        case 'q':
                            SymbolQ(x0, y0);
                            break;
                        case 'R':
                        case 'r':
                            SymbolR(x0, y0);
                            break;
                        case 'S':
                        case 's':
                            SymbolS(x0, y0);
                            break;
                        case 'T':
                        case 't':
                            SymbolT(x0, y0);
                            break;
                        case 'U':
                        case 'u':
                            SymbolU(x0, y0);
                            break;
                        case 'V':
                        case 'v':
                            SymbolV(x0, y0);
                            break;
                        case 'W':
                        case 'w':
                            SymbolW(x0, y0);
                            break;
                        case 'X':
                        case 'x':
                            SymbolX(x0, y0);
                            break;
                        case 'Y':
                        case 'y':
                            SymbolY(x0, y0);
                            break;
                        case 'Z':
                        case 'z':
                            SymbolZ(x0, y0);
                            break;

                        default:
                            break;

                    }
                    x0 += Convert.ToInt32(2 * size * 1.3f);
                }


            }


        }

        //List that stores figure objects
        public static List<Figure> Figures = new List<Figure>();
        //List that stores symbol texts
        public static List<TextAsSymbols> Texts = new List<TextAsSymbols>();

        //Drawing methods
        public static void SetScreenSize(MessageData command)
        {
            width = command.x0;
            height = command.y0;
        }

        public void DrawSymbol(MessageData command)
        {
            var SymbolLine = new TextAsSymbols(command.x0, command.y0, command.x1, command.RGB);
            SymbolLine.TextProcessor(command.text);
            Texts.Add(SymbolLine);
            Invalidate();
        }
        public void DrawPixel(MessageData command)
        {
            var Figure = new Figure("Pixel", command.x0, command.y0, command.RGB);
            Figures.Add(Figure);
            Invalidate();
        }

        public void DrawLine(MessageData command)
        {
            var Figure = new Figure("Line", command.x0, command.y0, command.x1, command.y1, command.RGB);
            Figures.Add(Figure);
            Invalidate();
        }

        public void DrawRectangle(MessageData command)
        {
            var Figure = new Figure("RectangleOutline", command.x0, command.y0, command.x1, command.y1, command.RGB);
            Figures.Add(Figure);
            Invalidate();
        }

        public void FillRectangle(MessageData command)
        {
            var Figure = new Figure("RectangleFilled", command.x0, command.y0, command.x1, command.y1, command.RGB);
            Figures.Add(Figure);
            Invalidate();
        }

        public void DrawEllipse(MessageData command)
        {
            var Figure = new Figure("EllipseOutline", command.x0, command.y0, command.x1, command.y1, command.RGB);
            Figures.Add(Figure);
            Invalidate();
        }

        public void FillEllipse(MessageData command)
        {
            var Figure = new Figure("EllipseFilled", command.x0, command.y0, command.x1, command.y1, command.RGB);
            Figures.Add(Figure);
            Invalidate();
        }

        public void DrawCircle(MessageData command)
        {
            var Figure = new Figure("CircleOutline", command.x0, command.y0, command.x1, command.RGB);
            Figures.Add(Figure);
            Invalidate();
        }

        public void FillCircle(MessageData command)
        {
            var Figure = new Figure("CircleFilled", command.x0, command.y0, command.x1, command.RGB);
            Figures.Add(Figure);
            Invalidate();
        }

        public void DrawRoundedRectangle(MessageData command)
        {
            var Figure = new Figure("RoundedRectangleOutline", command.x0, command.y0, command.x1, command.y1, command.rounding, command.RGB);
            Figures.Add(Figure);
            Invalidate();
        }

        public void FillRoundedRectangle(MessageData command)
        {
            var Figure = new Figure("RoundedRectangleFilled", command.x0, command.y0, command.x1, command.y1, command.rounding, command.RGB);
            Figures.Add(Figure);
            Invalidate();
        }
        public void DrawText(MessageData command)
        {
            var Figure = new Figure("Text", command.x0, command.y0, command.x1, command.text, command.RGB);
            Figures.Add(Figure);
            Invalidate();
        }

        public void DrawImage(MessageData command)
        {
            var Figure = new Figure("Image", command.x0, command.y0, command.img);
            Figures.Add(Figure);
            Invalidate();
        }

        public static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            path.AddArc(arc, 180, 90);

            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }

        public void ClearDisplay(MessageData md)
        {
            Figures.Clear();
            Texts.Clear();
            this.BackColor = Color.FromArgb(md.RGB[0], md.RGB[1], md.RGB[2]);
            Invalidate();
        }

        public void RotateImage(MessageData md)
        {
            rotation = (int)md.x0;
            Invalidate();
        }

        // End of Drawing methods

        //Method that draws all symbols and figures
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int xMiddle = this.Width / 2, yMiddle = this.Height / 2;
            Graphics g = e.Graphics;
            g.TranslateTransform(xMiddle, yMiddle);
            g.RotateTransform(rotation);
            g.TranslateTransform(-xMiddle, -yMiddle);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            if (Figures != null)
            {
                foreach (var Figure in Figures)
                {
                    if (Figure == null)
                    {
                        continue;
                    }
                    if (Figure.Name == "Pixel")
                    {
                        Brush brush = new SolidBrush(Color.FromArgb(Figure.RGB[0], Figure.RGB[1], Figure.RGB[2]));
                        g.FillRectangle(brush, Figure.x1 + this.Width / 2, Figure.y1 + yMiddle, 1, 1);
                    }
                    else if (Figure.Name == "Line")
                    {
                        Pen Pen = new Pen(Color.FromArgb(Figure.RGB[0], Figure.RGB[1], Figure.RGB[2]), 5);
                        g.DrawLine(Pen, Figure.x1 + xMiddle - Figure.x1 / 2, Figure.y1 + yMiddle - Figure.y1 / 2, Figure.x2 + xMiddle - Figure.x2 / 2, Figure.y2 + yMiddle - Figure.y2 / 2);
                    }
                    else if (Figure.Name == "RectangleOutline")
                    {
                        Pen Pen = new Pen(Color.FromArgb(Figure.RGB[0], Figure.RGB[1], Figure.RGB[2]), 1);
                        g.DrawRectangle(Pen, Figure.x1 + xMiddle - Figure.x2 / 2, Figure.y1 + yMiddle - Figure.y2 / 2, Figure.x2, Figure.y2);
                    }
                    else if (Figure.Name == "RectangleFilled")
                    {
                        Brush brush = new SolidBrush(Color.FromArgb(Figure.RGB[0], Figure.RGB[1], Figure.RGB[2]));
                        g.FillRectangle(brush, Figure.x1 + xMiddle - Figure.x2 / 2, Figure.y1 + yMiddle - Figure.y2 / 2, Figure.x2, Figure.y2);
                    }
                    else if (Figure.Name == "EllipseOutline")
                    {
                        Pen Pen = new Pen(Color.FromArgb(Figure.RGB[0], Figure.RGB[1], Figure.RGB[2]), 1);
                        g.DrawEllipse(Pen, Figure.x1 + xMiddle - Figure.x2 / 2, Figure.y1 + yMiddle - Figure.y2 / 2, Figure.x2, Figure.y2);
                    }
                    else if (Figure.Name == "EllipseFilled")
                    {
                        Brush brush = new SolidBrush(Color.FromArgb(Figure.RGB[0], Figure.RGB[1], Figure.RGB[2]));
                        g.FillEllipse(brush, Figure.x1 + xMiddle - Figure.x2 / 2, Figure.y1 + yMiddle - Figure.y2 / 2, Figure.x2, Figure.y2);
                    }
                    else if (Figure.Name == "CircleOutline")
                    {
                        Pen Pen = new Pen(Color.FromArgb(Figure.RGB[0], Figure.RGB[1], Figure.RGB[2]), 1);
                        g.DrawEllipse(Pen, Figure.x1 + xMiddle - Figure.x2 / 2, Figure.y1 + yMiddle - Figure.x2 / 2, Figure.x2, Figure.x2);
                    }
                    else if (Figure.Name == "CircleFilled")
                    {
                        Brush brush = new SolidBrush(Color.FromArgb(Figure.RGB[0], Figure.RGB[1], Figure.RGB[2]));
                        g.FillEllipse(brush, Figure.x1 + xMiddle - Figure.x2 / 2, Figure.y1 + yMiddle - Figure.x2 / 2, Figure.x2, Figure.x2);
                    }
                    else if (Figure.Name == "RoundedRectangleOutline")
                    {
                        Pen Pen = new Pen(Color.FromArgb(Figure.RGB[0], Figure.RGB[1], Figure.RGB[2]), 1);
                        Rectangle rectangle = new Rectangle(Figure.x1 + xMiddle - Figure.x2 / 2, Figure.y1 + yMiddle - Figure.y2 / 2, Figure.x2, Figure.y2);

                        g.DrawPath(Pen, RoundedRect(rectangle, Figure.rad));

                    }
                    else if (Figure.Name == "RoundedRectangleFilled")
                    {
                        Brush brush = new SolidBrush(Color.FromArgb(Figure.RGB[0], Figure.RGB[1], Figure.RGB[2]));
                        Rectangle rectangle = new Rectangle(Figure.x1 + xMiddle - Figure.x2 / 2, Figure.y1 + yMiddle - Figure.x2 / 2, Figure.x2, Figure.y2);
                        g.FillPath(brush, RoundedRect(rectangle, Figure.rad));
                    }

                    else if (Figure.Name == "Text")
                    {
                        Brush brush = new SolidBrush(Color.FromArgb(Figure.RGB[0], Figure.RGB[1], Figure.RGB[2]));
                        Font font = new Font("Arial", Figure.size);
                        g.DrawString(Figure.text, font, brush, Figure.x1 + xMiddle, Figure.y1 + yMiddle);

                    }
                    else if (Figure.Name == "Image")
                    {
                        g.DrawImage(Figure.Image, Figure.x1 + xMiddle, Figure.y1 + yMiddle);
                    }

                }



            }
            if (Texts != null)
            {
                foreach (var text in Texts)
                {
                    Pen Pen = new Pen(Color.FromArgb(text.RGB[0], text.RGB[1], text.RGB[2]), 1);
                    foreach (var character in text.symbols)
                    {
                        for (int i = 0; i < character.GetLength(0); i++)
                        {
                            g.DrawLine(Pen, character[i, 0] + xMiddle, character[i, 1] + yMiddle, character[i, 2] + xMiddle, character[i, 3] + yMiddle);
                        }

                    }


                }
            }

        }

        //Method to Decode Messages
        private void Drawer(MessageData command, UdpClient server, IPEndPoint localEP)
        {
            if (command == null)
            {
                Console.WriteLine("Error: command null");
                return;
            }
            IPEndPoint remoteEP;
            string text;
            byte[] sendMessage;
            List<byte> bytes;
            switch (command.name)
            {
                case "Clear Display":
                    ClearDisplay(command);
                    text = $"command:Clear Display; color: Red = {command.RGB[0]}, Green = {command.RGB[1]}, Blue = {command.RGB[2]};";
                    Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                    sendMessage = Encoding.ASCII.GetBytes(text);
                    remoteEP = new IPEndPoint(localEP.Address, localEP.Port);
                    server.Send(sendMessage, sendMessage.Length, remoteEP);
                    break;

                case "Draw Pixel":
                    DrawPixel(command);
                    text = $"command:Draw Pixel; Coordinates: x = {command.x0}, y = {command.y0}, color: Red = {command.RGB[0]}, Green = {command.RGB[1]}, Blue = {command.RGB[2]} ";
                    Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                    sendMessage = Encoding.ASCII.GetBytes(text);
                    remoteEP = new IPEndPoint(localEP.Address, localEP.Port);
                    server.Send(sendMessage, sendMessage.Length, remoteEP);
                    break;
                case "Draw Line":
                    DrawLine(command);
                    text = $"command:Draw Line; Coordinates: x1 = {command.x0}, y1 = {command.y0}, x2 = {command.x1}, y2 = {command.y1}, color: Red = {command.RGB[0]}, Green = {command.RGB[1]}, Blue = {command.RGB[2]}";
                    Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                    sendMessage = Encoding.ASCII.GetBytes(text);
                    remoteEP = new IPEndPoint(localEP.Address, localEP.Port);
                    server.Send(sendMessage, sendMessage.Length, remoteEP);
                    break;

                case "Draw Rectangle":
                    DrawRectangle(command);
                    text = $"command:Draw Rectangle; Coordinates: x1 = {command.x0}, y1 = {command.y0}, width = {command.x1}, height = {command.y1}, color: Red = {command.RGB[0]}, Green = {command.RGB[1]}, Blue = {command.RGB[2]}";
                    Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                    sendMessage = Encoding.ASCII.GetBytes(text);
                    remoteEP = new IPEndPoint(localEP.Address, localEP.Port);
                    server.Send(sendMessage, sendMessage.Length, remoteEP);
                    break;

                case "Fill Rectangle":
                    FillRectangle(command);
                    text = $"command:Fill Rectangle; Coordinates: x1 = {command.x0}, y1 = {command.y0}, width = {command.x1}, height = {command.y1}, color: Red = {command.RGB[0]}, Green = {command.RGB[1]}, Blue = {command.RGB[2]}";
                    Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                    sendMessage = Encoding.ASCII.GetBytes(text);
                    server.Send(sendMessage, sendMessage.Length, localEP);
                    break;

                case "Draw Ellipse":
                    DrawEllipse(command);
                    text = $"command:Draw Ellipse; Coordinates: x1 = {command.x0}, y1 = {command.y0}, radius x = {command.x1}, radius y = {command.y1}, color: Red = {command.RGB[0]}, Green = {command.RGB[1]}, Blue = {command.RGB[2]}";
                    Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                    sendMessage = Encoding.ASCII.GetBytes(text);
                    server.Send(sendMessage, sendMessage.Length, localEP);

                    break;

                case "Fill Ellipse":
                    FillEllipse(command);
                    text = $"command:Fill Ellipse; Coordinates: x1 = {command.x0}, y1 = {command.y0}, radius x = {command.x1}, radius y = {command.y1}, color: Red = {command.RGB[0]}, Green = {command.RGB[1]}, Blue = {command.RGB[2]}";
                    Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                    sendMessage = Encoding.ASCII.GetBytes(text);
                    server.Send(sendMessage, sendMessage.Length, localEP);
                    break;

                case "Draw Circle":
                    DrawCircle(command);
                    text = $"command:Draw Circle; Coordinates: x1 = {command.x0}, y1 = {command.y0}, radius = {command.x1}, color: Red = {command.RGB[0]}, Green = {command.RGB[1]}, Blue = {command.RGB[2]}";
                    Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                    sendMessage = Encoding.ASCII.GetBytes(text);
                    server.Send(sendMessage, sendMessage.Length, localEP);
                    break;

                case "Fill Circle":
                    FillCircle(command);
                    text = $"command:Fill Circle; Coordinates: x1 = {command.x0}, y1 = {command.y0}, radius = {command.x1}, color: Red = {command.RGB[0]}, Green = {command.RGB[1]}, Blue = {command.RGB[2]}";
                    Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                    sendMessage = Encoding.ASCII.GetBytes(text);
                    server.Send(sendMessage, sendMessage.Length, localEP);
                    break;

                case "Draw Rounded Rectangle":
                    DrawRoundedRectangle(command);
                    text = $"command:Draw Rounded Rectangle; Rounded Rectangle Drawn: Coordinates: x1 = {command.x0}, y1 = {command.y0}, width = {command.x1}, height = {command.x1}, radius = {command.rounding}, color: Red = {command.RGB[0]}, Green = {command.RGB[1]}, Blue = {command.RGB[2]}";
                    Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                    sendMessage = Encoding.ASCII.GetBytes(text);
                    server.Send(sendMessage, sendMessage.Length, localEP);
                    break;

                case "Fill Rounded Rectangle":
                    FillRoundedRectangle(command);
                    text = $"command:Fill Rounded Rectangle. Rounded Rectangle Filled: Coordinates: x1 = {command.x0}, y1 = {command.y0}, width = {command.x1}, height = {command.x1}, radius = {command.rounding}, color: Red = {command.RGB[0]}, Green = {command.RGB[1]}, Blue = {command.RGB[2]}";
                    Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                    sendMessage = Encoding.ASCII.GetBytes(text);
                    server.Send(sendMessage, sendMessage.Length, localEP);
                    break;

                case "Draw Text":
                    DrawText(command);
                    text = $"command:Draw Text. Coordinates: x1 = {command.x0}, y1 = {command.y0}, color: Red = {command.RGB[0]}, Green = {command.RGB[1]}, Blue = {command.RGB[2]}, font size = {command.x1}, text = \b {command.text} ";
                    Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                    sendMessage = Encoding.ASCII.GetBytes(text);
                    server.Send(sendMessage, sendMessage.Length, localEP);
                    break;
                case "Draw Image":
                    DrawImage(command);
                    text = $"command:Draw Image; Coordinates: x1 = {command.x0}, y1 = {command.y0}, image width = {command.width}, image height = {command.height}";
                    Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                    sendMessage = Encoding.ASCII.GetBytes(text);
                    server.Send(sendMessage, sendMessage.Length, localEP);
                    break;
                case "Set Orientation":
                    RotateImage(command);
                    text = $"Command: Set Orientation; {command.x0} degrees";
                    Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                    sendMessage = Encoding.ASCII.GetBytes(text);
                    server.Send(sendMessage, sendMessage.Length, localEP);
                    break;
                case "Get Width":
                    text = $"Command: Get Width; Width: {width} px";
                    Console.WriteLine(text);
                    Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                    bytes = new List<byte>();
                    bytes.AddRange(BitConverter.GetBytes(width));
                    sendMessage = bytes.ToArray();
                    server.Send(sendMessage, sendMessage.Length, localEP);

                    break;
                case "Get Height":
                    text = $"Command: Get Height; Height: {height} px";
                    Console.WriteLine(text);
                    Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                    bytes = new List<byte>();
                    bytes.AddRange(BitConverter.GetBytes(height));
                    sendMessage = bytes.ToArray();
                    server.Send(sendMessage, sendMessage.Length, localEP);

                    break;
                case "Upload Sprite":
                    Console.WriteLine("Command: Upload Image");
                    text = $"Image Uploaded";
                    Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                    sendMessage = Encoding.ASCII.GetBytes(text);
                    server.Send(sendMessage, sendMessage.Length, localEP);
                    break;
                case "Show Sprite":
                    Console.WriteLine("Command: Draw Image From List");
                    text = $"Command: Draw Image From List, x: {command.y0} , y: {command.x1}";
                    Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                    DrawImage(command);
                    sendMessage = Encoding.ASCII.GetBytes(text);
                    server.Send(sendMessage, sendMessage.Length, localEP);

                    break;
                case "Draw Text As Symbols":
                    Console.WriteLine("Command:Draw text as symbols");
                    DrawSymbol(command);
                    text = $"Command: Draw text as symbols: x = {command.x0}, y = {command.y0}, size = {command.x1}";
                    Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                    sendMessage = Encoding.ASCII.GetBytes(text);
                    server.Send(sendMessage, sendMessage.Length, localEP);
                    break;
                case "Set Screen Size":
                    Console.WriteLine("Command: Set Screen Size");
                    SetScreenSize(command);
                    Console.WriteLine($"Width: {width}");
                    Console.WriteLine($"Height: {height}");
                    Invoke((MethodInvoker)delegate { this.Size = new Size(width, height); });

                    text = $"Screen Size Set: Width = {width}, Height = {height}";
                    sendMessage = Encoding.ASCII.GetBytes(text);
                    server.Send(sendMessage, sendMessage.Length, localEP);
                    break;
            }




        }


        //Method to start UDP Server
        private void StartServer()
        {

            Console.WriteLine("Server Begin");
            UdpClient server = new UdpClient(port);
            IPEndPoint localEP = new IPEndPoint(IPAddress.Any, 0);
            try
            {
                //DrawSymbol(-500, 0,"Based", 10, RGB = new byte[] { 255, 0, 0 }); // Test for drawsymbol method
                while (true)
                {
                    Console.WriteLine("Waiting for message");
                    byte[] RecievedData = server.Receive(ref localEP);
                    Console.WriteLine($"Received broadcast from {localEP} :");
                    MessageData md = program.DecodeMessage(RecievedData);
                    Drawer(md, server, localEP);
                }

            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                server.Close();
            }
        }
        public Form1()
        {

            int middleHeight = this.ClientSize.Height / 2;
            int middleWidth = this.ClientSize.Width / 2;
            InitializeComponent();
            try
            {
                Thread receiveThread = new Thread(new ThreadStart(StartServer));
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}

