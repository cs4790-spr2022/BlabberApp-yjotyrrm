using BlabberApp.DataStore.Plugins;
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
    public class MySqlBlabRepositoryTest
    {
        //this class needs its own connection to the database to actually verify the data
        private MySql.Data.MySqlClient.MySqlConnection Conn;

        private MySqlBlabRepository _repo;
        private MySqlUserRepository _userRepo;

        [TestInitialize]
        public void Initialize()
        {
            _userRepo = new MySqlUserRepository(BlabberApp.DataStore.Plugins.Dsn.DSN);
            _repo = new MySqlBlabRepository(BlabberApp.DataStore.Plugins.Dsn.DSN, _userRepo);
            Conn = new MySql.Data.MySqlClient.MySqlConnection(BlabberApp.DataStore.Plugins.Dsn.DSN);

            //add a user for our blabs to tie back to
            User u = new User("sample@gmail.com", "blabUnitTestUser", "yunit", "testor"); { u.Id = new Guid("2df095b0-c78d-4789-a5e4-ba9d1282c673"); }
            _userRepo.Add(u);
        }


        [DataTestMethod]
        [DataRow("e4b3745d-398e-42b5-abc5-b467e23cdbac", "2022-03-30 03:27:53", "test content", "sample@gmail.com")]
        public void TestGetById(string sys_id, string dttm_created, string content, string email)
        {
            //arrange
            MySqlCommand _cmd = new MySqlCommand();
            _cmd.Connection = this.Conn;
            if (_cmd.Connection.State == ConnectionState.Closed)
            {
                _cmd.Connection.Open();
            }
            _cmd.CommandText = $"insert into rontene.blab (sys_id, dttm_created, content, usr_email) values('{sys_id}', '{dttm_created}', '{content}', '{email}')";
            _cmd.ExecuteNonQuery();
            _cmd.Connection.Close();

            Blab e = new Blab(_userRepo.GetByEmail(email), content);
            { e.Id = new Guid(sys_id); e.DttmCreated = DateTime.Parse(dttm_created); }
            //act
            Blab a = _repo.GetById(new Guid(sys_id));
            //assert
            //we could probaby test whether the Ids are the same, but to make 100% sure it is actually getting data from SQL I'll test on something I didn't give it.
            Assert.AreEqual(e.Content, a.Content);
        }

        [DataTestMethod]
        [DataRow("ee01974d-3b77-4ec4-8e53-283aadc4e42f", "2022-03-30 03:27:53", "add test content", "sample@gmail.com")]
        public void TestAdd(string sys_id, string dttm_created, string content, string email)
        {
            //arrange
            Blab e = new Blab(_userRepo.GetByEmail(email), content);
            { e.Id = new Guid(sys_id); e.DttmCreated = DateTime.Parse(dttm_created); }
            //act
            _repo.Add(e);

            MySqlCommand _cmd = new MySqlCommand();
            _cmd.Connection = this.Conn;
            if (_cmd.Connection.State == ConnectionState.Closed)
            {
                _cmd.Connection.Open();
            }
            _cmd.CommandText = $"select sys_id, dttm_created, content, usr_email from " +
                $"rontene.blab where sys_id = '{sys_id}'";
            var reader = _cmd.ExecuteReader();
            DataTable dt = new();
            dt.Load(reader);
            reader.Close();
            _cmd.Connection.Close();

            //assert
            //we could probaby test whether the Ids are the same, but to make 100% sure it is actually getting data from SQL I'll test on something I didn't give it.
            Assert.AreEqual(content, dt.Rows[0]["content"]);
        }

        [DataTestMethod]
        [DataRow("ee01974d-3b77-4ec4-8e53-283aadc4e42f", "2022-03-30 03:27:53", "2022 - 03 - 30 03:27:53", "add test content", "sample@gmail.com")]
        public void TestAddWithModifiedDttm(string sys_id, string dttm_created, string dttm_modified, string content, string email)
        {
            //arrange
            Blab e = new Blab(_userRepo.GetByEmail(email), content);
            { e.Id = new Guid(sys_id); e.DttmCreated = DateTime.Parse(dttm_created); e.DttmModified = DateTime.Parse(dttm_modified); }
            //act
            _repo.Add(e);

            MySqlCommand _cmd = new MySqlCommand();
            _cmd.Connection = this.Conn;
            if (_cmd.Connection.State == ConnectionState.Closed)
            {
                _cmd.Connection.Open();
            }
            _cmd.CommandText = $"select sys_id, dttm_created, content, usr_email from " +
                $"rontene.blab where sys_id = '{sys_id}'";
            var reader = _cmd.ExecuteReader();
            DataTable dt = new();
            dt.Load(reader);
            reader.Close();
            _cmd.Connection.Close();

            //assert
            //we could probaby test whether the Ids are the same, but to make 100% sure it is actually getting data from SQL I'll test on something I didn't give it.
            Assert.AreEqual(content, dt.Rows[0]["content"]);
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
            _cmd.CommandText = "DELETE FROM rontene.blab WHERE sys_id in ('e4b3745d-398e-42b5-abc5-b467e23cdbac', 'ee01974d-3b77-4ec4-8e53-283aadc4e42f')";
            _cmd.ExecuteNonQuery();
            _cmd.CommandText = "DELETE FROM rontene.users WHERE sys_id = '2df095b0-c78d-4789-a5e4-ba9d1282c673'";
            _cmd.ExecuteNonQuery();
            _cmd.Connection.Close();
        }
    }
}
