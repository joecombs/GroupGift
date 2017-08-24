using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupGift.Models
{
    public class UserContact
    {
        public UserContact(string firstName, string lastName, string emailId)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailId = emailId;
        }

        public bool IsSelected { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
    }
}
