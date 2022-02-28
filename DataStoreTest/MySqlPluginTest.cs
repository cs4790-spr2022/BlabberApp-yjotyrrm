using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlabberApp.DataStore.Plugins;

namespace DataStoreTest
{
    [TestClass]
    public class MySqlPluginTest
    {
        [TestMethod]
        public void TestInstantiate()
        {
            string dsn = Dsn.DSN;
            MySqlPlugin e = new MySqlPlugin(dsn);
            MySqlPlugin a = new MySqlPlugin(dsn);
            Assert.AreEqual(e.ToString(), a.ToString());
        }

        [TestMethod]
        public void TestConnectWithGoodString()
        {
            string dsn = Dsn.DSN;
            MySqlPlugin plugin = new MySqlPlugin(dsn);
            //this throws an error if it fails so assert is unnecessary
            try
            {
                plugin.Connect();
            } 
            catch(Exception e)
            {
                throw new Exception("failed to connect with message: " + e.Message);
            }
            
        }
        [TestMethod]
        public void TestConnectWithBadString()
        {
            string dsn = "fakestring";
            MySqlPlugin plugin = new MySqlPlugin(dsn);
            try
            {
                plugin.Connect();
            }
            catch (Exception e)
            {
                Assert.AreEqual("Format of the initialization string does not conform to specification starting at index 0.", e.Message);
            }
        }
    }
}
