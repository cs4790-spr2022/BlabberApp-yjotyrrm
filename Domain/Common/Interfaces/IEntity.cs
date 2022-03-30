namespace BlabberApp.Domain.Common.Interfaces
{
    public interface IEntity
    {
        public bool AreEqual(IEntity entity);
        public bool Validate();
        public DateTime DttmCreated { get; set; }
        public DateTime? DttmModified { get; set; }
    }
}
