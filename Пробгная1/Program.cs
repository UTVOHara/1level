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

            Console.WriteLine("����� ���������� � ������������!");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("�������� ��������:");
                Console.WriteLine("1 - �������� ������ ���� ��");
                Console.WriteLine("2 - �������� ������ ����������� � ��������� ��");
                Console.WriteLine("3 - �������� ������ � ������ ��");
                Console.WriteLine("4 - ������� ������ �� ������ ��");
                Console.WriteLine("5 - ������������� ������ � ������ ��");
                Console.WriteLine("0 - �����");

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
                        Console.WriteLine("������������ �����. ���������� ��� ���.");
                        break;
                }
            }
        }

        static void ShowAllBirthdays()
        {
            Console.WriteLine("������ ���� ��:");

            foreach (var birthday in birthdayList)
            {
                Console.WriteLine($"{birthday.Name} - {birthday.Date.ToShortDateString()}");
            }
        }

        static void ShowUpcomingBirthdays()
        {
            Console.WriteLine("������ ����������� � ��������� ��:");

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
            Console.WriteLine("������� ���:");

            string name = Console.ReadLine();

            Console.WriteLine("������� ���� �� (� ������� ��.��.����):");

            if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                birthdayList.Add(new Birthday { Name = name, Date = date });
                Console.WriteLine("�� ������� ���������!");
            }
            else
            {
                Console.WriteLine("������������ ������ ����. �� �� ���������.");
            }
        }

        static void DeleteBirthday()
        {
            Console.WriteLine("������� ��� ��� ��������:");

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
                Console.WriteLine("�� ������� �������!");
            }
            else
            {
                Console.WriteLine("�� ������� �� � ��������� ������.");
            }
        }

        static void EditBirthday()
        {
            Console.WriteLine("������� ��� ��� ��������������:");
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
                Console.WriteLine("������� ����� ���� �� (� ������� ��.��.����):");

                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    birthdayToEdit.Date = date;
                    Console.WriteLine("�� ������� ���������������!");
                }
                else
                {
                    Console.WriteLine("������������ ������ ����. �� �� ���������������.");
                }
            }
            else
            {
                Console.WriteLine("�� ������� �� � ��������� ������.");
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
                Console.WriteLine("���� � ������� �� ������. ������ ����� ���� ����� ������ ��� ���������� ������.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"������ ��� �������� ������: {e.Message}");
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

                Console.WriteLine("������ ������� ���������!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"������ ��� ���������� ������: {e.Message}");
            }
        }
    }

    class Birthday
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
