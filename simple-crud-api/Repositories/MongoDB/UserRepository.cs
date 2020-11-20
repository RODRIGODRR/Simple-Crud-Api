using MongoDB.Driver;
using simple_crud_api.Config;
using simple_crud_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace simple_crud_api.Repositories.MongoDB
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>(settings.UsersCollectionName);
        }

        public bool Delete(string id)
        {
            var result = _users.DeleteOne(e => e.Id == id).DeletedCount > 0;
            return result;
        }

        public User GetInitializeDatabasePAAS() => _users.Find(e => true).FirstOrDefault();

        public IList<User> GetAll()
        {
            var result = _users.Find(e => true).ToList();
            return result;
        }

        public User GetById(string id)
        {
            var result = _users.Find(e => e.Id == id).FirstOrDefault();
            return result;
        }

        public User Create(User obj)
        {
            obj.DtaCriacao = DateTime.Now;

            _users.InsertOne(obj);
            return obj;
        }

        public User Update(string id, User obj)
        {
            _users.ReplaceOne(e => e.Id == id, obj);
            return obj;
        }
    }
}
