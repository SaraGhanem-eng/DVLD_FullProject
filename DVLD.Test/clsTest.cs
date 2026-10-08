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
