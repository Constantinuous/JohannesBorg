using System;
using System.Data;
using System.Data.SQLite;
using FluentAssertions;
using JohannesBorg.Tests.CommandLine;
using MySql.Data.MySqlClient;
using NUnit.Framework;

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

        private IDbConnection _connection;

        [SetUp]
        public void BeforeEachTest()
        {
            if (CommandLine.Values.Context == Context.Travis)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine($"I'm running on Travis");
                _connection = new MySqlConnection(@"Server=127.0.0.1;Uid=root;Pwd=;Database=TEST_DB;");
            }
            else
            {
                _connection = new SQLiteConnection("FullUri=file::memory:?cache=shared");
            }
            _connection.Open();
        }

        [TearDown]
        public void AfterEachTest()
        {
            _connection.Dispose();
        }


        [Test]
        public void TestInsertAndSelectStatements()
        {
            new Class1().Should().NotBeNull();

            int resultCount = 0;

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
            using (var command = _connection.CreateCommand())
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
            resultCount.Should().Be(1);
        }
    }
}