using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD.DataAccess
{
    public class clsApplicationData
    {

        static string connectionString = clsDataAccessSettings.ConnectionString;

        static public bool FindApplicationByID(int ApplicationID, ref int ApplicantPersonID, ref DateTime ApplicationDate,
                                          ref int ApplicationTypeID, ref int ApplicationStatus, ref DateTime LastStatusDate,
                                          ref double PaidFees, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(connectionString);
            string Query = "Select * from Applications where ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();


                if (reader.Read())
                {
                    isFound = true;

                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = Convert.ToInt32(reader["ApplicationStatus"]);
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = Convert.ToDouble(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                }
                reader.Close();
            }
            catch (Exception Ex)
            {
                string Error = Ex.Message;

            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }

        static public int AddApplicaton(  int ApplicantPersonID,  DateTime ApplicationDate,
                                          int ApplicationTypeID, int ApplicationStatus, DateTime LastStatusDate,
                                          double PaidFees, int CreatedByUserID)
        {
            int ApplicationID = -1;

            SqlConnection connection = new SqlConnection(connectionString);

            string Query = @"Insert into Applications (ApplicantPersonID , ApplicationDate , ApplicationTypeID , ApplicationStatus ,
                            LastStatusDate , PaidFees , CreatedByUserID )
            values (@ApplicantPersonID , @ApplicationDate , @ApplicationTypeID , @ApplicationStatus ,
                            @LastStatusDate , @PaidFees , @CreatedByUserID );
            Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate); 
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ApplicationID = insertedID;

                }
            }
            catch (Exception ex)
            {
                string Error = ex.Message;

            }
            finally
            {
                connection.Close();
            }

            return ApplicationID;

        }

        static public bool UpdateApplication(int ApplicationID , int ApplicantPersonID, DateTime ApplicationDate,
                                          int ApplicationTypeID, int ApplicationStatus, DateTime LastStatusDate,
                                          double PaidFees, int CreatedByUserID)
        {

            bool isUpdated = false;

            SqlConnection connection = new SqlConnection(connectionString);

            string Query = @"UPDATE Applications SET ApplicantPersonID = @ApplicantPersonID,
                                            ApplicationDate = @ApplicationDate,
                                            ApplicationTypeID = @ApplicationTypeID,
                                            ApplicationStatus = @ApplicationStatus,
                                            LastStatusDate = @LastStatusDate,  
                                            PaidFees = @PaidFees,
                                            CreatedByUserID = @CreatedByUserID

                                            WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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

        static public bool DeleteApplication(int ApplicationID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(connectionString);
            string Query = @"Delete from Applications Where ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                string Error = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        static public bool IsApplicationExistByID(int ApplicationID)
        {
            bool isExist = false;

            SqlConnection connection = new SqlConnection(connectionString);
            string Query = "Select 1 from Applications where ApplicationID = @ApplicationID ";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    isExist = true;
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


            return isExist;
        }

        static public bool ChangeApplicationStatus(int ApplicationID, int ApplicationStatus)
        {
            bool IsUpdated = false;

            string Query = @"UPDATE Applications
                     SET ApplicationStatus = @ApplicationStatus,
                         LastStatusDate = @LastStatusDate
                     WHERE ApplicationID = @ApplicationID;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(Query, connection))
            {
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);

                try
                {
                    connection.Open();

                    int RowsAffected = command.ExecuteNonQuery();

                    if (RowsAffected > 0)
                        IsUpdated = true;
                }
                catch (Exception ex)
                {
                    string Error = ex.Message;
                }
            }

            return IsUpdated;
        }


        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {

            //incase the ActiveApplication ID !=-1 return true.
            return (GetActiveApplicationID(PersonID, ApplicationTypeID) != -1);
        }

        public static int GetActiveApplicationID(int PersonID, int ApplicationTypeID)
        {
            int ActiveApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT ActiveApplicationID=ApplicationID FROM Applications " +
                "WHERE ApplicantPersonID = @ApplicantPersonID" +
                " and ApplicationTypeID=@ApplicationTypeID and ApplicationStatus=1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int AppID))
                {
                    ActiveApplicationID = AppID;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return ActiveApplicationID;
            }
            finally
            {
                connection.Close();
            }

            return ActiveApplicationID;
        }


    }
}
