using Core.Entities;
using Core.Helpers;
using Data.Repositories.Concrete;
using System.Globalization;

namespace Presentation.Services
{
    public class GroupService
    {
        private readonly GroupRepository _groupRepository;
        private readonly StudentRepository _studentRepository;
        private readonly TeacherRepository _teacherRepository;
        public GroupService()
        {
            _groupRepository = new GroupRepository();
            _studentRepository = new StudentRepository();
            _teacherRepository = new TeacherRepository();

        }
        public void GetAll()
        {
            var groups = _groupRepository.GetAll();

            ConsoleHelper.WriteWithColor("---All groups---", ConsoleColor.Cyan);
            foreach (var group in groups)
            {
                ConsoleHelper.WriteWithColor($"id:{group.Id},Name:{group.Name},Max size:{group.MaxSize},Start date:{group.StartDate},End date:{group.EndDate},Created By:{group.CreatedBy}",ConsoleColor.Magenta);
            }
        }

        public void GetAllGroupsByTeacher()
        {
            var teachers = _teacherRepository.GetAll();
            foreach (var teacher in teachers)
            {
                ConsoleHelper.WriteWithColor($"Id:{teacher.Id} Fullname:{teacher.Name} {teacher.Surname}");
            }
            TeacherIdDesc: ConsoleHelper.WriteWithColor("Enter teacher id", ConsoleColor.Red);

            int id;
            bool isSucceeded=int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Id is not correct format", ConsoleColor.Red);
                goto TeacherIdDesc;
            }

