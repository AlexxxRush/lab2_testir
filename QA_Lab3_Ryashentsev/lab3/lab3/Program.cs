using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;
using System.Diagnostics;

namespace Курсовая_работа_на_шарпе
{

    class product : Program
    {
        private int inv_num;//инвентарный номер товара
        private int col_countr;//количество стран, покупающих товар
        private int volume;//объём продаваемой партии(в штуках) для каждой страны

        public product()//конструктор без параметров
        {
            inv_num = -1;//определяет по умолчанию значение
            col_countr = -1;
            volume = -1;
        }
        public product(int A, int B, int C)//конструктор с параметрами
        {
            inv_num = A;
            col_countr = B;
            volume = C;
        }
        public int get_inv_num()//геттер для получения инвентарного номера
        {
            return inv_num;
        }
        public void set_country(int col_countr)//сеттер для установки значения количества стран, покупающих товар
        {
            this.col_countr = col_countr;
        }
        public void input(string rx) //String rx - это шаблон вводимой строки
        {//метод для ввода данных
            Console.Write("Введите инвентарный № товара: ");
            string str;
            str = Console.ReadLine();


            inv_num = error_of_input(str, rx);//проверка функцией корректность введённых данных

            Console.Write("Введите количество стран, покупающих товар (<=10): ");
            str = Console.ReadLine();

            col_countr = error_of_input(str, rx);
            while (col_countr > 10)
            {
                Console.WriteLine("Количество стран должно быть в пределах от 0 до 10 включительно!");
                Console.Write("Введите количество стран <=10: ");
                str = Console.ReadLine();

                col_countr = error_of_input(str, rx);
            }

            Console.Write("Введите объём продаваемой партии (в штуках): ");
            str = Console.ReadLine();


            volume = error_of_input(str, rx);
            Console.WriteLine();
        }
        public void output()//метод для вывода данных в консоль
        {
            Console.WriteLine("Инвентарный номер: " + inv_num);
            Console.WriteLine("Количество стран, покупающих товар: " + col_countr);
            Console.WriteLine("Объём продаваемой партии: " + volume + " шт.");
            Console.WriteLine();
        }

        public void diskOut(StreamWriter fout)//запись в исходный файл
        {
            fout.WriteLine(inv_num);
            fout.WriteLine(col_countr);
            fout.WriteLine(volume);
            fout.WriteLine();
        }
        public void diskOutNew(StreamWriter fout1)//запись в новый файл
        {
            fout1.WriteLine(inv_num);
            fout1.WriteLine(col_countr);
            fout1.WriteLine();
        }
        public void diskIn(StreamReader fin)//считывание с исходного файла
        {
            inv_num = int.Parse(fin.ReadLine());
            col_countr = int.Parse(fin.ReadLine());
            volume = int.Parse(fin.ReadLine());
            fin.ReadLine();
        }

        public void newdiskIn(StreamReader fin)//считывание с нового файла
        {
            inv_num = int.Parse(fin.ReadLine());
            col_countr = int.Parse(fin.ReadLine());
            fin.ReadLine();
        }

        public void excess_volume(int zadan, ref int a)//проверка превышения заданного объёма
        {

            if (volume > zadan)
            {
                Console.WriteLine("________________________________________________________________");
                Console.WriteLine("<У данного товара объём продаж превышает заданную величину>");
                Console.WriteLine("Инвентраный номер: " + inv_num);
                Console.WriteLine("Объём продаж: " + volume + " шт.");
                Console.WriteLine();
            }
            else
            {
                a++;
            }
        }
    }

    class Program
    {
        public static int error_of_input(string str, string rx)//функция для проверки корректности вводимых данных
        {
            if (str == "")
                Environment.Exit(0);
            while (!Regex.IsMatch(str, rx))
            {
                Console.WriteLine(" | **ERROR** | : Вы ввели НЕ КОРРЕКТНОЕ число!");
                Console.Write("Введите КОРРЕКТНОЕ число: ");
                str = Console.ReadLine();
            }
            return int.Parse(str);
        }
        static void Create_a_file(string rx, List<product> mass, StreamWriter fout)//создание файла с заданной структурой записи
        {
            string str;
            Console.WriteLine("Введите количество добавляемых элементов: ");
            str = Console.ReadLine();
            int N = 0;
            N = error_of_input(str, rx);
            mass.Clear();
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("\n<<< Введите данные товара №" + (i + 1) + " >>>");
                mass.Insert(i, new product());
                mass[i].input(rx);
                for (int j = 0; j < i; j++)
                {

                    if (mass[j].get_inv_num() == mass[i].get_inv_num())
                    {
                        Console.WriteLine("Такой инвентарный номер уже есть!");
                        Console.WriteLine("Введите данные не существующего товара:");
                        mass[i].input(rx);
                        j = -1;
                    }
                }
                mass[i].diskOut(fout);
            }
        }


