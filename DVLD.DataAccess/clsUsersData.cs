using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.DataAccess
{
    public class clsUsersData
    {

        static string connectionString = clsDataAccessSettings.ConnectionString;

        static public bool FindUserByID(int UserID, ref int PersonID, ref string UserName, ref string Password ,ref bool IsActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(connectionString);
            string Query = "Select * from Users where UserID = @UserID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();


                if (reader.Read())
                {
                    isFound = true;

                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];


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

        static public bool FindUserByUserNameAndPassword(string UserName,string Password ,ref int UserID ,  ref int PersonID, ref bool IsActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(connectionString);
            string Query = "Select * from Users where UserName = @UserName and Password = @Password";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();


                if (reader.Read())
                {
                    isFound = true;

                    PersonID = (int)reader["PersonID"];
                    UserID = (int)reader["UserID"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];


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

        static public int AddUser(int PersonID , string UserName , string Password , bool IsActive)
        {
            int UserID = -1;

            SqlConnection connection = new SqlConnection(connectionString);

            string Query = @"Insert into Users (PersonID , UserName , Password , IsActive )
            values (@PersonID , @UserName , @Password , @IsActive );
            Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    UserID = insertedID;

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

            return UserID;

        }

        static public bool UpdateUser(int UserID ,  string UserName, string Password, bool IsActive)
        {

            bool isUpdated = false;

            SqlConnection connection = new SqlConnection(connectionString);

            string Query = @"UPDATE Users SET 
                                              UserName = @UserName,
                                              Password = @Password,
                                              IsActive = @IsActive
                                              WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@UserID", UserID);

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

        static public bool DeleteUser(int UserID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(connectionString);
            string Query = @"Delete from Users Where UserID = @UserID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

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

        static public DataTable GetUsersList()
        {
            DataTable DT = new DataTable();

            SqlConnection connection = new SqlConnection(connectionString);
            string Query = "Select * from UsersView ";

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

        static public bool IsUserExistByID(int UserID)
        {
            bool isExist = false;

            SqlConnection connection = new SqlConnection(connectionString);
            string Query = "Select 1 from Users where UserID = @UserID ";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

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

        static public bool IsUserExistByPersonID(int PersonID)
        {
            bool isExist = false;

            SqlConnection connection = new SqlConnection(connectionString);
            string Query = "Select 1 from Users where PersonID = @PersonID ";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

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

        static public bool IsUserExistByUserName(string UserName)
        {
            bool isExist = false;

            SqlConnection connection = new SqlConnection(connectionString);
            string Query = "Select 1 from Users where UserName = @UserName ";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);

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
    
      
        static public bool IsUserNameExistExceptUserID(string UserName , int UserID )
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(connectionString);

            string Query = @"SELECT 1
                     FROM Users
                     WHERE UserName = @UserName
                     AND UserID <> @UserID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    IsFound = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        static public bool ChangePassword(int UserID , string Password)
        {
            bool isUpdated = false;

            SqlConnection connection = new SqlConnection(connectionString);

            string Query = @"UPDATE Users SET Password = @Password WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@UserID", UserID);

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
    
    }
}
