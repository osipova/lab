using System;


class Program
{
    static void Main(string[] args)
    {
        bool IsRunning = true;
        while (IsRunning)
        {
            int act = SelectAction();

            switch (act)
            {
                case 1:
                    Console.WriteLine("\n=== Отгадай ответ ===");
                    
                    bool isCalculated = false;
                    double a, b, userRes;
                    double res = 0;
                    while (!isCalculated)
                    {
                        a = DoubleInput("\nВведите число a: ");
                        b = DoubleInput("Введите число b: ");

                        res = Calculate(a, b);

                        if (!double.IsNaN(res))
                        {
                            isCalculated = true;
                        }
 
                    }
                    

                    int i = 3;
                    while (i > 0)
                    {
                        userRes = DoubleInput("Отгадайте число: ");
                        if(userRes == res)
                        {
                            Console.WriteLine("Вы угадали число. Ура!");
                            i = 0;
                        }
                        else
                        {
                            Console.WriteLine("Неверно");
                            i--;
                            if (i > 0)
                            {
                                Console.WriteLine($"Попыток осталось {i}");
                            }
                            else
                            {
                                Console.WriteLine("Вы проиграли :(");
                                Console.WriteLine($"Правильный ответ: {res}");
                            }
                        }
                    }

                    break;
                case 2:
                    Console.WriteLine("\n=== Об авторе ===");
                    Console.WriteLine("Осипова Анастасия группа 6101-090301D");
                    break;
                case 3:
                    IsRunning = Exit("\nВы уверены? [да/нет] ");
                    break;
            }
        }
        
    }

    public static int SelectAction()
    {
        Console.WriteLine("\n==== Меню ====");
        Console.WriteLine("\n1. Отгадай ответ");
        Console.WriteLine("2. Об авторе");
        Console.WriteLine("3. Выход");

        int act = 0;
        bool IsActValued = false;
        while (!IsActValued)
        {
            try
            {
                act = IntInput("\nВыберите действие: ");
                if (act < 1 || act > 3)
                {
                    throw new ArgumentException("Действия под таким номером нет");
                }
                IsActValued = true;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Некорректные данные: {e.Message}");
                Console.Write("Введите заново: ");
            }
        }
        return act;
    }

    public static double Calculate(double a, double b)
    {
        try
        {
            const double pi = Math.PI;

            double sinValue = Math.Sin(a * pi / 180);
            double expressionUnderRoot = b + sinValue;

            if (expressionUnderRoot < 0)
            {
                throw new NegativeNumberException("Ошибка: нельзя извлечь корень из отрицательного числа");
            }

            double radical = Math.Sqrt(expressionUnderRoot);
            double power = Math.Pow(Math.Cos(a * pi / 180), 3);

            if (power == 0)
            {
                throw new DivideByZeroException("Ошибка: деление на ноль!");
            }

            double logArgument = radical / power;
            if (logArgument <= 0)
            {
                throw new ArgumentException("Аргумент логарифма должен быть > 0");
            }

            if (b <= 0 || b == 1)
            {
                throw new ArgumentException("Основание логарифма должно быть > 0 и != 1");
            }

            double f = Math.Log(logArgument, b);
            double result = Math.Round(f, 2);

            return result;
        }
        catch (DivideByZeroException e)
        {
            Console.WriteLine($"{e.Message}");
            return double.NaN;
        }
        catch (NegativeNumberException e)
        {
            Console.WriteLine($"{e.Message}");
            return double.NaN;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine($"{e.Message}");
            return double.NaN;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Другая ошибка: {e.Message}");
            return double.NaN;
        }

    }
    public class NegativeNumberException : Exception
    {
        public NegativeNumberException(string message) : base(message) { }
    }

    public static int IntInput(string text)
    {
        Console.Write(text);
        int value = 0;
        bool isValued = false;
        while (!isValued)
        {
            try
            {
                if (int.TryParse(Console.ReadLine(), out value) != true)
                {
                    throw new OverflowException();
                    throw new FormatException();
                }

                isValued = true;
            }
            catch (OverflowException)
            {
                Console.WriteLine("Ошибка: Введенное число слишком большое или слишком маленькое для типа int.");
                Console.Write("Введите другое число: ");
            }
            catch (FormatException)
            {
                Console.Write("Некорректный ввод. Введите число цифрами: ");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Произошла непредвиденная ошибка: {e.Message}");
            }
        }
        return value;
    }
    public static double DoubleInput(string text)
    {
        Console.Write(text);
        double value = 0;
        bool isValued = false;
        while (!isValued)
        {
            try
            {
                if (!double.TryParse(Console.ReadLine(), out value))
                {
                    throw new FormatException();
                }

                isValued = true;
            }
            catch (FormatException)
            {
                Console.Write("Некорректный ввод. Введите число цифрами: ");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Произошла непредвиденная ошибка: {e.Message}");
            }
        }
        return value;
    }
    
    public static bool Exit(string text)
    {
        Console.Write(text);
        string value = "";
        bool isValued = false;
        while (!isValued)
        {
            try
            {
                value = Console.ReadLine();
                if (value != "да" && value != "нет")
                {
                    throw new ArgumentException();
                }
                isValued = true;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Ошибка ввода. Ожидается 'да' или 'нет'");
                Console.Write("Введите заново: ");
            }
        }
        if (value == "да")
        {
            return false;
        }
        else return true;

    }
}