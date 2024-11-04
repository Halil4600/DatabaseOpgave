using HotelDatabaseOpgave6;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HotelDBConnection
{
    public class DBClient
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HotelDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private int GetMaxFacilityNo(SqlConnection connection)
        {
            Console.WriteLine("GetMaxFacilityNo");
            string queryStringMaxFacilityNo = "SELECT MAX(Facility_no) FROM DemoFacility";
            Console.WriteLine(queryStringMaxFacilityNo);

            SqlCommand command = new SqlCommand(queryStringMaxFacilityNo, connection);
            SqlDataReader reader = command.ExecuteReader();

            int maxFacilityNo = 0;
            if (reader.Read())
            {
                maxFacilityNo = reader.GetInt32(0);
            }
            reader.Close();
            Console.WriteLine($"Max FacilityNo: {maxFacilityNo}");
            return maxFacilityNo;
        }
        private int DeleteFacility(SqlConnection connection, int facilityNo)
        {
            Console.WriteLine("DeleteFacility");
            string deleteQuery = "DELETE FROM DemoFacility WHERE Facility_no = @FacilityNo";
            Console.WriteLine(deleteQuery);

            SqlCommand command = new SqlCommand(deleteQuery, connection);
            command.Parameters.AddWithValue("@FacilityNo", facilityNo);

            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"Facility deleted. Rows affected: {rowsAffected}");
            return rowsAffected;
        }

        private int UpdateFacility(SqlConnection connection, Facility facility)
        {
            Console.WriteLine("UpdateFacility");
            string updateQuery = "UPDATE DemoFacility SET Name = @NewName WHERE Facility_no = @FacilityNo";
            Console.WriteLine(updateQuery);

            SqlCommand command = new SqlCommand(updateQuery, connection);
            command.Parameters.AddWithValue("@NewName", facility.Name);
            command.Parameters.AddWithValue("@FacilityNo", facility.Facility_no);

            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"Facility updated. Rows affected: {rowsAffected}");
            return rowsAffected;
        }

        private int CreateFacility(SqlConnection connection, Facility facility)
        {
            Console.WriteLine("CreateFacility");
            string insertQuery = "INSERT INTO DemoFacility (Name) VALUES (@Name)";
            Console.WriteLine(insertQuery);

            SqlCommand command = new SqlCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@Name", facility.Name);

            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"Facility created. Rows affected: {rowsAffected}");
            return rowsAffected;
        }

        private List<Facility> ReadFacilities(SqlConnection connection)
        {
            Console.WriteLine("ReadFacilities");
            string queryString = "SELECT Facility_no, Name FROM DemoFacility";
            Console.WriteLine(queryString);

            List<Facility> facilities = new List<Facility>();

            SqlCommand command = new SqlCommand(queryString, connection);
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
            return facilities;
        }

        public void Start()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                List<Facility> facilities = ReadFacilities(connection);
                foreach (var facility in facilities)
                {
                    Console.WriteLine(facility);
                }

                //CreateFacility(connection, new Facility { Name = "Disco", });
                //UpdateFacility(connection, new Facility { Facility_no = 8, Name = "Updated Spa", });
                DeleteFacility(connection, 9);

                Console.WriteLine("After changes:");
                facilities = ReadFacilities(connection);
                foreach (var facility in facilities)
                {
                    Console.WriteLine(facility);
                }
            }
        }
    }
}
