using Core.Entities;
using Core.Helpers;
using Data.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;

namespace Data
{
    public static class DbInitializer
    {
        static int Id;
        public static void SeedAdmins()
        {
            var admins = new List<Admin>
            {
                new Admin
                {

                    Id=++Id,
                    Username= "admin1",
                    Password=PasswordHasher.Encrypt("12345678"),
                    CreatedBy="System"
                },
                new Admin
                {
                    
                    Id=++Id,
                    Username= "admin2",
                    Password=PasswordHasher.Encrypt("salam123"),
                    CreatedBy="System"
                }

            };

            DbContext.Admins.AddRange(admins);
        }

    }

}
