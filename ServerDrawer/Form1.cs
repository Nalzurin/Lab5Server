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

namespace ServerDrawer
{

public partial class Form1 : Form
    {

        public static int rotation;

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

            public Figure(string _name, short _x1, short _y1, short _x2,  byte[] _RGB)
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

            public Figure(string _name, short _x1, short _y1, short _size, string _text, byte[] _RGB )
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
                coords = new float[3,4];
                coords[0,0] =  _x0;
                coords[0,1] = _y0;
                coords[0,2] = (_x0 + (1 * size));
                coords[0,3] = (_y0 - (2 * size));
                coords[1, 0] = coords[0, 2];
                coords[1, 1] = coords[0, 3];
                coords[1, 2] = (_x0 + (2 * size));
                coords[1, 3] = _y0;
                coords[2,0] = ((_x0 + coords[1,0]) / 2);
                coords[2,1] = ((_y0 + coords[1, 1]) / 2);
                coords[2,2] = ((coords[1,0] + coords[1, 2]) / 2);
                coords[2,3] = ((coords[1,1] + coords[1, 3]) / 2);
                symbols.Add(coords);
            }

            public void SymbolB(int _x0, int _y0)
            {
                coords = new float[10,4];
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
                coords = new float[9,4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0 - (size * 0.33f);
                coords[0, 2] = _x0;
                coords[0, 3] = _y0 - size*1.66f;
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
                coords[4, 3] = _y0 - size*0.5f;

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
                coords[3, 2] = _x0 + size* 2;
                coords[3, 3] = coords[0, 1];
                coords[4, 0] = _x0 + size * 2;
                coords[4, 1] = coords[0, 1];
                coords[4, 2] = coords[4, 0];
                coords[4, 3] =_y0 - size * 2;
                symbols.Add(coords);
            }
            public void SymbolV(int _x0, int _y0)
            {
                coords = new float[2, 4];
                coords[0, 0] = _x0;
                coords[0, 1] = _y0 - size*2;
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
                coords[0, 2] = _x0 + size*2;
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
                foreach(char c in text)
                {
                    switch(c)
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
        public static List<Figure> Figures = new List<Figure>();
        public static List<TextAsSymbols> Texts = new List<TextAsSymbols>();

        public static System.Drawing.Image[] UploadedImages = new System.Drawing.Image[255];

        public void DrawSymbol(short x1, short y1, string text, short size, byte[] RGB)
        {
            var SymbolLine = new TextAsSymbols(x1, y1, size, RGB);
            SymbolLine.TextProcessor(text);
            Texts.Add(SymbolLine);
            Invalidate();
        }
        public void DrawPixel(byte command, short x1, short y1, byte[] RGB)
        {
            var Figure = new Figure("Pixel", x1, y1, RGB);
            Figures.Add(Figure);
            Invalidate();
        }

        public void DrawLine(byte command, short x1, short y1, short x2, short y2, byte[] RGB)
        {
            var Figure = new Figure("Line", x1, y1, x2, y2, RGB);
            Figures.Add(Figure);
            Invalidate();
        }

        public void DrawRectangle (byte command, short x1, short y1, short x2, short y2, byte[] RGB)
        {
            var Figure = new Figure("RectangleOutline", x1, y1, x2, y2, RGB);
            Figures.Add(Figure);
            Invalidate();
        }

        public void FillRectangle(byte command, short x1, short y1, short x2, short y2, byte[] RGB)
        {
            var Figure = new Figure("RectangleFilled", x1, y1, x2, y2, RGB);
            Figures.Add(Figure);
            Invalidate();
        }

        public void DrawEllipse(byte command, short x1, short y1, short x2, short y2, byte[] RGB)
        {
            var Figure = new Figure("EllipseOutline", x1, y1, x2, y2, RGB);
            Figures.Add(Figure);
            Invalidate();
        }

        public void FillEllipse(byte command, short x1, short y1, short x2, short y2, byte[] RGB)
        {
            var Figure = new Figure("EllipseFilled", x1, y1, x2, y2, RGB);
            Figures.Add(Figure);
            Invalidate();
        }

        public void DrawCircle(byte command, short x1, short y1, short x2,  byte[] RGB)
        {
            var Figure = new Figure("CircleOutline", x1, y1, x2, RGB);
            Figures.Add(Figure);
            Invalidate();
        }

        public void FillCircle(byte command, short x1, short y1, short x2, byte[] RGB)
        {
            var Figure = new Figure("CircleFilled", x1, y1, x2, RGB);
            Figures.Add(Figure);
            Invalidate();
        }

        public void DrawRoundedRectangle(byte command, short x1, short y1, short x2, short y2, short rad, byte[] RGB)
        {
            var Figure = new Figure("RoundedRectangleOutline", x1, y1, x2, y2, rad, RGB);
            Figures.Add(Figure);
            Invalidate();
        }

        public void FillRoundedRectangle(byte command, short x1, short y1, short x2, short y2, short rad, byte[] RGB)
        {
            var Figure = new Figure("RoundedRectangleFilled", x1, y1, x2, y2, rad, RGB);
            Figures.Add(Figure);
            Invalidate();
        }
        public void DrawText(byte command, short x1, short y1, short size, string text, byte[] RGB)
        {
            var Figure = new Figure("Text", x1, y1, size, text, RGB);
            Figures.Add(Figure);
            Invalidate();
        }

        public void DrawImage(byte command, short x1, short y1, System.Drawing.Image img)
        {
            var Figure = new Figure("Image", x1, y1, img);
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


        const int port = 1984;

        public void ClearDisplay(byte[] RecievedData, out byte[] RGB)
        {
            byte[] transfer;
            int val1place = 1;
            transfer = new byte[2];
            RGB = new byte[3];
            Array.Copy(RecievedData, val1place, RGB, 0, RGB.Length);
            Figures.Clear();
            Texts.Clear();
            this.BackColor = Color.FromArgb(RGB[0], RGB[1], RGB[2]);
            Invalidate();

        }
        
        public void RotateImage(byte[] RecievedData, out Int16 Orientation)
        {
            byte[] transfer;
            int val1place = 1;
            transfer = new byte[2];
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Orientation = BitConverter.ToInt16(transfer, 0);
            rotation = (int)Orientation;
            Invalidate();
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int xMiddle = this.Width/2, yMiddle = this.Height/2;
            Graphics g =  e.Graphics;
            g.TranslateTransform(xMiddle, yMiddle);
            g.RotateTransform(rotation);
            g.TranslateTransform(-xMiddle, -yMiddle);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic; 
            if (Figures != null)
            {
                foreach(var Figure in Figures)
                {
                    if (Figure.Name == "Pixel")
                    {
                        Brush brush = new SolidBrush(Color.FromArgb(Figure.RGB[0], Figure.RGB[1], Figure.RGB[2]));
                        g.FillRectangle(brush, Figure.x1+this.Width/2, Figure.y1+yMiddle, 1, 1);
                    }
                    else if (Figure.Name == "Line")
                    {
                        Pen Pen = new Pen(Color.FromArgb(Figure.RGB[0], Figure.RGB[1], Figure.RGB[2]),5);
                        g.DrawLine(Pen, Figure.x1 + xMiddle - Figure.x1 / 2, Figure.y1 + yMiddle - Figure.y1 / 2, Figure.x2 + xMiddle - Figure.x2 / 2, Figure.y2 + yMiddle - Figure.y2/2);
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

                            g.DrawPath(Pen, RoundedRect(rectangle,Figure.rad));
        
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
                    else if(Figure.Name == "Image")
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
                    foreach(var character in text.symbols)
                    {
                        for (int i = 0; i < character.GetLength(0); i++)
                        {
                            g.DrawLine(Pen, character[i, 0] + xMiddle, character[i,1] + yMiddle, character[i,2] + xMiddle, character[i,3] + yMiddle);
                        }

                    }


                }
            }

        }
        public static void ThreeVarsDecode(byte[] RecievedData, out Int16 val1, out Int16 val2, out byte[] RGB)
        {
            byte[] transfer;
            int val1place = 1;
            transfer = new byte[2];
            RGB = new byte[3];
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            val1 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            val2 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, RGB, 0, RGB.Length);

        }

        public static void FiveVarsDecode(byte[] RecievedData, out Int16 val1, out Int16 val2, out Int16 val3, out Int16 val4, out byte[] RGB)
        {
            byte[] transfer;
            int val1place = 1;
            transfer = new byte[2];
            RGB = new byte[3];
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            val1 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            val2 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            val3 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            val4 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, RGB, 0, RGB.Length);
        }

        public static void CircleDecoder(byte[] RecievedData, out Int16 val1, out Int16 val2, out Int16 val3, out byte[] RGB)
        {
            byte[] transfer;
            int val1place = 1;
            transfer = new byte[2];
            RGB = new byte[3];
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            val1 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            val2 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            val3 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, RGB, 0, RGB.Length);

        }

        public static void RoundedRectangleDecoder(byte[] RecievedData, out Int16 val1, out Int16 val2, out Int16 val3, out Int16 val4, out Int16 val5, out byte[] RGB)
        {
            byte[] transfer;
            int val1place = 1;
            transfer = new byte[2];
            RGB = new byte[3];
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            val1 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            val2 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            val3 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            val4 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            val5 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, RGB, 0, RGB.Length);
        }

        public static void TextDecoder(byte[] RecievedData, out Int16 val1, out Int16 val2, out Int16 val3, out Int16 val4, out byte[] RGB, out string text)
        {
            byte[] transfer;
            int val1place = 1;
            transfer = new byte[2];
            RGB = new byte[3];
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            val1 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            val2 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, RGB, 0, RGB.Length);
            val1place += 3;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            val3 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            val4 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            transfer = new byte[val4];
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            text = Encoding.ASCII.GetString(transfer);

        }
        public static void ImageDecoder(byte[] RecievedData, out Int16 val1, out Int16 val2, out Int16 width, out Int16 height, out System.Drawing.Image pic)
        {
            byte[] transfer;
            int val1place = 1;
            transfer = new byte[2];
            Bitmap bitmap;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            val1 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            val2 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            width = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            height = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            bitmap = new Bitmap(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {

                    transfer = new byte[3];
                    Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
                    bitmap.SetPixel(i,j, Color.FromArgb(transfer[0], transfer[1], transfer[2]));
                    val1place += 3;
                }
            }
            Bitmap resized = new Bitmap(bitmap, new Size(bitmap.Width * 10, bitmap.Height * 10));
            pic = resized;

        }
        public static void ImageUploader(byte[] RecievedData)
        {
            Bitmap bitmap;
            int val1place = 2, index, width, height;
            byte[] transfer;
            index = RecievedData[1];
            transfer = new byte[2];
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            width = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            height = BitConverter.ToInt16(transfer, 0);
            transfer = new byte[3];
            bitmap = new Bitmap(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {

                    Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
                    bitmap.SetPixel(i, j, Color.FromArgb(transfer[2], transfer[0], transfer[1]));
                    val1place += 3;
                }
            }
            UploadedImages[index] = bitmap;

        }

