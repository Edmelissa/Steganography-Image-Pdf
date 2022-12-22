using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using Org.Mentalis.Security.Cryptography;
using System.IO;
using System.Drawing.Imaging;

//using System.Runtime.InteropServices;
//using PdfSharp.Pdf.Filters;

namespace КР
{
    public partial class Form1 : Form
    {
        public bool stegimageorpdf;
        private string filename; //путь к файлу контейнеру
        private string name; //имя файла контейнера

        private string[,] xreftable; //таблица перекрестных ссылок файла 
        private int numobj; 

        private Bitmap fileimage;

        private string filenamesource = ""; //путь к скрываемому файлу

        private bool textorimage; //тип скрываемого сообщения (0 - текст, 1 - картинка)

        private string textsource = ""; //текст скрываемого сообщения
        private string enctextsource = ""; //зашифрованный текст скрываемого сообщения

        private long maxsizeinf; //максимальная длина сообщения в битах
        private long sizeinf; //длина сообщения в битах

        public string key = ""; //ключ пользователя для шифрования
        public bool mode; //режим работы(true - скрытие ,false - извлечение)

        //Криптография
        private TripleDESCryptoServiceProvider DES3; // объект класса для криптоалгоритма
        private MD4CryptoServiceProvider MD4; // объект класса хеширования
        private RNGCryptoServiceProvider rand; // создание объекта для генерации случайной примеси        
        private byte[] randBytes; // буфер для случайной примеси
        private CryptoStream crypts; //объект для криптографического потока данных
        private MemoryStream mems; // объект класса для потока данных в оперативной памяти

        private byte[] pwd; // пароль пользователя
        private byte[] IV; // начальный вектор.
     
        public byte[] EncText(string text, int sizerand) //шифрование сообщения
        {
            text = "ШИФРОВАНИЕ " + text;

            if(!stegimageorpdf)
                pwd = Encoding.Unicode.GetBytes(key);
            else
                pwd = Encoding.Default.GetBytes(key);

            randBytes = new byte[sizerand];
            rand.GetBytes(randBytes); // соль

            byte[] pwds = new byte[pwd.Length + randBytes.Length]; // пароль + соль

            // добавление к паролю пользователя соли
            for (int i = 0; i < (pwd.Length + randBytes.Length); i++)
            {
                if (i < pwd.Length)
                    pwds[i] = pwd[i];
                else
                    pwds[i] = randBytes[i - pwd.Length];
            }

            if (!stegimageorpdf)
                pwds = Encoding.Unicode.GetBytes(Encoding.Unicode.GetString(pwds));
            else
                pwds = Encoding.Default.GetBytes(Encoding.Default.GetString(pwds));

            DES3.Key = Convert.FromBase64String(Convert.ToBase64String(MD4.ComputeHash(pwds))); // генерация ключа шифрования

            ICryptoTransform encryptor = DES3.CreateEncryptor(DES3.Key, IV); // создание объекта шифрования

            while (text.Length % 8 != 0)
            {
                text = text + '0';
            }

            byte[] fullfileb;
            if (!stegimageorpdf)
                fullfileb = Encoding.Unicode.GetBytes(text);
            else
                fullfileb = Encoding.Default.GetBytes(text);

            mems = new MemoryStream();
            crypts = new CryptoStream(mems, encryptor, CryptoStreamMode.Write);

            crypts.Write(fullfileb, 0, fullfileb.Length);
            crypts.FlushFinalBlock();
            crypts.Close();

            mems.Flush();
            byte[] encfile = mems.ToArray();

            DES3.Clear();
            mems.Close();

            byte[] bytestextsource = new byte[randBytes.Length + encfile.Length]; 

            // добавление к файлу пользователей соли
            for (int i = 0; i < (randBytes.Length + encfile.Length); i++)
            {
                if (i < randBytes.Length)
                    bytestextsource[i] = randBytes[i];
                else
                    bytestextsource[i] = encfile[i - randBytes.Length];
            }

            return bytestextsource;
        }

