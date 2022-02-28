using BlabberApp.Domain.Common.Interfaces;

namespace BlabberApp.Domain.Entities
{

    public abstract class BaseEntity : IEntity
    {
        public abstract void AreEqual(IEntity entity);
        public abstract void Validate();
        public Guid Id { get; private set; }
        public DateTime DttmCreated { get; set; }
        public DateTime DttmModified { get; set; }

        public BaseEntity()
        {
            this.Id = Guid.NewGuid();
        }
    }
}