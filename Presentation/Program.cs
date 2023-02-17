using Core.Helpers;
using System.Threading.Tasks.Dataflow;
using Core.Constants;
using System.Globalization;
using Core.Entities;
using Data.Repositories.Concrete;

namespace Presentation
{
    public  static class Program
    {
        private static int maxSize;
        public  readonly static GroupRepository _groupRepository;
        static Program()
        {
             _groupRepository = new GroupRepository();
        }
    
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
                ConsoleHelper.WriteWithColor("0-Exit",ConsoleColor.Yellow);

                ConsoleHelper.WriteWithColor("---Select Option---", ConsoleColor.Cyan);

                int number;
                bool isSucceeded = int.TryParse(Console.ReadLine(), out number);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Inputed number is not correct format!", ConsoleColor.Red);

                }
                else
                {
                    if (!(number>=0 && number<=6))
                    {
                        ConsoleHelper.WriteWithColor("Inputed number is not exist!", ConsoleColor.Red);
                    }
                    else
                    {
                        switch(number)
                        {
                                case (int)GroupOptions.CreatedGroup:
                                ConsoleHelper.WriteWithColor("---Enter name---", ConsoleColor.Cyan);
                                    string name= Console.ReadLine();
                                int maxSize;


                                MaxSizeDescription: ConsoleHelper.WriteWithColor("---Enter group max size---", ConsoleColor.Cyan);
                                isSucceeded= int.TryParse(Console.ReadLine(), out maxSize);
                                if (!isSucceeded)
                                {
                                    ConsoleHelper.WriteWithColor("max size is not correct format!",ConsoleColor.Red);
                                    goto MaxSizeDescription;
                                }
                                if (maxSize>18)
                                {
                                    ConsoleHelper.WriteWithColor("Max size should be less than or equals to 18", ConsoleColor.Red);
                                    goto MaxSizeDescription;

                                }

                                StartDateDescription: ConsoleHelper.WriteWithColor("---Enter start date---", ConsoleColor.Cyan);
                                DateTime startDate;
                                isSucceeded=DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy",CultureInfo.InvariantCulture,DateTimeStyles.None, out startDate);
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
                                if (startDate>endDate)
                                {
                                    ConsoleHelper.WriteWithColor("End date must be bigger than start date", ConsoleColor.Red);
                                    goto EndDateDescription;
                                }

                                var group = new Group
                                {
                                    Name = name,
                                    MaxSize = maxSize,
                                    StartDate= startDate,
                                    EndDate= endDate
                                };

                                _groupRepository.Add(group);
                                ConsoleHelper.WriteWithColor($"Group successfully created with Name:{group.Name},Max Size:{group.MaxSize},Start date:{group.StartDate.ToShortTimeString()},End date:{group.EndDate.ToShortTimeString()}");


                                break;
                                case(int)GroupOptions.UpdateGroup:
                                break;
                                case(int)GroupOptions.DeleteGroup:
                                var groupss = _groupRepository.GetAll();

                                ConsoleHelper.WriteWithColor("---All groups---", ConsoleColor.Cyan);
                                foreach (var group_ in groupss)
                                {
                                    ConsoleHelper.WriteWithColor($"id:{group_.Id},Name:{group_.Name}, Max size:{ group_.MaxSize},Start date:{ group_.StartDate}");
                                }

                            IdDescription: ConsoleHelper.WriteWithColor("---Enter Id---", ConsoleColor.Cyan);


                                int id;
                                isSucceeded=int.TryParse(Console.ReadLine(), out id);
                                if (!isSucceeded)
                                {
                                    ConsoleHelper.WriteWithColor("Id is not correct format!", ConsoleColor.Red);
                                    goto IdDescription;
                                }
                                 
                                var dbGroup= _groupRepository.Get(id);
                                if (dbGroup is null)
                                {
                                    ConsoleHelper.WriteWithColor("There is no any group in this id",ConsoleColor.Red);
                                }
                                else
                                {
                                    _groupRepository.Delete(dbGroup);
                                    ConsoleHelper.WriteWithColor("Group successfully deleted",ConsoleColor.Green) ;
                                }




                                break;
                                case(int)GroupOptions.GetAllGroups:
                                var groups = _groupRepository.GetAll();

                                ConsoleHelper.WriteWithColor("---All groups---",ConsoleColor.Cyan);
                                foreach(var group_ in groups)
                                {
                                    ConsoleHelper.WriteWithColor($"id:{group_.Id},Name:{group_.Name},Max size:{group_.MaxSize},Start date:{group_.StartDate}");
                                }
                                break;


                                case(int)GroupOptions.GetGroupById:
                                break;
                                case(int)GroupOptions.GetGroupByName:   
                                break;
                                case (int)GroupOptions.Exit:
                                return;
                              
                            default:
                                break;





                        }
                    }
                }

            }

           

            
        }
    }
}
