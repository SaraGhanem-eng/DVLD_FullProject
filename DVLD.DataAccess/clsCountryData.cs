using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;

namespace DVLD.DataAccess
{
    public class clsCountryData
    {
        static string ConnectionString = clsDataAccessSettings.ConnectionString;


        public static bool FindCountryByID(int CountryID, ref string CountryName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * FROM Countries WHERE CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    CountryName = (string)reader["CountryName"];
                }


                reader.Close();

            }
            catch (Exception ex)
            {
                string Error = ex.Message;

            }
            finally // it will be executed rater than try happens of catch .
            {
                connection.Close();
            }


            return isFound;

        }

        public static bool FindCountryByCountryName(string CountryName, ref int CountryID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * FROM Countries WHERE CountryName = @CountryName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    CountryID = (int)reader["CountryID"];
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                string Error = ex.Message;

            }
            finally // it will be executed rater than try happens of catch .
            {
                connection.Close();
            }


            return isFound;

        }

        public static DataTable GetAllCountries()
        {
            DataTable DT = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionString);
            string Query = "Select * from Countries ";
            SqlCommand command = new SqlCommand(Query, connection);


            try
            {

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    DT.Load(reader);
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                string Error = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return DT;
        }


    }
}
