using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD.DataAccess
{
    public class clsTestsTypesData
    {

        static string ConnectionString = clsDataAccessSettings.ConnectionString;

        public static bool FindTestTypeByID(int TestTypeID, ref string TestTypeTitle, ref string TestTypeDescription, ref double TestTypeFees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    TestTypeTitle = (string)reader["TestTypeTitle"];
                    TestTypeDescription = (string)reader["TestTypeDescription"];
                    TestTypeFees = Convert.ToDouble(reader["TestTypeFees"]);
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

        static public bool UpdateTestType(int TestTypeID, string TestTypeTitle, string TestTypeDescription ,double TestTypeFees)
        {

            bool isUpdated = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = @"UPDATE TestTypes SET TestTypeTitle = @TestTypeTitle,
                                            TestTypeFees = @TestTypeFees,
                                            TestTypeDescription = @TestTypeDescription
                                            WHERE TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                    isUpdated = true;

            }
            catch (Exception ex)
            {
                string Error = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return isUpdated;
        }

        public static DataTable GetTestsTypes()
        {
            DataTable DT = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionString);
            string Query = "Select * from TestTypes ";
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
