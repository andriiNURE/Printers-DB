using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Printers
{
    class CartridgeDAOImpl : ICartridgeDAO
    {
        public Cartridge Update(Cartridge cartridge, string newName, Proxy proxy)
        {
            if(proxy.GetRole() == Role.User)
            {
                Console.WriteLine("Accses denied!");
                return null;
            }

            string sqlExpression =
                string.Format(@"UPDATE cartridge SET name = ""{0}"" WHERE id = {1}",
                     newName, cartridge.ID);

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command =
                    new MySqlCommand(sqlExpression, connection);

                var number = command.ExecuteNonQuery();
                Console.WriteLine($"{number} rows was updated");
            }

            return this.GetByName(newName);
        }

        public Cartridge UpdatePrinterID(Cartridge cartridge, int newPrinter, Proxy proxy)
        {
            if (proxy.GetRole() == Role.User)
            {
                Console.WriteLine("Accses denied!");
                return null;
            }

            string sqlExpression =
                string.Format(@"UPDATE cartridge SET id_printer = {0} WHERE id = {1}",
                     newPrinter, cartridge.ID);

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command =
                    new MySqlCommand(sqlExpression, connection);

                var number = command.ExecuteNonQuery();
                Console.WriteLine($"{number} rows was updated");
            }

            return this.GetByName(cartridge.Name);
        }
        
        public Cartridge Update(Cartridge cartridge, float newPaint, Proxy proxy)
        {
            if (proxy.GetRole() == Role.User)
            {
                Console.WriteLine("Accses denied!");
                return null;
            }

            string sqlExpression =
                string.Format(@"UPDATE cartridge SET amount = {0} WHERE id = {1}",
                     newPaint, cartridge.ID);

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command =
                    new MySqlCommand(sqlExpression, connection);

                var number = command.ExecuteNonQuery();
                Console.WriteLine($"{number} rows was updated");
            }

            return this.GetByName(cartridge.Name);
        }

        public Cartridge Update(Cartridge cartridge, Cartridge newCartridge, Proxy proxy)
        {
            if (proxy.GetRole() == Role.User)
            {
                Console.WriteLine("Accses denied!");
                return null;
            }

            Update(cartridge, newCartridge.Name, proxy);
            UpdatePrinterID(cartridge, newCartridge.Printer_ID, proxy);
            Update(cartridge, newCartridge.Amount, proxy);

            return GetByName(newCartridge.Name);
        }

        public void Delete(Cartridge cartridge, Proxy proxy)
        {
            if (proxy.GetRole() == Role.User)
            {
                Console.WriteLine("Accses denied!");
                return;
            }

            string sqlExpression =
                string.Format(@"DELETE FROM cartridge WHERE id = {0}",
                    cartridge.ID);

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command =
                    new MySqlCommand(sqlExpression, connection);

                var number = command.ExecuteNonQuery();
                Console.WriteLine($"Deleted {number} rows");
            }
        }

        public Cartridge Create(int printer_id, string name, bool isRGB, float amount, Proxy proxy)
        {
            if (proxy.GetRole() == Role.User)
            {
                Console.WriteLine("Accses denied!");
                return null;
            }

            return Create(new Cartridge.Builder(name).setPrinterID(printer_id).setRGB(isRGB).setAmount(amount).Build(), proxy);
        }

        public Cartridge Create(Cartridge newCartridge, Proxy proxy)
        {
            if (proxy.GetRole() == Role.User)
            {
                Console.WriteLine("Accses denied!");
                return null;
            }

            Cartridge cartridge = null;

            var sqlExpression =
                string.Format(@"INSERT INTO cartridge (id_printer, name, isRGB, amount) VALUES ({0}, ""{1}"", {2}, {3})",
                    newCartridge.Printer_ID, newCartridge.Name, Convert.ToInt32(newCartridge.IsRGB), newCartridge.Amount);

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command =
                    new MySqlCommand(sqlExpression, connection);

                int number = command.ExecuteNonQuery();
                Console.WriteLine($"Added {number} elements");
            }

            cartridge = GetByName(newCartridge.Name);

            return cartridge;
        }

        public Cartridge GetByName(string name)
        {
            Cartridge cartridge = null;

            string sqlExpression =
                string.Format(@"SELECT * FROM cartridge WHERE name = ""{0}""",
                    name);

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
                        cartridge = new Cartridge((int)reader.GetValue(0),
                            (int)reader.GetValue(1),
                            (string)reader.GetValue(2),
                            Convert.ToBoolean(reader.GetValue(3)),
                            (float)reader.GetValue(4)); ;
                    }
                }
                reader.Close();
            }
            return cartridge;
        }

        public Cartridge GetById(int id)
        {
            Cartridge cartridge = null;

            string sqlExpression =
                string.Format(@"SELECT * FROM cartridge WHERE id = id",
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
                        cartridge = new Cartridge((int)reader.GetValue(0),
                            (int)reader.GetValue(1),
                            (string)reader.GetValue(2),
                            Convert.ToBoolean(reader.GetValue(3)),
                            (float)reader.GetValue(4));
                    }
                }
                reader.Close();
            }
            return cartridge;
        }

        public List<Cartridge> GetByPrinter(Printer printer)
        {
            List<Cartridge> cartridges =
                new List<Cartridge>();

            string sqlExpression =
                string.Format(@"SELECT * FORM cartridge WHERE id_printer = {0}",
                    printer.ID);

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
                    Console.WriteLine($"{reader.GetName(0)} \t{reader.GetName(1)} \t{reader.GetName(2)} \t{reader.GetName(3)} \t{reader.GetName(4)}");

                    while (reader.Read())
                    {
                        object id = reader.GetValue(0);
                        object printer_id = reader.GetValue(1);
                        object name = reader.GetValue(2);
                        object isRGB = reader.GetValue(3);
                        object amount = reader.GetValue(4);

                        cartridges.Add(new Cartridge((int)id,
                            (int)printer_id,
                            (string)name,
                            (bool)isRGB,
                            (float)amount));

                        Console.WriteLine($"{id} \t{printer_id} \t{name} \t{isRGB} \t{amount}");
                    }
                }

                reader.Close();
            }

            return cartridges;
        }

        public List<Cartridge> GetAll()
        {
            List<Cartridge> cartridges =
                new List<Cartridge>();

            string sqlExpression =
                "SELECT * FORM cartridge";

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
                    Console.WriteLine($"{reader.GetName(0)} \t{reader.GetName(1)} \t{reader.GetName(2)} \t{reader.GetName(3)} \t{reader.GetName(4)}");

                    while (reader.Read())
                    {
                        object id = reader.GetValue(0);
                        object printer_id = reader.GetValue(1);
                        object name = reader.GetValue(2);
                        object isRGB = reader.GetValue(3);
                        object amount = reader.GetValue(4);

                        cartridges.Add(new Cartridge((int)id,
                            (int)printer_id,
                            (string)name,
                            (bool)isRGB,
                            (float)amount));

                        Console.WriteLine($"{id} \t{printer_id} \t{name} \t{isRGB} \t{amount}");
                    }
                }

                reader.Close();
            }

            return cartridges;
        }

        public Cartridge Restore(Cartridge cartridge, Memento memento, Proxy proxy)
        {
            if (proxy.GetRole() == Role.User)
            {
                Console.WriteLine("Accses denied!");
                return null;
            }

            return Update(cartridge, memento.GetState(), proxy);
        }
    }
}