        static void Generate_new_file(List<product> mass, StreamWriter fout1)//создание нового файла с новой структурой записи
        {
            Console.WriteLine("________________________________________________________________");

            for (int i = 0; i < mass.Count(); i++)
            {
                mass[i].diskOutNew(fout1);
            }
        }

        static void Add_note(List<product> mass, StreamWriter fout, string rx)//добавление записи в исходный файл
        {
            Console.WriteLine("Введите количество товаров, которые хотите добавить: ");
            int N_add = 0;
            string str;
            str = Console.ReadLine();

            N_add = error_of_input(str, rx);
            int N = mass.Count();

            for (int i = N; i < N + N_add; i++)
            {
                Console.WriteLine("\n<<< Введите данные товара №" + (i + 1) + " >>>");
                mass.Insert(i, new product());
                mass[i].input(rx);
                for (int j = 0; j < i; j++)
                {

                    if (mass[j].get_inv_num() == mass[i].get_inv_num())
                    {
                        Console.WriteLine("Такой инвентарный номер уже есть!");
                        Console.WriteLine("Введите данные не существующего товара:");
                        mass[i].input(rx);
                        j = 0;
                    }
                }
                mass[i].diskOut(fout);
            }
        }

        static int Delete_note(List<product> mass, StreamWriter fout, string rx, ref int flag)//удаление записи по инвентарному номеру
        {
            Console.WriteLine("________________________________________________________________");
            Console.Write("Введите количество элементов, которые хотите удалить: ");
            int N_del;
            string str;
            str = Console.ReadLine();

            N_del = error_of_input(str, rx);

            while (N_del > mass.Count())
            {

                Console.WriteLine("Вы ввели слишком много удаляемых элементов!");
                Console.WriteLine("Максимум удаляемых элементов могут быть только все элементы!");
                Console.WriteLine("Введите корректное количество удаляемых элементов или 0, чтобы выйти: ");
                str = Console.ReadLine();
                N_del = error_of_input(str, rx);
                if (N_del == 0)
                    return 0;
            }


            int Del_Num;

            for (int i = 0; i < N_del; i++)
            {
                Console.WriteLine("Введите " + (i + 1) + " - й инвентарный номер: ");
                str = Console.ReadLine();

                Del_Num = error_of_input(str, rx);

                for (int j = 0; j < mass.Count();)
                {
                    if (mass[j].get_inv_num() == Del_Num)
                    {
                        mass.Remove(mass[j]);
                        flag++;
                        Console.WriteLine("Товар с инвентарным номером \"" + Del_Num + "\" успешно удалён!");
                    }
                    else
                    {
                        j++;
                    }
                }

                if (flag == 0)
                {
                    Console.WriteLine("Инвентарного номера \"" + Del_Num + "\" нет!");
                    i--;
                }
                else
                {
                    flag = 0;
                }
            }


            for (int i = 0; i < mass.Count(); i++)
            {
                mass[i].diskOut(fout);
            }

            flag = 0;
            return 0;
        }

        static void Change_note(List<product> mass, StreamWriter fout, string rx, ref int flag)//изменение количества стран, покупающих товар
        {
            flag = 0;
            if (mass.Count() == 0)
                Console.WriteLine("Вы удалили ВСЕ записи по экспортируемым товарам!");
            else
            {
                Console.WriteLine("________________________________________________________________");
                Console.WriteLine("Введите инвентарный номер товара,");
                Console.WriteLine("у которого хотите изменить количество стран, ");
                Console.Write("покупающих товар: ");

                int INV_NUMBER;
                int kol_con;
                string str;
                str = Console.ReadLine();

                while (flag == 0)
                {
                    INV_NUMBER = error_of_input(str, rx);
                    for (int i = 0; i < mass.Count(); i++)
                    {
                        if (mass[i].get_inv_num() == INV_NUMBER)
                        {
                            Console.Write("Введите новое количество стран: ");
                            str = Console.ReadLine();
                            kol_con = error_of_input(str, rx);
                            while (kol_con > 10)
                            {
                                Console.WriteLine("Количество стран должно быть в пределах от 0 до 10 включительно!");
                                Console.Write("Введите количество стран <=10: ");
                                str = Console.ReadLine();
                                kol_con = error_of_input(str, rx);
                            }


                            mass[i].set_country(kol_con);
                            flag++;
                        }
                    }
                    if (flag == 0)
                    {
                        Console.WriteLine("Введенный инвентарный номер \"" + INV_NUMBER + "\" не существует!");
                        Console.Write("Ведите существующий инвентарный номер: ");
                        str = Console.ReadLine();
                    }

                }

                for (int i = 0; i < mass.Count(); i++)
                {
                    mass[i].diskOut(fout);
                }

            }
        }

