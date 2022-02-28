namespace BlabberApp.Domain.Common.Interfaces
{
    public interface IEntity
    {
        public void AreEqual(IEntity entity);
        public void Validate();
        public DateTime DttmCreated { get; set; }
        public DateTime DttmModified { get; set; }
    }
}