        public static void GetImage(byte[] RecievedData, out Int16 x, out Int16 y, out System.Drawing.Image img)
        {
            int val1place = 1, index;
            byte[] transfer;
            transfer = new byte[2];
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            index = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            x = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            y = BitConverter.ToInt16(transfer, 0);

            img = UploadedImages[index];

        }
        public static void SetScreenSize(byte[] RecievedData, out Int16 width, out Int16 height)
        {
            int val1place = 1;
            byte[] transfer = new byte[2];
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            width = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            height = BitConverter.ToInt16(transfer, 0);
        }

        private void StartServer()
        {
            Int16 val1, val2, val3, val4, val5; byte[] RGB, sendMessage; string text; byte command; System.Drawing.Image img;
            List<byte> bytes;
            short width = 0, height = 0;
            Console.WriteLine("Server Begin");
            UdpClient server = new UdpClient(port);
            IPEndPoint localEP = new IPEndPoint(IPAddress.Any, 0);
            IPEndPoint remoteEP;
            try
            {
               // DrawSymbol(-500, 0,"Based", 10, RGB = new byte[] { 255, 0, 0 });
                while (true)
                {
                    Console.WriteLine("Waiting for message");
                    byte[] RecievedData = server.Receive(ref localEP);
                    command = RecievedData[0];
                    Console.WriteLine($"Received broadcast from {localEP} :");
                    
                    switch (command)
                    {
                        case 1:
                            ClearDisplay(RecievedData, out RGB);
                            text = $"command:Clear Display; color: Red = {RGB[0]}, Green = {RGB[1]}, Blue = {RGB[2]};";
                            Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text ); });
                            sendMessage = Encoding.ASCII.GetBytes(text);
                            remoteEP = new IPEndPoint(localEP.Address, localEP.Port);
                            server.Send(sendMessage, sendMessage.Length, remoteEP);
                            break;

                        case 2:
                            ThreeVarsDecode(RecievedData, out val1, out val2, out RGB);
                            DrawPixel(command, val1, val2, RGB);
                            text = $"command:Draw Pixel; Coordinates: x = {val1}, y = {val2}, color: Red = {RGB[0]}, Green = {RGB[1]}, Blue = {RGB[2]} ";
                            Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                            sendMessage = Encoding.ASCII.GetBytes(text);
                            remoteEP = new IPEndPoint(localEP.Address, localEP.Port);
                            server.Send(sendMessage, sendMessage.Length, remoteEP);
                            break;
                        case 3:
                            FiveVarsDecode(RecievedData, out val1, out val2, out val3, out val4, out RGB);
                            DrawLine(command,val1,val2,val3,val4,RGB);
                            text = $"command:Draw Line; Coordinates: x1 = {val1}, y1 = {val2}, x2 = {val3}, y2 = {val4}, color: Red = {RGB[0]}, Green = {RGB[1]}, Blue = {RGB[2]}";
                            Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                            sendMessage = Encoding.ASCII.GetBytes(text);
                            remoteEP = new IPEndPoint(localEP.Address, localEP.Port);
                            server.Send(sendMessage, sendMessage.Length, remoteEP);
                            break;

                        case 4:
                            FiveVarsDecode(RecievedData, out val1, out val2, out val3, out val4, out RGB);
                            DrawRectangle(command, val1, val2, val3, val4, RGB);
                            text = $"command:Draw Rectangle; Coordinates: x1 = {val1}, y1 = {val2}, width = {val3}, height = {val4}, color: Red = {RGB[0]}, Green = {RGB[1]}, Blue = {RGB[2]}";
                            Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                            sendMessage = Encoding.ASCII.GetBytes(text);
                            remoteEP = new IPEndPoint(localEP.Address, localEP.Port);
                            server.Send(sendMessage, sendMessage.Length, remoteEP);
                            break;

                        case 5:
                            FiveVarsDecode(RecievedData, out val1, out val2, out val3, out val4, out RGB);
                            FillRectangle(command, val1, val2, val3, val4, RGB);
                            text = $"command:Fill Rectangle; Coordinates: x1 = {val1}, y1 = {val2}, width = {val3}, height = {val4}, color: Red = {RGB[0]}, Green = {RGB[1]}, Blue = {RGB[2]}";
                            Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                            sendMessage = Encoding.ASCII.GetBytes(text);
                            server.Send(sendMessage, sendMessage.Length, localEP);
                            break;

                        case 6:
                            FiveVarsDecode(RecievedData, out val1, out val2, out val3, out val4, out RGB);
                            DrawEllipse(command, val1, val2, val3, val4, RGB);
                            text = $"command:Draw Ellipse; Coordinates: x1 = {val1}, y1 = {val2}, radius x = {val3}, radius y = {val4}, color: Red = {RGB[0]}, Green = {RGB[1]}, Blue = {RGB[2]}";
                            Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                            sendMessage = Encoding.ASCII.GetBytes(text);
                            server.Send(sendMessage, sendMessage.Length, localEP);

                            break;

                        case 7:
                            FiveVarsDecode(RecievedData, out val1, out val2, out val3, out val4, out RGB);
                            FillEllipse(command, val1, val2, val3, val4, RGB);
                            text = $"command:Fill Ellipse; Coordinates: x1 = {val1}, y1 = {val2}, radius x = {val3}, radius y = {val4}, color: Red = {RGB[0]}, Green = {RGB[1]}, Blue = {RGB[2]}";
                            Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                            sendMessage = Encoding.ASCII.GetBytes(text);
                            server.Send(sendMessage, sendMessage.Length, localEP);
                            break;

                        case 8:
                            CircleDecoder(RecievedData, out val1, out val2, out val3, out RGB);
                            DrawCircle(command, val1, val2, val3, RGB);
                            text = $"command:Draw Circle; Coordinates: x1 = {val1}, y1 = {val2}, radius = {val3}, color: Red = {RGB[0]}, Green = {RGB[1]}, Blue = {RGB[2]}";
                            Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                            sendMessage = Encoding.ASCII.GetBytes(text);
                            server.Send(sendMessage, sendMessage.Length, localEP);
                            break;

                        case 9:
                            CircleDecoder(RecievedData, out val1, out val2, out val3, out RGB);
                            FillCircle(command, val1, val2, val3, RGB);
                            text = $"command:Fill Circle; Coordinates: x1 = {val1}, y1 = {val2}, radius = {val3}, color: Red = {RGB[0]}, Green = {RGB[1]}, Blue = {RGB[2]}";
                            Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                            sendMessage = Encoding.ASCII.GetBytes(text);
                            server.Send(sendMessage, sendMessage.Length, localEP);
                            break;

                        case 10:
                            RoundedRectangleDecoder(RecievedData, out val1, out val2, out val3, out val4, out val5, out RGB);
                            DrawRoundedRectangle(command, val1, val2, val3, val4, val5, RGB);
                            text = $"command:Draw Rounded Rectangle; Rounded Rectangle Drawn: Coordinates: x1 = {val1}, y1 = {val2}, width = {val3}, height = {val4}, radius = {val5}, color: Red = {RGB[0]}, Green = {RGB[1]}, Blue = {RGB[2]}";
                            Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                            sendMessage = Encoding.ASCII.GetBytes(text);
                            server.Send(sendMessage, sendMessage.Length, localEP);
                            break;

                        case 11:
                            RoundedRectangleDecoder(RecievedData, out val1, out val2, out val3, out val4, out val5, out RGB);
                            FillRoundedRectangle(command, val1, val2, val3, val4, val5, RGB);
                            text = $"command:Fill Rounded Rectangle. Rounded Rectangle Filled: Coordinates: x1 = {val1}, y1 = {val2}, width = {val3}, height = {val4}, radius = {val5}, color: Red = {RGB[0]}, Green = {RGB[1]}, Blue = {RGB[2]}";
                            Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                            sendMessage = Encoding.ASCII.GetBytes(text);
                            server.Send(sendMessage, sendMessage.Length, localEP);
                            break;

                        case 12:
                            TextDecoder(RecievedData, out val1, out val2, out val3, out val4, out RGB, out text);
                            DrawText(command,val1,val2,val3,text,RGB);
                            text = $"command:Draw Text. Coordinates: x1 = {val1}, y1 = {val2}, color: Red = {RGB[0]}, Green = {RGB[1]}, Blue = {RGB[2]}, font size = {val3}, text = \b {text} ";
                            Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                            sendMessage = Encoding.ASCII.GetBytes(text);
                            server.Send(sendMessage, sendMessage.Length, localEP);
                            break;
                        case 13:
                            ImageDecoder(RecievedData, out val1, out val2, out val3, out val4, out img);
                            DrawImage(command, val1, val2, img);
                            text = $"command:Draw Image; Coordinates: x1 = {val1}, y1 = {val2}, image width = {val3}, image height = {val4}";
                            Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                            sendMessage = Encoding.ASCII.GetBytes(text);
                            server.Send(sendMessage, sendMessage.Length, localEP);
                            break;
                        case 14:
                            RotateImage(RecievedData, out val1);
                            text = $"Command: Set Orientation; {val1} degrees";
                            Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                            sendMessage = Encoding.ASCII.GetBytes(text);
                            server.Send(sendMessage, sendMessage.Length, localEP);
                            break;
                        case 15:
                            val1 = Convert.ToInt16(this.Width);
                            text = $"Command: Get Width; Width: {val1} px";
                            Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                            bytes = new List<byte>();
                            bytes.AddRange(BitConverter.GetBytes(val1));
                            sendMessage = bytes.ToArray();
                            server.Send(sendMessage, sendMessage.Length, localEP);

                            break;
                        case 16:
                            val1 = Convert.ToInt16(this.Height);
                            Console.WriteLine($"Height: {val1} px");
                            text = $"Command: Get Height; Height: {val1} px";
                            Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                            bytes = new List<byte>();
                            bytes.AddRange(BitConverter.GetBytes(val1));
                            sendMessage = bytes.ToArray();
                            server.Send(sendMessage, sendMessage.Length, localEP);

                            break;
                        case 17:
                            Console.WriteLine("Command: Upload Image");
                            ImageUploader(RecievedData);
                            text = $"Image Uploaded";
                            Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                            sendMessage = Encoding.ASCII.GetBytes(text);
                            server.Send(sendMessage, sendMessage.Length, localEP);
                            break;
                        case 18:
                            Console.WriteLine("Command: Draw Image From List");
                            GetImage(RecievedData, out val1, out val2, out img);
                            text = $"Command: Draw Image From List, x: {val1} , y: {val2}";
                            Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });
                            DrawImage(command, val1, val2, img);
                            sendMessage = Encoding.ASCII.GetBytes(text);
                            server.Send(sendMessage, sendMessage.Length, localEP);

                            break;
                        case 19:
                            Console.WriteLine("Command:Draw text as symbols");
                            TextDecoder(RecievedData, out val1, out val2, out val3, out val4, out RGB, out text);
                            DrawSymbol(val1, val2, text, val3, RGB);
                            text = $"Command: Draw text as symbols: x = {val1}, y = {val2}, size = {val3}";
                            Invoke((MethodInvoker)delegate { listBox1.Items.Add(Text = text); });

                            break;
                        case 254:
                            Console.WriteLine("Command: Set Screen Size");
                            SetScreenSize(RecievedData, out width, out height);
                            Console.WriteLine($"Width: {width}");
                            Console.WriteLine($"Height: {height}");
                            Invoke((MethodInvoker)delegate { this.Size = new Size(width, height); });
                            
                            text = $"Screen Size Set: Width = {width}, Height = {height}";
                            sendMessage = Encoding.ASCII.GetBytes(text);
                            server.Send(sendMessage, sendMessage.Length, localEP);
                            break;
                    }

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
            int middleHeight = this.ClientSize.Height/2;
            int middleWidth = this.ClientSize.Width/2;
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

