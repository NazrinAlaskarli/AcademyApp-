using Core.Entities;
using Core.Helpers;
using Data.Contexts;
using Data.Repositories.Abstract;

namespace Presentation.Services
{
    public  class AdminRepository : IAdminRepository
    {
        public Admin GetByUsernameAndPassword(string username, string password)
        {
           return DbContext.Admins.FirstOrDefault(a=>a.Username.ToLower()== username.ToLower() && PasswordHasher.Decrypt(a.Password) == password);
        }
    }
}