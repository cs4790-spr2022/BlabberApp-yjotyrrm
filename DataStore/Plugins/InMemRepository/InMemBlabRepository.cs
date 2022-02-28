using BlabberApp.Domain.Entities;
using BlabberApp.Domain.Common.Interfaces;
using System.Collections.Generic;
using Domain.Entities;

namespace BlabberApp.DataStore.Plugins
{

    public class InMemBlabRepository : IBlabRepository
    {

        private List<Blab> _buf = new List<Blab>();

        public int Count()
        {
            return _buf.Count;
        }

        public void Add(Blab blab)
        {
            _buf.Add(blab);
        }

        public IEnumerable<Blab> GetAll()
        {
            return _buf;
        }

        public Blab GetById(Guid Id)
        {
            foreach (Blab blab in _buf)
            {
                if (Id.Equals(blab.Id)) return blab;
            }
            throw new Exception("Not found");
        }

        public void Update(Blab blab)
        {
            try
            {
                blab.Validate();
            } catch
            {
                throw new Exception("Invalid update");
            }

            foreach(Blab b in _buf)
            {
                if(blab.Id.Equals(b.Id))
                {
                    _buf.Remove(b);
                    _buf.Add(blab);
                }
            }
            throw new Exception("Not Found");
        }

        public void Remove(Blab blab)
        {
            _buf.Remove(blab);
        }

        public void RemoveAll()
        {
            _buf.Clear();
        }

        public IEnumerable<Blab> GetByUser(User usr) { throw new NotImplementedException(); }
        public IEnumerable<Blab> GetByDateTime(DateTime Dttm) { throw new NotImplementedException(); }

        

    }
}