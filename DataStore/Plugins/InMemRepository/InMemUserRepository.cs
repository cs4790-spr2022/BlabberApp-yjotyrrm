using BlabberApp.Domain.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlabberApp.DataStore.Plugins
{
    public class InMemUserRepository : IUserRepository
    {
        private List<User> _buf;

        public InMemUserRepository()
        {
            _buf = new List<User>();
        }

        public void Add(User entity)
        {
            if(entity.Validate())
            {
                _buf.Add(entity);
            }
            else
            { 
                throw new Exception("Invalid User");
            }
            
            
        }

        public IEnumerable<User> GetAll()
        {
            return _buf;
        }

        public User GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public User GetById(Guid Id)
        {
            foreach (User u in _buf)
            {
                if (Id.Equals(u.Id))
                {
                    return u;
                }
            }

            throw new Exception("Not found");
        }

        public User GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public void Remove(User entity)
        {
            if(!_buf.Remove(entity))
            {
                throw new Exception("could not remove entity.  Did it actually exist?");
            }
        }

        public void RemoveAll()
        {
            _buf.Clear();
        }

        public void Update(User entity)
        {
            try
            {
                entity.Validate();
            }
            catch
            {
                throw new Exception("Invalid User Update");
            }
            
            foreach (User u in _buf)
            {
                if (entity.Id.Equals(u.Id))
                {
                    _buf.Remove(u);
                    _buf.Add(entity);
                    return;
                }
            }          

            throw new Exception("Not found");
        }
    }
}
