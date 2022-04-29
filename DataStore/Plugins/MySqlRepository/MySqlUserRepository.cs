using BlabberApp.DataStore.Plugins;
using BlabberApp.Domain.Common.Interfaces;
using DataStore.Exceptions;
using Domain.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlabberApp.DataStore.Plugins
{
    public class MySqlUserRepository : MySqlPlugin, IUserRepository
    {
        //I do not like hardcoding this in a non-centralized file.  TBname is inevitable, but maybe dbname should move to DSN.cs
        private static string _dbname = "`rontene`";
        //I would call it user for consistency but that's an SQL reserved keyword
        private static string _tbname = "`users`";
        private readonly string _srcname = _dbname + "." + _tbname;

        public MySqlUserRepository(string connStr) : base(connStr)
        {

        }

        public void Add(User entity)
        {
            var _cmd = new MySqlCommand();
            _cmd.Connection = this.Conn;
            try
            {
                if (_cmd.Connection.State == ConnectionState.Closed)
                    _cmd.Connection.Open();
                if(entity.DttmModified.HasValue)
                {
                    _cmd.CommandText = $"INSERT INTO {_srcname} (sys_id, dttm_created, dttm_modified, dttm_lastlogin, email, username, first_name, last_name) " +
                    $"VALUES ('{entity.Id}', '{entity.DttmCreated.ToString("yyyy-MM-dd HH:mm:ss.fff")}', '{entity.DttmModified.Value.ToString("yyyy-MM-dd HH:mm:ss.fff")}', '{entity.lastloginDttm.ToString("yyyy-MM-dd HH:mm:ss.fff")}', '{entity.Email}', '{entity.Username}', '{entity.FirstName}', '{entity.LastName}')";
                }
                else
                {
                    _cmd.CommandText = $"INSERT INTO {_srcname} (sys_id, dttm_created, dttm_lastlogin, email, username, first_name, last_name) " +
                    $"VALUES ('{entity.Id}', '{entity.DttmCreated.ToString("yyyy-MM-dd HH:mm:ss.fff")}', '{entity.lastloginDttm.ToString("yyyy-MM-dd HH:mm:ss.fff")}', '{entity.Email}', '{entity.Username}', '{entity.FirstName}', '{entity.LastName}')";
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

        public IEnumerable<User> GetAll()
        {
            var _cmd = new MySqlCommand();
            _cmd.Connection = this.Conn;
            if (_cmd.Connection.State == ConnectionState.Closed)
                _cmd.Connection.Open();
            _cmd.CommandText = $"SELECT sys_id, dttm_created, dttm_lastlogin, email, username, first_name, last_name FROM {_srcname}";

            var reader = _cmd.ExecuteReader();

            List<User> buf = new();

            DataTable table = new();
            //I think readers can actually do a lot of what I'm doing with this datatable, but I am familiar with datatables so I'll use them.
            table.Load(reader);
            reader.Close();

            try
            {
                foreach (DataRow r in table.Rows)
                {
                    buf.Add(this.UserFromDataRow(r));
                }
            }
            catch (NullReferenceException e)
            {
                throw new Exception("null value in user data");
            }

            _cmd.Connection.Close();
            return buf;
        }

        public User GetByEmail(string email)
        {
            var _cmd = new MySqlCommand();
            _cmd.Connection = this.Conn;
            try
            {
                if (_cmd.Connection.State == ConnectionState.Closed)
                    _cmd.Connection.Open();
                _cmd.CommandText = "SELECT sys_id, dttm_created, dttm_lastlogin, email, username, first_name, last_name " +
                                   "FROM " + _srcname + " WHERE " + _srcname + ".`email` " +
                                   "LIKE '" + email + "'";
                var reader = _cmd.ExecuteReader();
                DataTable t = new();
                t.Load(reader);
                reader.Close();
                if(t.Rows.Count > 0)
                {
                    return this.UserFromDataRow(t.Rows[0]);
                }
                else
                {
                    throw new NotFoundException("user not found");
                }
                
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

        public User GetById(Guid Id)
        {
            var _cmd = new MySqlCommand();
            _cmd.Connection = this.Conn;
            try
            {
                if (_cmd.Connection.State == ConnectionState.Closed)
                    _cmd.Connection.Open();
                _cmd.CommandText = "SELECT sys_id, dttm_created, dttm_lastlogin, email, username, first_name, last_name " +
                                   "FROM " + _srcname + " WHERE " + _srcname + ".`sys_id` " +
                                   "LIKE '" + Id + "'";
                var reader = _cmd.ExecuteReader();
                DataTable t = new();
                t.Load(reader);
                reader.Close();
                if(t.Rows.Count > 0)
                {
                    return this.UserFromDataRow(t.Rows[0]);
                }
                else
                {
                    throw new NotFoundException("User not found");
                }
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

        public User GetByUsername(string username)
        {
            var _cmd = new MySqlCommand();
            _cmd.Connection = this.Conn;
            try
            {
                if (_cmd.Connection.State == ConnectionState.Closed)
                    _cmd.Connection.Open();
                _cmd.CommandText = "SELECT sys_id, dttm_created, dttm_lastlogin, email, username, first_name, last_name " +
                                   "FROM " + _srcname + " WHERE " + _srcname + ".`username` " +
                                   "LIKE '" + username + "'";
                var reader = _cmd.ExecuteReader();
                DataTable t = new();
                t.Load(reader);
                reader.Close();
                if (t.Rows.Count > 0)
                {
                    return this.UserFromDataRow(t.Rows[0]);
                }
                else
                {
                    throw new NotFoundException("user not found");
                }

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

        public void Remove(User entity)
        {
            var _cmd = new MySqlCommand();
            _cmd.Connection = this.Conn;
            try
            {
                if (_cmd.Connection.State == ConnectionState.Closed)
                    _cmd.Connection.Open();
                _cmd.CommandText = "DELETE " +
                                   "FROM " + _srcname + " WHERE " + _srcname + ".`email` " +
                                   "LIKE '" + entity.Email.ToString() + "'";
                var reader = _cmd.ExecuteReader();

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

        public void Update(User entity)
        {
            var _cmd = new MySqlCommand();
            _cmd.Connection = this.Conn;
            try
            {
                if (_cmd.Connection.State == ConnectionState.Closed)
                    _cmd.Connection.Open();

                _cmd.CommandText = $"UPDATE {_srcname} SET email = '{entity.Email.ToString()}', username = '{entity.Username}', first_name = '{entity.FirstName}', last_name='{entity.LastName}'" +
                    $"WHERE sys_id = '{entity.Id}'";
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

        public User UserFromDataRow(DataRow r)
        {
            //string _Email, string _Username, string _FirstName, string _LastName
            User u = new(r["email"].ToString(), r["username"].ToString(), r["first_name"].ToString(), r["last_name"].ToString());
            {
                u.Id = (Guid)r["sys_id"];
                u.DttmCreated = (DateTime)r["dttm_created"];
                //set dttm modified if there is one, otherwise have it null.
                u.DttmModified = r.Table.Columns.Contains("dttm_modified") ? (DateTime)r["dttm_modified"]: null; 
            }
            return u;
        }
    }
}
