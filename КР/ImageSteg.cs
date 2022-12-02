using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;

namespace КР
{
    class ImageSteg
    {
        private Bitmap image;
        private Bitmap newimage;
        private int salt;
        private string depth;
         
        public ImageSteg(Image img)
        {            
            image = new Bitmap(img);
            newimage = new Bitmap(img);
            salt = 8 + ((image.Width * image.Height) % 8);
            depth = img.PixelFormat.ToString();

            Console.WriteLine(depth);
        }

        public int SaltInf()
        {
            return salt;
        }

        public long SizeImage()
        {
            long size = image.Width * image.Height;
            if (depth == "Format32bppArgb")
                size = size * 4; // кол-во пикселей умноженное на кол-во байт под RGBA
            else if (depth == "Format24bppRgb")
                size = size * 3; // кол-во пикселей умноженное на кол-во байт под RGBA

            return size;
        }
        
        public Bitmap TextToImage(byte[] bytestext)
        {
            BitArray bitstext = new BitArray(bytestext);

            if (depth == "Format32bppArgb")
            {
                for (int i = 0; i < image.Width; i++)
                {
                    for (int j = 0; j < image.Height; j++)
                    {
                        Color pixelColor = image.GetPixel(i, j); //получение цвета пикселя

                        //значение каждого цвета (0-255)
                        byte alpha = pixelColor.A;
                        byte red = pixelColor.R;
                        byte green = pixelColor.G;
                        byte blue = pixelColor.B;


                        //массив бит каждого цвета
                        BitArray bitsalpha = new BitArray(8);
                        BitArray bitsred = new BitArray(8);
                        BitArray bitsgreen = new BitArray(8);
                        BitArray bitsblue = new BitArray(8);

                        //запись битов каждого цвета + скрытие текста в последних 2 битах        
                        for (int k = 0; k < 8; k++)
                        {
                            if (k > 1)
                            {
                                if ((alpha >> k & 1) == 1)
                                    bitsalpha[k] = true;
                                else
                                    bitsalpha[k] = false;

                                if ((red >> k & 1) == 1)
                                    bitsred[k] = true;
                                else
                                    bitsred[k] = false;

                                if ((green >> k & 1) == 1)
                                    bitsgreen[k] = true;
                                else
                                    bitsgreen[k] = false;

                                if ((blue >> k & 1) == 1)
                                    bitsblue[k] = true;
                                else
                                    bitsblue[k] = false;
                            }
                            else
                            {
                                int l = k * 4 + j * 8 + i * image.Height * 8;
                                if (l < bitstext.Length)
                                {
                                    bitsalpha[k] = bitstext[l];
                                }
                                else
                                {
                                    if ((alpha >> k & 1) == 1)
                                        bitsalpha[k] = true;
                                    else
                                        bitsalpha[k] = false;
                                }
                                if (l + 1 < bitstext.Length)
                                {
                                    bitsred[k] = bitstext[l + 1];
                                }
                                else
                                {
                                    if ((red >> k & 1) == 1)
                                        bitsred[k] = true;
                                    else
                                        bitsred[k] = false;

                                }
                                if (l + 2 < bitstext.Length)
                                {
                                    bitsgreen[k] = bitstext[l + 2];
                                }
                                else
                                {
                                    if ((green >> k & 1) == 1)
                                        bitsgreen[k] = true;
                                    else
                                        bitsgreen[k] = false;

                                }
                                if (l + 3 < bitstext.Length)
                                {
                                    bitsblue[k] = bitstext[l + 3];
                                }
                                else
                                {
                                    if ((blue >> k & 1) == 1)
                                        bitsblue[k] = true;
                                    else
                                        bitsblue[k] = false;
                                }
                            }
                        }

                        //преобразование массива бит в байты каждого цвета
                        byte newalpha = 0;
                        byte newred = 0;
                        byte newgreen = 0;
                        byte newblue = 0;

                        for (int k = 0; k < 8; k++)
                        {
                            if (bitsalpha[k])
                                newalpha += (byte)Math.Pow(2, k);
                            if (bitsred[k])
                                newred += (byte)Math.Pow(2, k);
                            if (bitsgreen[k])
                                newgreen += (byte)Math.Pow(2, k);
                            if (bitsblue[k])
                                newblue += (byte)Math.Pow(2, k);
                        }

                        Color newpixelColor = Color.FromArgb(newalpha, newred, newgreen, newblue);

                        newimage.SetPixel(i, j, newpixelColor);
                    }
                }
            }
            else if(depth == "Format24bppRgb")
            {
                for (int i = 0; i < image.Width; i++)
                {
                    for (int j = 0; j < image.Height; j++)
                    {
                        Color pixelColor = image.GetPixel(i, j); //получение цвета пикселя

                        //значение каждого цвета (0-255)
                        byte red = pixelColor.R;
                        byte green = pixelColor.G;
                        byte blue = pixelColor.B;

                        //массив бит каждого цвета
                        BitArray bitsred = new BitArray(8);
                        BitArray bitsgreen = new BitArray(8);
                        BitArray bitsblue = new BitArray(8);

                        //запись битов каждого цвета + скрытие текста в последних 2 битах        
                        for (int k = 0; k < 8; k++)
                        {
                            if (k > 1)
                            {
                                if ((red >> k & 1) == 1)
                                    bitsred[k] = true;
                                else
                                    bitsred[k] = false;

                                if ((green >> k & 1) == 1)
                                    bitsgreen[k] = true;
                                else
                                    bitsgreen[k] = false;

                                if ((blue >> k & 1) == 1)
                                    bitsblue[k] = true;
                                else
                                    bitsblue[k] = false;
                            }
                            else
                            {
                                int l = k * 3 + j * 6 + i * image.Height * 6;
                                if (l < bitstext.Length)
                                {
                                    bitsred[k] = bitstext[l];
                                }
                                else
                                {
                                    if ((red >> k & 1) == 1)
                                        bitsred[k] = true;
                                    else
                                        bitsred[k] = false;

                                }
                                if (l + 1 < bitstext.Length)
                                {
                                    bitsgreen[k] = bitstext[l + 1];
                                }
                                else
                                {
                                    if ((green >> k & 1) == 1)
                                        bitsgreen[k] = true;
                                    else
                                        bitsgreen[k] = false;

                                }
                                if (l + 2 < bitstext.Length)
                                {
                                    bitsblue[k] = bitstext[l + 2];
                                }
                                else
                                {
                                    if ((blue >> k & 1) == 1)
                                        bitsblue[k] = true;
                                    else
                                        bitsblue[k] = false;
                                }
                            }
                        }

                        //преобразование массива бит в байты каждого цвета
                        byte newred = 0;
                        byte newgreen = 0;
                        byte newblue = 0;

                        for (int k = 0; k < 8; k++)
                        {
                            if (bitsred[k])
                                newred += (byte)Math.Pow(2, k);
                            if (bitsgreen[k])
                                newgreen += (byte)Math.Pow(2, k);
                            if (bitsblue[k])
                                newblue += (byte)Math.Pow(2, k);
                        }

                        Color newpixelColor = Color.FromArgb(newred, newgreen, newblue);

                        newimage.SetPixel(i, j, newpixelColor);
                    }
                }

            }
            return newimage;           
        }

