using DVLD.DataAccess;
using DVLD.Business;
using System;
using System.Data;

namespace DVLD.Test
{
    public class clsTest
    {
        static void Main(string[] args)
        {

            //Console.Write("Enter an available PersonID #1: ");
            //int personID1 = int.Parse(Console.ReadLine());

            //Console.Write("Enter an available PersonID #2: ");
            //int personID2 = int.Parse(Console.ReadLine());

            //// Both people must exist and have no User.
            //if (!clsPerson.IsPersonExist(personID1) ||
            //    !clsPerson.IsPersonExist(personID2) ||
            //    clsUsers.IsUserExistByPersonID(personID1) ||
            //    clsUsers.IsUserExistByPersonID(personID2))
            //{
            //    Console.WriteLine("Choose two existing people without Users.");
            //    return;
            //}

            //string userName1 = "TestUser_" + Guid.NewGuid().ToString("N").Substring(0, 8);
            //string userName2 = "TestUser_" + Guid.NewGuid().ToString("N").Substring(0, 8);

            //clsUsers user1 = new clsUsers();
            //user1.PersonID = personID1;
            //user1.UserName = userName1;
            //user1.Password = "Test123";
            //user1.IsActive = true;

            //clsUsers user2 = new clsUsers();
            //user2.PersonID = personID2;
            //user2.UserName = userName2;
            //user2.Password = "Test123";
            //user2.IsActive = true;

            //clsUsers duplicateUserName = new clsUsers();
            //duplicateUserName.PersonID = personID2;
            //duplicateUserName.UserName = userName1;
            //duplicateUserName.Password = "Test123";
            //duplicateUserName.IsActive = true;

            //try
            //{
            //    // 1. Add first user.
            //    bool added1 = user1.Save();
            //    Console.WriteLine("1. Add first user: " + added1);

            //    if (!added1)
            //        return;

            //    // 2. Duplicate username must fail.
            //    bool duplicateNameResult = duplicateUserName.Save();
            //    Console.WriteLine("2. Duplicate username rejected: " +
            //                      !duplicateNameResult);

            //    // 3. Duplicate PersonID must fail.
            //    clsUsers duplicatePerson = new clsUsers();
            //    duplicatePerson.PersonID = personID1;
            //    duplicatePerson.UserName = "Other_" +
            //        Guid.NewGuid().ToString("N").Substring(0, 8);
            //    duplicatePerson.Password = "Test123";
            //    duplicatePerson.IsActive = true;

            //    bool duplicatePersonResult = duplicatePerson.Save();
            //    Console.WriteLine("3. Duplicate PersonID rejected: " +
            //                      !duplicatePersonResult);

            //    // 4. Add second user with a different username.
            //    bool added2 = user2.Save();
            //    Console.WriteLine("4. Add second user: " + added2);

            //    if (!added2)
            //        return;

            //    // 5. Updating without changing username must work.
            //    bool sameNameUpdate = user1.Save();
            //    Console.WriteLine("5. Keep current username: " +
            //                      sameNameUpdate);

            //    // 6. Updating to another user's username must fail.
            //    string originalName = user2.UserName;
            //    user2.UserName = user1.UserName;

            //    bool conflictUpdate = user2.Save();
            //    Console.WriteLine("6. Conflicting username rejected: " +
            //                      !conflictUpdate);

            //    // Restore the original username.
            //    user2.UserName = originalName;
            //    bool restored = user2.Save();
            //    Console.WriteLine("7. Restore username: " + restored);
            //}
            //finally
            //{
            //    // Clean up only the temporary users created by this test.
            //    if (user1.UserID > 0)
            //        Console.WriteLine("Delete user 1: " +
            //                          clsUsers.DeleteUser(user1.UserID));

            //    if (user2.UserID > 0)
            //        Console.WriteLine("Delete user 2: " +
            //                          clsUsers.DeleteUser(user2.UserID));
            //}
        
   

        // Change this to a valid PersonID
        // that does not already have a User.


        //int testPersonID = 1033;

        //string testUsername = "Soha@12";


        //string updatedUsername =
        //    testUsername + "_Updated";

        //string password = "0000";

        //int userID = 16;

        //Console.WriteLine("=== User Data Access Test ===");

        //// 1. Check whether the Person already has a User.
        //// This method must be added to clsUsersData.
        //if (clsUsersData.IsUserExistByPersonID(testPersonID))
        //{
        //    Console.WriteLine(
        //        "Choose another PersonID. This Person already has a User.");
        //    Console.ReadKey();
        //    return;
        //}

        //// 2. Add User
        //userID = clsUsersData.AddUser(
        //    testPersonID,
        //    testUsername,
        //    password,
        //    true);

        //if (userID == -1)
        //{
        //    Console.WriteLine(
        //        "AddUser failed. Check the PersonID and SQL errors.");
        //    Console.ReadKey();
        //    return;
        //}

        //Console.WriteLine($"AddUser: PASS - UserID = {userID}");

        // 3. Find User by ID
        //int personID = -1;
        //string username = "";
        //string foundPassword = "";
        //bool isActive = false;

        //bool foundByID = clsUsersData.FindUserByID(
        //    userID,
        //    ref personID,
        //    ref username,
        //    ref foundPassword,
        //    ref isActive);

        //Console.WriteLine(
        //    $"FindUserByID: {(foundByID && username == testUsername ? "PASS" : "FAIL")}");

        //// 4. Find User by Username
        //int foundUserID = -1;
        //int foundPersonID = -1;
        //string foundUserPassword = "";
        //bool foundIsActive = false;

        //bool foundByUsername = clsUsersData.FindUserByUserName(
        //    testUsername,
        //    ref foundUserID,
        //    ref foundPersonID,
        //    ref foundUserPassword,
        //    ref foundIsActive);

        //Console.WriteLine(
        //    $"FindUserByUserName: {(foundByUsername && foundUserID == userID && foundPersonID == testPersonID ? "PASS" : "FAIL")}");

        //// 5. Check existence
        //Console.WriteLine($"IsUserExistByID: {(clsUsersData.IsUserExistByID(userID) ? "PASS" : "FAIL")}");

        //Console.WriteLine($"IsUserExistByUserName: " + $"{(clsUsersData.IsUserExistByUserName(testUsername) ? "PASS" : "FAIL")}");

        //// 6. Get Users List
        //DataTable users = clsUsersData.GetUsersList();

        //Console.WriteLine(
        //    $"GetUsersList: {(users != null && users.Columns.Contains("UserID") && users.Columns.Contains("PersonID") && users.Columns.Contains("FullName") && users.Columns.Contains("UserName") && users.Columns.Contains("IsActive") ? "PASS" : "FAIL")}");

        //// 7. Update User
        //bool updated = clsUsersData.UpdateUser(
        //    userID,
        //    updatedUsername,
        //    password,
        //    false);

        //Console.WriteLine($"UpdateUser: {(updated ? "PASS" : "FAIL")}");

        //  // 8. Verify updated data
        // int personID = -1;
        // string   username = "";
        //string  foundPassword = "";
        //bool  isActive = true;

        //  bool foundUpdatedUser = clsUsersData.FindUserByID(
        //      userID,
        //      ref personID,
        //      ref username,
        //      ref foundPassword,
        //      ref isActive);

        //  Console.WriteLine(
        //      $"Verify Update: {(foundUpdatedUser && username == updatedUsername && isActive == false ? "PASS" : "FAIL")}");

        //// 9. Delete User
        //bool deleted = clsUsersData.DeleteUser(userID);

        //Console.WriteLine(
        //    $"DeleteUser: {(deleted ? "PASS" : "FAIL")}");

        //// 10. Verify deletion
        //bool stillExists = clsUsersData.IsUserExistByID(userID);

        //Console.WriteLine(
        //    $"Verify Delete: {(!stillExists ? "PASS" : "FAIL")}");

        //Console.WriteLine("\nTesting completed.");
        //Console.ReadKey();



        //   Console.WriteLine("===== DVLD DAL TEST =====");

        //   // 1. Get People List
        //   DataTable dtPeople = clsPersonData.GetPeopleList();

        //   Console.WriteLine("\n1- GetPeopleList");
        //   Console.WriteLine("People Count = " + dtPeople.Rows.Count);


        //   // 2. Test Find Person
        //   Console.WriteLine("\n2- FindPersonByID");

        //   int PersonID = 1;

        //   string NationalNo = "";
        //   string FirstName = "";
        //   string SecondName = "";
        //   string ThirdName = "";
        //   string LastName = "";
        //   DateTime DateOfBirth = DateTime.Now;
        //   int Gender = 0;
        //   string Address = "";
        //   string Phone = "";
        //   string Email = "";
        //   int NationalityCountryID = 0;
        //   string ImagePath = "";

        //   bool Found = clsPersonData.FindPersonByID(
        //       PersonID,
        //       ref NationalNo,
        //       ref FirstName,
        //       ref SecondName,
        //       ref ThirdName,
        //       ref LastName,
        //       ref DateOfBirth,
        //       ref Gender,
        //       ref Address,
        //       ref Phone,
        //       ref Email,
        //       ref NationalityCountryID,
        //       ref ImagePath
        //   );

        //   Console.WriteLine("Found = " + Found);

        //   if (Found)
        //   {
        //       Console.WriteLine("FirstName = [" + FirstName + "]");
        //       Console.WriteLine("SecondName = [" + SecondName + "]");
        //       Console.WriteLine("ThirdName = [" + ThirdName + "]");
        //       Console.WriteLine("LastName = [" + LastName + "]");

        //       Console.WriteLine("NationalNo = [" + NationalNo + "]");
        //       Console.WriteLine("Address = [" + Address + "]");
        //       Console.WriteLine("Phone = [" + Phone + "]");

        //       Console.WriteLine("Email = [" + Email + "]");
        //       Console.WriteLine("ImagePath = [" + ImagePath + "]");
        //   }


        //   // 3. Test Exists
        //   Console.WriteLine("\n3- IsPersonExistByID");

        //   Console.WriteLine(
        //       clsPersonData.IsPersonExistByID(1023)
        //   );

        //   Console.WriteLine(
        //   clsPersonData.IsPersonExistByID(9999)
        //   );

        ////   4.Test Add Person

        //  Console.WriteLine("\n4- AddPerson");

        //   int NewPersonID = clsPersonData.AddPerson(
        //       "TEST123456",
        //       "SaraTest",
        //       "Second",
        //       "",
        //       "Test",
        //       new DateTime(2000, 1, 1),
        //       1,
        //       "Test Address",
        //       "01000000000",
        //       "",
        //       1,
        //       ""
        //   );

        //   Console.WriteLine("New Person ID = " + NewPersonID);


        //   // 5. Find the new person
        //   Console.WriteLine("\n5- Find New Person");

        //   bool NewPersonFound = clsPersonData.IsPersonExistByID(NewPersonID);

        //   Console.WriteLine("Found = " + NewPersonFound);


        // //  6.Update Person
        //   Console.WriteLine("\n6- UpdatePerson");

        //   bool Updated = clsPersonData.UpdatePerson(
        //       1030,
        //       "TEST123456",
        //       "SaraUpdated",
        //       "Second",
        //       "",
        //       "Test",
        //       new DateTime(2000, 1, 1),
        //       1,
        //       "New Address",
        //       "01111111111",
        //       "",
        //       1,
        //       ""
        //   );

        //   Console.WriteLine("Updated = " + Updated);


        ////   7.Filter
        //   Console.WriteLine("\n7- FilterBy");

        //   DataTable dtFiltered =
        //       clsPersonData.FilterBy("FirstName", "Sara");

        //   Console.WriteLine(
        //       "Filtered Count = " + dtFiltered.Rows.Count
        //   );


        // //  8.Delete test person

        //  Console.WriteLine("\n8- DeletePerson");

        //   bool Deleted = clsPersonData.DeletePerson(1030);

        //   Console.WriteLine("Deleted = " + Deleted);


        // //  9.Make sure it was deleted

        //  Console.WriteLine("\n9- Check After Delete");

        //   Console.WriteLine(
        //       "Exists = " +
        //       clsPersonData.IsPersonExistByID(1030)
        //   );


        //   Console.WriteLine("\n===== TEST FINISHED =====");




        //    // Test Get All Countries
        //    Console.WriteLine("All Countries:");

        //    var Countries = clsCountry.GetAllCountries();

        //    foreach (System.Data.DataRow row in Countries.Rows)
        //    {
        //        Console.WriteLine(
        //            row["CountryID"] + " - " +
        //            row["CountryName"]
        //        );
        //    }


        //    // Test Find By ID
        //    Console.WriteLine("\nFind Country By ID:");

        //    clsCountry Country = clsCountry.FindByCountryID(200);

        //    if (Country != null)
        //    {
        //        Console.WriteLine(
        //            Country._CountryID + " - " +
        //            Country._CountryName
        //        );
        //    }
        //    else
        //    {
        //        Console.WriteLine("Country Not Found");
        //    }


        //    // Test Find By Name
        //    Console.WriteLine("\nFind Country By Name:");

        //    clsCountry Country2 =
        //        clsCountry.FindByCountryName("Egypt");

        //    if (Country2 != null)
        //    {
        //        Console.WriteLine(
        //            Country2._CountryID + " - " +
        //            Country2._CountryName
        //        );
        //    }
        //    else
        //    {
        //        Console.WriteLine("Country Not Found");
        //    }

    }
    }
}
