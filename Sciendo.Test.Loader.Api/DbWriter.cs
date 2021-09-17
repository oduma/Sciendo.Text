using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Sciendo.Test.Loader.Api
{
    public class DbWriter : IWriter
    {
        private readonly IDbConnection dbConnection;

        public DbWriter (IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public IList<int> Write(IEnumerable<Item> batch)
        {
            if (dbConnection == null || dbConnection.State != ConnectionState.Open)
                throw new Exception("Cannot write to database.");
            var result = new List<int>();
            foreach(var item in batch)
            {
                var itemId = item.GetHashCode();
                var existingItem = dbConnection.Query<Item>("SELECT * FROM Messages where [When]=@When and Owner=@Owner and Subject=@Subject and Link=@Link", item);
                if (!existingItem.Any())
                {
                    dbConnection.Execute("INSERT INTO Messages( [When], Owner, Subject, Link, ContentType, MessageId) Values(@When, @Owner,@Subject,@Link,@ContentType, @MessageId)",
                        new { When = item.When, Owner = item.Owner, Subject = item.Subject, Link = item.Link, ContentType = (int)item.ContentType, MessageId = itemId });
                    result.Add(itemId);
                }
            }
            return result;
        }
    }
}
