using System.IO;
using System.Reflection;

namespace _10._6Homework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите номер комманды:");
            Console.WriteLine("1: Вывести данные на экран");
            Console.WriteLine("2: Заполнить данные и добавить новую запись в конец файла.");
            byte choice = ReadCommand();
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Data.txt");
            string file = Path.GetFullPath(sFile);
            Console.WriteLine(file);

            while (choice < 3 && choice > 0)
            {
                switch (choice)
                {
                    case 1:
                        if (File.Exists(file) == true) { 
                            InfoOutput(file); 
                        }
                        else
                        {
                            Console.WriteLine("Данного файла не существует. Создаем новый.");
                            InputData(file);
                        } 
                        break;

                    case 2:
                        InputData(file);
                        break;
                    
                    default:
                        break;
                }
                Console.WriteLine("\n Введите номер комманды:");
                choice = ReadCommand();
            }
        }

        static byte ReadCommand()
        {
            byte choice;
            string input = Console.ReadLine();
            if (input == null) choice = 0; else choice = byte.Parse(input);
            return choice;
        }

        static void InputData(string file)
        {
            int id = CreateId(file);
            string dateString = CreateCurrentDate();
            string[] humanValues = CreateHumanValues();
            string[] humanValuesFirst = humanValues.Take(humanValues.Length - 1).ToArray();
            string humanText = string.Join("#", humanValuesFirst);
            string birthday = CreateBirthday();

            string[] array = [id.ToString(), dateString, humanText, birthday, humanValues.Last()];
            string text = string.Join("#", array);
            text = "\n" + text;
            CreateFile(text, file);
        }

        static int CreateId(string file)
        {
            int id;
            if (File.Exists(file))
            {
                var lines = File.ReadAllLines(file);
                id = lines.Length + 1;
            }
            else
            {
                id = 1;
            }
            return id;
        }

        static string CreateCurrentDate()
        {
            DateTime date = DateTime.Now;
            string dateString = date.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
            return _ = dateString.Remove(dateString.Length - 3);
        }

        static string[] CreateHumanValues()
        {
            string[] name;

            Console.WriteLine("Введите Фамилию:");
            string lastname = Console.ReadLine();

            Console.WriteLine("Введите Имя:");
            string firstname = Console.ReadLine();

            Console.WriteLine("Введите Отчество:");
            string middlename = Console.ReadLine();

            string fullname = lastname + " " + firstname + " " + middlename;

            Console.WriteLine("Введите Возраст:");
            string age = Console.ReadLine();

            Console.WriteLine("Введите Рост:");
            string height = Console.ReadLine();

            Console.WriteLine("Введите Город:");
            string city = "город " + Console.ReadLine();

            return name = [fullname, age, height, city];
        }

        static string CreateBirthday()
        {
            Console.WriteLine("Введите Год Рождения:");
            int year = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите Месяц Рождения (числом):");
            int month = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите День Рождения:");
            int day = int.Parse(Console.ReadLine());

            DateTime birthDate = new(year, month, day, 00, 00, 00);
            string birthdayString = birthDate.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
            return birthdayString = birthdayString.Remove(birthdayString.Length - 9);
        }

        static void CreateFile(string text, string file)
        {
            TextWriter tw = new StreamWriter(file, true);
            tw.WriteLine(text);
            tw.Close();
            InfoOutput(file);
        }

        static void InfoOutput(string file)
        {
            using (StreamReader sr = new(file))
            {
                Console.WriteLine(sr.ReadToEnd());
            }
        }
    }
}
