using BlabberApp.Domain.Common.Interfaces;
using Domain.Entities;
using System.Collections;

namespace BlabberApp.DataStore.Plugins
{

    public class MySqlBlabRepository : MySqlPlugin, IBlabRepository
    {
        public MySqlBlabRepository(string connStr) : base(connStr)
        {
            this.Connect();
        }

        public void Add(Blab blab)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Blab> GetAll()
        {
            throw new NotImplementedException();
        }

        public Blab GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Update(Blab blab)
        {
            throw new NotImplementedException();
        }

        public void Remove(Blab blab)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Blab> GetByUser(User usr)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Blab> GetByDateTime(DateTime Dttm)
        {
            throw new NotImplementedException();
        }
    }
}