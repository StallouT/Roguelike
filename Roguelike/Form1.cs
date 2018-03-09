using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roguelike
{
    public partial class Form1 : Form
    {
        public Font font;
        public PrivateFontCollection privateFont = new PrivateFontCollection();
        public Random random = new Random();
        public Color background = Color.FromArgb(0, 0, 0);
        public Color borderColor = Color.FromArgb(24, 24, 24);
        public Color accessoryBorderColor = Color.FromArgb(48, 48, 48);
        public Color florColor = Color.FromArgb(31, 28, 30);
        public Color accessoryFlorColor = Color.FromArgb(0, 0, 0);
        public Color heroColor = Color.FromArgb(255, 32, 32);
        public List<int> shift = new List<int> { 0, 0 };


        public enum Brushes : byte
        {
            DiagonalBrick,
            DiagonalCross,
            LargeCheckerBoard,
            LargeConfetti,
            Weave,
            Shingle,
        }

        ////
        public List<Rectangle> flor = new List<Rectangle> { };
        ////


        public Rectangle[] wallRectangles = new Rectangle[20];
        public Rectangle[] florRectangles = new Rectangle[20];
        //
        #region Way
        public Rectangle[] way = new Rectangle[20];
        #endregion
        //

        BufferedGraphics bufferedGraphics;
        BufferedGraphicsContext bufferedGraphicsContext = new BufferedGraphicsContext();




        //Ширина, Высота, Толщина
        //int x = 2560;
        //int y = 1600;
        //int width = 80;


        int x = 1280;
        int y = 800;
        int width = 20;

        #region Текстуры

        //uE904
        //uE908
        //Ступеньки

        //uEE94
        //Таблетка

        //uE129
        //Флаг

        //uE9F3
        //Крест

        //uEB52
        //uEB51
        //uEA92
        //Жизни

        //graphics.DrawIcon
        //Иконка

        //graphics.RotateTransform(30);
        //Опьянение

        //Brush brush = new HatchBrush(HatchStyle.Divot, fillColor);
        //Brush brush = new HatchBrush(HatchStyle.Percent05, fillColor);
        //Ловушка, шипы

        //Brush brush = new HatchBrush(HatchStyle.DiagonalBrick, fillColor);
        //Brush brush = new HatchBrush(HatchStyle.DiagonalCross, fillColor);
        //Brush brush = new HatchBrush(HatchStyle.LargeCheckerBoard, fillColor);
        //Brush brush = new HatchBrush(HatchStyle.LargeConfetti, fillColor);
        //Плитка

        //Brush brush = new HatchBrush(HatchStyle.DottedGrid, fillColor);
        //Brush brush = new HatchBrush(HatchStyle.LargeGrid, fillColor);
        //Brush brush = new HatchBrush(HatchStyle.SmallGrid, fillColor);
        //Решетка

        //Brush brush = new HatchBrush(HatchStyle.Weave, fillColor);
        //Ковер

        //Brush brush = new HatchBrush(HatchStyle.HorizontalBrick, fillColor);
        //Стена

        //Brush brush = new HatchBrush(HatchStyle.Shingle, fillColor);
        //Кости

        //Brush brush = new HatchBrush(HatchStyle.Wave, fillColor);
        //Brush brush = new HatchBrush(HatchStyle.ZigZag, fillColor);
        //Вода
        #endregion

        public Form1()
        {
            InitializeComponent();
            //HP.Font = new Font("Segoe MDL2 Assets", 16);
            //HP.ForeColor = heroColor;
            //HP.Text = "\uEB52 \uEB52 \uEB52 \uEA92 \uEB51";
            
        }


        public void CreateFont()
        {
            privateFont.AddFontFile(@"font\Finalnew.ttf");
            privateFont.AddFontFile(@"font\LongaIberica.ttf");
            font = new Font(privateFont.Families[1], 12);
        }

        public void CreatePosition(Graphics graphics, List<int> shift)
        {
            Brush brush = new SolidBrush(heroColor);
            //Отрисовка комнат
            //Стены
            brush = new HatchBrush(HatchStyle.HorizontalBrick, borderColor, accessoryBorderColor);
            graphics.FillRectangles(brush, wallRectangles);

            //Пол
            brush = new HatchBrush(HatchStyle.LargeConfetti, florColor, accessoryFlorColor);
            graphics.FillRectangles(brush, florRectangles);

            //foreach (Rectangle florRectangle in florRectangles)
            //{
            //    brush = new HatchBrush((HatchStyle)((Brushes)random.Next(0, 6)), florColor, accessoryFlorColor);
            //    graphics.FillRectangle(brush, florRectangle);
            //}



    brush = new SolidBrush(Color.Red);
    foreach (Rectangle rect in flor)
    {
        graphics.FillRectangle(brush, rect);
    }


            //
            #region Way      
            //graphics.FillRectangles(new HatchBrush(HatchStyle.LargeConfetti, fillColor), way);
            #endregion
            //

            //Создание героя
            brush = new SolidBrush(heroColor);
            Rectangle rectangle = new Rectangle((x / 2 - width / 2) - shift[0], (y / 2 - width / 2) - shift[1], width, width);
            //graphics.FillRectangle(brush, rectangle);

            brush = new SolidBrush(Color.White);
            //uE739     Квадрат
            //uE805
            //uE822
            graphics.DrawString("\uE805", new Font("Segoe MDL2 Assets", width - width/4), brush, rectangle, StringFormat.GenericTypographic);

            #region
            //Собака
            //brush = new SolidBrush(Color.White);
            //graphics.DrawString("@", new Font(privateFont.Families[0], 14), brush, rectangle);

            //Иконка
            //Icon icon = new Icon(@"icon\Fighter256.ico");
            //graphics.DrawIcon(icon, rectangle);

            //Обводка вокруг гг
            //Pen pen = new Pen(heroLineColor, 1);
            //graphics.DrawRectangle(pen, rectangle);
            #endregion
        }

        public void RedrawField(Graphics graphics)
        {
            Pen pen = new Pen(borderColor, width);
            Brush brush = new SolidBrush(background);
            //brush = new HatchBrush(HatchStyle.Min, Color.Red);

            graphics.DrawRectangle(pen, 0, 0, x, y);
            graphics.FillRectangle(brush, width / 2, width / 2, x - width, y - width);


        }

        public bool Contains(Rectangle rectangle)
        {
            for (int i = 0; i < wallRectangles.Length; i++)
            {
                //
                #region Way
                //if (florRectangles[i].Contains(rectangle.X, rectangle.Y) || way[i].Contains(rectangle.X, rectangle.Y))
                #endregion
                //
                if (florRectangles[i].Contains(rectangle.X, rectangle.Y))
                {
                    Console.WriteLine("Внутри");
                    return true;
                }
            }
            Console.WriteLine("Снаружи? Нет");
            return false;
        }




        //public bool Test(Rectangle rectangle, Rectangle TrueRectangle)
        //{
        //    for (int i = 0; i < flor.Count; i++)
        //    {
        //        if (TrueRectangle.Contains(rectangle.X, rectangle.Y) || !(florRectangles[i].Contains(rectangle.X, rectangle.Y)))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //public bool CreateWay(Rectangle TrueRectangle)
        //{
        //    Rectangle rectangle = new Rectangle();
        //    do
        //    {
        //        switch (random.Next(0, 8))
        //        {
        //            case 0:
        //                rectangle = new Rectangle(new Point(TrueRectangle.X, TrueRectangle.Y + width * random.Next(1, TrueRectangle.Height / width)), new Size(width, width));
        //                if (!Contains(rectangle))
        //                {
        //                    flor.Add(rectangle);
        //                }
        //                break;
        //            case 1:
        //                rectangle = new Rectangle(new Point(TrueRectangle.X, TrueRectangle.Y - width * random.Next(1, TrueRectangle.Height / width)), new Size(width, width));
        //                if (!Contains(rectangle))
        //                {
        //                    flor.Add(rectangle);
        //                }
        //                break;
        //            case 2:
        //                rectangle = new Rectangle(new Point(TrueRectangle.X + width * random.Next(1, TrueRectangle.Width / width), TrueRectangle.Y), new Size(width, width));
        //                if (!Contains(rectangle))
        //                {
        //                    flor.Add(rectangle);
        //                }
        //                break;
        //            case 3:
        //                rectangle = new Rectangle(new Point(TrueRectangle.X - width * random.Next(1, TrueRectangle.Width / width), TrueRectangle.Y), new Size(width, width));
        //                if (!Contains(rectangle))
        //                {
        //                    flor.Add(rectangle);
        //                }
        //                break;
        //            case 4:
        //                rectangle = new Rectangle(new Point(TrueRectangle.X + width * random.Next(1, TrueRectangle.Width / width), TrueRectangle.Y + width * random.Next(1, TrueRectangle.Height / width)), new Size(width, width));
        //                if (!Contains(rectangle))
        //                {
        //                    flor.Add(rectangle);
        //                }
        //                break;
        //            case 5:
        //                rectangle = new Rectangle(new Point(TrueRectangle.X + width * random.Next(1, TrueRectangle.Width / width), TrueRectangle.Y - width * random.Next(1, TrueRectangle.Height / width)), new Size(width, width));
        //                if (!Contains(rectangle))
        //                {
        //                    flor.Add(rectangle);
        //                }
        //                break;
        //            case 6:
        //                rectangle = new Rectangle(new Point(TrueRectangle.X - width * random.Next(1, TrueRectangle.Width / width), TrueRectangle.Y + width * random.Next(1, TrueRectangle.Height / width)), new Size(width, width));
        //                if (!Contains(rectangle))
        //                {
        //                    flor.Add(rectangle);
        //                }
        //                break;
        //            case 7:
        //                rectangle = new Rectangle(new Point(TrueRectangle.X - width * random.Next(1, TrueRectangle.Width / width), TrueRectangle.Y - width * random.Next(1, TrueRectangle.Height / width)), new Size(width, width));
        //                if (!Contains(rectangle))
        //                {
        //                    flor.Add(rectangle);
        //                }
        //                break;
        //        }
        //    }
        //    while (Test(rectangle, TrueRectangle));
        //    return false;
        //}

        public void CreateWay(Rectangle[] wallRectangles)
        {
            int x1, y1, x2, y2;
            for (int i = 0; i < this.wallRectangles.Length; i++)
            //for (int i = 0; i < 1; i++)
            {
                x1 = wallRectangles[i].X + wallRectangles[i].Width / 2 - width / 2;
                y1 = wallRectangles[i].Y + wallRectangles[i].Height / 2 - width / 2;
                x2 = wallRectangles[i].Width;
                y2 = wallRectangles[i].Height;
                //Rectangle rectangle = new Rectangle();
                //do
                //{
                //    switch (random.Next(0, 8))
                //    {
                //        case 0:
                //            rectangle = new Rectangle(new Point(x1, y1 + width * random.Next(1, y2 / width)), new Size(width, width));
                //            break;
                //        case 1:
                //            rectangle = new Rectangle(new Point(x1, y1 - width * random.Next(1, y2 / width)), new Size(width, width));
                //            break;
                //        case 2:
                //            rectangle = new Rectangle(new Point(x1 + width * random.Next(1, x2 / width), y1), new Size(width, width));
                //            break;
                //        case 3:
                //            rectangle = new Rectangle(new Point(x1 - width * random.Next(1, x2 / width), y1), new Size(width, width));
                //            break;
                //        case 4:
                //            rectangle = new Rectangle(new Point(x1 + width * random.Next(1, x2 / width), y1 + width * random.Next(1, y2 / width)), new Size(width, width));
                //            break;
                //        case 5:
                //            rectangle = new Rectangle(new Point(x1 + width * random.Next(1, x2 / width), y1 - width * random.Next(1, y2 / width)), new Size(width, width));
                //            break;
                //        case 6:
                //            rectangle = new Rectangle(new Point(x1 - width * random.Next(1, x2 / width), y1 + width * random.Next(1, y2 / width)), new Size(width, width));
                //            break;
                //        case 7:
                //            rectangle = new Rectangle(new Point(x1 - width * random.Next(1, x2 / width), y1 - width * random.Next(1, y2 / width)), new Size(width, width));
                //            break;
                //    }
                //}
                //while (!Contains(rectangle));
                //Console.WriteLine(rectangle);
                //flor.Add(rectangle);
                //Console.WriteLine("++" + random.Next(1, y2 / width));



                int coofX = 1;
                int coofY = 1;

                Console.WriteLine($"Rectangle {i}:\t{coofX}");
                Console.WriteLine($"Rectangle {i}:\t{coofY}");


                flor.Add(new Rectangle(new Point(x1, y1 + width * coofY), new Size(width, width)));
                flor.Add(new Rectangle(new Point(x1, y1 - width * coofY), new Size(width, width)));
                flor.Add(new Rectangle(new Point(x1 + width * coofX, y1), new Size(width, width)));
                flor.Add(new Rectangle(new Point(x1 - width * coofX, y1), new Size(width, width)));
                //flor.Add(new Rectangle(new Point(x1 + width + coofX, y1 + width + coofY), new Size(width, width)));
                //flor.Add(new Rectangle(new Point(x1 + width + coofX, y1 - width + coofY), new Size(width, width)));
                //flor.Add(new Rectangle(new Point(x1 - width + coofX, y1 + width + coofY), new Size(width, width)));
                //flor.Add(new Rectangle(new Point(x1 - width + coofX, y1 - width + coofY), new Size(width, width)));

                //flor.Add(new Rectangle(new Point(x1, y1 + width * 2), new Size(width, width)));
                //flor.Add(new Rectangle(new Point(x1, y1 - width * 2), new Size(width, width)));
                //flor.Add(new Rectangle(new Point(x1 + width * 2, y1), new Size(width, width)));
                //flor.Add(new Rectangle(new Point(x1 - width * 2, y1), new Size(width, width)));
                //flor.Add(new Rectangle(new Point(x1 + width * 2, y1 + width * 2), new Size(width, width)));
                //flor.Add(new Rectangle(new Point(x1 + width * 2, y1 - width * 2), new Size(width, width)));
                //flor.Add(new Rectangle(new Point(x1 - width * 2, y1 + width * 2), new Size(width, width)));
                //flor.Add(new Rectangle(new Point(x1 - width * 2, y1 - width * 2), new Size(width, width)));


            }


        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            bufferedGraphics = bufferedGraphicsContext.Allocate(pictureBox1.CreateGraphics(), 
            new Rectangle(0, 0, pictureBox1.Width + Math.Abs(shift[0]), pictureBox1.Height + Math.Abs(shift[1])));


            OutputLabel.Text = "" + e.KeyCode;
            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                case Keys.NumPad8:
                    shift[1] += width;
                    //
                    //Достаточно только !(Contains())
                    //
                    if (shift[1] > (y / 2 - width / 2) || !(Contains(new Rectangle((x / 2 - width / 2) - shift[0], (y / 2 - width / 2) - shift[1], width, width))))
                    {
                        shift[1] -= width;
                    }
                    OutputLabel.Text = $"You move Up\n";
                    break;
                case Keys.D:
                case Keys.Right:
                case Keys.NumPad6:
                    shift[0] -= width;
                    if (shift[0] < -(x / 2 - width / 2) || !(Contains(new Rectangle((x / 2 - width / 2) - shift[0], (y / 2 - width / 2) - shift[1], width, width))))
                    {
                        shift[0] += width;
                    }
                    OutputLabel.Text = "You move Right\n";
                    break;
                case Keys.A:
                case Keys.Left:
                case Keys.NumPad4:
                    shift[0] += width;
                    if (shift[0] > (x / 2 - width / 2) || !(Contains(new Rectangle((x / 2 - width / 2) - shift[0], (y / 2 - width / 2) - shift[1], width, width))))
                    {
                        shift[0] -= width;
                    }
                    OutputLabel.Text = "You move Left\n";
                    break;
                case Keys.S:
                case Keys.Down:
                case Keys.NumPad2:
                    shift[1] -= width;
                    if (shift[1] < -(y / 2 - width / 2) || !(Contains(new Rectangle((x / 2 - width / 2) - shift[0], (y / 2 - width / 2) - shift[1], width, width))))
                    {
                        shift[1] += width;
                    };
                    OutputLabel.Text = "You move Down\n";
                    break;
                ///////////////////////////////////////////////
                case Keys.Home:
                case Keys.NumPad7:
                    shift[0] += width;
                    shift[1] += width;
                    if (shift[0] > (x / 2 - width / 2) || shift[1] > (y / 2 - width / 2) || !(Contains(new Rectangle((x / 2 - width / 2) - shift[0], (y / 2 - width / 2) - shift[1], width, width))))
                    {
                        shift[0] -= width;
                        shift[1] -= width;
                    }
                    OutputLabel.Text = $"You move Up.Left\n";


                    break;
                case Keys.PageUp:
                case Keys.NumPad9:
                    shift[0] -= width;
                    shift[1] += width;
                    if (shift[0] < -(x / 2 - width / 2) || shift[1] > (y / 2 - width / 2) || !(Contains(new Rectangle((x / 2 - width / 2) - shift[0], (y / 2 - width / 2) - shift[1], width, width))))
                    {
                        shift[0] += width;
                        shift[1] -= width;
                    }
                    OutputLabel.Text = "You move Up.Right\n";

                    break;
                case Keys.End:
                case Keys.NumPad1:
                    shift[0] += width;
                    shift[1] -= width;
                    if (shift[0] > (x / 2 - width / 2) || shift[1] < -(y / 2 - width / 2) || !(Contains(new Rectangle((x / 2 - width / 2) - shift[0], (y / 2 - width / 2) - shift[1], width, width))))
                    {
                        shift[0] -= width;
                        shift[1] += width;
                    }
                    OutputLabel.Text = "You move Down.Left\n";

                    break;
                case Keys.Next:
                case Keys.NumPad3:
                    shift[0] -= width;
                    shift[1] -= width;
                    if (shift[0] < -(x / 2 - width / 2) || shift[1] < -(y / 2 - width / 2) || !(Contains(new Rectangle((x / 2 - width / 2) - shift[0], (y / 2 - width / 2) - shift[1], width, width))))
                    {
                        shift[0] += width;
                        shift[1] += width;
                    }
                    OutputLabel.Text = "You move Down.Right\n";

                    break;


                default:
                    //refr(graphics);
                    //Обработка других клавиш
                    break;


            }
            //Сдвиг и очистка поля
            bufferedGraphics.Graphics.TranslateTransform((pictureBox1.Width - x) / 2 + shift[0], (pictureBox1.Height - y) / 2 + shift[1]);
            Console.WriteLine(shift[0] + "," + shift[1]);
            RedrawField(bufferedGraphics.Graphics);
            //Отрисовка комнат и персонажа
            CreatePosition(bufferedGraphics.Graphics, shift);
            label2.Text = $"Shift: {shift[0]}, {shift[1]}";
            pictureBox1.Refresh();
        }


        public bool ContainsPoint(int x, int y)
        {
            for (int i = 0; i < wallRectangles.Length; i++)
            {
                if (wallRectangles[i].Contains(x, y))
                {
                    Console.WriteLine("Содержится");
                    return true;
                }
            }
            Console.WriteLine("Не содержится");
            return false;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            bufferedGraphicsContext.MaximumBuffer = new Size(pictureBox1.Width + 1, pictureBox1.Height + 1);
            bufferedGraphics = bufferedGraphicsContext.Allocate(pictureBox1.CreateGraphics(), new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));

            CreateFont();
            label1.Font = new Font(privateFont.Families[0], 18);
            label1.Font = new Font(privateFont.Families[0], 18);
            OutputLabel.Font = new Font(privateFont.Families[1], 24);

            florRectangles[0] = new Rectangle((x / 2 - width / 2) - width - shift[0], (y / 2 - width / 2) - width - shift[1], width * 3, width * 3);
            wallRectangles[0] = new Rectangle((x / 2 - width / 2) - width * 2 - shift[0], (y / 2 - width / 2) - width * 2 - shift[1], width * 5, width * 5);
            //
            #region Way
            //way[0] = new Rectangle(new Point((x / 2 - width / 2) - shift[0], (y / 2 - width / 2) - width * 2 - shift[1]), new Size(width, width));
            //way[1] = new Rectangle(new Point((x / 2 - width / 2) - shift[0], (y / 2 - width / 2) + width * 2 - shift[1]), new Size(width, width));
            //way[2] = new Rectangle(new Point((x / 2 - width / 2) - width * 2 - shift[0], (y / 2 - width / 2) - shift[1]), new Size(width, width));
            //way[3] = new Rectangle(new Point((x / 2 - width / 2) + width * 2 - shift[0], (y / 2 - width / 2) - shift[1]), new Size(width, width));
            #endregion
            //
            for (int i = 1; i < wallRectangles.Length; i++)
            {
                int x1, y1, x2, y2;
                x2 = width * random.Next(4, 11);
                y2 = width * random.Next(4, 11);
                do
                {
                    x1 = width * random.Next(1, (x - x2) / width) - width / 2;
                    y1 = width * random.Next(1, (y - y2) / width) - width / 2;
                }
                while (ContainsPoint(x1, y1));
                //OutputLabel.Text += $"\n{x1},{y1},\t{x2},{y2}";
                florRectangles[i] = new Rectangle(x1, y1, x2, y2);
                wallRectangles[i] = new Rectangle(x1 - width, y1 - width, x2 + width * 2, y2 + width * 2);
            }
            this.Focus();


            CreateWay(wallRectangles);


        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            bufferedGraphics.Render(e.Graphics);
        }
    }

    //class MyPanel : Panel
    //{
    //    public MyPanel()
    //    {
    //        BackColor = Color.FromArgb(16, 16, 16);
    //        //this.DoubleBuffered = true;
    //        //this.ResizeRedraw = true;
    //    }
    //}

    //    class MyPictureBox : PictureBox
    //{
    //    public MyPictureBox()
    //    {
    //        this.DoubleBuffered = true;
    //        this.ResizeRedraw = true;
    //    }
    //}

}