using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD.DataAccess
{
    public class clsApplicationTypesData
    {
        static string ConnectionString = clsDataAccessSettings.ConnectionString;

        public static bool FindApplicationTypeByID(int ApplicationTypeID, ref string Title , ref double Fees )
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    Title = (string)reader["ApplicationTypeTitle"];
                    Fees = Convert.ToDouble(reader["ApplicationFees"]);
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

        static public bool UpdateApplicationType(int ApplicationTypeID , string Title , double Fees)
        {

            bool isUpdated = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = @"UPDATE ApplicationTypes SET ApplicationTypeTitle = @ApplicationTypeTitle,
                                            ApplicationFees = @ApplicationFees
                                            WHERE ApplicationTypeID = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", Title);
            command.Parameters.AddWithValue("@ApplicationFees", Fees);

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

        public static DataTable GetAllApplicationTypes()
        {
            DataTable DT = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionString);
            string Query = "Select * from ApplicationTypes ";
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
