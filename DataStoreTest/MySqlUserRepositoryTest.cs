using BlabberApp.DataStore.Plugins;
using BlabberApp.Domain.Common.Interfaces;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreTest
{
    [TestClass]
    public class MySqlUserRepositoryTest
    {

        //this class needs its own connection to the database to actually verify the data
        private MySql.Data.MySqlClient.MySqlConnection Conn;

        private MySqlUserRepository _repo;
        

        [TestInitialize]
        public void Initialize()
        {
            _repo = new MySqlUserRepository(BlabberApp.DataStore.Plugins.Dsn.DSN);
            Conn = new MySql.Data.MySqlClient.MySqlConnection(BlabberApp.DataStore.Plugins.Dsn.DSN);
        }

        [DataTestMethod]
        [DataRow("4ec08c9f-7cfd-4421-b249-6f16948fc893", "2022-03-30 03:27:53", "sample@gmail.com", "sample", "sample", "sampleman")]
        public void TestGetByEmail(string sys_id, string dttm_created, string email, string username, string first_name, string last_name)
        {
            //arrange
            MySqlCommand _cmd = new MySqlCommand();
            _cmd.Connection = this.Conn;
            if (_cmd.Connection.State == ConnectionState.Closed)
            {
                _cmd.Connection.Open();
            }                      
            _cmd.CommandText = $"insert into rontene.users (sys_id, dttm_created, email, username, first_name, last_name) values('{sys_id}', '{dttm_created}', '{email}', '{username}', '{first_name}', '{last_name}')";
            _cmd.ExecuteNonQuery();
            _cmd.Connection.Close();

            User e = new User(email, username, first_name, last_name); { e.Id = new Guid(sys_id);  e.DttmCreated = DateTime.Parse(dttm_created); }
            //act
            User a = _repo.GetByEmail(email);
            //assert
            Assert.IsTrue(e.AreEqual(a));
        }


        [DataTestMethod]
        [DataRow("4ec08c9f-7cfd-4421-b249-6f16948fc893", "2022-03-30 03:27:53", "sample@gmail.com", "sample", "sample", "sampleman")]
        public void TestGetById(string sys_id, string dttm_created, string email, string username, string first_name, string last_name)
        {
            //arrange
            MySqlCommand _cmd = new MySqlCommand();
            _cmd.Connection = this.Conn;
            if (_cmd.Connection.State == ConnectionState.Closed)
            {
                _cmd.Connection.Open();
            }
            _cmd.CommandText = $"insert into rontene.users (sys_id, dttm_created, email, username, first_name, last_name) values('{sys_id}', '{dttm_created}', '{email}', '{username}', '{first_name}', '{last_name}')";
            _cmd.ExecuteNonQuery();
            _cmd.Connection.Close();

            User e = new User(email, username, first_name, last_name);
            { e.Id = new Guid(sys_id); e.DttmCreated = DateTime.Parse(dttm_created); }
            //act
            User a = _repo.GetById(new Guid(sys_id));
            //assert
            Assert.IsTrue(e.AreEqual(a));
        }

        [DataTestMethod]
        [DataRow("4ec08c9f-7cfd-4421-b249-6f16948fc893", "2022-03-30 03:27:53", "sample@gmail.com", "sample", "sample", "sampleman")]
        public void TestRemove(string sys_id, string dttm_created, string email, string username, string first_name, string last_name)
        {
            //arrange
            MySqlCommand _cmd = new MySqlCommand();
            _cmd.Connection = this.Conn;
            if (_cmd.Connection.State == ConnectionState.Closed)
            {
                _cmd.Connection.Open();
            }
            _cmd.CommandText = $"insert into rontene.users (sys_id, dttm_created, email, username, first_name, last_name) values('{sys_id}', '{dttm_created}', '{email}', '{username}', '{first_name}', '{last_name}')";
            _cmd.ExecuteNonQuery();
            _cmd.Connection.Close();

            User e = new User(email, username, first_name, last_name);
            { e.Id = new Guid(sys_id); e.DttmCreated = DateTime.Parse(dttm_created); }
            //act
            _repo.Remove(e);
            //assert
            if (_cmd.Connection.State == ConnectionState.Closed)
            {
                _cmd.Connection.Open();
            }
            _cmd.CommandText = $"select * from rontene.users where sys_id = '{e.Id}'";
            var reader = _cmd.ExecuteReader();
            _cmd.Connection.Close();
            DataTable dt = new();
            dt.Load(reader);
            reader.Close();
            _cmd.Connection.Close();

            Assert.AreEqual(0, dt.Rows.Count);
        }


        [DataTestMethod]
        [DataRow("b8fc3919-80f5-450f-9030-c3b32a24d061", "2022-03-30 03:27:53", "addtest@gmail.com", "addtest", "addtest", "addtest")]
        public void TestAdd(string sys_id, string dttm_created, string email, string username, string first_name, string last_name)
        {
            //arrange
            User e = new User(email, username, first_name, last_name);
            { e.Id = new Guid(sys_id); e.DttmCreated = DateTime.Parse(dttm_created); }
            //act
            _repo.Add(e);
        
            MySqlCommand _cmd = new MySqlCommand();
            _cmd.Connection = this.Conn;
            if (_cmd.Connection.State == ConnectionState.Closed)
            {
                _cmd.Connection.Open();
            }
            _cmd.CommandText = $"select sys_id, dttm_created, dttm_lastlogin, email, username, first_name, last_name from " +
                $"rontene.users where sys_id = '{sys_id}'";
            var reader = _cmd.ExecuteReader();
            DataTable dt = new();
            dt.Load(reader);
            reader.Close();
            _cmd.Connection.Close();

            var a = _repo.UserFromDataRow(dt.Rows[0]);
            //assert
            //we could probaby test whether the Ids are the same, but to make 100% sure it is actually getting data from SQL I'll test on something I didn't give it.
            Assert.IsTrue(e.AreEqual(a));
        }

        [DataTestMethod]
        [DataRow("b8fc3919-80f5-450f-9030-c3b32a24d061", "2022-03-30 03:27:53", "addtest@gmail.com", "addtest", "addtest", "addtest")]
        public void TestUpdate(string sys_id, string dttm_created, string email, string username, string first_name, string last_name)
        {
            //arrange
            User e = new User(email, username, first_name, last_name);
            { e.Id = new Guid(sys_id); e.DttmCreated = DateTime.Parse(dttm_created); }
            _repo.Add(e);
            //act
            e.FirstName = "changedfirstname";
            _repo.Update(e);

            MySqlCommand _cmd = new MySqlCommand();
            _cmd.Connection = this.Conn;
            if (_cmd.Connection.State == ConnectionState.Closed)
            {
                _cmd.Connection.Open();
            }
            _cmd.CommandText = $"select sys_id, dttm_created, dttm_lastlogin, email, username, first_name, last_name from " +
                $"rontene.users where sys_id = '{sys_id}'";
            var reader = _cmd.ExecuteReader();
            DataTable dt = new();
            dt.Load(reader);
            reader.Close();
            _cmd.Connection.Close();

            var a = _repo.UserFromDataRow(dt.Rows[0]);
            //assert
            Assert.IsTrue(e.AreEqual(a));
        }

        //TODO: add tests with nullable datetimes, like dttm modified
        [DataTestMethod]
        [DataRow("b8fc3919-80f5-450f-9030-c3b32a24d061", "2022-03-30 03:27:53", "2022-03-30 03:27:53", "addtest@gmail.com", "addtest", "addtest", "addtest")]
        public void TestAddWithModifiedDttm(string sys_id, string dttm_created, string dttm_modified, string email, string username, string first_name, string last_name)
        {
            //arrange
            User e = new User(email, username, first_name, last_name);
            { e.Id = new Guid(sys_id); e.DttmCreated = DateTime.Parse(dttm_created);
                e.DttmModified = DateTime.Parse(dttm_modified);
            }
            //act
            _repo.Add(e);

            MySqlCommand _cmd = new MySqlCommand();
            _cmd.Connection = this.Conn;
            if (_cmd.Connection.State == ConnectionState.Closed)
            {
                _cmd.Connection.Open();
            }
            _cmd.CommandText = $"select sys_id, dttm_created, dttm_lastlogin, email, username, first_name, last_name from " +
                $"rontene.users where sys_id = '{sys_id}'";
            var reader = _cmd.ExecuteReader();
            DataTable dt = new();
            dt.Load(reader);
            reader.Close();
            _cmd.Connection.Close();

            var a = _repo.UserFromDataRow(dt.Rows[0]);
            //assert
            //we could probaby test whether the Ids are the same, but to make 100% sure it is actually getting data from SQL I'll test on something I didn't give it.
            Assert.IsTrue(e.AreEqual(a));
        }

        [TestMethod]
        public void TestGetAll()
        {
            //we can't ensure a particular value 
            var list = _repo.GetAll();
            Assert.IsNotNull(list);
        }

        //we are not unit testing delete_all for obvious reasons.  Honestly, I'm not even sure I'm ethically okay with it existing, but I guess its accidental use is a problem for the DBadmin.

        [TestCleanup]
        public void Cleanup()
        {
            MySqlCommand _cmd = new MySqlCommand();
            _cmd.Connection = this.Conn;
            if (_cmd.Connection.State == ConnectionState.Closed)
            {
                _cmd.Connection.Open();
            }
            //delete all the system ids these tests may have added to the database.
            _cmd.CommandText = "DELETE FROM rontene.users WHERE sys_id in ('4ec08c9f-7cfd-4421-b249-6f16948fc893', 'b8fc3919-80f5-450f-9030-c3b32a24d061')";
            _cmd.ExecuteNonQuery();
            _cmd.Connection.Close();
        }

    }
}
