using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using SQLite;
using static JohannesBorg.Tests.Common.Assembly;

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

            var folder = Path.Combine(AssemblyDirectory, "..", "tmp");
            var dirInfo = new DirectoryInfo(folder);

            if (dirInfo.Exists)
            {
                dirInfo.Delete(recursive: true);
                dirInfo.Refresh();
            }
            dirInfo.Create();

            int resultCount = 0;
            using (SQLiteConnection connection = new SQLiteConnection(System.IO.Path.Combine(folder, "foofoo.db")))
            {
                var tableCommand = connection.CreateCommand(CreateTable);
                tableCommand.ExecuteNonQuery();

                var insertCommand = connection.CreateCommand(@"
                        INSERT INTO Persons (PersonID,LastName)
                            VALUES (1,'blib');
                    ");

                insertCommand.ExecuteNonQuery();

                var selectCommand = connection.CreateCommand(@"
                        SELECT * FROM Persons
                    ");
                var data = selectCommand.ExecuteQuery<object>();
                foreach (var o in data)
                {
                    resultCount++;
                }
            }
            resultCount.Should().Be(1);
        }
    }
}