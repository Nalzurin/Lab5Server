using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace Decoder
{
    public class ServerProgram
    {
        //Variables
        public static Int16 width = 0, height = 0;
        //Stores Uploaded Sprites
        public static System.Drawing.Image[] UploadedImages = new System.Drawing.Image[255];

        //Call this when receiving datagrams
        public MessageData DecodeMessage(byte[] RecievedData)
        {

            byte command;
            string errortext = "Recieved Data Error: Wrong Size";
            if (RecievedData.Length > 0)
            {
                command = RecievedData[0];
                switch (command)
                {
                    case 1:
                        if (RecievedData.Length == 4)
                        {
                            Console.WriteLine("command:Clear Display");
                            return ClearDisplay("Clear Display", RecievedData);
                        }
                        else
                        {
                            Console.WriteLine(errortext);
                            return null;
                        }


                    case 2:
                        if (RecievedData.Length == 8)
                        {
                            Console.WriteLine("command:Draw Pixel");
                            return ThreeVarsDecode("Draw Pixel", RecievedData);
                        }
                        else
                        {
                            Console.WriteLine(errortext);
                            return null;
                        }
                    case 3:
                        if (RecievedData.Length == 12)
                        {
                            Console.WriteLine("command:Draw Line");
                            return FiveVarsDecode("Draw Line", RecievedData);
                        }
                        else
                        {
                            Console.WriteLine(errortext);
                            return null;
                        }

                    case 4:
                        if (RecievedData.Length == 12)
                        {
                            Console.WriteLine("command:Draw Rectangle");
                            return FiveVarsDecode("Draw Rectangle", RecievedData);
                        }
                        else
                        {
                            Console.WriteLine(errortext);
                            return null;
                        }

                    case 5:
                        if (RecievedData.Length == 12)
                        {
                            Console.WriteLine("command:Fill Rectangle");

                            return FiveVarsDecode("Fill Rectangle", RecievedData);
                        }
                        else
                        {
                            Console.WriteLine(errortext);
                            return null;
                        }

                    case 6:
                        if (RecievedData.Length == 12)
                        {
                            Console.WriteLine("command:Draw Ellipse");

                            return FiveVarsDecode("Draw Ellipse", RecievedData);
                        }
                        else
                        {
                            Console.WriteLine(errortext);
                            return null;
                        }

                    case 7:
                        if (RecievedData.Length == 12)
                        {
                            Console.WriteLine("command:Fill Ellipse");

                            return FiveVarsDecode("Fill Ellipse", RecievedData);
                        }
                        else
                        {
                            Console.WriteLine(errortext);
                            return null;
                        }
                    case 8:
                        if (RecievedData.Length == 10)
                        {
                            Console.WriteLine("command:Draw Circle");
                            return CircleDecoder("Draw Circle", RecievedData);
                        }
                        else
                        {
                            Console.WriteLine(errortext);
                            return null;
                        }
                    case 9:
                        if (RecievedData.Length == 10)
                        {
                            Console.WriteLine("command:Fill Circle");
                            return CircleDecoder("Fill Circle", RecievedData);
                        }
                        else
                        {
                            Console.WriteLine(errortext);
                            return null;
                        }
                    case 10:
                        if (RecievedData.Length == 14)
                        {
                            Console.WriteLine("command:Draw Rounded Rectangle");
                            return RoundedRectangleDecoder("Draw Rounded Rectangle", RecievedData);
                        }
                        else
                        {
                            Console.WriteLine(errortext);
                            return null;
                        }
                    case 11:
                        if (RecievedData.Length == 14)
                        {
                            Console.WriteLine("command:Fill Rounded Rectangle");
                            return RoundedRectangleDecoder("Fill Rounded Rectangle", RecievedData);
                        }
                        else
                        {
                            Console.WriteLine(errortext);
                            return null;
                        }
                    case 12:
                        if (RecievedData.Length > 10)
                        {
                            Console.WriteLine("command:Draw Text");
                            return TextDecoder("Draw Text", RecievedData);
                        }
                        else
                        {
                            Console.WriteLine(errortext);
                            return null;
                        }
                    case 13:
                        if (RecievedData.Length == 9)
                        {
                            Console.WriteLine("command:Draw Image");
                            return ImageDecoder("Draw Image", RecievedData);
                        }
                        else
                        {
                            Console.WriteLine(errortext);
                            return null;
                        }
                    case 14:
                        if (RecievedData.Length == 3)
                        {
                            Console.WriteLine("command:Set Orientation");
                            return RotateImage("Set Orientation", RecievedData);
                        }
                        else
                        {
                            Console.WriteLine(errortext);
                            return null;
                        }
                    case 15:
                        if (RecievedData.Length == 1)
                        {
                            Console.WriteLine("command:Get Width");
                            return new MessageData("Get Width");
                        }
                        else
                        {
                            Console.WriteLine(errortext);
                            return null;
                        }
                    case 16:
                        if (RecievedData.Length == 1)
                        {
                            Console.WriteLine("command:Get Height");
                            return new MessageData("Get Height");
                        }
                        else
                        {
                            Console.WriteLine(errortext);
                            return null;
                        }
                    case 17:
                        if (RecievedData.Length > 9)
                        {
                            Console.WriteLine("command:Upload Sprite");
                            return ImageUploader("Upload Sprite", RecievedData);
                        }
                        else
                        {
                            Console.WriteLine(errortext);
                            return null;
                        }
                    case 18:
                        if (RecievedData.Length == 7)
                        {
                            Console.WriteLine("command:Show Sprite");
                            return GetImage("Show Sprite", RecievedData);
                        }
                        else
                        {
                            Console.WriteLine(errortext);
                            return null;
                        }
                    case 19:
                        if (RecievedData.Length > 10)
                        {
                            Console.WriteLine("command:Draw Text As Symbols");
                            return TextDecoder("Draw Text As Symbols", RecievedData);
                        }
                        else
                        {
                            Console.WriteLine(errortext);
                            return null;
                        }
                    case 254:
                        if (RecievedData.Length == 5)
                        {
                            Console.WriteLine("command: Set Screen Size");
                            return SetScreenSize("Set Screen Size", RecievedData);
                        }
                        else
                        {
                            Console.WriteLine(errortext);
                            return null;
                        }
                    default:
                        Console.WriteLine("Recieved Data Error: Command Unrecognized");
                        return null;
                }
            }
            else
            {
                Console.WriteLine("Recieved Data Error: Empty Message!");
                return null;
            }


        }


        //Sets Size Size
        public static MessageData SetScreenSize(string name, byte[] RecievedData)
        {
            int val1place = 1;
            byte[] transfer = new byte[2];
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            width = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            height = BitConverter.ToInt16(transfer, 0);
            return new MessageData(name, width, height);
        }

        //Clears Display
        public static MessageData ClearDisplay(string name, byte[] RecievedData)
        {

            int val1place = 1;
            byte[] RGB = new byte[3];
            Array.Copy(RecievedData, val1place, RGB, 0, RGB.Length);
            return new MessageData(name, RGB);

        }
        //Drawing area orientation
        public MessageData RotateImage(string name, byte[] RecievedData)
        {
            byte[] transfer;
            int val1place = 1;
            transfer = new byte[2];
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 Orientation = BitConverter.ToInt16(transfer, 0);
            return new MessageData(name, Orientation);
        }

        //Decodes datagrams with three variables
        public static MessageData ThreeVarsDecode(string name, byte[] RecievedData)
        {
            byte[] transfer;
            int val1place = 1;
            transfer = new byte[2];
            byte[] RGB = new byte[3];
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 val1 = BitConverter.ToInt16(transfer, 0);

            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 val2 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, RGB, 0, RGB.Length);
            return new MessageData(name, val1, val2, RGB);
        }
        //Decodes datagrams with five variables
        public static MessageData FiveVarsDecode(string name, byte[] RecievedData)
        {
            byte[] transfer;
            int val1place = 1;
            transfer = new byte[2];
            byte[] RGB = new byte[3];
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 val1 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 val2 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 val3 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 val4 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, RGB, 0, RGB.Length);
            return new MessageData(name, val1, val2, val3, val4, RGB);
        }

        //Decodes Circle datagrams(draw/fill)
        public static MessageData CircleDecoder(string name, byte[] RecievedData)
        {
            byte[] transfer;
            int val1place = 1;
            transfer = new byte[2];
            byte[] RGB = new byte[3];
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 val1 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 val2 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 val3 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, RGB, 0, RGB.Length);
            return new MessageData(name, val1, val2, val3, RGB);

        }
        //Decodes Rounded Rectangle datagrams(draw/fill)
        public static MessageData RoundedRectangleDecoder(string name, byte[] RecievedData)
        {
            byte[] transfer;
            int val1place = 1;
            transfer = new byte[2];
            byte[] RGB = new byte[3];
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 val1 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 val2 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 val3 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 val4 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 val5 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, RGB, 0, RGB.Length);
            return new MessageData(name, val1, val2, val3, val4, val5, RGB);
        }

        //Decodes Text
        public static MessageData TextDecoder(string name, byte[] RecievedData)
        {
            byte[] transfer;
            int val1place = 1;
            transfer = new byte[2];
            byte[] RGB = new byte[3];
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 val1 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 val2 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, RGB, 0, RGB.Length);
            val1place += 3;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 val3 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 val4 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            transfer = new byte[val4];
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            string text = Encoding.ASCII.GetString(transfer);
            return new MessageData(name, val1, val2, val3, val4, RGB, text);

        }

        //Decodes a sprite, superceded by ImageUploader
        public static MessageData ImageDecoder(string name, byte[] RecievedData)
        {
            byte[] transfer;
            int val1place = 1;
            transfer = new byte[2];
            Bitmap bitmap;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 val1 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 val2 = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 width = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 height = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            bitmap = new Bitmap(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {

                    transfer = new byte[3];
                    Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
                    bitmap.SetPixel(i, j, Color.FromArgb(transfer[0], transfer[1], transfer[2]));
                    val1place += 3;
                }
            }
            Bitmap resized = new Bitmap(bitmap, new Size(bitmap.Width * 10, bitmap.Height * 10));
            System.Drawing.Image pic = resized;
            return new MessageData(name, val1, val2, width, height, pic);

        }

        //Decodes a sprite and uploads it to storage
        public static MessageData ImageUploader(string name, byte[] RecievedData)
        {
            Bitmap bitmap;
            Int16 val1place = 2, index, width, height;
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
            System.Drawing.Image pic = bitmap;
            return new MessageData(name, index, width, height, pic);

        }


        //Gets a sprite from storage by its index
        public static MessageData GetImage(string name, byte[] RecievedData)
        {
            Int16 val1place = 1, index;
            byte[] transfer;
            transfer = new byte[2];
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            index = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 x = BitConverter.ToInt16(transfer, 0);
            val1place += 2;
            Array.Copy(RecievedData, val1place, transfer, 0, transfer.Length);
            Int16 y = BitConverter.ToInt16(transfer, 0);

            System.Drawing.Image img = UploadedImages[index];
            return new MessageData(name, x, y, img);
        }


    }
    public class MessageData
    {

        public System.Drawing.Image img;
        public Int16 x0, x1, y0, y1, rounding, width = 0, height = 0; public byte[] RGB;
        public string name;
        public string text;
        public MessageData(string _name)
        {
            this.name = _name;
        }
        public MessageData(string _name, Int16 _x0, Int16 _y0)
        {
            this.name = _name;
            this.x0 = _x0;
            this.y0 = _y0;
        }
        public MessageData(string _name, Byte[] _RGB)
        {
            this.name = _name;
            this.RGB = _RGB;
        }
        public MessageData(string _name, Int16 _x0)
        {
            this.name = _name;
            this.x0 = _x0;
        }
        public MessageData(string _name, Int16 _x0, Int16 _y0, Int16 _x1)
        {
            this.name = _name;
            this.x1 = _x1;
            this.x0 = _x0;
            this.y0 = _y0;
        }
        public MessageData(string _name, Int16 _x0, Int16 _y0, Byte[] _RGB)
        {
            this.name = _name;
            this.RGB = _RGB;
            this.x0 = _x0;
            this.y0 = _y0;
        }
        public MessageData(string _name, Int16 _x0, Int16 _y0, Int16 _x1, Byte[] _RGB)
        {
            this.name = _name;
            this.RGB = _RGB;
            this.x0 = _x0;
            this.y0 = _y0;
            this.x1 = _x1;
        }
        public MessageData(string _name, Int16 _x0, Int16 _y0, Int16 _x1, Int16 _y1, Byte[] _RGB)
        {
            this.name = _name;
            this.RGB = _RGB;
            this.x0 = _x0;
            this.y0 = _y0;
            this.x1 = _x1;
            this.y1 = _y1;
        }
        public MessageData(string _name, Int16 _x0, Int16 _y0, Int16 _x1, Int16 _y1, Int16 _rounding, Byte[] _RGB)
        {
            this.name = _name;
            this.RGB = _RGB;
            this.x0 = _x0;
            this.y0 = _y0;
            this.x1 = _x1;
            this.y1 = _y1;
            this.rounding = _rounding;
        }
        public MessageData(string _name, Int16 _x0, Int16 _y0, Int16 _x1, Int16 _y1, Byte[] _RGB, string _text)
        {
            this.name = _name;
            this.RGB = _RGB;
            this.x0 = _x0;
            this.y0 = _y0;
            this.x1 = _x1;
            this.y1 = _y1;
            this.text = _text;
        }
        public MessageData(string _name, Int16 _x0, Int16 _y0, System.Drawing.Image _img)
        {
            this.name = _name;
            this.x0 = _x0;
            this.y0 = _y0;
            this.img = _img;
        }
        public MessageData(string _name, Int16 _x0, Int16 _y0, Int16 _x1, System.Drawing.Image _img)
        {
            this.name = _name;
            this.x0 = _x0;
            this.y0 = _y0;
            this.x1 = _x1;
            this.img = _img;
        }
        public MessageData(string _name, Int16 _x0, Int16 _y0, Int16 _x1, Int16 _y1, System.Drawing.Image _img)
        {
            this.name = _name;
            this.x0 = _x0;
            this.y0 = _y0;
            this.x1 = _x1;
            this.y1 = _y1;
            this.img = _img;
        }
    }
}
