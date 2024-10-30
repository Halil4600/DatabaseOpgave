using HotelDBConnection;

namespace HotelDatabaseOpgave6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DBClient dbc = new DBClient();
            dbc.Start();
        }
    }
}
