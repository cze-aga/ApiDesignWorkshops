using System;
using System.Data;

using Autofac;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Tests.Fixture
{
    internal sealed class DatabaseManager : IDisposable
    {
        public const string ConnectionString = "file:todo.db?mode=memory?cache=shared?nolock=1";

        private readonly IDbConnection connection;

        private readonly DbContext dbContext;

        public DatabaseManager(ILifetimeScope scope)
        {
            connection = CreateAndOpenDbConnection();

            using var nestedLifetimeScope = scope.BeginLifetimeScope();

            dbContext = scope.Resolve<DbContext>();

            dbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            dbContext.Dispose();
            connection.Dispose();
        }

        internal void ClearDatabase()
        {
            var tableListCommand = connection.CreateCommand();
            tableListCommand.CommandText = "select name from sqlite_master where type = 'table'";
            var result = tableListCommand.ExecuteReader();

            while (result.Read())
            {
                var clearTableCommand = connection.CreateCommand();
                clearTableCommand.CommandText = $"delete from {result.GetString(0)}";
                clearTableCommand.ExecuteNonQuery();
            }
        }

        private IDbConnection CreateAndOpenDbConnection()
        {
            var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