        public byte[] DecText(byte[] bytestextsource, int sizerand)
        {
            byte[] file = bytestextsource;

            randBytes = new byte[sizerand];
            byte[] defile = new byte[file.Length - sizerand];

            for (int i = 0; i < file.Length; i++)
            {
                if (i < sizerand)
                    randBytes[i] = file[i];
                else
                    defile[i - sizerand] = file[i];
            }

            byte[] pwds = new byte[pwd.Length + randBytes.Length]; // пароль + соль

            // добавление к паролю пользователя соли
            for (int i = 0; i < (pwd.Length + randBytes.Length); i++)
            {
                if (i < pwd.Length)
                    pwds[i] = pwd[i];
                else
                    pwds[i] = randBytes[i - pwd.Length];
            }

            if (!stegimageorpdf)
               pwds = Encoding.Unicode.GetBytes(Encoding.Unicode.GetString(pwds));
            else
                pwds = Encoding.Default.GetBytes(Encoding.Default.GetString(pwds));

            DES3.Key = Convert.FromBase64String(Convert.ToBase64String(MD4.ComputeHash(pwds))); // генерация ключа шифрования

            ICryptoTransform decryptor = DES3.CreateDecryptor(DES3.Key, IV);

            mems = new MemoryStream(defile);

            crypts = new CryptoStream(mems, decryptor, CryptoStreamMode.Read);

            byte[] toEncrypt = new byte[defile.Length];

            Console.WriteLine(defile.Length);

            crypts.Read(toEncrypt, 0, toEncrypt.Length);

            crypts.Flush();
            crypts.Close();
            mems.Flush();
            mems.Close();

            return toEncrypt;
        }
        
        public string[,] XrefTable(string filename)
        {
            string[,] values = new string[0, 3];
            int kol;
            int firstobj = 0;

            int numxref = 0;

            int i = 0;
            bool endxref = false;

            bool isstream = false;

            string text1 = File.ReadAllText(filename, Encoding.Default);
            string[] alllines = text1.Split('\n');

            foreach (string s in alllines)
            {
                if (!isstream && s.Contains("xref"))
                {
                    numxref = i;
                }
                else if (!endxref && numxref != 0)
                {
                    if (s.Contains("trailer"))
                    {
                        endxref = true;
                    }
                    else if (s.Length < 18)
                    {
                        string[] subs = s.Split(' ');

                        firstobj = Convert.ToInt32(subs[0]);
                        kol = Convert.ToInt32(subs[1]);

                        values = new string[firstobj + kol, 3];
                    }
                    else if (s.Length >= 18)
                    {
                        string[] subs = s.Split(' ');

                        values[firstobj + i - numxref - 2, 0] = subs[0];
                        values[firstobj + i - numxref - 2, 1] = subs[1];
                        values[firstobj + i - numxref - 2, 2] = subs[2];
                    }
                }

                if (s.Contains("stream"))
                    isstream = true;
                else
                    isstream = false;

                i++;
            }
            return values;
        }

