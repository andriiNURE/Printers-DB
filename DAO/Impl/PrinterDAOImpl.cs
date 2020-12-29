using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Printers
{
    class PrinterDAOImpl : IPrinterDAO
    {
        public Printer Create(string name, int company_id, int computer_id, Proxy proxy)
        {
            if (proxy.GetRole() == Role.User)
            {
                Console.WriteLine("Accses denied!");
                return null;
            }
            Printer printer = null;

            string sqlExpression =
                string.Format(@"INSERT INTO printer(name, company_id, computer_id) VALUES (""{0}"", {1}, {2})",
                    name, company_id, computer_id);

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command =
                    new MySqlCommand(sqlExpression, connection);

                int number = command.ExecuteNonQuery();
                Console.WriteLine($"Added {number} rows");
            }

            printer = GetByName(name);

            return printer;
        }
       
        public Printer Create(Printer toCopy, Proxy proxy)
        {
            if (proxy.GetRole() == Role.User)
            {
                Console.WriteLine("Accses denied!");
                return null;
            }
            return Create(toCopy.Name, 
                    toCopy.Company_ID, 
                    toCopy.Computer_ID, proxy);
        }

        public void Update(Printer printer, string newName, Proxy proxy)
        {
            if (proxy.GetRole() == Role.User)
            {
                Console.WriteLine("Accses denied!");
                return null;
            }
            string sqlExpression =
                string.Format(@"UPDATE printer SET name = ""{0}"" WHERE id = {1}",
                    newName, printer.ID);

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command =
                    new MySqlCommand(sqlExpression, connection);

                int number = command.ExecuteNonQuery();
                Console.WriteLine($"{number} rows was updated");
            }
        }

        public void Delete(Printer printer, Proxy proxy)
        {
            if (proxy.GetRole() == Role.User)
            {
                Console.WriteLine("Accses denied!");
                return null;
            }
            string sqlExpression =
                string.Format(@"DELETE FROM printer WHERE id = {0}", 
                    printer.ID);

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command = 
                    new MySqlCommand(sqlExpression, connection);

                var number = command.ExecuteNonQuery();
                Console.WriteLine($"Deleted {number} rows");
            }
        }

        public Printer GetById(int id)
        {
            Printer printer = null;

            string sqlExpression =
                string.Format(@"SELECT * FROM printer WHERE id = {0}",
                    id);

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command =
                    new MySqlCommand(sqlExpression, connection);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine($"{reader.GetName(0)} \t{reader.GetName(1)} \t{reader.GetName(2)} \t{reader.GetName(3)}");

                    while (reader.Read())
                    {
                        printer =
                            new Printer((int)reader.GetValue(0),
                                    (string)reader.GetValue(1),
                                    (int)reader.GetValue(2),
                                    (int)reader.GetValue(3));
                    }
                }

                reader.Close();
            }

            return printer;
        }

        public Printer GetByName(string name)
        {
            Printer printer = null;

            string sqlExpression =
                string.Format(@"SELECT * FROM printer WHERE name = ""{0}""",
                    name);

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command =
                    new MySqlCommand(sqlExpression, connection);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine($"{reader.GetName(0)} \t{reader.GetName(1)} \t{reader.GetName(2)} \t{reader.GetName(3)}");

                    while (reader.Read())
                    {
                        printer = new Printer((int)reader.GetValue(0),
                            (string)reader.GetValue(1),
                            (int)reader.GetValue(2),
                            (int)reader.GetValue(3));
                    }
                }

                reader.Close();
            }

            return printer;
        }

        public List<Printer> GetByComputer(int computer_id)
        {
            List<Printer> printers =
                new List<Printer>();

            string sqlExpression =
                string.Format(@"SELECT * FROM printer WHERE computer_id = {0}",
                    computer_id);

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command =
                    new MySqlCommand(sqlExpression, connection);

                var reader = command.ExecuteReader();

                object[] obj = new object[reader.FieldCount];
                if (reader.HasRows)
                {
                    Console.WriteLine($"{reader.GetName(0)} \t{reader.GetName(1)} \t{reader.GetName(2)} \t{reader.GetName(3)}");

                    while (reader.Read())
                    {
                        object id = reader.GetValue(0);
                        object name = reader.GetValue(1);
                        object company_id = reader.GetValue(2);
                        object computerid = reader.GetValue(3);

                        printers.Add(new Printer((int)id,
                            (string)name,
                            (int)computerid,
                            (int)company_id));
                    }
                }

                reader.Close();
            }

            return printers;
        }

        public List<Printer> GetByCompany(int company_id)
        {
            List<Printer> printers =
                new List<Printer>();

            string sqlExpression =
                string.Format(@"SELECT * FROM printer WHERE company_id = {0}",
                    company_id);

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command =
                    new MySqlCommand(sqlExpression, connection);

                var reader = command.ExecuteReader();

                object[] obj = new object[reader.FieldCount];
                if (reader.HasRows)
                {
                    Console.WriteLine($"{reader.GetName(0)} \t{reader.GetName(1)} \t{reader.GetName(2)} \t{reader.GetName(3)}");

                    while (reader.Read())
                    {
                        object id = reader.GetValue(0);
                        object name = reader.GetValue(1);
                        object companyid = reader.GetValue(2);
                        object computer_id = reader.GetValue(3);

                        printers.Add(new Printer((int)id,
                            (string)name,
                            (int)computer_id,
                            (int)companyid));
                    }
                }

                reader.Close();
            }

            return printers;
        }

        public List<Printer> GetAll()
        {
            List<Printer> printers =
                new List<Printer>();

            string sqlExpression =
                string.Format(@"SELECT * FROM printer");

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command =
                    new MySqlCommand(sqlExpression, connection);

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    Console.WriteLine($"{reader.GetName(0)} \t{reader.GetName(1)} \t{reader.GetName(2)} \t{reader.GetName(3)}");

                    while (reader.Read())
                    {
                        object id = reader.GetValue(0);
                        object name = reader.GetValue(1);
                        object company_id = reader.GetValue(2);
                        object computer_id = reader.GetValue(3);

                        printers.Add(new Printer((int)id,
                            (string)name,
                            (int)computer_id,
                            (int)company_id));
                    }
                }

                reader.Close();
            }

            return printers;
        }
    }
}
