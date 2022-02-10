using System.Net.Mail;

namespace Domain;
public class User: IEntity
{

	private Guid Id;

	public DateTime lastloginDttm {get;}
	public DateTime registeredDttm {get;}
	public MailAddress Email {get;set;}
	public string Username{get;set;}
	public virtual ICollection<Blab> Blabs {get;set;}

	public Guid GetId() {
		return Id;
	}
	public string? LastName {get;set;}
	public string? FirstName {get;set;}

	public string GetFullNameLastFirst() {
		return LastName + ", " + FirstName;
	}

	public string GetFullNameFirstLast() {
		return FirstName + " " + LastName;
	}

	public void Validate() 
	{
		//validation of mail address is implicit in the MailAddress class; the field is not nullable, and the class itself doesn't accept invalid formats for addresses.
		//validate username exists
		if(Username == "") {
			throw new InvalidUsernameException();
		}
		
	}

	public User(string _Email, string _Username, string _FirstName, string _LastName) 
	{
		FirstName = _FirstName;
		LastName = _LastName;
		Email = new MailAddress(_Email);
		Username = _Username;
	}


}