        public string PdfStegFun(string filename, byte[] text, int num)
        {
            string stext = Encoding.Default.GetString(text);

            string newobj;
            newobj = num.ToString() + " 0 obj\n<</Length " + stext.Length + ">> \nstream\n" + stext + "\nendstream\n" + "endobj \n";

            int offsetobj = newobj.Length + (num+1).ToString().Length - num.ToString().Length;

            string newxrefstr = "";

            string oldsizexrefstr = "";
            string newsizexrefstr = "";

            string newstartxreffile = "";

            string startfile = "";
            string xreffile = "";
            string middlefile = "";
            string startxreffile = "";
            string endfile = "";

            int i = 0;
            bool start = true;

            bool startxref = false;
            bool endxref = false;

            bool startstartxref = false;
            bool endstartxref = false;

            string text1 = File.ReadAllText(filename, Encoding.Default);
            string[] alllines = text1.Split('\n');

            foreach (string s in alllines)
            {
                if (s.Contains("xref"))
                {
                    start = false;
                    startxref = true;
                }
                if (s.Contains("trailer"))
                {
                    endxref = true;
                }
                if (s.Contains("startxref"))
                {
                    startstartxref = true;
                }

                if (start)
                {
                    startfile += s + '\n';
                }
                else if (!endxref && startxref)
                {
                    if (!s.Contains("xref"))
                    {
                        if (s.Length < 18)
                            oldsizexrefstr = s;
                        else
                            xreffile += s + '\n';
                    }
                }
                else if (!endstartxref && startstartxref)
                {
                    if (!s.Contains("startxref"))
                    {
                        startxreffile += s;
                        endstartxref = true;
                    }
                }
                else if (!start && endxref && !startstartxref && !endstartxref)
                {
                    middlefile += s + '\n';
                }
                else
                {
                    endfile += s + '\n';
                }
                i++;
            }
                    

            string[] substr1 = oldsizexrefstr.Split(' ');
            newsizexrefstr = "xref\n" + substr1[0] + " " + ((numobj + 1)-Convert.ToInt32(substr1[0])).ToString() + "\n";

            string[] substr2 = xreffile.Split('\n');
            string str2 = substr2[substr2.Length - 2];
            string[] substr3 = str2.Split(' ');
            string str3 = substr3[0];
            int newoffset = Convert.ToInt32(str3) + offsetobj;
            str3 = newoffset.ToString();
            while (str3.Length < 10)
                str3 = "0" + str3;

            newstartxreffile = (Convert.ToInt32(startxreffile) + offsetobj + newsizexrefstr.Length - 5).ToString() + "\n";

            newxrefstr = str3 + " " + substr3[1] + " " + substr3[2] + "\n";

            string newfile = startfile + newobj + newsizexrefstr + xreffile + newxrefstr + middlefile + "startxref\n"+ newstartxreffile + endfile;

            return newfile;
        }

        public string PdfEncFun(string filename, int num)
        {
            string text1 = File.ReadAllText(filename, Encoding.Default);
            string[] alllines = text1.Split('\n');

            string obj = num.ToString() + " 0 obj";

            bool startobj = false;
            int i = 0;

            string text = "";

            foreach (string s in alllines)
            {
                if (s.Contains(obj))
                {
                    startobj = true;
                }
                if(s.Contains("endstream"))
                {
                    startobj = false;
                }

                if(startobj)
                {                    
                    if(i > 2)
                    {
                        text += s + '\n';
                    }
                    i++;
                }
            }

            return text;
        }
       
        public Form1()
        {
            InitializeComponent();
            MainPanel.Visible = false;
            MainGB.Visible = false;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void выбратьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if(!stegimageorpdf)
            {
                openFileDialog1.Filter = "png files (*.png)|*.png";
                openFileDialog1.RestoreDirectory = false;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filename = openFileDialog1.FileName;
                    var fi1 = new FileInfo(filename);
                    name = fi1.Name;

                    fileimage = new Bitmap(filename);

                    RBStegPict.Text = "Скрыть информацию внутрь изображений";
                    RBExtPict.Text = "Извлечь информацию из изображений";
                    MainPanel.Visible = true;
                }
            }
            else
            {
                openFileDialog1.Filter = "pdf files (*.pdf)|*.pdf";
                openFileDialog1.RestoreDirectory = false;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filename = openFileDialog1.FileName;
                    var fi1 = new FileInfo(filename);
                    name = fi1.Name;

                    try
                    {
                        xreftable = XrefTable(filename);

                        numobj = xreftable.GetUpperBound(0) + 1;

                        RBStegPict.Text = "Скрыть информацию внутрь pdf";
                        RBExtPict.Text = "Извлечь информацию из pdf";
                        MainPanel.Visible = true;
                    }
                    catch
                    {
                        MessageBox.Show("В pdf-файле не найдена xref-таблица", "Ошибка",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                    /* неудачная попытка стеганографии картинки в пдф
                    for (int i = 0; i <= xreftable.GetUpperBound(0); i++)
                    {
                        if (xreftable[i, 1] != "65535" && IsImage(filestring, Convert.ToInt32(xreftable[i, 0])))
                        {
                            offsetobj = Convert.ToInt32(xreftable[i, 0]);
                            numobj = i;
                            break;
                        }
                    }

                    if (numobj < xreftable.GetUpperBound(0))
                    {
                        fileimage = ExtractImage(xreftable, filestring, offsetobj);
                        MainPanel.Visible = true;
                    }
                    else
                    {
                        MainPanel.Visible = false;
                    }
                    */
                }
            }

            LabelNameFile.Text = "Работа с файлом " + name;
        }

