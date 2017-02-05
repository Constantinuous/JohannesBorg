using FluentAssertions;
using NUnit.Framework;
using SQLite;

namespace JohannesBorg.Tests
{
    [TestFixture]
    public class NUnitTest1
    {

        private readonly string CreateTable = @"
                    CREATE TABLE Persons
                    (
                    PersonID int,
                    LastName varchar(255),
                    FirstName varchar(255),
                    Address varchar(255),
                    City varchar(255)
                    ); 
        ";

        [Test]
        public void TestMethod1()
        {
            new Class1().Should().NotBeNull();


            int resultCount = 0;
            using (SQLiteConnection connection = new SQLiteConnection("foofoo"))
            {
                var tableCommand = connection.CreateCommand(CreateTable);
                tableCommand.ExecuteNonQuery();

                var insertCommand = connection.CreateCommand(@"
                        INSERT INTO Persons (PersonID,LastName)
                            VALUES (1,'blib');
                    ");

                var reader = insertCommand.ExecuteNonQuery();
                //foreach (var o in reader)
                //{
                //    resultCount++;
                //}

                //using (var command = connection.CreateCommand())
                //{
                //    command.CommandText = @"
                //        SELECT * FROM Persons
                //    ";
                //    using (var reader = command.ExecuteReader())
                //    {
                //        while (reader.Read())
                //        {
                //            resultCount++;
                //        }
                //    }
                //}
            }
            resultCount.Should().Be(1);
        }
    }
}