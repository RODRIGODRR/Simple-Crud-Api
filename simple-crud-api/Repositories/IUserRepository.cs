using simple_crud_api.Models;
using System.Collections.Generic;

namespace simple_crud_api.Repositories
{
    public interface IUserRepository
    {
        IList<User> GetAll();
        User GetById(string id);
        User Create(User obj);
        User Update(string id, User obj);
        bool Delete(string id);
        User GetInitializeDatabasePAAS();
    }
}
