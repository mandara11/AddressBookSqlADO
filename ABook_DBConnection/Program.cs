using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABook_DBConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            AddressBookRepo aRepo = new AddressBookRepo();

            List<ContactsModel> contactsList = new List<ContactsModel>();

            //Retrieve all Contacts in AddressBook
            aRepo.RetrieveAllContacts();

            //Update a contact in AddressBook
            string updateQuery = @"update AddressInfo set State='T.L.' where FirstName='Mandara';";
            aRepo.UpdateContact(updateQuery);
            string testQuery = @"select State from AddressInfo where FirstName='Mandara';";
            Console.WriteLine(aRepo.RetrieveForTesting(testQuery));

            //Delete Rows with selected DateRange
            string deleteQuery = @"delete from ABookTable where DateAdded between '2012-07-29' and '2013-06-29';";
            aRepo.DeleteRowsForSelectedDateRange(deleteQuery);

            //Addnew Contact
            ContactsModel contact = new ContactsModel();
            contact.FirstName = "Ritwick";
            contact.LastName = "Sharma";
            contact.Address = "Lajpat Colony";
            contact.City = "Delhi";
            contact.State = "Chandigarh";
            contact.Zipcode = "490087";
            contact.PhoneNumber = "23456789";
            contact.DateAdded = Convert.ToDateTime("2015-03-30");
            contact.Email = "ritwick@yahoo.com";
            contact.RelationType = "Friend";
            aRepo.AddContact(contact);

            //Add Multiple Contacts Using thread
            ContactsModel contact1 = new ContactsModel();
            contact1.FirstName = "Ritwick";
            contact1.LastName = "Sharma";
            contact1.Address = "Lajpat Colony";
            contact1.City = "Delhi";
            contact1.State = "Chandigarh";
            contact1.Zipcode = "490087";
            contact1.PhoneNumber = "9823456789";
            contact1.DateAdded = Convert.ToDateTime("2015-03-30");
            contact1.Email = "ritwick@yahoo.com";
            contact1.RelationType = "Friend";

            ContactsModel contact2 = new ContactsModel();
            contact2.FirstName = "Sheela";
            contact2.LastName = "Rao";
            contact2.Address = "Faridabad Circle";
            contact2.City = "Delhi";
            contact2.State = "Chandigarh";
            contact2.Zipcode = "490087";
            contact2.PhoneNumber = "9223456789";
            contact2.DateAdded = Convert.ToDateTime("2015-03-30");
            contact2.Email = "raosheela@yahoo.com";
            contact2.RelationType = "Cousin";

            ContactsModel contact3 = new ContactsModel();
            contact.FirstName = "Ramesh";
            contact.LastName = "Verma";
            contact.Address = "CityCenter";
            contact.City = "Hyderabad";
            contact.State = "T.L.";
            contact.Zipcode = "497887";
            contact.PhoneNumber = "9145678998";
            contact.DateAdded = Convert.ToDateTime("2015-03-30");
            contact.Email = "vermaramesh@yahoo.com";
            contact.RelationType = "Colleague";

            contactsList.Add(contact1);
            contactsList.Add(contact2);
            contactsList.Add(contact3);

            aRepo.AddMultipleContactsUsingThreads(contactsList);

        }
    }
}