using Microsoft.EntityFrameworkCore;
using simple_crud_api.Data;
using simple_crud_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace simple_crud_api.Repositories.MSSql
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public bool Delete(string id)
        {
            _context.Remove(this.GetById(id));
            var result = _context.SaveChanges();

            return result > 0;
        }

        public IList<User> GetAll()
        {
            return _context.Users.AsNoTracking().ToList();
        }

        public User GetById(string id)
        {
            return _context.Users.AsNoTracking().Where(e => e.Id == id).FirstOrDefault();
        }

        public User Create(User obj)
        {            
            obj.Id = Guid.NewGuid().ToString().Substring(0, 24);

            _context.Add(obj);
            _context.SaveChanges();

            return obj;
        }

        public User Update(string id, User obj)
        {
            _context.Entry(obj).State = EntityState.Modified;

            if(id == obj.Id)
                _context.SaveChanges();

            return obj;
        }
    }
}
