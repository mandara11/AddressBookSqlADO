using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABook_DBConnection
{

    public class AddressBookRepo
    {
        /// <summary>
        /// Setting up the connection
        /// </summary>
        public static string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=Abook;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);


        /// <summary>
        /// Method to retrieve and display all contacts from addressBook
        /// hence there are 4 interdependent tables, used joins
        /// </summary>
        public void RetrieveAllContacts()
        {
            try
            {
                ContactsModel contactModel = new ContactsModel();
                using (this.connection)
                {
                    string query = @"select n.FirstName, n.LastName, af.Address, af.City, af.State, af.Zipcode, cf.PhoneNumber,cf.Email, rt.RelationType"
                                    + " from AddressInfo af join ABookTable n on  af.FirstName= n.FirstName" +
                                    " join ContactInfo cf on cf.FirstName=af.FirstName  join RelationTable rt on af.FirstName=rt.FirstName;";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    System.Console.WriteLine("FirstName,LastName,Address,City,State,Zipcode,PhoneNumber,Email,RelationType");
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            contactModel.FirstName = dr.GetString(0);
                            contactModel.LastName = dr.GetString(1);
                            contactModel.Address = dr.GetString(2);
                            contactModel.City = dr.GetString(3);
                            contactModel.State = dr.GetString(4);
                            contactModel.Zipcode = dr.GetString(5);
                            contactModel.PhoneNumber = dr.GetString(6);
                            contactModel.Email = dr.GetString(7);
                            contactModel.RelationType = dr.GetString(8);
                            System.Console.WriteLine(contactModel.FirstName+","+contactModel.LastName + "," +contactModel.Address + "," +contactModel.City +
                                "," +contactModel.State + "," +contactModel.PhoneNumber + "," +contactModel.Email + "," +contactModel.RelationType);
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("No data found");
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }


        /// <summary>
        /// Method to execute update query
        /// </summary>
        /// <param name="updateQuery">query specifying what needs to be updated</param>
        public void UpdateContact(string updateQuery)
        {
            try
            {
                using (this.connection)
                {
                    string query = updateQuery;

                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        Console.WriteLine(rows + " row(s) affected");
                    }
                    else
                    {
                        Console.WriteLine("Please check your query");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Method to count the number of Contacts belonging to a city/state
        /// </summary>
        /// <param name="testQuery">query specifying the city/state to find count of</param>
        /// <returns></returns>
        public int CountData(string testQuery)
        {
            int count = 0;
            try
            {
                ContactsModel contactModel = new ContactsModel();
                using (this.connection)
                {
                    string query = testQuery;
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            count = dr.GetInt32(0);
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("No data found");
                    }
                }
                return count;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return count;
            }
        }


        /// <summary>
        /// Method to execute the delete query
        /// </summary>
        /// <param name="deleteQuery">query specifying the conditions on delete</param>
        /// <returns></returns>
        public int DeleteRowsForSelectedDateRange(string deleteQuery)
        {
            int rowsDeleted = 0;
            try
            {
                using (this.connection)
                {
                    string query = deleteQuery;

                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    rowsDeleted = cmd.ExecuteNonQuery();
                    if (rowsDeleted > 0)
                    {
                        Console.WriteLine(rowsDeleted + " row(s) affected");
                    }
                    else
                    {
                        Console.WriteLine("Please check your query");
                    }
                }
                return rowsDeleted;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Method to be specifically used in testing purposes for updation
        /// </summary>
        /// <param name="testQuery">query specifying the details of updation</param>
        /// <returns></returns>
        public string RetrieveForTesting(string testQuery)
        {
            string cString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=Abook;Integrated Security=True";
            SqlConnection connection2 = new SqlConnection(cString);

            string modifiedCity = "";
            try
            {
                ContactsModel contactModel = new ContactsModel();
                using (connection2)
                {
                    string query = testQuery;
                    SqlCommand cmd = new SqlCommand(query, connection2);
                    connection2.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            modifiedCity = dr.GetString(0);
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("No data found");
                    }
                }
                return modifiedCity;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return modifiedCity;
            }
        }

        /// <summary>
        /// MEthod to add a new contact with all the details
        /// </summary>
        /// <param name="model">Contact object to be added</param>
        /// <returns>true if contact added successfully</returns>
        public bool AddContact(ContactsModel model)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SpAddContactDetails", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", model.FirstName);
                    command.Parameters.AddWithValue("@LastName", model.LastName);
                    command.Parameters.AddWithValue("@DateAdded", model.DateAdded);
                    command.Parameters.AddWithValue("@Address", model.Address);
                    command.Parameters.AddWithValue("@City", model.City);
                    command.Parameters.AddWithValue("@State", model.State);
                    command.Parameters.AddWithValue("@Zipcode", model.Zipcode);
                    command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@RelationType", model.RelationType);

                    connection.Open();
                    var rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected!=0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// add contacts from a list using THREADS
        /// </summary>
        /// <param name="list">list of contacts</param>
        /// <returns></returns>
        public int AddMultipleContactsUsingThreads(List<ContactsModel> list)
        {
            int noOfContactsAdded = 0;
            list.ForEach(contact =>
            {
                noOfContactsAdded++;
                Task thread = new Task(() =>
                {
                    bool isAdded = AddContact(contact);
                });
                thread.Start();
            });
            return noOfContactsAdded;
        }




    }
}