using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Printers
{
    class CompanyDAOImpl : ICompanyDAO
    {
        public Company Create(string name, Proxy proxy)
        {
            if (proxy.GetRole() == Role.User)
            {
                Console.WriteLine("Accses denied!");
                return null;
            }
            Company company = null;

            var sqlExpression =
                string.Format(@"INSERT INTO company (name) VALUES (""{0}"")", 
                    name);

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command = 
                    new MySqlCommand(sqlExpression, connection);

                int number = command.ExecuteNonQuery();
                Console.WriteLine($"Added {number} elements");
            }

            company = GetByName(name);

            return company;
        }

        public void Delete(Company company, Proxy proxy)
        {
            if (proxy.GetRole() == Role.User)
            {
                Console.WriteLine("Accses denied!");
                return null;
            }
            string sqlExpression =
                string.Format(@"DELETE FROM company WHERE id = {0}",
                    company.ID);

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command = 
                    new MySqlCommand(sqlExpression, connection);

                var number = command.ExecuteNonQuery();
                Console.WriteLine($"Deleted {number} rows");
            }
        }

        public List<Company> GetAll()
        {
            List<Company> list = 
                new List<Company>();

            string sqlExpression =
                "SELECT * FORM Company";

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command = 
                    new MySqlCommand(sqlExpression, connection);

                var reader =
                    command.ExecuteReader();

                object[] obj =
                    new object[reader.FieldCount];
                
                if (reader.HasRows)
                {
                    Console.WriteLine($"{reader.GetName(0)}\t{reader.GetName(1)}");

                    while (reader.Read())
                    {
                        object id = reader.GetValue(0);
                        object name = reader.GetValue(1);
                        var quantity = reader.GetValues(obj);

                        list.Add(new Company(obj));

                        Console.WriteLine($"{id}\t{name}");
                    }
                }

                reader.Close();
            }

            return list;
        }

        public Company GetById(int id)
        {
            Company company = null;
            string sqlExpression = 
                string.Format(@"SELECT * FROM company WHERE id = {0}",
                    id);

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command = 
                    new MySqlCommand(sqlExpression, connection);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine($"{reader.GetName(0)}\t{reader.GetName(1)}");

                    while (reader.Read())
                    {
                        company = 
                            new Company((int)reader.GetValue(0), (string)reader.GetValue(1));
                    }
                }
                reader.Close();
            }
            return company;
        }

        public Company GetByName(string companyname)
        {
            Company company = null;

            string sqlexpression = 
                string.Format(@"SELECT * FROM company WHERE name = ""{0}""", 
                companyname);

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command = 
                    new MySqlCommand(sqlexpression, connection);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine($"{reader.GetName(0)}\t{reader.GetName(1)}");

                    while (reader.Read())
                    {
                        company = 
                            new Company((int)reader.GetValue(0), (string)reader.GetValue(1));
                    }
                }
                reader.Close();
            }
            return company;
        }

        public void Update(Company company, string newName, Proxy proxy)
        {
            if (proxy.GetRole() == Role.User)
            {
                Console.WriteLine("Accses denied!");
                return null;
            }
            string sqlexpression =
                string.Format(@"UPDATE company SET name = ""{0}"" WHERE id = {1}", 
                newName, company.ID);

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command = 
                    new MySqlCommand(sqlexpression, connection);

                var number = command.ExecuteNonQuery();
                Console.WriteLine($"{number} rows was updated");
            }
        }
    }
}
