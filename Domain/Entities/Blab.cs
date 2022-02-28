using BlabberApp.Domain.Common.Interfaces;
using BlabberApp.Domain.Entities;

namespace Domain.Entities
{
    public class Blab : BaseEntity
    {
        public string? Content { get; set; }

        public User User { get; set; }

        public DateTime CreatedDttm { get; }

        private Guid Id;

        public Blab(User _user, string _Content)
        {
            User = _user;
            Id = Guid.NewGuid();
            Content = _Content;
            CreatedDttm = DateTime.UtcNow;
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



    

