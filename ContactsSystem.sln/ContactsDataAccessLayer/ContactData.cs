using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Net;
using System.Security.Policy;

namespace ContactsDataAccessLayer
{
    public class clsContactData
    {
        public static bool GetContactInfoByID(int ID, ref string FirstName, ref string LastName, ref string Email,
            ref string Phone, ref string Address, ref DateTime DateOfTime, ref string ImagePath, ref int CountryID)
        {
            bool IsFind = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = "SELECT * FROM Contacts WHERE ContactID = @ContactID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("ContactID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFind = true;

                    ID = (int)reader["ContactID"];
                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    DateOfTime = (DateTime)reader["DateOfBirth"];
                    ImagePath = (string)reader["ImagePath"].ToString();
                    CountryID = (int)reader["CountryID"];
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            finally
            {
                connection.Close();
            }

            return IsFind;
        }

        public static int AddNewContact(string FirstName, string LastName, string Email, string Phone, string Address,
            DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = @"
                            INSERT INTO [dbo].[Contacts]
                                ([FirstName], [LastName], [Email], [Phone], [Address], [DateOfBirth], [CountryID], [ImagePath])
                            VALUES
                                (@FirstName, @LastName, @Email, @Phone, @Address, @DateOfBirth, @CountryID, @ImagePath);
                                SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName",LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            if(ImagePath != "")
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }


            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    ID = InsertedID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return ID;
        }

        public static bool UpdateContact(int ContactID, string FirstName, string LastName, string Email, string Phone, string Address,
    DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            bool IsUpdated = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = @"UPDATE [dbo].[Contacts]
                       SET [FirstName] = @FirstName,
                          [LastName] = @LastName,
                          [Email] = @Email,
                          [Phone] = @Phone,
                          [Address] = @Address,
                          [DateOfBirth] = @DateOfBirth,
                          [CountryID] = @CountryID,
                          [ImagePath] = @ImagePath
                       WHERE ContactID = @ContactID";

            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue("@ContactID", ContactID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            if (ImagePath != "")
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }


            try
            {
                connection.Open();
                int roweffected = command.ExecuteNonQuery();

                if (roweffected > 0)
                {
                    IsUpdated = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return IsUpdated;
        }

        public static bool DeleteContact(int ContactID)
        {
            int rowsaffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = @"Delete Contacts Where ContactID = @ContactID;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"ContactID", ContactID);

            try
            {
                connection.Open();
                rowsaffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            finally
            {
                connection.Close();
            }
            return (rowsaffected > 0);
        }

        public static DataTable GetAllContacts()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = "SELECT * FROM Contacts;" ;

            SqlCommand command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }

            catch (System.Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }

            return dt;
        }

        public static bool IsContactExist(int ContactID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = @"SELECT Found = 1 from contacts WHERE ContactID = @ContactID;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"ContactID", ContactID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    IsFound = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            finally
            {
                connection.Close();
            }
            return IsFound;
        }


 
    }


}
