namespace Domain;
public interface IUserRepository : IRepository
{
    public void EditFirstName(string firstName);
    
}