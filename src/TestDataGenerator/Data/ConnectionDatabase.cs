﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Configuration;

namespace DataProducer.Data
{
    public class ConnectionDatabase : IDatabase
    {
        private readonly string _ConnectionString;
        private readonly DbProviderFactory _ProviderFactory;

        public ConnectionDatabase(string connectionStringName)
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];
            var connectionString = connectionStringSettings.ConnectionString;
            _ConnectionString = connectionString;
            _ProviderFactory = DbProviderFactories.GetFactory(connectionStringSettings.ProviderName);
        }

        public ConnectionDatabase(string connectionString, string providerName)
        {
            _ConnectionString = connectionString;
            _ProviderFactory = DbProviderFactories.GetFactory(providerName);
        }

        private DbConnection CreateConnection()
        {
            var connection = _ProviderFactory.CreateConnection();
            connection.ConnectionString = _ConnectionString;
            connection.Open();
            return connection;
        }

        public int ExecuteNonQuery(string sql, object parameters)
        {
            using (var connection = CreateConnection())
            {
                using (var cmd = connection.CreateCommand())
                {
                    return new CommandDatabase(cmd).ExecuteNonQuery(sql, parameters);
                }
            }
        }

        public IEnumerable<T> ExecuteDataReader<T>(string sql, object parameters, Func<DbDataReader, T> action)
        {
            using (var connection = CreateConnection())
            {
                using (var cmd = connection.CreateCommand())
                {
                    var db = new CommandDatabase(cmd);
                    // 这里一定要用yield，这样可以延迟执行，直接用return db.ExecuteDataReader(sql, parameters, action)在执行dr.Read()的时候，cmd对象早就释放掉了
                    foreach (var r in db.ExecuteDataReader(sql, parameters, action))
                        yield return r;
                }
            }
        }

        public void ExecuteDataReader(string sql, object parameters, Action<DbDataReader> action)
        {
            using (var connection = CreateConnection())
            {
                using (var cmd = connection.CreateCommand())
                {
                    var db = new CommandDatabase(cmd);
                    db.ExecuteDataReader(sql, parameters, action);
                }
            }
        }

        public void ExecuteTransaction(Action<IDatabase> action)
        {
            using (var connection = CreateConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.Transaction = transaction;

                            var db = new CommandDatabase(cmd);
                            db.ExecuteTransaction(action);
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public T ExecuteScalar<T>(string sql, object parameters)
        {
            using (var connection = CreateConnection())
            {
                using (var cmd = connection.CreateCommand())
                {
                    var db = new CommandDatabase(cmd);
                    return db.ExecuteScalar<T>(sql, parameters);
                }
            }
        }

        public void BulkCopy(DataTable table, int batchSize)
        {
            using (var connection = CreateConnection())
            {
                using (var cmd = connection.CreateCommand())
                {
                    var db = new CommandDatabase(cmd);
                    db.BulkCopy(table, batchSize);
                }
            }
        }

        public bool HasRow(string sql, object parameters)
        {
            using (var connection = CreateConnection())
            {
                using (var cmd = connection.CreateCommand())
                {
                    var db = new CommandDatabase(cmd);
                    return db.HasRow(sql, parameters);
                }
            }
        }

        public override string ToString()
        {
            return _ConnectionString;
        }
    }
}