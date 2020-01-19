//Справка по методам работы с классом string - https://metanit.com/sharp/tutorial/7.2.php

using System;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {   //Переменные описанные в этих фигурных скобочка ввидны везде (в пределах этих скобок, переменные заданные в теле класса называются "полями" (поле) )
        //Функции (они же подпрограммы возвращающие значение) заданные в теле класса называются "методами" (метод)

        //Активный знак ('n' я обозначил как 'none')
        char sign = 'n';
        //Если было нажато равно (пример посчитан)
        bool fend = false;
        //Конструктор формы обязательный метод который создает(инициализирует саму форму и ее комоненты(кнопки, лейблы, текстбоксы...)
        public Form1()
        {
            InitializeComponent();
        }
        //Метод обрабатывающий нажатия на цифровые кнопки и запятую.
        private void numbutton_Click(object sender, EventArgs e)
        {
            //Получаем текст кнопки
            string text = (sender as Button).Text;
            //Прибавляем текст кнопки к текстбоксу на котором записывается пример
            textBox1.Text += text;
        }
        //Метод обрабатывающий нажатия на цифровые знаки.
        private void signbutton_Click(object sender, EventArgs e)
        {
            //Если строка пуста, то знак ставить ненужно (чтобы небыло подобного "+23")
            if (textBox1.Text == "")
            {
                //return - выход из метода
                return;
            }
            //Если занк до этого никакой не применялся то
            if (sign == 'n')
            {
                //Задаем активный знак, знак мы берем из текста кнопки на которую мы нажали
                sign = (sender as Button).Text[0];
                //Прибавляем текст кнопки к текстбоксу на котором записывается пример
                textBox1.Text += sign;
            }
            //Иначе, то есть мы уже использовали какойто знак (данное условие можно полность удалить, если не нужна данная информация)
            else
            {
                //Выкидываем поле с сообщением о том что мы не можем поставить еще один знак
                MessageBox.Show("Калькулятор поддерживает только один занк!", "Ошибка!");
            }
        }

        //Метод обрабатывающий нажатие на равно.
        private void button12_Click(object sender, EventArgs e)
        {
            //Если нет знака("23" или "") или он идет последним в примере ("23+"), то пример не закончен и поэтому считать мы его не быдем
            if (sign == 'n' || textBox1.Text[textBox1.Text.Length - 1] == sign)
            {
                //return - выход из метода
                return;
            }
            //try - блок попытки выполнить код
            try
            {
                //Разбиваем пример на массив строк по знаку (было: "1+2"; стало: {"1", "2"}, Length = 2) подробный о комадле Split - https://docs.microsoft.com/ru-ru/dotnet/csharp/how-to/parse-strings-using-split#code-try-0
                string[] strs = textBox1.Text.Split(sign);
                //Преобразовываем левое число из string к double
                double number1 = Convert.ToDouble(strs[0]);
                //Преобразовываем правое число из string к double
                double number2 = Convert.ToDouble(strs[1]);
                //Переменная которая будет хранить результат
                double result = 0;
                //Просматриваем значения переменной sign
                switch (sign)
                {
                    //Если плюс
                    case '+':
                        result = number1 + number2;
                        break;
                    //Если минус
                    case '-':
                        result = number1 - number2;
                        break;
                    //Если умножить
                    case '×':
                        result = number1 * number2;
                        break;
                    //Если разделить
                    case '÷':
                        result = number1 / number2;
                        break;
                }
                //Добавляем результат в конец примера
                textBox1.Text += "=" + result;
                //Перебираем все компоненты на форме
                foreach (Control item in Controls)
                    //Делаем их неактивными (отключаем)
                    item.Enabled = false;
                //Включаем очищение 
                btn_clr.Enabled = true;
                //Включаем удаление левого символа 
                btn_del.Enabled = true;
                //Пример посчитан
                fend = true;
            }
            //Если происходит ошибка в коде блока try, то она обрабатывается здесь 
            catch(Exception ex)
            {
                //Выводи информацию по ошибке
                MessageBox.Show(ex.Message, "Ошибка ввода!");
            }
        }
        //Метод обрабатывающий нажатие на очищение текстбокса.
        private void btn_clr_Click(object sender, EventArgs e)
        {
            //Очищаем активный знак
            sign = 'n';
            //Очищаем текст бокс
            textBox1.Text = "";
            //Если мы очистили посчитанный пример
            if (fend)
            {
                //Включаем все компоненты 
                foreach (Control item in Controls)
                    item.Enabled = true;
                //Говорим о том что пример не посчитан
                fend = false;
            }
        }
        //Метод обрабатывающий нажатия на удаление левого символа (последнего символа)
        private void btn_del_Click(object sender, EventArgs e)
        {
            //Если пример посчитан
            if (fend)
            {
                //Находим индекс символа '='
                int index = textBox1.Text.IndexOf('=');
                //Количество элементов от равно до конца
                int count = textBox1.Text.Length - index;
                //Удаляем все от равно до конца
                textBox1.Text = textBox1.Text.Remove(index, count);
                //Включаем все компоненты 
                foreach (Control item in Controls)
                    item.Enabled = true;
                //Говорим о том что пример не посчитан
                fend = false;
            }
            //Иначе
            else
            {
                //Если текстбокс не пустой
                if (textBox1.Text != "")
                {
                    //Если последний символ равен знаку
                    if (textBox1.Text[textBox1.Text.Length - 1] == sign)
                        //сбрасываем знак
                        sign = 'n';
                    //удаляем последний символ
                    textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                }
            }
        }
    }
}
