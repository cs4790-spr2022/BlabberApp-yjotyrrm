namespace Domain;

public interface IRepository 
{
    // CRUD ~ AGED
    void Add(IEntity entity);
    IEntity GetByEntityId(Guid id);
    IEntity Get(IEntity entity);
    IEnumerable<IEntity> List();
    void Edit(IEntity entity);
    void Delete(IEntity entity);

}