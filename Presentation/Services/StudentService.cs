using Core.Entities;
using Core.Extensions;
using Core.Helpers;
using Data.Repositories.Concrete;
using System.Globalization;

namespace Presentation.Services
{
    public class StudentService
    {
        private readonly GroupService _groupService;
        private readonly GroupRepository _groupRepository;
        private readonly StudentRepository _studentRepository;
        public StudentService()
        {
            _groupService = new GroupService();
            _groupRepository = new GroupRepository();
            _studentRepository = new StudentRepository();

        }


        public void GetAll()
        {
            var students = _studentRepository.GetAll();
            ConsoleHelper.WriteWithColor("-- ALL STUDENTS --", ConsoleColor.Cyan);

            foreach (var student in students)
            {
                ConsoleHelper.WriteWithColor($"Id:{student.Id},Fullname:{student.Name} {student.Surname},Email:{student.Email},Group:{student.Group?.Name},Created By:{student.CreatedBy}");
            }
        }
        public void GetAllByGroup()
        {
            _groupService.GetAll();

        GroupDesc: ConsoleHelper.WriteWithColor("Enter group id", ConsoleColor.Cyan);

            int groupId;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out groupId);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Group id is not correct format", ConsoleColor.Red);
                goto GroupDesc;
            }

            var group = _groupRepository.Get(groupId);
            if (group is null)
            {
                ConsoleHelper.WriteWithColor("There is no  any group in this id", ConsoleColor.Red);

            }

            if (group.Students.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no student in this group", ConsoleColor.Red);
            }
            else
            {
                foreach (var student in group.Students)
                {
                    ConsoleHelper.WriteWithColor($"Id:{student.Id},Fullname:{student.Name} {student.Surname},Email:{student.Email}");

                }
            }
        }
        public void Create(Admin admin)
        {
            if (_groupRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("You must create a group first", ConsoleColor.Red);
                return;

            }



            ConsoleHelper.WriteWithColor("Enter student name", ConsoleColor.Cyan);
            string name = Console.ReadLine();

            ConsoleHelper.WriteWithColor("Enter student surname", ConsoleColor.Cyan);
            string surname = Console.ReadLine();

        EmailDesc: ConsoleHelper.WriteWithColor("Enter student email", ConsoleColor.Cyan);
            string email = Console.ReadLine();

            if (!email.IsEmail())
            {
                ConsoleHelper.WriteWithColor("Email is not a correct format", ConsoleColor.Red);
                goto EmailDesc;
            }


            if (_studentRepository.IsDublicateEmail(email))
            {
                ConsoleHelper.WriteWithColor("This email is already used", ConsoleColor.Red);
                goto EmailDesc;
            }

        BirthDateDescription: ConsoleHelper.WriteWithColor("---Enter birth date---", ConsoleColor.Cyan);
            DateTime birthDate;
            bool isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Birth date is not correct format!", ConsoleColor.Red);
                goto BirthDateDescription;
            }

        GroupDescription: _groupService.GetAll();


            ConsoleHelper.WriteWithColor("Enter group id", ConsoleColor.Cyan);

            int groupId;
            isSucceeded = int.TryParse(Console.ReadLine(), out groupId);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Group id is not correct format", ConsoleColor.Red);
                goto GroupDescription;
            }

            var group = _groupRepository.Get(groupId);
            if (group is null)
            {
                ConsoleHelper.WriteWithColor("Group is not exist in this id", ConsoleColor.Red);
                goto GroupDescription;

            }

            if (group.MaxSize <= group.Students.Count)
            {
                ConsoleHelper.WriteWithColor("This group is full", ConsoleColor.Red);
                goto GroupDescription;

            }

            var student = new Student
            {
                Name = name,
                Surname = surname,
                Email = email,
                BirthDate = birthDate,
                Group = group,
                GroupId = groupId,
                CreatedBy = admin.Username

            };

            group.Students.Add(student);
            _studentRepository.Add(student);
            ConsoleHelper.WriteWithColor($"{student.Name},{student.Surname} is successfully added", ConsoleColor.Green);
        }
        public void Update(Admin admin)
        {
        StudentDesc: GetAll();

            ConsoleHelper.WriteWithColor("Enter student id", ConsoleColor.Cyan);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Inputed id is not correct format", ConsoleColor.Red);
                goto StudentDesc;
            }
            var student = _studentRepository.Get(id);
            if (student is null)
            {
                ConsoleHelper.WriteWithColor("There is no any student in this id", ConsoleColor.Red);
                goto StudentDesc;
            }

            ConsoleHelper.WriteWithColor("Enter new name", ConsoleColor.Cyan);
            string name = Console.ReadLine();

            ConsoleHelper.WriteWithColor("Enter new surname", ConsoleColor.Cyan);
            string surname = Console.ReadLine();



            BirthDateDescription: ConsoleHelper.WriteWithColor("---Enter birth date---", ConsoleColor.Cyan);
            DateTime birthDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Birth date is not correct format!", ConsoleColor.Red);
                goto BirthDateDescription;
            }
              GroupDesc: _groupService.GetAll();

            ConsoleHelper.WriteWithColor("Enter new group id", ConsoleColor.Cyan);
            int groupId;
            isSucceeded=int.TryParse(Console.ReadLine(), out groupId);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Group id is not correct format",ConsoleColor.Red);
                goto GroupDesc;
            }

            var group = _groupRepository.Get(groupId);
            if (group is null)
            {
                ConsoleHelper.WriteWithColor("There is no any group in this id",ConsoleColor.Red);
                goto GroupDesc;
            }

            student.Name = name;
            student.Surname = surname;
            student.BirthDate = birthDate;
            student.Group= group;
            student.GroupId= groupId;
            student.ModifiedBy = admin.Username;

            _studentRepository.Update(student);
            Console.WriteLine($"{student.Name} {student.Surname},Group:{student.Group.Name} successfully updated");


        }
        public void Delete()
        {
            GetAll();

        EnterIdDsc: ConsoleHelper.WriteWithColor("Enter id", ConsoleColor.Cyan);

            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Id is not correct format", ConsoleColor.Red);
                goto EnterIdDsc;
            }

            var student = _studentRepository.Get(id);
            if (student is null)
            {
                ConsoleHelper.WriteWithColor("There is no student in this id", ConsoleColor.Red);
            }

            _studentRepository.Delete(student);
            ConsoleHelper.WriteWithColor($"{student.Name},{student.Surname} is successfully deleted", ConsoleColor.Green);






        }
    }
}
