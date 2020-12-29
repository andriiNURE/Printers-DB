using MySql.Data.MySqlClient;

namespace Printers
{
    public class Proxy
    {
        private string login;
        public string Login
        {
            get { return login; }
            set { login = value; }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private Role role;

        Proxy()
        {
            role = Role.User;
        }

        public Proxy(string login, string password)
        {
            this.login = login;
            this.password = password;
            LogIn();
        }

        public void LogIn()
        {
            if (login.Length == 0 &&
                password.Length == 0)
            {
                role = Role.User;
                return;
            }

            string sqlExpression = 
                string.Format(@"Select `user`.`id_role` from `role` where `user`.`login` = ""{0}"" and `user`.`password` = ""{1}""",
                login, password);

            using (var connection = Connection.GetInstance().GetConnection())
            {
                connection.Open();

                MySqlCommand command =
                    new MySqlCommand(sqlExpression, connection);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        role = (Role)reader.GetValue(0);
                    }
                }
                reader.Close();
            }
        }

        public Role GetRole()
        {
            return role;
        }
    }
}