        private void RBStegPict_CheckedChanged(object sender, EventArgs e)//изменение рб скрытия в изображения  
        {
            if (RBStegPict.Checked)
            {
                MainGB.Visible = true;
                RBInfFile.Checked = false;
                RBInfText.Checked = false;
                MainGB.Text = "Ввод скрываемой информации";
                ButProc.Text = "Произвести сокрытие информации";
                ButProc.Enabled = false;
                TBTypeText.Text = "";
                CBEnc.Visible = true;
                CBEnc.Checked = false;
                mode = true;
            }
        }

        private void RBInfFile_CheckedChanged(object sender, EventArgs e)//изменение рб ввода/вывода через файл 
        {
            if (RBInfFile.Checked)
            {
                if(RBStegPict.Checked)
                {
                    OpenFileDialog openFileDialog1 = new OpenFileDialog();

                    openFileDialog1.Filter = "txt files (*.txt)|*.txt|png files (*.png)|*.png";
                    openFileDialog1.RestoreDirectory = false;

                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        filenamesource = openFileDialog1.FileName;

                    textorimage = Path.GetExtension(filenamesource) == ".png";  
                }

                if (RBStegPict.Checked && filenamesource != "" || !RBStegPict.Checked)
                {
                    ButProc.Enabled = true;
                }
            }
        }

        private void RBInfText_CheckedChanged(object sender, EventArgs e)//изменение рб ввода/вывода через тб
        {
            if (RBInfText.Checked)
            {
                textorimage = false;
                ButProc.Enabled = true;
            }
            else
            {
                ButProc.Enabled = false;
            }
        }

        private void RBExtPict_CheckedChanged(object sender, EventArgs e)//изменение рб извлечения в изображения
        {
            if(RBExtPict.Checked)
            {
                MainGB.Visible = true;
                RBInfFile.Checked = false;
                RBInfText.Checked = false;
                MainGB.Text = "Вывод скрываемой информации";
                ButProc.Text = "Произвести извлечение информации";
                ButProc.Enabled = false;
                TBTypeText.Text = "";
                CBEnc.Visible = false;
                mode = false;
            }
        }

