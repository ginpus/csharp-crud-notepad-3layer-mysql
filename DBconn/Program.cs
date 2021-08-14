using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DBconn
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var connectionString = "Server=localhost:3306;Uid=test;" + "Pwd=test;Database=noteapp";
            //same as above
            var connectionStringBuilder = new MySqlConnectionStringBuilder();

            connectionStringBuilder.Server = "localhost";
            connectionStringBuilder.Port = 3306;
            connectionStringBuilder.UserID = "test";
            connectionStringBuilder.Password = "test";
            connectionStringBuilder.Database = "noteapp";

            var connectionString = connectionStringBuilder.GetConnectionString(true);

            Console.WriteLine(connectionString);

            using var connection = new MySqlConnection(connectionString); // object represents connectivity

            connection.Open();

            var querySelect = "select * from customers";

            using var command = new MySqlCommand(querySelect, connection);

            var reader = command.ExecuteReader();

            var customers = new List<Customer>();

            while (reader.Read()) // marker, returns tru or false (for all entries in table)
            {
                customers.Add(new Customer
                {
                    CustomerId = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Email = reader.GetString(3),
                    Street = reader.GetString(4),
                    City = reader.GetString(5),
                    State = reader.GetString(6),
                    Age = reader.GetInt32(7)
                }
                );
            }

            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
                Console.WriteLine();
            }

            connection.Close();

            var fakeCustomers = GeneratePersons(10);

            foreach (var fakeCustomer in fakeCustomers)
            {
                connection.Open();

                // problem with backticked names:
                //var quryInsertFakeCustomer = "INSERT INTO customers (first_name, last_name, email, street, city, state, age) VALUES('" + fakeCustomer.FirstName + "','" + fakeCustomer.LastName + "','" + fakeCustomer.Email + "','" + fakeCustomer.Street + "','" + fakeCustomer.City + "','" + fakeCustomer.State + "','" + fakeCustomer.Age + "');";

                var quryInsertFakeCustomer = $"INSERT INTO customers (first_name, last_name, email, street, city, state, age) VALUES({fakeCustomer.CustomerToQuery()});";

                using var commandInsert = new MySqlCommand(quryInsertFakeCustomer, connection);

                var readerInsert = commandInsert.ExecuteReader();

                while (readerInsert.Read())
                {
                }

                Console.WriteLine(quryInsertFakeCustomer);

                connection.Close();
            }
        }

        public static IEnumerable<Customer> GeneratePersons(int customerCount)
        {
            var listOfCustomers = new List<Customer> { };
            for (var i = 0; i < customerCount; i++)
            {
                var customer = new Customer
                {
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    Email = Faker.Internet.Email(),
                    Street = Faker.Address.StreetAddress(),
                    City = Faker.Address.City(),
                    State = Faker.Address.UsState(),
                    Age = Faker.RandomNumber.Next(1, 100)
                };
                //Console.WriteLine(customer);
                listOfCustomers.Add(customer);
            }
            return listOfCustomers;
        }
    }
}