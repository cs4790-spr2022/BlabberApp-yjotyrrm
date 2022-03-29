using BlabberApp.Domain.Common.Interfaces;
using Domain.Entities;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Data;

namespace BlabberApp.DataStore.Plugins
{

    public class MySqlBlabRepository : MySqlPlugin, IBlabRepository
    {

        private readonly MySqlCommand _cmd;
        //I do not like hardcoding this in a non-centralized file.  TBname is inevitable, but maybe dbname should move to DSN.cs
        private static string _dbname = "`rontene`";
        private static string _tbname = "`blabs`";
        private readonly string _srcname = _dbname + "." + _tbname;

        public MySqlBlabRepository(string connStr) : base(connStr)
        {
            _cmd = new MySqlCommand();
            _cmd.Connection = this.Conn;
        }

        public void Add(Blab blab)
        {
            try
            {
                if (_cmd.Connection.State == ConnectionState.Closed)
                    _cmd.Connection.Open();

                _cmd.CommandText = $"INSERT INTO {_srcname} (sys_id, dttm_created, dttm_modified, content, usr) " +
                    $"VALUES ({blab.Id}, {blab.DttmCreated}, {blab.DttmModified}, {blab.Content}, {blab.User})";

                _cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception(
                    "Error " + ex.Number + " has occurred: " + ex.Message
                );
            }
            finally
            {
                _cmd.Connection.Close();
            }
        }

        public IEnumerable<Blab> GetAll()
        {
            if (_cmd.Connection.State == ConnectionState.Closed)
                _cmd.Connection.Open();
            _cmd.CommandText = $"SELECT sys_id, dttm_created, dttm_modified, content, usr FROM {_srcname}";

            var reader = _cmd.ExecuteReader();

            List<Blab> buf = new();

            DataTable table = new();
            //I think readers can actually do a lot of what I'm doing with this datatable, but I am familiar with datatables so I'll use them.
            table.Load(reader);
            reader.Close();

            try
            {
                foreach (DataRow r in table.Rows)
                {
                    buf.Add(this.BlabFromDataRow(r));
                }
            } 
            catch(NullReferenceException e)
            {
                throw new Exception("null value in blab data");
            }
            

            return buf;
        }

        public Blab GetById(Guid Id)
        {
            try
            {
                if(_cmd.Connection.State == ConnectionState.Closed)
                _cmd.Connection.Open();
                _cmd.CommandText = "SELECT sys_id, dttm_created, dttm_modified, content, usr " +
                                   "FROM " + _srcname + " WHERE " + _srcname + ".`sys_id` " +
                                   "LIKE '" + Id + "'";
                var reader = _cmd.ExecuteReader();
                DataTable t = new();
                t.Load(reader);
                reader.Close();
                return this.BlabFromDataRow(t.Rows[0]);
            }
            catch (MySqlException ex)
            {
                throw new Exception(
                    "Error " + ex.Number + " has occurred: " + ex.Message
                );
            }
            finally
            {
                _cmd.Connection.Close();
            }    
        }

        public void Update(Blab blab)
        {
            throw new NotImplementedException();
        }

        public void Remove(Blab blab)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            try
            {
                if (_cmd.Connection.State == ConnectionState.Closed)
                    _cmd.Connection.Open();
                _cmd.CommandText = "TRUNCATE " + _srcname;
                _cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception(
                    "Error " + ex.Number + " has occurred: " + ex.Message
                );
            }
            finally
            {
                _cmd.Connection.Close();
            }
        }

        public IEnumerable<Blab> GetByUser(User usr)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Blab> GetByDateTime(DateTime Dttm)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// helper to turn datarows from the SQL into blab objects.
        /// </summary>
        /// <param name="r">the row that holds the blab.</param>
        private Blab BlabFromDataRow(DataRow r)
        {
            Blab b = new(r["usr"].ToString(), r["content"].ToString());
            {
                b.Id = (Guid)r["sys_id"];
                b.DttmCreated = (DateTime)r["dttm_created"];
                b.DttmModified = (DateTime)r["dttm_modified"];
            }
            return b;
        }
    }
}