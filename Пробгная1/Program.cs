using System;
using System.Collections.Generic;
using System.IO;

namespace BirthdayReminder
{
    class Program
    {
        static List<Birthday> birthdayList = new List<Birthday>();

        static void Main(string[] args)
        {
            LoadData();

            Console.WriteLine("Добро пожаловать в Поздравлятор!");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Показать список всех ДР");
                Console.WriteLine("2 - Показать список сегодняшних и ближайших ДР");
                Console.WriteLine("3 - Добавить запись в список ДР");
                Console.WriteLine("4 - Удалить запись из списка ДР");
                Console.WriteLine("5 - Редактировать запись в списке ДР");
                Console.WriteLine("0 - Выход");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllBirthdays();
                        break;
                    case "2":
                        ShowUpcomingBirthdays();
                        break;
                    case "3":
                        AddBirthday();
                        break;
                    case "4":
                        DeleteBirthday();
                        break;
                    case "5":
                        EditBirthday();
                        break;
                    case "0":
                        SaveData();
                        return;
                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте еще раз.");
                        break;
                }
            }
        }

        static void ShowAllBirthdays()
        {
            Console.WriteLine("Список всех ДР:");

            foreach (var birthday in birthdayList)
            {
                Console.WriteLine($"{birthday.Name} - {birthday.Date.ToShortDateString()}");
            }
        }

        static void ShowUpcomingBirthdays()
        {
            Console.WriteLine("Список сегодняшних и ближайших ДР:");

            DateTime today = DateTime.Today;

            foreach (var birthday in birthdayList)
            {
                if (birthday.Date.Month == today.Month && birthday.Date.Day >= today.Day)
                {
                    Console.WriteLine($"{birthday.Name} - {birthday.Date.ToShortDateString()}");
                }
            }
        }

        static void AddBirthday()
        {
            Console.WriteLine("Введите имя:");

            string name = Console.ReadLine();

            Console.WriteLine("Введите дату ДР (в формате ДД.ММ.ГГГГ):");

            if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                birthdayList.Add(new Birthday { Name = name, Date = date });
                Console.WriteLine("ДР успешно добавлено!");
            }
            else
            {
                Console.WriteLine("Некорректный формат даты. ДР не добавлено.");
            }
        }

        static void DeleteBirthday()
        {
            Console.WriteLine("Введите имя для удаления:");

            string name = Console.ReadLine();

            Birthday birthdayToDelete = null;

            foreach (var birthday in birthdayList)
            {
                if (birthday.Name == name)
                {
                    birthdayToDelete = birthday;
                    break;
                }
            }

            if (birthdayToDelete != null)
            {
                birthdayList.Remove(birthdayToDelete);
                Console.WriteLine("ДР успешно удалено!");
            }
            else
            {
                Console.WriteLine("Не найдено ДР с указанным именем.");
            }
        }

        static void EditBirthday()
        {
            Console.WriteLine("Введите имя для редактирования:");
            string name = Console.ReadLine();

            Birthday birthdayToEdit = null;

            foreach (var birthday in birthdayList)
            {
                if (birthday.Name == name)
                {
                    birthdayToEdit = birthday;
                    break;
                }
            }

            if (birthdayToEdit != null)
            {
                Console.WriteLine("Введите новую дату ДР (в формате ДД.ММ.ГГГГ):");

                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    birthdayToEdit.Date = date;
                    Console.WriteLine("ДР успешно отредактировано!");
                }
                else
                {
                    Console.WriteLine("Некорректный формат даты. ДР не отредактировано.");
                }
            }
            else
            {
                Console.WriteLine("Не найдено ДР с указанным именем.");
            }
        }

        static void LoadData()
        {
            try
            {
                using (StreamReader sr = new StreamReader("birthdays.txt"))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] data = line.Split(',');

                        if (data.Length == 2)
                        {
                            string name = data[0];
                            DateTime date = DateTime.Parse(data[1]);

                            birthdayList.Add(new Birthday { Name = name, Date = date });
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл с данными не найден. Создан новый файл будет создан при сохранении данных.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка при загрузке данных: {e.Message}");
            }
        }

        static void SaveData()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("birthdays.txt"))
                {
                    foreach (var birthday in birthdayList)
                    {
                        sw.WriteLine($"{birthday.Name},{birthday.Date.ToShortDateString()}");
                    }
                }

                Console.WriteLine("Данные успешно сохранены!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка при сохранении данных: {e.Message}");
            }
        }
    }

    class Birthday
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
