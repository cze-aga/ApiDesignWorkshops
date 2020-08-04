using System;
using System.Data;

using Autofac;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Todo.Tests.Fixture
{
    internal sealed class DatabaseManager : IDisposable
    {
        public const string ConnectionString = "DataSource=:memory:;Cache=Shared";

        public static readonly IDbConnection Connection = CreateAndOpenDbConnection();


        private readonly DbContext dbContext;

        public DatabaseManager(ILifetimeScope scope)
        {
            using var nestedLifetimeScope = scope.BeginLifetimeScope();

            dbContext = scope.Resolve<DbContext>();
            dbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            dbContext.Dispose();
            Connection.Dispose();
        }

        internal void ClearDatabase()
        {
            var tableListCommand = Connection.CreateCommand();
            tableListCommand.CommandText = "select name from sqlite_master where type = 'table'";
            var result = tableListCommand.ExecuteReader();

            while (result.Read())
            {
                var clearTableCommand = Connection.CreateCommand();
                clearTableCommand.CommandText = $"delete from {result.GetString(0)}";
                clearTableCommand.ExecuteNonQuery();
            }
        }

        private static IDbConnection CreateAndOpenDbConnection()
        {
            var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
