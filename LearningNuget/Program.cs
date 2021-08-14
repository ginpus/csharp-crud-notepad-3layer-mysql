using System;
using Humanizer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faker;

namespace LearningNuget
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var people = GeneratePersons(10);
        }

        public static IEnumerable<Person> GeneratePersons(int persons)
        {
            var listOfPersons = new List<Person> { };
            for (var i = 0; i < persons; i++)
            {
                var person = new Person
                {
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    Address = Faker.Address.StreetAddress(),
                    City = Faker.Address.City(),
                    Age = Faker.RandomNumber.Next(1, 100),
                    DateOfBirth = Faker.Identification.DateOfBirth()
                };
                Console.WriteLine(person);
                listOfPersons.Add(person);
            }
            return listOfPersons;
        }
    }
}