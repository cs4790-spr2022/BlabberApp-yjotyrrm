using BlabberApp.Domain.Common.Interfaces;
using BlabberApp.Domain.Entities;
using System.Net.Mail;

namespace Domain.Entities
{
	public class User : BaseEntity
	{

		public DateTime lastloginDttm { get; }
		public DateTime registeredDttm { get; }
		public MailAddress Email { get; set; }
		public string Username { get; set; }
		public virtual ICollection<Blab> Blabs { get; set; }

		public User(string _Email, string _Username, string _FirstName, string _LastName)
		{
			FirstName = _FirstName;
			LastName = _LastName;
			Email = new MailAddress(_Email);
			Username = _Username;
			
		}

		public Guid GetId()
		{
			return Id;
		}
		public string? LastName { get; set; }
		public string? FirstName { get; set; }

		public string GetFullNameLastFirst()
		{
			return LastName + ", " + FirstName;
		}

		public string GetFullNameFirstLast()
		{
			return FirstName + " " + LastName;
		}

        public override void AreEqual(IEntity entity)
        {
            throw new NotImplementedException();
        }

        public override void Validate()
        {
			//TODO email exists (this should not be initializable otherwise, so maybe unnecessary?)
			//TODO valid email
			//TODO username exists
            throw new NotImplementedException();
        }

        
	}
}

