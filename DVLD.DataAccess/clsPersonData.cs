using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;

namespace DVLD.DataAccess
{
    public class clsPersonData
    {
        static string connectionString = clsDataAccessSettings.ConnectionString;

        static public bool FindPersonByID(int PersonID , ref string NationalNo , ref string FirstName ,
                                          ref string SecondName , ref string ThirdName , ref string LastName ,
                                          ref DateTime DateOfBirth , ref int Gender , ref string Address , ref string Phone,
                                          ref string Email , ref int NationalityCountryID , ref string ImagePath)
        {
            bool isFound = false;
             
            SqlConnection connection = new SqlConnection(connectionString);
            string Query = "Select * from People where PersonID = @PersonID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();


                if (reader.Read())
                {
                    isFound = true;

                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    LastName = (string)reader["LastName"];
                    Phone = (string)reader["Phone"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = Convert.ToInt32(reader["Gender"]);
                    Address = (string)reader["Address"];
                    NationalNo = (string)reader["NationalNo"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];


                    if (reader["ThirdName"] != DBNull.Value)
                        ThirdName = (string)reader["ThirdName"];
                    else
                        ThirdName = "";

                    if (reader["Email"] != DBNull.Value)
                        Email = (string)reader["Email"];
                    else
                        Email = "";


                    if (reader["ImagePath"] != DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];
                    else
                        ImagePath = "";



                }
                reader.Close();
            }
            catch(Exception Ex)
            {
                string Error = Ex.Message;
                
            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }


        static public bool FindPersonByNationalNumber(string NationalNumber, ref int PersonID, ref string FirstName,
                                  ref string SecondName, ref string ThirdName, ref string LastName,
                                  ref DateTime DateOfBirth, ref int Gender, ref string Address, ref string Phone,
                                  ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(connectionString);
            string Query = "Select * from People where NationalNo = @NationalNo";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("NationalNo", NationalNumber);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();


                if (reader.Read())
                {
                    isFound = true;
                    PersonID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    LastName = (string)reader["LastName"];
                    Phone = (string)reader["Phone"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = Convert.ToInt32(reader["Gender"]);
                    Address = (string)reader["Address"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];


                    if (reader["ThirdName"] != DBNull.Value)
                        ThirdName = (string)reader["ThirdName"];
                    else
                        ThirdName = "";

                    if (reader["Email"] != DBNull.Value)
                        Email = (string)reader["Email"];
                    else
                        Email = "";


                    if (reader["ImagePath"] != DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];
                    else
                        ImagePath = "";



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

        static public int AddPerson( string NationalNo,string FirstName,
                                           string SecondName, string ThirdName, string LastName,
                                           DateTime DateOfBirth, int Gender, string Address, string Phone,
                                           string Email, int NationalityCountryID,  string ImagePath)
        {
            int PersonID = -1;

            SqlConnection connection = new SqlConnection(connectionString);
            
            string Query = @"Insert into People (NationalNo , FirstName , SecondName ,ThirdName , LastName , DateOfBirth , Gender , Address 
            , Phone , Email , NationalityCountryID , ImagePath)
            values (@NationalNo , @FirstName , @SecondName , @ThirdName , @LastName , @DateOfBirth , @Gender , @Address
            , @Phone , @Email , @NationalityCountryID , @ImagePath );
            Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);



            if (ImagePath != "")
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else 
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);


            if (ThirdName != "")
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            if (Email != "")
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    PersonID = insertedID;

                }
            }
            catch(Exception ex)
            {
                string Error = ex.Message;

            }
            finally
            {
                connection.Close();
            }

            return PersonID;

        }

        static public bool UpdatePerson(int PersonID , string NationalNo, string FirstName,
                                           string SecondName, string ThirdName, string LastName,
                                           DateTime DateOfBirth, int Gender, string Address, string Phone,
                                           string Email, int NationalityCountryID, string ImagePath)
        {

            bool isUpdated = false;

            SqlConnection connection = new SqlConnection(connectionString);

            string Query = @"UPDATE People SET NationalNo = @NationalNo,
                                            FirstName = @FirstName,
                                            SecondName = @SecondName,
                                            ThirdName = @ThirdName,
                                            LastName = @LastName,  
                                            Gender = @Gender,
                                            Email = @Email, 
                                            Phone = @Phone, 
                                            Address = @Address, 
                                            NationalityCountryID = @NationalityCountryID,
                                            ImagePath = @ImagePath,
                                            DateOfBirth = @DateOfBirth
                                            WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);



            if (ImagePath != "")
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);


            if (ThirdName != "")
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            if (Email != "")
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                    isUpdated = true;

            }
            catch(Exception ex)
            {
                string Error = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return isUpdated;
        }
        
        static public bool DeletePerson (int PersonID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(connectionString);
            string Query = @"Delete from People Where PersonID = @PersonID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);


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

        static public DataTable GetPeopleList()
        {

            DataTable DT = new DataTable();

            SqlConnection connection = new SqlConnection(connectionString);
            string Query = "Select * from PeopleView ";
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

        static public DataTable FilterBy (string ColumnName , string SearchString)
        {
            DataTable dt = new DataTable();

            string Column = "";

            switch (ColumnName)
            {
                case "PersonID":
                    Column = "PersonID";
                    break;

                case "FirstName":
                    Column = "FirstName";
                    break;

                case "SecondName":
                    Column = "SecondName";
                    break;

                case "ThirdName":
                    Column = "ThirdName";
                    break;

                case "LastName":
                    Column = "LastName";
                    break;

                case "NationalNo":
                    Column = "NationalNo";
                    break;

                case "Nationality":
                    Column = "Nationality";
                    break;

                case "Gender":
                    Column = "Gender";
                    break;

                case "Phone":
                    Column = "Phone";
                    break;

                case "Email":
                    Column = "Email";
                    break;

                default:
                    return dt;
            }


            SqlConnection connection = new SqlConnection(connectionString);
            string Query = " ";
            SqlCommand command;

            if (Column != "PersonID")
            {
                Query = $"select * from PeopleView where {Column} Like @SearchString ";
            }
            else
            {
                Query = $"select * from PeopleView where {Column} = @SearchString ";
            }

            command = new SqlCommand(Query, connection);

            if (Column != "PersonID")
            {
                command.Parameters.AddWithValue("@SearchString", SearchString + "%");
            }
            else
            {

                    int PersonID = Convert.ToInt32(SearchString);
                    command.Parameters.AddWithValue("@SearchString", PersonID);
              

            }
            // command.Parameters.AddWithValue("@SearchString", SearchString + "%");

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                dt.Load(reader);
                reader.Close();

            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        static public bool IsPersonExistByID(int PersonID)
        {
            bool isExist = false;

            SqlConnection connection = new SqlConnection(connectionString);
            string Query = "Select 1 from People where PersonID = @PersonID ";
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

        static public bool IsPersonExistByNationalNo(string NationalNo)
        {
            bool isExist = false;

            SqlConnection connection = new SqlConnection(connectionString);
            string Query = "Select 1 from People where NationalNo = @NationalNo ";
            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue("@NationalNo", NationalNo);
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

        static public bool IsPersonExistByNationalNoExceptPersonID(string NationalNo,int  PersonID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(connectionString);

            string Query = @"SELECT 1
                     FROM People
                     WHERE NationalNo = @NationalNo
                     AND PersonID <> @PersonID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@PersonID", PersonID);

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
    }
}
