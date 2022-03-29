using BlabberApp.Domain.Common.Interfaces;
using BlabberApp.Domain.Entities;

namespace Domain.Entities
{
    public class Blab : BaseEntity
    {
        public string? Content { get; set; }

        public string User { get; set; }

        public Blab(string _user, string _Content)
        {
            User =  _user;
            Content = _Content;
        }

        public Guid GetId()
        {
            return Id;
        }

        override public void AreEqual(IEntity blab)
        {
            throw new NotImplementedException();
        }

        override public void Validate()
        {
            // TODO content exists
            // TODO a user exists
            // throw new InvalidDataException("Blab");
            throw new NotImplementedException();
        }
    }
}



    

