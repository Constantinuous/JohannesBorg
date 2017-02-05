using System.Data;
using System.Data.SQLite;
using FluentAssertions;
using NUnit.Framework;

namespace JohannesBorg.Tests
{
    [TestFixture]
    public class NUnitTest1
    {



        [Test]
        public void TestMethod1()
        {
            new Class1().Should().NotBeNull();
            

            int resultCount = 0;
            using (IDbConnection connection = new SQLiteConnection("FullUri=file::memory:?cache=shared"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
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
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        INSERT INTO Persons (PersonID,LastName)
                            VALUES (1,'blib');
                    ";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultCount++;
                        }
                    }
                }

                using (var command = connection.CreateCommand())
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
            }
            resultCount.Should().Be(1);
        }
    }
}