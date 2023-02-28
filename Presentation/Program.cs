using Core.Constants;
using Core.Helpers;
using Data;
using Presentation.Services;
using System.Text;

namespace Presentation
{
    public static class Program
    {
        private readonly static GroupService _groupService;
        private readonly static StudentService _studentService;
        private readonly static TeacherService _teacherService;
        private readonly static AdminService _adminService;

        static Program()
        {
            Console.OutputEncoding =Encoding.UTF8;
            DbInitializer.SeedAdmins();
            _groupService = new GroupService();
            _studentService = new StudentService();
            _teacherService = new TeacherService();
            _adminService = new AdminService();

        }
        static void Main()
        {
            Authorize: var admin = _adminService.Authorize();
            if (admin is not null)
            {
                ConsoleHelper.WriteWithColor($"--- Welcome,{admin.Username} ---", ConsoleColor.Cyan);

                while (true)
                {
                MainMenuDesc: ConsoleHelper.WriteWithColor("1-Groups", ConsoleColor.Yellow);
                    ConsoleHelper.WriteWithColor("2-Students", ConsoleColor.Yellow);
                    ConsoleHelper.WriteWithColor("3-Teachers", ConsoleColor.Yellow);
                    ConsoleHelper.WriteWithColor("3-Logout", ConsoleColor.Yellow);

                    int number;
                    bool isSucceeded = int.TryParse(Console.ReadLine(), out number);
                    if (!isSucceeded)
                    {
                        ConsoleHelper.WriteWithColor("Inputed number is not correct format!", ConsoleColor.Red);
                        goto MainMenuDesc;
                    }
                    else
                    {
                        switch (number)
                        {
                            case (int)MainMenuOptions.Groups:
                                while (true)
                                {
                                GroupDesc: ConsoleHelper.WriteWithColor("1-Create Group", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("2-Update Group", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("3-Delete Group", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("4-Get All Groups", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("5-Get Group By Id", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("6-Get Group By Name", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("7-Get All Groups By Teacher", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("0-Back to Main Menu", ConsoleColor.Yellow);

                                    ConsoleHelper.WriteWithColor("---Select Option---", ConsoleColor.Cyan);


                                    isSucceeded = int.TryParse(Console.ReadLine(), out number);
                                    if (!isSucceeded)
                                    {
                                        ConsoleHelper.WriteWithColor("Inputed number is not correct format!", ConsoleColor.Red);

                                    }
                                    else
                                    {

                                        switch (number)
                                        {
                                            case (int)GroupOptions.CreatedGroup:
                                                _groupService.Create(admin);

                                                break;
                                            case (int)GroupOptions.UpdateGroup:
                                                _groupService.Update(admin);
                                                break;
                                            case (int)GroupOptions.DeleteGroup:
                                                _groupService.Delete();
                                                break;
                                            case (int)GroupOptions.GetAllGroups:
                                                _groupService.GetAll();
                                                break;
                                            case (int)GroupOptions.GetGroupById:
                                                _groupService.GetGroupById(admin);
                                                break;
                                            case (int)GroupOptions.GetGroupByName:
                                                _groupService.GetGroupByName();
                                                break;
                                            case (int)GroupOptions.GetAllGroupsByTeacher:
                                                _groupService.GetAllGroupsByTeacher();
                                                break;
                                            case (int)GroupOptions.BackToMainMenu:
                                                goto MainMenuDesc;
                                                break;
                                            default:
                                                ConsoleHelper.WriteWithColor("Inputed number is not exist!", ConsoleColor.Red);
                                                goto GroupDesc;
                                        }

                                    }

                                }
                            case (int)MainMenuOptions.Students:
                                while (true)
                                {
                                    ConsoleHelper.WriteWithColor("1-Create Student", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("2-Update Student", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("3-Delete Student", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("4-Get All Student", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("5-Get All Student By Group", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("0-Go to Main Menu", ConsoleColor.Yellow);


                                    isSucceeded = int.TryParse(Console.ReadLine(), out number);
                                    if (!isSucceeded)
                                    {
                                        ConsoleHelper.WriteWithColor("Inputed number is not correct format!", ConsoleColor.Red);

                                    }
                                    else
                                    {

                                        switch (number)
                                        {
                                            case (int)StudentOptions.CreateStudent:
                                                _studentService.Create(admin);
                                                break;
                                            case (int)StudentOptions.UpdateStudent:
                                                _studentService.Update(admin);
                                                break;
                                            case (int)StudentOptions.DeleteStudent:
                                                _studentService.Delete();
                                                break;
                                            case (int)StudentOptions.GetAllStudent:
                                                _studentService.GetAll();
                                                break;
                                            case (int)StudentOptions.GetAlleStudentsByGroup:
                                                _studentService.GetAllByGroup();
                                                break;
                                            case (int)StudentOptions.BackToMainMenu:
                                                goto MainMenuDesc;
                                        }

                                    }
                                }
                            case (int)MainMenuOptions.Teachers:
                                while (true)
                                {
                                TeacherDesc: ConsoleHelper.WriteWithColor("1-Create Teacher", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("2-Update Teacher", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("3-Delete Teacher", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("4-Get All Teachers", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("0-Go to Main Menu", ConsoleColor.Yellow);


                                    isSucceeded = int.TryParse(Console.ReadLine(), out number);
                                    if (!isSucceeded)
                                    {
                                        ConsoleHelper.WriteWithColor("Inputed number is not correct format!", ConsoleColor.Red);

                                    }
                                    else
                                    {

                                        switch (number)
                                        {
                                            case (int)TeacherOptions.CreateTeacher:
                                                _teacherService.Create();

                                                break;
                                            case (int)TeacherOptions.UpdateTeacher:
                                                _teacherService.Update();

                                                break;
                                            case (int)TeacherOptions.DeleteTeacher:
                                                _teacherService.Delete();

                                                break;
                                            case (int)TeacherOptions.GetAllTeachers:
                                                _teacherService.GetAll();

                                                break;
                                            case (int)TeacherOptions.BackToMainMenu:
                                                goto MainMenuDesc;
                                        }

                                    }
                                }
                            case (int)MainMenuOptions.Logout:
                                goto Authorize;
                            default:
                                ConsoleHelper.WriteWithColor("Inputed number is not exist!", ConsoleColor.Red);
                                goto MainMenuDesc;


                        }
                    }
                }
            }













        }
    }
}
