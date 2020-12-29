using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Printers
{
    class ComputerDAOImpl : IComputerDAO
    {
        public void Update(Computer computer, string newName, Proxy proxy)
        {
            if (proxy.GetRole() == Role.User)
            {
                Console.WriteLine("Accses denied!");
            }
            string sqlExpression =
                string.Format(@"UPDATE computer SET name = {0} WHERE id = {1}",
                    newName, computer.ID);

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command =
                    new MySqlCommand(sqlExpression, connection);

                var number = command.ExecuteNonQuery();
                Console.WriteLine($"{number} rows was updated");
            }
        }

        public void Delete(Computer computer, Proxy proxy)
        {
            if (proxy.GetRole() == Role.User)
            {
                Console.WriteLine("Accses denied!");
            }
            string sqlExpression =
                string.Format(@"DELETE FROM computer WHERE id = {0}",
                    computer.ID);

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command =
                    new MySqlCommand(sqlExpression, connection);

                var number = command.ExecuteNonQuery();
                Console.WriteLine($"{number} rows was deleted");
            }
        }

        public Computer Create(string name, Proxy proxy)
        {
            if (proxy.GetRole() == Role.User)
            {
                Console.WriteLine("Accses denied!");
                return null;
            }
            Computer computer = null;

            string sqlExpression =
                string.Format(@"INSERT INTO computer(name) VALUES (""{0}"")",
                    name);

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command =
                    new MySqlCommand(sqlExpression, connection);

                int number = command.ExecuteNonQuery();
                Console.WriteLine($"Added {number} rows");
            }

            computer = GetByName(name);

            return computer;
        }

        public Computer GetByName(string name)
        {
            Computer computer = null;

            string sqlExpression =
                string.Format(@"SELECT * FROM computer WHERE name = ""{0}""",
                    name);

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command =
                    new MySqlCommand(sqlExpression, connection);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine($"{reader.GetName(0)} \t{reader.GetName(1)}");

                    while (reader.Read())
                    {
                        computer = new Computer((int)reader.GetValue(0),
                            (string)reader.GetValue(1));
                    }
                }

                reader.Close();
            }

            return computer;
        }

        public Computer GetById(int id)
        {
            Computer computer = null;

            string sqlExpression =
                string.Format(@"SELECT * FROM computer WHERE id = {0}",
                    id);

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command =
                    new MySqlCommand(sqlExpression, connection);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine($"{reader.GetName(0)} \t{reader.GetName(1)}");

                    while (reader.Read())
                    {
                        computer = new Computer((int)reader.GetValue(0),
                            (string)reader.GetValue(1));
                    }
                }

                reader.Close();
            }

            return computer;
        }

        public List<Computer> GetAll()
        {
            List<Computer> computers =
     new List<Computer>();

            string sqlExpression =
                string.Format(@"SELECT * FROM computer");

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command =
                    new MySqlCommand(sqlExpression, connection);

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    Console.WriteLine($"{reader.GetName(0)} \t{reader.GetName(1)}");

                    while (reader.Read())
                    {
                        object id = reader.GetValue(0);
                        object name = reader.GetValue(1);

                        computers.Add(new Computer((int)id,
                            (string)name));
                    }
                }

                reader.Close();
            }

            return computers;
        }
    }
}
