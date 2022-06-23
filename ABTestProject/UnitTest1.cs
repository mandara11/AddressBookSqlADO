using NUnit.Framework;
using ABook_DBConnection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Assert = NUnit.Framework.Assert;

namespace ABTestProject
{
    public class UnitTest1
    {
        [SetUp]
        public void Setup()
        {
        }


        /// <summary>
        /// Test to check for functionality of Update Function
        /// </summary>
        [Test]
        public void GivenUpdateStatement_ShouldCheck_UpdatedRow()
        {
            AddressBookRepo aRepo = new AddressBookRepo();

            string updateQuery = @"update AddressInfo set State='T.L.' where FirstName='Mandara';";
            aRepo.UpdateContact(updateQuery);
            string testQuery = @"select State from AddressInfo where FirstName='Mandara';";
            string expectedData = aRepo.RetrieveForTesting(testQuery);
            string actualData = "T.L.";
            Assert.AreEqual(actualData, expectedData);
        }


        /// <summary>
        /// Test to check for functioality of Count Method to retrieve count of a city/state
        /// </summary>
        [Test]
        public void GivenQuery_ShouldReturn_CountofCityorState()
        {
            AddressBookRepo aRepo = new AddressBookRepo();

            /*  string cityCountQuery = @"select count(city) as Count from AddressInfo where City='Bhilai' group by City;";
              int actualCityCount= aRepo.CountData(cityCountQuery);
              int expectedCityCount = 1;
            */
            string stateCountQuery = @"select count(state) as Count from AddressInfo where state='C.G.' group by State;";
            int actualStateCount = aRepo.CountData(stateCountQuery);
            int expectedStateCount = 2;

            Assert.AreEqual(actualStateCount, expectedStateCount);
            //Assert.AreEqual(actualCityCount, expectedCityCount);
        }


        /// <summary>
        /// Test to check the number of rows deleted when used Delete Query
        /// </summary>
        [Test]
        public void GivenDeleteQuery_ShouldReturn_NumberofDeletedRows()
        {
            AddressBookRepo aRepo = new AddressBookRepo();
            //Delete Rows with selected DateRange
            string deleteQuery = @"delete from ABookTable where DateAdded between '2012-07-29' and '2013-06-29';";
            int actualRowsDeleted = aRepo.DeleteRowsForSelectedDateRange(deleteQuery);

            Assert.AreEqual(actualRowsDeleted, 3);
        }


        /// <summary>
        /// Test to check if the contacts are added successfully 
        /// </summary>
        [Test]
        public void GivenStoreProdecure_ShouldReturnTrue_ifContactsAddedSuccesfully()
        {
            AddressBookRepo aRepo = new AddressBookRepo();

            ContactsModel contact = new ContactsModel();
            contact.FirstName = "Reenu";
            contact.LastName = "Shaurya";
            contact.Address = "LJP Colony";
            contact.City = "Delhi";
            contact.State = "CHG";
            contact.Zipcode = "493487";
            contact.PhoneNumber = "23457889";
            contact.DateAdded = Convert.ToDateTime("2015-03-25");
            contact.Email = "rshaurya@yahoo.com";
            contact.RelationType = "Professional";
            bool actualStatus = aRepo.AddContact(contact);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(actualStatus, true);

        }
    }
}
