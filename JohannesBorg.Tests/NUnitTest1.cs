using System;
using System.Data;
using System.IO;
using FluentAssertions;
using JohannesBorg.Tests.CommandLine;
using MySql.Data.MySqlClient;
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
        public void TravisTest()
        {
            if (CommandLine.Values.Context == Context.Travis)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine($"I'm running on Travis");
                using (IDbConnection connection = new MySqlConnection(@"Server=127.0.0.1;Uid=root;Pwd=;Database=dotnet;"))
                {
                    connection.Open();
                }
                    
            }
        }


        [Test]
        public void TestSqlLite()
        {

            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Params are: {CommandLine.Values.Context}");

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