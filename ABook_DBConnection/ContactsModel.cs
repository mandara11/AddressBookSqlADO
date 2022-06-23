using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABook_DBConnection
{
    /// <summary>
    /// Getters and Setters for all column names
    /// </summary>
    public class ContactsModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string RelationType { get; set; }
        public DateTime DateAdded { get; set; }


    }

}