            var dbTeacher=_teacherRepository.Get(id);
            if (dbTeacher is null)
            {
                ConsoleHelper.WriteWithColor("There is no any teacher in this id", ConsoleColor.Red);
            }
            else
            {
                foreach (var group in dbTeacher.Groups)
                {
                    ConsoleHelper.WriteWithColor($"Id:{group.Id} Name:{group.Name}",ConsoleColor.Green);
                }
            }
        }
        public void GetGroupById(Admin admin)
        {
            var groups = _groupRepository.GetAll();
            if (groups.Count == 0)
            {
            AreYouSureDescription: ConsoleHelper.WriteWithColor("There is no any group,Do you want to create new group? -- y or n --", ConsoleColor.DarkRed);
                char decision;
                bool isSucceededResult = char.TryParse(Console.ReadLine(), out decision);
                if (!isSucceededResult)
                {
                    ConsoleHelper.WriteWithColor("Your choice is not correct format", ConsoleColor.Red);
                    goto AreYouSureDescription;
                }


                if (!(decision == 'y' || decision == 'n'))
                {
                    ConsoleHelper.WriteWithColor("Your shoice is not correct", ConsoleColor.Red);
                    goto AreYouSureDescription;
                }

                if (decision == 'y')
                {
                    Create(admin);
                }
            }
            else
            {
                GetAll();
            EnterIdDescription: ConsoleHelper.WriteWithColor("--- Enter  id ---", ConsoleColor.Cyan);
                int id;
                bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Inputed id is not correct variant", ConsoleColor.Red);
                    goto EnterIdDescription;

                }

                var group = _groupRepository.Get(id);
                if (group is null)
                {
                    ConsoleHelper.WriteWithColor("There is no any group in this id", ConsoleColor.Red);
                }


                ConsoleHelper.WriteWithColor($"id:{group.Id},Name:{group.Name},Max size:{group.MaxSize},Start date:{group.StartDate} End date:{group.EndDate} Created By{group.CreatedBy}",ConsoleColor.Magenta);
            }

        }
        public void GetGroupByName()
        {
            GetAll();
            ConsoleHelper.WriteWithColor("Enter group name", ConsoleColor.Cyan);
            string name = Console.ReadLine();

            var group = _groupRepository.GetByName(name);
            if (group is null)
            {
                ConsoleHelper.WriteWithColor("There is any group in this name", ConsoleColor.Red);
            }

            ConsoleHelper.WriteWithColor($"id:{group.Id},Name:{group.Name},Max size:{group.MaxSize},Start date:{group.StartDate}");

        }
        public void Create(Admin admin)
        {
            if (_teacherRepository.GetAll().Count == 0) 
            {
                ConsoleHelper.WriteWithColor("You must create a teacher before", ConsoleColor.Yellow);
            }
            else
            {
               NameDesc: ConsoleHelper.WriteWithColor("---Enter name---", ConsoleColor.Cyan);
                string name = Console.ReadLine();
                var group = _groupRepository.GetByName(name);
                if (group is not null)
                {
                    ConsoleHelper.WriteWithColor("This group is already added", ConsoleColor.Red);
                    goto NameDesc;
                }

                int maxSize;

              MaxSizeDescription: ConsoleHelper.WriteWithColor("---Enter group max size---", ConsoleColor.Cyan);
                bool isSucceeded = int.TryParse(Console.ReadLine(), out maxSize);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("max size is not correct format!", ConsoleColor.Red);
                    goto MaxSizeDescription;
                }
                if (maxSize > 18)
                {
                    ConsoleHelper.WriteWithColor("Max size should be less than or equals to 18", ConsoleColor.Red);
                    goto MaxSizeDescription;

                }

               StartDateDescription: ConsoleHelper.WriteWithColor("---Enter start date---", ConsoleColor.Cyan);
                DateTime startDate;
                isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Start date is not correct format!", ConsoleColor.Red);
                    goto StartDateDescription;
                }

                DateTime boundaryDate = new DateTime(2015, 1, 1);
                if (startDate < boundaryDate)
                {
                    ConsoleHelper.WriteWithColor("Start date is not chosen right", ConsoleColor.Red);
                    goto StartDateDescription;

                }

               EndDateDescription: ConsoleHelper.WriteWithColor("---Enter end date---", ConsoleColor.Cyan);
                DateTime endDate;
                isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Start date is not correct format!", ConsoleColor.Red);
                    goto StartDateDescription;
                }
                if (startDate > endDate)
                {
                    ConsoleHelper.WriteWithColor("End date must be bigger than start date", ConsoleColor.Red);
                    goto EndDateDescription;
                }

                var teachers = _teacherRepository.GetAll();
                foreach (var teacher in teachers)
                {
                    ConsoleHelper.WriteWithColor($"Id:{teacher.Id} Fullname:{teacher.Name} {teacher.Surname}");
                }

               TeacherIdDesc: ConsoleHelper.WriteWithColor("Enter teacher id",ConsoleColor.Green);
                int teacherId;
                isSucceeded = int.TryParse(Console.ReadLine(), out teacherId);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Teacher id is not correct format!", ConsoleColor.Red);
                    goto TeacherIdDesc;

                }

                var dbTeacher=_teacherRepository.Get(teacherId);
                if (dbTeacher is null)
                {
                    ConsoleHelper.WriteWithColor("There is no any taecher in this id", ConsoleColor.Red);
                    goto TeacherIdDesc;
                }



                group = new Group
                {
                    Name = name,
                    MaxSize = maxSize,
                    StartDate = startDate,
                    EndDate = endDate,
                    CreatedBy = admin.Username,
                    Teacher= dbTeacher

                };

                _groupRepository.Add(group);
                dbTeacher.Groups.Add(group);
                ConsoleHelper.WriteWithColor($"Group successfully created with Name:{group.Name},Max Size:{group.MaxSize},Start date:{group.StartDate.ToShortTimeString()},End date:{group.EndDate.ToShortTimeString()}");
            }
        }
        public void Update(Admin admin)
        {
            GetAll();

        EnterGroupDesc: ConsoleHelper.WriteWithColor("Enter group \n 1. id \n 2. name", ConsoleColor.DarkCyan);

            int number;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out number);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Inputed number is not correct format", ConsoleColor.Red);
                goto EnterGroupDesc;
            }

            if (!(number == 1 || number == 2))
            {
                ConsoleHelper.WriteWithColor("Inputed number is not correct", ConsoleColor.Red);
                goto EnterGroupDesc;
            }


            if (number == 1)
            {
            EnterGroupIdDesc: ConsoleHelper.WriteWithColor("Enter group id", ConsoleColor.DarkCyan);

                int id;
                isSucceeded = int.TryParse(Console.ReadLine(), out id);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Inputed id is not correct format", ConsoleColor.Red);
                    goto EnterGroupIdDesc;
                }

                var group = _groupRepository.Get(id);

                if (group is null)
                {
                    ConsoleHelper.WriteWithColor("There is no any group in this id", ConsoleColor.Red);
                }
                InternalUpdate(group,admin);

            }
            else
            {
            EnterGroupNameDesc: ConsoleHelper.WriteWithColor("Enter group name", ConsoleColor.DarkCyan);
                string name = Console.ReadLine();
                var group = _groupRepository.GetByName(name);

                if (group is null)
                {
                    ConsoleHelper.WriteWithColor("There is no any group in this name", ConsoleColor.Red);
                }
                InternalUpdate(group, admin);
            }

        }
        private void InternalUpdate(Group group, Admin admin)
        {
            ConsoleHelper.WriteWithColor("Enter new name");
            string name = Console.ReadLine();

        MaxSizeDesc: ConsoleHelper.WriteWithColor("Enter new max size");
            int maxSize;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out maxSize);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Max size is not correct format", ConsoleColor.Red);
                goto MaxSizeDesc;
            }

        StartDateDesc: ConsoleHelper.WriteWithColor("Enter new start date");
            DateTime startDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Start date is not correct format!", ConsoleColor.Red);
                goto StartDateDesc;
            }
        EndDateDesc: ConsoleHelper.WriteWithColor("Enter new end date");
            DateTime endDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("End date is not correct format!", ConsoleColor.Red);
                goto EndDateDesc;
            }

            group.Name = name;
            group.MaxSize = maxSize;
            group.StartDate = startDate;
            group.EndDate = endDate;
            group.ModifiedBy = admin.Username;




            _groupRepository.Update(group);



        }
        public void Delete()
        {
            GetAll();

        IdDescription: ConsoleHelper.WriteWithColor("---Enter Id---", ConsoleColor.Cyan);


            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Id is not correct format!", ConsoleColor.Red);
                goto IdDescription;
            }

            var dbGroup = _groupRepository.Get(id);
            if (dbGroup is null)
            {
                ConsoleHelper.WriteWithColor("There is no any group in this id", ConsoleColor.Red);
            }


            else
            {
                foreach (var student in dbGroup.Students)
                {
                    student.Group = null;
                    _studentRepository.Update(student);
                }
                _groupRepository.Delete(dbGroup);
                ConsoleHelper.WriteWithColor("Group successfully deleted", ConsoleColor.Green);
            }
        }

        public static implicit operator GroupService(GroupRepository v)
        {
            throw new NotImplementedException();
        }
    }
}
