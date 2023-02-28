using Core.Entities;
using Core.Helpers;
using Data.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public class TeacherService
    {
        private readonly TeacherRepository _teacherRepository;
        public TeacherService()
        {
            _teacherRepository = new TeacherRepository();
        }

        public void GetAll()
        {
            var teachers = _teacherRepository.GetAll();
            if (teachers.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no any teacher", ConsoleColor.Red);
            }
            foreach (var teacher in teachers)
            {
                ConsoleHelper.WriteWithColor($"Id: {teacher.Id} , Fullname : {teacher.Name}{teacher.Surname}, Speciality ; {teacher.Speciality}", ConsoleColor.Cyan);

                if (teacher.Groups.Count == 0)
                {
                    ConsoleHelper.WriteWithColor($" There is no any group in this teacher", ConsoleColor.Red);

                }

                foreach (var group in teacher.Groups)
                {
                    ConsoleHelper.WriteWithColor($"Id: {group.Id}, Name: {group.Name}", ConsoleColor.Cyan);
                }

                Console.WriteLine();
            }

        }
        public void Create()
        {
            ConsoleHelper.WriteWithColor("Enter teacher name", ConsoleColor.Cyan);
            string name = Console.ReadLine();

            ConsoleHelper.WriteWithColor("Enter teacher surname", ConsoleColor.Cyan);
            string surname = Console.ReadLine();

         BirthDateDescription: ConsoleHelper.WriteWithColor("--- Enter teacher birth date  ---", ConsoleColor.Cyan);
            DateTime birthDate;
            bool isSucceded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
            if (!isSucceded)
            {
                ConsoleHelper.WriteWithColor("Birth Date is not correct format ! ", ConsoleColor.Red);
                goto BirthDateDescription;
            }

            ConsoleHelper.WriteWithColor("Enter teacher speciality", ConsoleColor.Cyan);
            string speciality = Console.ReadLine();

            var teacher = new Teacher
            {
                Name = name,
                Surname = surname,
                BirthDate = birthDate,
                Speciality = speciality,
                CreatedAt = DateTime.Now
            };

            _teacherRepository.Add(teacher);
            string teachersBirthDay = teacher.BirthDate.ToString("dddd, dd MMMM yyyy");
            ConsoleHelper.WriteWithColor($"Name : {teacher.Name} , Surname: {teacher.Surname}, Speciality: {teacher.Speciality},Birthday: {teachersBirthDay}", ConsoleColor.Cyan);

        }

        public void Delete()
        {

           List: GetAll();

            if (_teacherRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no teacher", ConsoleColor.Red);
            }
            else
            {
                ConsoleHelper.WriteWithColor("Enter teacher id", ConsoleColor.Cyan);
                int id;
                bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Id is not correct format!", ConsoleColor.Red);
                    goto List;
                }
                var teacher = _teacherRepository.Get(id);
                if (teacher is null)
                {
                    ConsoleHelper.WriteWithColor("There is no any teacher in this id ", ConsoleColor.Red);

                }

                _teacherRepository.Delete(teacher);
                ConsoleHelper.WriteWithColor($"{teacher.Name} {teacher.Surname} is successfully deleted", ConsoleColor.Green);
            }
        }

        public void Update()
        {
            GetAll();

         EnterUpdateDesc: ConsoleHelper.WriteWithColor("Enter Id for update ", ConsoleColor.Cyan);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);

            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Input is not true format", ConsoleColor.Red);
                goto EnterUpdateDesc;

            }

            var teacher = _teacherRepository.Get(id);
            if (teacher is null)
            {
                ConsoleHelper.WriteWithColor("There is no any teacher in this id", ConsoleColor.Red);
                goto EnterUpdateDesc;
            }

            ConsoleHelper.WriteWithColor("Enter Name", ConsoleColor.Cyan);
            string name = Console.ReadLine();

            ConsoleHelper.WriteWithColor("Enter Surname", ConsoleColor.Cyan);
            string surname = Console.ReadLine();

           BithDateDescription: ConsoleHelper.WriteWithColor("Enter Birth Date", ConsoleColor.Cyan);
            DateTime birthDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Birth date is not correct format!", ConsoleColor.Red);
                goto BithDateDescription;
            }



            ConsoleHelper.WriteWithColor("Enter Speciality", ConsoleColor.Cyan);
            string speciality = Console.ReadLine();

            teacher.Name = name;
            teacher.Surname = surname;
            teacher.BirthDate = birthDate;
            teacher.Speciality = speciality;

            _teacherRepository.Update(teacher);

            ConsoleHelper.WriteWithColor($"{teacher.Name} {teacher.Surname} is successfully updated", ConsoleColor.Green);



        }
    }
}
