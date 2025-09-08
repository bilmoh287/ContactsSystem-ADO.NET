using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ContactsDataAccessLayer
{
    public class clsCountryData
    {
        public static bool FindCountryByID(int CountryID, ref string CountryName, ref string Code, ref string PhoneCode)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = "SELECT * FROM Countries WHERE CountryID = @CountryID;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    CountryID = (int)reader["CountryID"];
                    CountryName = (string)reader["CountryName"];
                    Code = (string)reader["Code"].ToString();
                    PhoneCode = (string)reader["PhoneCode"].ToString();

                    IsFound = true;
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

            return IsFound;
        }

        public static bool FindCountryByName(string CountryName, ref int CountryID, ref string Code, ref string PhoneCode)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = "SELECT * FROM Countries WHERE CountryName = @CountryName;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    CountryID = (int)reader["CountryID"];
                    Code = (string)reader["Code"].ToString();
                    PhoneCode = (string)reader["PhoneCode"].ToString();
                    IsFound = true;
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

            return IsFound;
        }

        public static int AddNewCountry(string CountryName, string Code, string PhoneCode)
        {
            int CountryID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = @"
                            INSERT INTO [dbo].[Countries]
                                ([CountryName], [Code], [PhoneCode])
                            VALUES
                                (@CountryName, @Code, @PhoneCode);
                                SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);
            if (Code != "")
            {
                command.Parameters.AddWithValue("@Code", Code);
            }
            else
            {
                command.Parameters.AddWithValue("@Code", System.DBNull.Value);
            }
            if (PhoneCode != "")
            {
                command.Parameters.AddWithValue("@PhoneCode", PhoneCode);
            }
            else
            {
                command.Parameters.AddWithValue("@PhoneCode", System.DBNull.Value);
            }

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    CountryID = InsertedID;
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
            return CountryID;
        }

        public static bool UpdateCountry(int CountryID, string CountryName, string Code, string PhoneCode)
        {
            bool IsUpdated = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = @"UPDATE [dbo].[Countries]
                       SET [CountryName] = @CountryName,
                           [Code] = @Code,
                           [PhoneCode] = @PhoneCode
                       WHERE CountryID = @CountryID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            if (Code != "")
            {
                command.Parameters.AddWithValue("@Code", Code);
            }
            else
            {
                command.Parameters.AddWithValue("@Code", System.DBNull.Value);
            }
            if (PhoneCode != "")
            {
                command.Parameters.AddWithValue("@PhoneCode", PhoneCode);
            }
            else
            {
                command.Parameters.AddWithValue("@PhoneCode", System.DBNull.Value);
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

        public static bool IsCountryExist(int CountryID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = "SELECT Found = 1 FROM Countries WHERE CountryID = @CountryID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@CountryID", CountryID);

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

        public static bool IsCountryExist(string CountryName)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = "SELECT Found = 1 FROM Countries WHERE CountryName = @CountryName";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);

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

        public static bool DeleteCountry(int CountryID)
        {
            int rowsaffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = @"Delete Countries Where CountryID = @CountryID;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"CountryID", CountryID);

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

        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = "SELECT * FROM Countries;";

            SqlCommand command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
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
    }
}
