using Core.Entities;

namespace Data.Contexts
{
    public static class DbContext
    {
        static DbContext()
        {
            Groups = new List<Group>();
            Students = new List<Student>();
            Teachers= new List<Teacher>();
            Admins = new List<Admin>();
        }


        public static List<Group> Groups { get; set; }
        public static List<Student> Students { get; set; }
        public static List<Teacher> Teachers { get; set; }
        public static List<Admin> Admins { get; set; }

    }
}