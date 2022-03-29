using BlabberApp.Domain.Common.Interfaces;

namespace BlabberApp.Domain.Entities
{

    public abstract class BaseEntity : IEntity
    {
        public abstract void AreEqual(IEntity entity);
        public abstract void Validate();
        public Guid Id { get; set; }
        public DateTime DttmCreated { get; set; }
        public DateTime DttmModified { get; set; }

        /// <summary>
        /// the base entity, handles id and datetime created/modified.  Initializes ID and DttmCreated, so make sure to overwrite those when pulling from DB.
        /// </summary>
        public BaseEntity()
        {
            this.Id = Guid.NewGuid();
            DttmCreated = DateTime.UtcNow;
        }
    }
}