namespace Domain;

public interface IBlabRepository: IRepository
{
    IEnumerable<IEntity> GetByUser(User usr);
    IEnumerable<IEntity> GetByDttm(DateTime Dttm);
}