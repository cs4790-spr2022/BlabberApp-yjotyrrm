using BlabberApp.Domain.Common.Interfaces;
using BlabberApp.Domain.Entities;
using System.Net.Mail;

namespace Domain.Entities
{
	public class User : BaseEntity
	{

		public DateTime lastloginDttm { get; }
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

		//doesn't check any of the datetime fields, but if two users have the same Id but different creation datetimes you have bigger problems.
        public override bool AreEqual(IEntity entity)
        {
            try
            {
				User u = (User)entity;
				if (this.Id != u.Id) return false;
				if (this.Email.ToString() != u.Email.ToString()) return false;
				if (this.Username != u.Username) return false;
				if (this.FirstName != u.FirstName) return false;
				if (this.LastName != u.LastName) return false;
				return true;
			}
			catch {
				return false;
            }
        }

        public override bool Validate()
        {
			//email verification is handled by the MailAddress class so we don't need it here
			if (this.Username == null || this.Username == "") return false;
			return true;
        }

        
	}
}