        public byte[] ImageToText()
        {
            BitArray bitstext = new BitArray(Convert.ToInt32(this.SizeImage()*2));

            Console.WriteLine("Распаковка");

            byte[] text = new byte[bitstext.Length/8];

            if (depth == "Format32bppArgb")
            {
                for (int i = 0; i < image.Width; i++)
                {
                    for (int j = 0; j < image.Height; j++)
                    {
                        Color pixelColor = image.GetPixel(i, j); //получение цвета пикселя

                        //значение каждого цвета (0-255)
                        byte alpha = pixelColor.A;
                        byte red = pixelColor.R;
                        byte green = pixelColor.G;
                        byte blue = pixelColor.B;

                        //массив бит каждого цвета
                        BitArray bitsalpha = new BitArray(8);
                        BitArray bitsred = new BitArray(8);
                        BitArray bitsgreen = new BitArray(8);
                        BitArray bitsblue = new BitArray(8);

                        //запись битов каждого цвета + извлечение текста в последних 2 битах        
                        for (int k = 0; k < 8; k++)
                        {
                            if (k > 1)
                            {
                                if ((alpha >> k & 1) == 1)
                                    bitsalpha[k] = true;
                                else
                                    bitsalpha[k] = false;

                                if ((red >> k & 1) == 1)
                                    bitsred[k] = true;
                                else
                                    bitsred[k] = false;

                                if ((green >> k & 1) == 1)
                                    bitsgreen[k] = true;
                                else
                                    bitsgreen[k] = false;

                                if ((blue >> k & 1) == 1)
                                    bitsblue[k] = true;
                                else
                                    bitsblue[k] = false;
                            }
                            else
                            {
                                int l = k * 4 + j * 8 + i * image.Height * 8;
                                if ((alpha >> k & 1) == 1)
                                    bitstext[l] = true;
                                else
                                    bitstext[l] = false;

                                if ((red >> k & 1) == 1)
                                    bitstext[l + 1] = true;
                                else
                                    bitstext[l + 1] = false;

                                if ((green >> k & 1) == 1)
                                    bitstext[l + 2] = true;
                                else
                                    bitstext[l + 2] = false;

                                if ((blue >> k & 1) == 1)
                                    bitstext[l + 3] = true;
                                else
                                    bitstext[l + 3] = false;
                            }
                        }
                    }
                }
            }
            else if(depth == "Format24bppRgb")
            {
                for (int i = 0; i < image.Width; i++)
                {
                    for (int j = 0; j < image.Height; j++)
                    {
                        Color pixelColor = image.GetPixel(i, j); //получение цвета пикселя

                        //значение каждого цвета (0-255)
                        byte red = pixelColor.R;
                        byte green = pixelColor.G;
                        byte blue = pixelColor.B;

                        //массив бит каждого цвета
                        BitArray bitsred = new BitArray(8);
                        BitArray bitsgreen = new BitArray(8);
                        BitArray bitsblue = new BitArray(8);


                        //запись битов каждого цвета + извлечение текста в последних 2 битах        
                        for (int k = 0; k < 8; k++)
                        {
                            if (k > 1)
                            {
                                if ((red >> k & 1) == 1)
                                    bitsred[k] = true;
                                else
                                    bitsred[k] = false;

                                if ((green >> k & 1) == 1)
                                    bitsgreen[k] = true;
                                else
                                    bitsgreen[k] = false;

                                if ((blue >> k & 1) == 1)
                                    bitsblue[k] = true;
                                else
                                    bitsblue[k] = false;
                            }
                            else
                            {
                                int l = k * 3 + j * 6 + i * image.Height * 6;

                                if ((red >> k & 1) == 1)
                                    bitstext[l] = true;
                                else
                                    bitstext[l] = false;

                                if ((green >> k & 1) == 1)
                                    bitstext[l + 1] = true;
                                else
                                    bitstext[l + 1] = false;

                                if ((blue >> k & 1) == 1)
                                    bitstext[l + 2] = true;
                                else
                                    bitstext[l + 2] = false;
                            }
                        }
                    }
                }

            }

            bitstext.CopyTo(text, 0);

            Console.WriteLine(text.Length);

            return text;
        }     

    }
}
