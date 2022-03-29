namespace BlabberApp.DataStore.Plugins
{

    public class MySqlPlugin
    {
        public readonly MySql.Data.MySqlClient.MySqlConnection Conn;
        private string connectionString; //Also know as DSN

        public MySqlPlugin(string connStr)
        {
            this.Conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
        }

        /// <summary>
        /// opens and closes the connection to ensure we can connect with the conneciton string.
        /// </summary>
        public void TestConnection()
        {
            try
            {
                this.Conn.Open();
                this.Conn.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            this.Conn.Close();
        }
    }
}