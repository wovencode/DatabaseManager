// =======================================================================================
// Wovencore
// by Weaver (Fhiz)
// MIT licensed
// =======================================================================================

using wovencode;
using UnityEngine;
using System;
using System.Net;
using System.Collections.Generic;
using System.IO;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace wovencode
{
	
	// ===================================================================================
	// DatabaseLayerMySQL
	// ===================================================================================
	[System.Serializable]
	public partial class DatabaseLayerMySQL : DatabaseAbstractionLayer
	{
	
		[Header("Settings")]
        public string address 		= "127.0.0.1";
        public uint port 			= 3306;
        public string username 		= "root";
        public string password		= "";
        public string dbName 		= "mysqldatabase";
		public string charset 		= "utf8mb4";
		
		protected MySqlConnection connection = null;
		protected string connectionString = null;
		
		// ================================ API METHODS ==================================
		
		// -------------------------------------------------------------------------------
		// Init
		// -------------------------------------------------------------------------------
		public override void Init()
		{
		}
		
		// -------------------------------------------------------------------------------
		// OpenConnection
		// -------------------------------------------------------------------------------
		public override void OpenConnection()
		{
			connection.Open();
		}
		
		// -------------------------------------------------------------------------------
		// CloseConnection
		// -------------------------------------------------------------------------------
		public override void CloseConnection()
		{
			connection.Close();
		}
		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		public override void CreateTable<T>()
		{
		
		}
		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		public override void CreateIndex(string tableName, string[] columnNames, bool unique = false)
		{
		
		}
		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		public override List<T> Query<T>(string query, params object[] args)
		{
			return null;
		}
		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		public override void Execute(string query, params object[] args)
		{
		
		}
		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		public override T FindWithQuery<T>(string query, params object[] args)
		{
			return new T{};
		}
		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		public override void Insert(object obj)
		{
		}
		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		public override void InsertOrReplace(object obj)
		{
		
		}
		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		public override void BeginTransaction()
		{
			ExecuteNonQuery("START TRANSACTION");
		}
		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		public override void Commit()
		{
			ExecuteNonQuery("COMMIT");
		}
		
		// ============================== PRIVATE METHODS ================================
		
		/*
		
		ThirdPartyNotice:
		
		F. insthync/UnityMultiplayerARPG_MMO
		MIT License

		Copyright (c) 2018 Ittipon Teerapruettikulchai

		Permission is hereby granted, free of charge, to any person obtaining a copy
		of this software and associated documentation files (the "Software"), to deal
		in the Software without restriction, including without limitation the rights
		to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
		copies of the Software, and to permit persons to whom the Software is
		furnished to do so, subject to the following conditions:

		The above copyright notice and this permission notice shall be included in all
		copies or substantial portions of the Software.

		THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
		IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
		FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
		AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
		LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
		OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
		SOFTWARE.
		
		*/
		
		// -------------------------------------------------------------------------------
		// GetConnectionString
		// -------------------------------------------------------------------------------
		string GetConnectionString
		{
			get
			{
				if (connectionString == null)
				{
					MySqlConnectionStringBuilder connectionStringBuilder = new MySqlConnectionStringBuilder
					{
						Server 			= string.IsNullOrWhiteSpace(address) 	? "127.0.0.1" 	: address,
						Database 		= string.IsNullOrWhiteSpace(dbName) 	? "database" 	: dbName,
						UserID 			= string.IsNullOrWhiteSpace(username) 	? "root" 		: username,
						Password 		= string.IsNullOrWhiteSpace(password) 	? "password" 	: password,
						Port 			= port,
						CharacterSet 	= string.IsNullOrWhiteSpace(charset) 	? "utf8mb4" 	: charset
					};
					connectionString = connectionStringBuilder.ConnectionString;
				}
				return connectionString;
			}
		}
		
		// -------------------------------------------------------------------------------
		// NewConnection
		// -------------------------------------------------------------------------------
		MySqlConnection NewConnection()
        {
            return new MySqlConnection(GetConnectionString);
        }
		
		// -------------------------------------------------------------------------------
		// ExecuteInsertData
		// -------------------------------------------------------------------------------
        long ExecuteInsertData(string sql, params MySqlParameter[] args)
        {
            MySqlConnection connection = NewConnection();
            connection.Open();
            long result = ExecuteInsertData(connection, null, sql, args);
            connection.Close();
            return result;
        }
		
		// -------------------------------------------------------------------------------
		// ExecuteInsertData
		// -------------------------------------------------------------------------------
        long ExecuteInsertData(MySqlConnection connection, MySqlTransaction transaction, string sql, params MySqlParameter[] args)
        {
            bool createLocalConnection = false;
            if (connection == null)
            {
                connection = NewConnection();
                transaction = null;
                connection.Open();
                createLocalConnection = true;
            }
            long result = 0;
            using (MySqlCommand cmd = new MySqlCommand(sql, connection))
            {
                if (transaction != null)
                    cmd.Transaction = transaction;
                foreach (MySqlParameter arg in args)
                {
                    cmd.Parameters.Add(arg);
                }
                cmd.ExecuteNonQuery();
                result = cmd.LastInsertedId;
            }
            if (createLocalConnection)
                connection.Close();
            return result;
        }
		
		// -------------------------------------------------------------------------------
		// ExecuteNonQuery
		// -------------------------------------------------------------------------------
        int ExecuteNonQuery(string sql, params MySqlParameter[] args)
        {
            MySqlConnection connection = NewConnection();
            connection.Open();
            int result = ExecuteNonQuery(connection, null, sql, args);
            connection.Close();
            return result;
        }
		
		// -------------------------------------------------------------------------------
		// ExecuteNonQuery
		// -------------------------------------------------------------------------------
        int ExecuteNonQuery(MySqlConnection connection, MySqlTransaction transaction, string sql, params MySqlParameter[] args)
        {
            bool createLocalConnection = false;
            if (connection == null)
            {
                connection = NewConnection();
                transaction = null;
                connection.Open();
                createLocalConnection = true;
            }
            int numRows = 0;
            using (MySqlCommand cmd = new MySqlCommand(sql, connection))
            {
                if (transaction != null)
                    cmd.Transaction = transaction;
                foreach (MySqlParameter arg in args)
                {
                    cmd.Parameters.Add(arg);
                }
                numRows = cmd.ExecuteNonQuery();
            }
            if (createLocalConnection)
                connection.Close();
            return numRows;
        }
		
		// -------------------------------------------------------------------------------
		// ExecuteScalar
		// -------------------------------------------------------------------------------
        object ExecuteScalar(string sql, params MySqlParameter[] args)
        {
            MySqlConnection connection = NewConnection();
            connection.Open();
            object result = ExecuteScalar(connection, null, sql, args);
            connection.Close();
            return result;
        }
		
		// -------------------------------------------------------------------------------
		// ExecuteScalar
		// -------------------------------------------------------------------------------
        object ExecuteScalar(MySqlConnection connection, MySqlTransaction transaction, string sql, params MySqlParameter[] args)
        {
            bool createLocalConnection = false;
            if (connection == null)
            {
                connection = NewConnection();
                transaction = null;
                connection.Open();
                createLocalConnection = true;
            }
            object result;
            using (MySqlCommand cmd = new MySqlCommand(sql, connection))
            {
                if (transaction != null)
                    cmd.Transaction = transaction;
                foreach (MySqlParameter arg in args)
                {
                    cmd.Parameters.Add(arg);
                }
                result = cmd.ExecuteScalar();
            }
            if (createLocalConnection)
                connection.Close();
            return result;
        }
		
		// -------------------------------------------------------------------------------
		// ExecuteReader
		// -------------------------------------------------------------------------------
        MySQLRowsReader ExecuteReader(string sql, params MySqlParameter[] args)
        {
            MySqlConnection connection = NewConnection();
            connection.Open();
            MySQLRowsReader result = ExecuteReader(connection, null, sql, args);
            connection.Close();
            return result;
        }
		
		// -------------------------------------------------------------------------------
		// ExecuteReader
		// -------------------------------------------------------------------------------
        MySQLRowsReader ExecuteReader(MySqlConnection connection, MySqlTransaction transaction, string sql, params MySqlParameter[] args)
        {
            bool createLocalConnection = false;
            if (connection == null)
            {
                connection = NewConnection();
                transaction = null;
                connection.Open();
                createLocalConnection = true;
            }
            MySQLRowsReader result = new MySQLRowsReader();
            using (MySqlCommand cmd = new MySqlCommand(sql, connection))
            {
                if (transaction != null)
                    cmd.Transaction = transaction;
                foreach (MySqlParameter arg in args)
                {
                    cmd.Parameters.Add(arg);
                }
                MySqlDataReader dataReader = cmd.ExecuteReader();
                result.Init(dataReader);
                dataReader.Close();
            }
            if (createLocalConnection)
                connection.Close();
            return result;
        }
		
		// -------------------------------------------------------------------------------

	}

}

// =======================================================================================