using Microsoft.Data.Sqlite;
using NUnit.Framework;
using Sciendo.Test.Loader.Api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Sciendo.TextLoader.Api.Tests
{
    [TestFixture]
    public class DbWriterTests
    {
        [Test]
        public void InsertNewItemOk()
        {
            IDbConnection dbConnection = new SqliteConnection($"Data Source=messages.db;");
            dbConnection.Open();
            var newItemBatch = new[] { new Item { When = DateTime.Now, Owner = "testOwner", ContentType = ContentType.Message, Link = "https://link.to.test/", Subject = "some text test" } };
            DbWriter dbWriter = new DbWriter(dbConnection);
            var actual = dbWriter.Write(newItemBatch);
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(newItemBatch[0].GetHashCode(), actual[0]);
        }

        [Test]
        public void NotInsertDuplicateItemOk()
        {
            IDbConnection dbConnection = new SqliteConnection($"Data Source=messages.db;");
            dbConnection.Open();
            var when = new DateTime(2021, 10, 01, 10, 01,00);
            var newItemBatch = new[] { new Item { When = when, Owner = "testOwner", ContentType = ContentType.Message, Link = "https://link.to.test/", Subject = "some text test" }, 
                new Item { When = when, Owner = "testOwner", ContentType = ContentType.Message, Link = "https://link.to.test/", Subject = "some text test" } };
            DbWriter dbWriter = new DbWriter(dbConnection);
            var actual = dbWriter.Write(newItemBatch);
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(newItemBatch[0].GetHashCode(), actual[0]);
        }
    }
}
