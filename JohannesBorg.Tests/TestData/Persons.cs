using System.Data;

namespace JohannesBorg.Tests.TestData
{
    public class Persons
    {
        private IDbConnection _connection;

        public Persons(IDbConnection connection)
        {
            _connection = connection;
        }

        public void CreateTable()
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"
                    CREATE TABLE Persons
                    (
                    PersonID int,
                    LastName varchar(255),
                    FirstName varchar(255),
                    Address varchar(255),
                    City varchar(255)
                    ); 
                ";
                command.ExecuteNonQuery();
            }
        }

        public void Insert(int i, string lastName)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = $@"
                    INSERT INTO Persons (PersonID,LastName)
                        VALUES ({i},'{lastName}');
                ";
                command.ExecuteNonQuery();
            }
        }

        public int Count()
        {
            int resultCount = 0;
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"
                    SELECT * FROM Persons
                ";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        resultCount++;
                    }
                }
            }
            return resultCount;
        }
    }
}