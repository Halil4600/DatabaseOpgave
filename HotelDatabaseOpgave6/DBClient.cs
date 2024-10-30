using HotelDatabaseOpgave6;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HotelDBConnection
{
    public class DBClient
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HotelDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public void Start()
        {
            List<Facility> facilities = ReadFacilities();
            foreach (var facility in facilities)
            {
                Console.WriteLine(facility);
            }

            // CRUD eksempel:
            CreateFacility(new Facility { Name = "Spa",});
            UpdateFacility(new Facility { Facility_no = 1, Name = "Updated Spa", });
            DeleteFacility(1);
        }

        // Create Facility
        public int CreateFacility(Facility facility)
        {
            string insertQuery = "INSERT INTO DemoFacility (Name) VALUES (@Name)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@Name", facility.Name);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"Facility created. Rows affected: {rowsAffected}");
                return rowsAffected;
            }
        }

        // Read Facilities (returns list of Facility objects)
        public List<Facility> ReadFacilities()
        {
            string queryString = "SELECT Facility_no, Name FROM DemoFacility";
            List<Facility> facilities = new List<Facility>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Facility facility = new Facility
                    {
                        Facility_no = reader.GetInt32(reader.GetOrdinal("Facility_no")),
                        Name = reader.GetString(reader.GetOrdinal("Name"))
                    };
                    facilities.Add(facility);
                }
                reader.Close();
            }
            return facilities;
        }


        // Update Facility
        public int UpdateFacility(Facility facility)
        {
            string updateQuery = "UPDATE DemoFacility SET Name = @NewName WHERE Facility_no = @FacilityNo";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@NewName", facility.Name);
                command.Parameters.AddWithValue("@FacilityNo", facility.Facility_no);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"Facility updated. Rows affected: {rowsAffected}");
                return rowsAffected;
            }
        }

        // Delete Facility
        public int DeleteFacility(int facilityNo)
        {
            string deleteQuery = "DELETE FROM DemoFacility WHERE Facility_no = @FacilityNo";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(deleteQuery, connection);
                command.Parameters.AddWithValue("@FacilityNo", facilityNo);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"Facility deleted. Rows affected: {rowsAffected}");
                return rowsAffected;
            }
        }
    }

}
