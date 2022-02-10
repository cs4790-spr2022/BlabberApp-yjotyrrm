namespace Domain;

public interface IEntity 
{
    Guid GetId();
    void Validate();

}