        static void Main(string[] args)
        {

            //Regex rx = new Regex("[0-9]*");
            string rx = "[0-9]";
            Console.WriteLine("________________________________________________________________");
            List<product> mass = new List<product>();//список типа класс
            int selector = 0;//номер команды
            int sel = 0;//команда вводимая во время выполнения другой команды
            string str;//вводимые данные
            int flag = 0;//увеличивается на 1, если был удален элемент, после выхода из функции обнуляется
            int a = 0;//увеличивается на 1 если при выполнении команды 3 не было найдено элемента

            int iter = 0;//номер элемента при записи в список
            StreamReader fin0 = new StreamReader("in_out_put.txt");//если данные в исходном файле есть, то записываем их в list
            while (fin0.Peek() > -1)
            {
                mass.Insert(iter, new product());
                mass[iter].diskIn(fin0);
                iter++;
            }
            fin0.Close();
            try
            {
                while (selector != 11)//цикл в котором вводятся команды
                {
                    Console.Clear();
                    Console.WriteLine("Если хотите заврешить выполнение программы досрочно введите ENTER");
                    Console.WriteLine("1 - Создать файл с заданной структурой записи(Удалить старые данные).");
                    Console.WriteLine("2 - Выдать на экран содержимое файла \"in_out_put.txt\".");
                    Console.WriteLine("3 - Выдать на экран список всех товаров, у которых объём продажи превышает некоторую заданную величину.");
                    Console.WriteLine("4 - Сформировать новый файл.");
                    Console.WriteLine("5 - Вывести на экран содержимое нового файла.");
                    Console.WriteLine("6 - Добавить запись в исходный файл.");
                    Console.WriteLine("7 - Удалить записи по заданным инвентарным номерам.");
                    Console.WriteLine("8 - В файле по заданному инвентарному № товара изменить количество стран, покупающих товар.");
                    Console.WriteLine("9 - Вызвать файл \"in_out_put.txt\".");
                    Console.WriteLine("10 - Вызвать файл \"in_out_put1.txt\".");
                    Console.WriteLine("11 - Выход из программы.");
                    Console.Write("Введите номер операции: ");
                    str = Console.ReadLine();
                    selector = error_of_input(str, rx);



                    switch (selector)
                    {
                        case 1://Создать файл с заданной структурой записи(Перезаписать данные)
                            {
                                Console.Clear();
                                StreamWriter fout = new StreamWriter("in_out_put.txt", false, System.Text.Encoding.Default);
                                Create_a_file(rx, mass, fout);
                                Console.Clear();
                                fout.Close();
                                break;
                            }
                        case 2://Выдать на экран содержимое файла "in_out_put.txt".
                            {
                                Console.Clear();

                                StreamReader fin = new StreamReader("in_out_put.txt");

                                for (int i = 0; i < mass.Count(); i++)
                                {
                                    mass[i].diskIn(fin);
                                }
                                fin.Close();
                                Console.WriteLine("________________________________________________________________");
                                Console.WriteLine("Содержимое файла:"); Console.WriteLine("________________________________________________________________");
                                for (int i = 0; i < mass.Count(); i++)
                                {
                                    mass[i].output();
                                }
                                Console.WriteLine("________________________________________________________________");
                                Console.WriteLine("Перейти к следующей команде?");
                                Console.WriteLine("1 - Да");
                                Console.WriteLine("Любая клавиша, кроме единицы - Выйти из программы");
                                str = Console.ReadLine();
                                if (str != "1")
                                {
                                    return;
                                }
                                break;
                            }
                        case 3://Выдать на экран список всех товаров, у которых объём продажи превышает некоторую заданную величину.
                            {
                                Console.Clear();
                                int zadan;

                                StreamReader fin = new StreamReader("in_out_put.txt");

                                for (int i = 0; i < mass.Count(); i++)
                                {
                                    mass[i].diskIn(fin);
                                }
                                fin.Close();
                                Console.Write("Введите заданную величину объёма, превосходить которую нельзя: ");
                                str = Console.ReadLine();
                                zadan = error_of_input(str, rx);
                                for (int i = 0; i < mass.Count(); i++)
                                    mass[i].excess_volume(zadan, ref a);
                                if (a == mass.Count())
                                    Console.WriteLine("Таких элементов нет!");
                                Console.WriteLine("Перейти к следующей команде?");
                                Console.WriteLine("1 - Да");
                                Console.WriteLine("Любая клавиша, кроме единицы - Выйти из программы");

                                str = Console.ReadLine();
                                if (str != "1")
                                {
                                    return;
                                }
                                break;
                            }
                        case 4://Сформировать новый файл.
                            {
                                Console.Clear();
                                StreamWriter fout1 = new StreamWriter("in_out_put1.txt", false, System.Text.Encoding.Default);
                                Generate_new_file(mass, fout1);
                                fout1.Close();
                                if (new FileInfo("in_out_put1.txt").Length == 0)
                                    Console.WriteLine("Новый файл сформирован НЕ успешно. ОН ПУСТ!");
                                else
                                    Console.WriteLine("Новый файл сформирвоан успешно!");

                                //StreamReader fin1 = new StreamReader("in_out_put1.txt");

                                //for (int i = 0; i < mass.Count(); i++)
                                //{
                                //	mass[i].newdiskIn(fin1);
                                //}


                                //fin1.Close();
                                Console.WriteLine("________________________________________________________________");

                                Console.WriteLine("Перейти к следующей команде?");
                                Console.WriteLine("1 - Да");
                                Console.WriteLine("Любая клавиша, кроме единицы - Выйти из программы");
                                str = Console.ReadLine();
                                if (str != "1")
                                {
                                    return;
                                }
                                break;
                            }
                        case 5://Вывести на экран содержимое нового файла.
                            {
                                Console.Clear();
                                StreamReader fin1 = new StreamReader("in_out_put1.txt");
                                StreamReader fin2 = new StreamReader("in_out_put1.txt");
                                string tmp;
                                tmp = fin2.ReadLine();
                                if (fin1.Peek() <= -1)
                                    Console.WriteLine("Файл пуст!");
                                fin2.Close();
                                while (fin1.Peek() > -1)
                                {
                                    Console.WriteLine("< New > Инвентарный номер: " + fin1.ReadLine());
                                    Console.WriteLine("< New > Количество стран, покупающих товар: " + fin1.ReadLine());
                                    Console.WriteLine(fin1.ReadLine());
                                }


                                fin1.Close();

                                Console.WriteLine("Перейти к следующей команде?");
                                Console.WriteLine("1 - Да");
                                Console.WriteLine("Любая клавиша, кроме единицы - Выйти из программы");
                                str = Console.ReadLine();
                                if (str != "1")
                                {
                                    return;
                                }
                                break;
                            }
                        case 6://Добавить запись в исходный файл.
                            {
                                Console.Clear();
                                StreamWriter fout = new StreamWriter("in_out_put.txt", true, System.Text.Encoding.Default);

                                Add_note(mass, fout, rx);

                                fout.Close();
                                Console.WriteLine("Перейти к следующей команде?");
                                Console.WriteLine("1 - Да");
                                Console.WriteLine("Любая клавиша, кроме единицы - Выйти из программы");
                                str = Console.ReadLine();
                                if (str != "1")
                                {
                                    return;
                                }
                                break;
                            }
                        case 7://Удалить записи по заданным инвентарным номерам.
                            {
                                Console.Clear();
                                StreamWriter fout = new StreamWriter("in_out_put.txt", false, System.Text.Encoding.Default);
                                if (mass.Count() == 0)
                                {
                                    Console.WriteLine("Элементов в массиве нет! Удалять нечего!");
                                }
                                else
                                {
                                    Delete_note(mass, fout, rx, ref flag);
                                }
                                Console.WriteLine("Перейти к следующей команде?");
                                Console.WriteLine("1 - Да");
                                Console.WriteLine("Любая клавиша, кроме единицы - Выйти из программы");

                                fout.Close();
                                str = Console.ReadLine();
                                if (str != "1")
                                {
                                    return;
                                }
                                break;
                            }
                        case 8://В файле по заданному инвентарному № товара изменить количество стран, покупающих товар.
                            {
                                Console.Clear();
                                StreamWriter fout = new StreamWriter("in_out_put.txt", false, System.Text.Encoding.Default);
                                Change_note(mass, fout, rx, ref flag);
                                Console.WriteLine("Перейти к следующей команде?");
                                Console.WriteLine("1 - Да");
                                Console.WriteLine("Любая клавиша, кроме единицы - Выйти из программы");

                                fout.Close();
                                str = Console.ReadLine();
                                if (str != "1")
                                {
                                    return;
                                }
                                break;
                            }
                        case 9:
                            {

                                Console.Clear();
                                Process.Start("in_out_put.txt");

                                break;
                            }
                        case 10:
                            {

                                Console.Clear();
                                Process.Start("in_out_put1.txt");
                                break;
                            }

                    }
                    Console.Clear();
                }
            }
            catch (Exception ex)//если было выбрашено исключение, оно ловится
            {
                Console.WriteLine("________________________________________________________________");
                Console.WriteLine("***Завершение программы!***");
                Console.WriteLine("***Возникла исключение***");
                Console.WriteLine("Исключение: " + ex.Message);
            }
        }
    }
}
