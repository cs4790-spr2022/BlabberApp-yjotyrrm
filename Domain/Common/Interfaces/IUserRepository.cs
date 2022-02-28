using Domain.Entities;

namespace BlabberApp.Domain.Common.Interfaces
{

    public interface IUserRepository : IRepository<User>
    {
        public User GetByEmail(string email);
        public User GetByUsername(string username);
    }
}