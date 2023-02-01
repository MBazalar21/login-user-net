using System;
using MySql.Data.MySqlClient;

namespace UserRegistration
{
    class Program
    {
        private static readonly string ConnectionString = "server=localhost;port=3306;database=pruebanet;uid=root;pwd=root;";

        static void Main(string[] args)
        {
            RegisterUser();
            ShowUserList();
        }

        private static void RegisterUser()
        {
            Console.WriteLine("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter Last Name: ");
            string lastName = Console.ReadLine();

            Console.WriteLine("Enter Email: ");
            string email = Console.ReadLine();

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string query = $"INSERT INTO users (first_name, last_name, email) VALUES ('{firstName}', '{lastName}', '{email}')";
                using (var command = new MySqlCommand(query, connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} row(s) affected.");
                }
            }
        }

        private static void ShowUserList()
        {
            Console.WriteLine("User List:");
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT * FROM users";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User
                            {
                                Id = reader.GetInt32("id"),
                                FirstName = reader.GetString("first_name"),
                                LastName = reader.GetString("last_name"),
                                Email = reader.GetString("email")
                            };
                            Console.WriteLine($"{user.Id}. {user.FirstName} {user.LastName} ({user.Email})");
                        }
                    }
                }
            }
        }
    }
}