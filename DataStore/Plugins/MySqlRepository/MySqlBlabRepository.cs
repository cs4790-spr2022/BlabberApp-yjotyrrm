using BlabberApp.Domain.Common.Interfaces;
using Domain.Entities;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Data;

namespace BlabberApp.DataStore.Plugins
{

    public class MySqlBlabRepository : MySqlPlugin, IBlabRepository
    {
        //the example uses username as the primary key of the user, but it makes more sense for me for it to be email.

        private readonly MySqlCommand _cmd;
        //I do not like hardcoding this in a non-centralized file.  TBname is inevitable, but maybe dbname should move to DSN.cs
        private static string _dbname = "`rontene`";
        private static string _tbname = "`blab`";
        private readonly string _srcname = _dbname + "." + _tbname;
        private readonly IUserRepository _userRepo;

        public MySqlBlabRepository(string connStr, IUserRepository userRepo) : base(connStr)
        {
            _cmd = new MySqlCommand();
            _cmd.Connection = this.Conn;
            _userRepo = userRepo;
        }

        public void Add(Blab blab)
        {
            try
            {
                if (_cmd.Connection.State == ConnectionState.Closed)
                    _cmd.Connection.Open();

                if(blab.DttmModified.HasValue)
                {
                    _cmd.CommandText = $"INSERT INTO {_srcname} (sys_id, dttm_created, dttm_modified, content, usr_email) " +
                    $"VALUES ('{blab.Id}', '{blab.DttmCreated.ToString("yyyy-MM-dd HH:mm:ss.fff")}', '{blab.DttmModified.Value.ToString("yyyy-MM-dd HH:mm:ss.fff")}', '{blab.Content}', '{blab.User.Email}')";
                }
                else
                {
                    _cmd.CommandText = $"INSERT INTO {_srcname} (sys_id, dttm_created, content, usr_email) " +
                    $"VALUES ('{blab.Id}', '{blab.DttmCreated.ToString("yyyy-MM-dd HH:mm:ss.fff")}', '{blab.Content}', '{blab.User.Email}')";
                }
                

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
            _cmd.CommandText = $"SELECT sys_id, dttm_created, dttm_modified, content, usr_email FROM {_srcname}";

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
                _cmd.CommandText = "SELECT sys_id, dttm_created, dttm_modified, content, usr_email " +
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
            //I am not convinced that a single unprotected command that can delete your entire sql table is a good idea.
            //I'm leaving it in for now because I don't want to rewrite the interfaces, but I have deliberately not implemented it for now,
            //and I don't really intend to unless i need its fucntionality.
            throw new NotImplementedException();
            //try
            //{
            //    if (_cmd.Connection.State == ConnectionState.Closed)
            //        _cmd.Connection.Open();
            //    _cmd.CommandText = "TRUNCATE " + _srcname;
            //    _cmd.ExecuteNonQuery();
            //}
            //catch (MySqlException ex)
            //{
            //    throw new Exception(
            //        "Error " + ex.Number + " has occurred: " + ex.Message
            //    );
            //}
            //finally
            //{
            //    _cmd.Connection.Close();
            //}
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
            Blab b = new(_userRepo.GetByEmail(r["usr_email"].ToString()), r["content"].ToString());
            {
                b.Id = new Guid(r["sys_id"].ToString());
                b.DttmCreated = (DateTime)r["dttm_created"];
                try
                {
                    b.DttmModified = (DateTime)r["dttm_modified"];
                }
                catch
                {
                    b.DttmModified = null;
                }
            }
            return b;
        }
    }
}