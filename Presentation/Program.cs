using Core.Constants;
using Core.Helpers;
using Data.Repositories.Concrete;
using Presentation.Services;

namespace Presentation
{
    public static class Program
    {
        private readonly static GroupService _groupService;
        static Program()
        {
            _groupService = new GroupService();
        }

        private static int maxSize;
        public readonly static GroupRepository _groupRepository;


        static void Main()
        {

            ConsoleHelper.WriteWithColor("---Welcome---", ConsoleColor.Cyan);

            while (true)
            {
                ConsoleHelper.WriteWithColor("1-Create Group", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor("2-Update Group", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor("3-Delete Group", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor("4-Get All Groups", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor("5-Get Group By Id", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor("6-Get Group By Name", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor("0-Exit", ConsoleColor.Yellow);

                ConsoleHelper.WriteWithColor("---Select Option---", ConsoleColor.Cyan);

                int number;
                bool isSucceeded = int.TryParse(Console.ReadLine(), out number);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Inputed number is not correct format!", ConsoleColor.Red);

                }
                else
                {
                    if (!(number >= 0 && number <= 6))
                    {
                        ConsoleHelper.WriteWithColor("Inputed number is not exist!", ConsoleColor.Red);
                    }
                    else
                    {
                        switch (number)
                        {
                            case (int)GroupOptions.CreatedGroup:
                                _groupService.Create();
                                break;
                            case (int)GroupOptions.UpdateGroup:
                                _groupService.Update();
                                break;
                            case (int)GroupOptions.DeleteGroup:
                                _groupService.Delete();
                                break;
                            case (int)GroupOptions.GetAllGroups:
                                _groupService.GetAll();
                                break;
                            case (int)GroupOptions.GetGroupById:
                                _groupService.GetGroupById();
                                break;
                            case (int)GroupOptions.GetGroupByName:
                                _groupService.GetGroupByName();
                                break;
                            case (int)GroupOptions.Exit:
                                _groupService.Exit();
                                break;
                            default:
                                break;





                        }
                    }
                }

            }




        }
    }
}
