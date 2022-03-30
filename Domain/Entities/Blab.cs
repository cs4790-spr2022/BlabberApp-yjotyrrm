using BlabberApp.Domain.Common.Interfaces;
using BlabberApp.Domain.Entities;

namespace Domain.Entities
{
    public class Blab : BaseEntity
    {
        public string? Content { get; set; }

        public User User { get; set; }

        public Blab(User _user, string _Content)
        {
            User =  _user;
            Content = _Content;
        }

        public Guid GetId()
        {
            return Id;
        }

        override public bool AreEqual(IEntity blab)
        {
            Blab other;
            try
            {
                other = (Blab)blab;
            }
            catch
            {
                return false;
            }
            if (!this.User.AreEqual(other.User)) return false;
            if (this.Content != other.Content) return false;
            return true;
        }

        override public bool Validate()
        {
            if (!User.Validate()) return false;
            //twitter clone business restriction, also to make sure that I don't cause problems for my sql where the content is only a varchar(128)
            if (this.Content.Length > 128) return false;
            if (this.Content == null) return false;
            if (this.Content == "") return false;
            return true;
        }
    }
}



    

