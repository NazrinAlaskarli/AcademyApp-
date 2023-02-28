using Core.Entities;

namespace Data.Repositories.Abstract
{
    public interface IStudentRepository:IRepository<Student>
    {
      bool IsDublicateEmail(string email);
    }
}
