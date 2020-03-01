using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ball
{
    public partial class Form1 : Form
    {
        //
        int mInPix = 20;
        int xcenter;
        int ycenter;
        int miliseconds;
        int frameInMiliseconds;
        int allTime;

        //Параметры меча
        //Начальная скорость
        double nSpeed = 0;
        //Начальная высота
        double nH = 0;
        //Коэффициэнт упругости
        double upr = 1;
        //g - ускорение свободного падения
        double g = -9.8;
        //Нахождение меча по оси X
        int ball_x = 400;
        //
        int timeVec = 0;

        private BufferedGraphics canvas;
        private Graphics graphics;
        private BufferedGraphicsContext context;

        Pen pen = new Pen(Color.Black);
        SolidBrush brush = new SolidBrush(Color.Black);

        //Конструктор формы
        public Form1()
        {
            InitializeComponent();
        }

        //Оброботчик события загрузки формы
        private void Form1_Load(object sender, EventArgs e)
        {
            //Объект для рисования на панели
            graphics = panel1.CreateGraphics();
            context = BufferedGraphicsManager.Current;
            canvas = context.Allocate(graphics, panel1.DisplayRectangle);

            //Точка (0, 0) на графике
            xcenter = 30;
            ycenter = panel1.Height;


        }

        //Метод отрисовки графика
        private void DrawOXY()
        {
            pen.Width = 10;
            canvas.Graphics.DrawLine(pen, 0, ycenter, panel1.Width, ycenter);
            pen.Width = 5;
            canvas.Graphics.DrawLine(pen, xcenter, 0, xcenter, ycenter);

            pen.Width = 5;
            int p = 1;
            int t = xcenter;
            while (t <= panel1.Width)
            {
                t += mInPix;
                canvas.Graphics.DrawLine(pen, t, ycenter - 8, t, ycenter);
            }
            p = 1;
            t = ycenter;
            while (t >= 0)
            {
                t -= mInPix;
                canvas.Graphics.DrawLine(pen, xcenter, t, xcenter + 6, t);
                if (p % 5 == 0)
                {
                    canvas.Graphics.DrawString(p.ToString(), this.Font, brush, xcenter - 17, t - 6);
                }
                p++;
            }
        }

        //Оброботчик изменения размера панели
        private void panel1_Resize(object sender, EventArgs e)
        {
            canvas.Graphics.Clear(Color.White);

            graphics = panel1.CreateGraphics();
            context = BufferedGraphicsManager.Current;
            canvas = context.Allocate(graphics, panel1.DisplayRectangle);

            xcenter = 30;
            ycenter = panel1.Height;

            //DrawOXY();
            //DrawBall(GetPoint(ball_x, (int)nH));
        }

        //Нажатие на кнопку Старт
        private void btnStart_Click(object sender, EventArgs e)
        {
            //Пробуем взять данные с формы
            try
            {
                nH = double.Parse(textBox2.Text);
                nSpeed = double.Parse(textBox6.Text);
                upr = double.Parse(textBox5.Text);
                g = double.Parse(textBox3.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Некоректные значения!", "Ошибка!");
                return;
            }

            //Определяем писелей в метре
            mInPix = (int)numericUpDown1.Value;
            //Время от отрисовки предыдущего кадра
            miliseconds = 0;
            //Все время
            allTime = 0;
            //Время требуемое для отрисовки кадра (время от отрисовки предыдущего кадра до следующего)
            frameInMiliseconds = 1000 / (int)numericUpDown2.Value;
            //Время полета меча от начала до нулевой высоты
            timeVec = 0;
            //Запускаем таймер
            timer1.Start();
        }

        //Обработчик каждого тика таймера
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Увеличиваем время от отрисовки предыдущего кадра
            miliseconds += timer1.Interval;
            //Прибовляем ко всему времени работы модели
            allTime += timer1.Interval;
            //Увеличиваем время полета в текущем направлении
            timeVec += timer1.Interval;
            //Если пришло время отрисовывать следующий кадр
            if (miliseconds >= frameInMiliseconds)
            {
                //Стираем все что было на панели
                canvas.Graphics.Clear(Color.White);
                //Рисуем график
                DrawOXY();
                //Получаем координаты и отрисовываем мяч
                DrawBall(GetNextPoint());
                //Выводим изображение
                canvas.Render();
                //Обнуляем время до следующего кадра
                miliseconds = 0;
                //Выводим время в текстбокс для времени
                int mm = allTime / 60000 % 60;
                int ss = allTime / 1000 % 60;
                int fff = allTime % 1000;
                textBox1.Text = mm.ToString("00") + ":" + ss.ToString("00") + ":" + fff.ToString("000");
            }

        }

        //Нажатие на кнопку стоп
        private void btnStop_Click(object sender, EventArgs e)
        {
            //Останавливаем таймер
            timer1.Stop();
        }
        //Метод получения координат меча
        private Point GetNextPoint()
        {
            //Текущее время
            double t = timeVec / 1000.0;
            //Текущая высота
            double h = nH + nSpeed * t + (g * t * t) / 2;
            //Текущая скорость
            double speed = nSpeed + g * t;
            //Если мяч достиг нулевой высоты, меняем направление меча
            if (h <= 0)
            {
                //Теперь начальная высота ноль
                nH = 0;
                //А начальная скорость = прошлая скорость по модулю умноженая на упругость 
                nSpeed = Math.Abs(speed) * upr;
                //Обнуляем время полета до нулевой высоты
                timeVec = 0;
                //Обнуляем скорость и высоту для вывода на текстбоксы
                speed = 0;
                h = 0;
            }
            //Выводим скорость и высоту на текстбоксы
            textBox7.Text = Math.Round(h).ToString();
            textBox4.Text = Math.Round(speed).ToString();
            //Вохвращаем точку отрисоки мяча
            return GetPoint(ball_x, (int)(h * mInPix));
        }
        //Метод перевода координат в точку на панели
        private Point GetPoint(int x, int y)
        {
            x += xcenter;
            y += ycenter;
            y = ycenter * 2 - y;
            return new Point(x, y);
        }
        //Метод рисования меча
        private void DrawBall(Point point)
        {
            brush.Color = Color.Red;
            int R = 30;
            canvas.Graphics.FillEllipse(brush, point.X - R, point.Y - R * 2, R * 2, R * 2);
            canvas.Graphics.DrawEllipse(pen, point.X - R, point.Y - R * 2, R * 2, R * 2);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(textBox2.Text, out double h))
            {
                nH = h;
                canvas.Graphics.Clear(Color.White);
                DrawOXY();
                DrawBall(GetPoint(ball_x, (int)nH * mInPix));
                canvas.Render();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //Определяем писелей в метре
            mInPix = (int)numericUpDown1.Value;
            canvas.Graphics.Clear(Color.White);
            DrawOXY();
            DrawBall(GetPoint(ball_x, (int)nH * mInPix));
            canvas.Render();
        }
    }
}
