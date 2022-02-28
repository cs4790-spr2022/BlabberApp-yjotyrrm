namespace BlabberApp.DataStore.Plugins
{

    public class MySqlPlugin
    {
        private MySql.Data.MySqlClient.MySqlConnection? conn;
        private string connectionString; //Also know as DSN

        public MySqlPlugin(string connStr)
        {
            this.connectionString = connStr;
        }

        public void Connect()
        {
            try
            {
                this.conn = new MySql.Data.MySqlClient.MySqlConnection(
                    this.connectionString
                );
                this.conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}