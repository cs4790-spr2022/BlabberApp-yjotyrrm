namespace Domain;
public class Blab : IEntity
{
    public string? Content {get; set;}

    public User User {get;set;}

    public DateTime CreatedDttm {get;}

    private Guid Id;

    public Blab(User _user, string _Content) {
        User = _user;
        Id = Guid.NewGuid();
        Content = _Content;
        CreatedDttm = DateTime.UtcNow;
    }

    public Guid GetId() {
        return Id;
    }

    public void Validate() {
        if(Content == "" || Content == null) {
            throw new InvalidContentException();
        }

        //user just needs to exist, and that's guaranteed by the fact that it's not nullable
    }

    
}