        private void ButProc_Click(object sender, EventArgs e)
        {
            try 
            {
                // работа с картинкой
                if (!stegimageorpdf)
                {
                    Image im = (Image)fileimage;
                    if (mode)
                    {
                        ImageSteg image = new ImageSteg(im);

                        maxsizeinf = image.SizeImage() * 2;
                        Console.WriteLine("Максимальное кол-во бит информации " + maxsizeinf.ToString());

                        if (!textorimage)
                        {
                            //Получение скрываемого текста
                            if (RBInfText.Checked)
                                textsource = "СТЕГАНОГРАФИЯ " + TBTypeText.Text + " СТЕГАНОГРАФИЯ" + '\n';
                            else if (RBInfFile.Checked)
                                textsource = "СТЕГАНОГРАФИЯ " + File.ReadAllText(filenamesource) + " СТЕГАНОГРАФИЯ" + '\n';
                        }
                        else
                        {
                            Image imagesourse = Image.FromFile(filenamesource);
                            using (MemoryStream m = new MemoryStream())
                            {
                                imagesourse.Save(m, imagesourse.RawFormat);
                                byte[] imageBytes = m.ToArray();

                                textsource = Convert.ToBase64String(imageBytes);
                            }

                            Console.WriteLine("Картинка в байтах" + textsource.Length);

                            textsource = "КАРТИНКА СТЕГАНОГРАФИЯ " + textsource + " СТЕГАНОГРАФИЯ" + '\n';
                        }

                        byte[] bytestextsource;

                        //режим работы (с шифрованием или без)
                        if (CBEnc.Checked)
                        {
                            //Шифрование текста сообщения
                            int sizerand = image.SaltInf();
                            bytestextsource = EncText(textsource, sizerand);
                        }
                        else
                        {
                            bytestextsource = Encoding.Unicode.GetBytes(textsource);
                        }

                        sizeinf = bytestextsource.Length * 8;
                        Console.WriteLine("Кол-во бит информации " + sizeinf.ToString());

                        if (maxsizeinf >= sizeinf)
                        {
                            //функция сокрытия
                            Bitmap newimage = image.TextToImage(bytestextsource);

                            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                            saveFileDialog1.Filter = "png files (*.png)|*.png";
                            saveFileDialog1.RestoreDirectory = false;

                            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                                return;
                            string savefilename = saveFileDialog1.FileName;

                            Bitmap newimageformat = new Bitmap(newimage.Width, newimage.Height, fileimage.PixelFormat);
                            using (Graphics gr = Graphics.FromImage(newimageformat))
                            {
                                gr.DrawImage(newimage, new Rectangle(0, 0, newimageformat.Width, newimageformat.Height));
                            }
                            newimageformat.Save(savefilename, ImageFormat.Png);

                            /* неудачная попытка стеганографии в картинки пдф
                            Bitmap bm = new Bitmap("newimg.png");

                            byte[ ] data = new byte[3 * bm.Height * bm.Width];

                            for (int i = 0; i < bm.Height; i++)
                            {
                                for(int j = 0; j < bm.Width; j++)
                                {
                                    Color color = bm.GetPixel(j, i);
                                    data[i * 3 * bm.Width + j * 3] = color.R;
                                    data[i * 3 * bm.Width + j * 3 + 1] = color.G;
                                    data[i * 3 * bm.Width + j * 3 + 2] = color.B;
                                }
                            }

                            byte[] newfile = InsertImage(filestring, offsetobj, numobj, data);
                            File.WriteAllBytes(savefilename, newfile);
                            */
                        }
                        else
                        {
                            MessageBox.Show("Невозможно скрыть даннный объем информации", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }

                    if (!mode)
                    {
                        //функция извлечения
                        ImageSteg image = new ImageSteg(im);
                        byte[] bytestextsource = image.ImageToText();

                        Console.WriteLine(Encoding.Unicode.GetString(bytestextsource));

                        if (!Encoding.Unicode.GetString(bytestextsource).Contains("СТЕГАНОГРАФИЯ"))
                        {
                            KeyForm kf = new KeyForm();
                            kf.f1 = this;
                            kf.ShowDialog();
                            pwd = Encoding.Unicode.GetBytes(key);

                            //Расшифрование текста сообщения
                            int sizerand = image.SaltInf();
                            enctextsource = Encoding.Unicode.GetString(DecText(bytestextsource, sizerand));

                            if (enctextsource.Contains("ШИФРОВАНИЕ"))
                            {
                                textsource = enctextsource.Substring(11);

                                if (textsource.Contains("КАРТИНКА"))
                                {
                                    textsource = textsource.Substring(9);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Неверный ключ", "Ошибка",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        else
                        {
                            textsource = Encoding.Unicode.GetString(bytestextsource);
                            if (Encoding.Unicode.GetString(bytestextsource).Contains("КАРТИНКА"))
                            {
                                textsource = textsource.Substring(9);
                            }
                        }

                        textsource = textsource.Substring(14);

                        Console.WriteLine(textsource);

                        textsource = textsource.Substring(0, textsource.LastIndexOf(" СТЕГАНОГРАФИЯ"));

                        if (RBInfText.Checked)
                        {
                            TBTypeText.Text = textsource;
                        }
                        else if (RBInfFile.Checked)
                        {
                            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|png files (*.png)|*.png";
                            saveFileDialog1.RestoreDirectory = false;

                            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                                return;
                            string savefilename = saveFileDialog1.FileName;

                            if (Path.GetExtension(savefilename) == ".png")
                            {
                                Console.WriteLine(textsource.Length);
                                byte[] data = Convert.FromBase64String(textsource);
                                using (var stream = new MemoryStream(data, 0, data.Length))
                                {
                                    Image newimage = Image.FromStream(stream);
                                    newimage.Save(savefilename, ImageFormat.Png);
                                }

                            }
                            else if (Path.GetExtension(savefilename) == ".txt")
                            {
                                StreamWriter writer = new StreamWriter(savefilename);
                                writer.Write(textsource);
                                writer.Dispose();
                                writer.Close();
                            }
                        }
                    }

                }
                // работа с пдф
                else
                {
                    if (mode)
                    {
                        maxsizeinf = 20000;
                        Console.WriteLine("Максимальное кол-во бит информации " + maxsizeinf.ToString());

                        if (!textorimage)
                        {
                            //Получение скрываемого текста
                            if (RBInfText.Checked)
                                textsource = "СТЕГАНОГРАФИЯ " + TBTypeText.Text + " СТЕГАНОГРАФИЯ" + '\n';
                            else if (RBInfFile.Checked)
                                textsource = "СТЕГАНОГРАФИЯ " + File.ReadAllText(filenamesource, Encoding.Default) + " СТЕГАНОГРАФИЯ" + '\n';

                            Console.WriteLine(textsource);                                
                        }
                        else
                        {
                            Image imagesourse = Image.FromFile(filenamesource);
                            using (MemoryStream m = new MemoryStream())
                            {
                                imagesourse.Save(m, imagesourse.RawFormat);
                                byte[] imageBytes = m.ToArray();

                                textsource = Convert.ToBase64String(imageBytes);
                            }

                            Console.WriteLine("Картинка в байтах" + textsource.Length);

                            textsource = "КАРТИНКА СТЕГАНОГРАФИЯ " + textsource + " СТЕГАНОГРАФИЯ" + '\n';
                        }

                        byte[] bytestextsource;

                        //режим работы (с шифрованием или без)
                        if (CBEnc.Checked)
                        {
                            //Шифрование текста сообщения
                            int sizerand = 8;
                            bytestextsource = EncText(textsource, sizerand);
                        }
                        else
                        {
                            bytestextsource = Encoding.Default.GetBytes(textsource);
                        }

                        Console.WriteLine("зашифрованный " + Encoding.Default.GetString(bytestextsource));

                        sizeinf = bytestextsource.Length * 8;
                        Console.WriteLine("Кол-во бит информации " + sizeinf.ToString());

                        if (maxsizeinf >= sizeinf)
                        {
                            //функция сокрытия
                            string newfile = PdfStegFun(filename, bytestextsource, numobj);

                            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                            saveFileDialog1.Filter = "pdf files (*.pdf)|*.pdf";
                            saveFileDialog1.RestoreDirectory = false;

                            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                                return;
                            string savefilename = saveFileDialog1.FileName;

                            //File.WriteAllText(savefilename, newfile, Encoding.Default);
                            using (StreamWriter writetext = new StreamWriter(savefilename, false, Encoding.Default))
                            {
                                string[] newfilestrings = newfile.Split('\n');                               
                                foreach (string s in newfilestrings)
                                {
                                    writetext.Write(s+'\n');
                                }
                            }

                            /* неудачная попытка стеганографии в картинки пдф
                            Bitmap bm = new Bitmap("newimg.png");

                            byte[ ] data = new byte[3 * bm.Height * bm.Width];

                            for (int i = 0; i < bm.Height; i++)
                            {
                                for(int j = 0; j < bm.Width; j++)
                                {
                                    Color color = bm.GetPixel(j, i);
                                    data[i * 3 * bm.Width + j * 3] = color.R;
                                    data[i * 3 * bm.Width + j * 3 + 1] = color.G;
                                    data[i * 3 * bm.Width + j * 3 + 2] = color.B;
                                }
                            }

                            byte[] newfile = InsertImage(filestring, offsetobj, numobj, data);
                            File.WriteAllBytes(savefilename, newfile);
                            */
                        }
                        else
                        {
                            MessageBox.Show("Невозможно скрыть даннный объем информации", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }

                    if (!mode)
                    {
                        //функция извлечения
                        textsource = PdfEncFun(filename, numobj - 1);
                        textsource = textsource.Substring(0, textsource.Length - 1);

                        Console.WriteLine(textsource);

                        if (!textsource.Contains("СТЕГАНОГРАФИЯ"))
                        {
                            KeyForm kf = new KeyForm();
                            kf.f1 = this;
                            kf.ShowDialog();
                            pwd = Encoding.Default.GetBytes(key);

                            //Расшифрование текста сообщения
                            byte[] bytestextsource = Encoding.Default.GetBytes(textsource);
                            int sizerand = 8;

                            Console.WriteLine("зашифрованный поток " + Encoding.Default.GetString(bytestextsource));

                            enctextsource = Encoding.Default.GetString(DecText(bytestextsource, sizerand));

                            Console.WriteLine(enctextsource);

                            if (enctextsource.Contains("ШИФРОВАНИЕ"))
                            {
                                textsource = enctextsource.Substring(11);

                                if (textsource.Contains("КАРТИНКА"))
                                {
                                    textsource = textsource.Substring(9);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Неверный ключ", "Ошибка",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        else
                        {
                            if (textsource.Contains("КАРТИНКА"))
                            {
                                textsource = textsource.Substring(9);
                            }
                        }

                        textsource = textsource.Substring(14);

                        textsource = textsource.Substring(0, textsource.LastIndexOf(" СТЕГАНОГРАФИЯ"));

                        if (RBInfText.Checked)
                        {
                            TBTypeText.Text = textsource;
                        }
                        else if (RBInfFile.Checked)
                        {
                            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|png files (*.png)|*.png";
                            saveFileDialog1.RestoreDirectory = false;

                            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                                return;
                            string savefilename = saveFileDialog1.FileName;

                            if (Path.GetExtension(savefilename) == ".png")
                            {
                                Console.WriteLine(textsource.Length);
                                byte[] data = Convert.FromBase64String(textsource);
                                using (var stream = new MemoryStream(data, 0, data.Length))
                                {
                                    Image newimage = Image.FromStream(stream);
                                    newimage.Save(savefilename, ImageFormat.Png);
                                }

                            }
                            else if (Path.GetExtension(savefilename) == ".txt")
                            {
                                File.WriteAllText(savefilename, textsource, Encoding.Default);
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm af;
            af = new AboutForm();
            af.ShowDialog();
        }

        private void CBEnc_CheckedChanged(object sender, EventArgs e)
        {
            if(CBEnc.Checked)
            {
                KeyForm kf = new KeyForm();
                kf.f1 = this;
                kf.ShowDialog();
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Selectfun sf = new Selectfun();
            sf.f1 = this;
            sf.ShowDialog();

            DES3 = new TripleDESCryptoServiceProvider();
            DES3.Padding = PaddingMode.None;
            IV = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 };// получение случайного начального вектора
            DES3.Mode = CipherMode.CFB; // установка режима блочного шифрования

            MD4 = new MD4CryptoServiceProvider();
            rand = new RNGCryptoServiceProvider();
        }
    }
}
