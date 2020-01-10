// =======================================================================================
// Wovencore
// by Weaver (Fhiz)
// MIT licensed
// =======================================================================================

using wovencode;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using SQLite;

namespace wovencode
{
	
	// ===================================================================================
	// Database
	// ===================================================================================
	public partial class Database
	{
		
		// -------------------------------------------------------------------------------
		// Awake
		// Sets the singleton on awake, database can be accessed from anywhere by using it
		// -------------------------------------------------------------------------------
		public void Awake()
		{
			if (singleton == null) singleton = this;
			databaseLayer.Awake();
		}
		
		// -------------------------------------------------------------------------------
		// Init
		// creates/connects to the database and creates all tables
		// for a multiplayer server based game, this should only be called on the server
		// -------------------------------------------------------------------------------
		public void Init()
		{
			
			OpenConnection();
			
			this.InvokeInstanceDevExtMethods("Init");
			
			if (saveInterval > 0)
				InvokeRepeating(nameof(SavePlayers), saveInterval, saveInterval);
			
			if (deleteInterval > 0)
				InvokeRepeating(nameof(DeletePlayers), deleteInterval, deleteInterval);
				
		}
		
		// -------------------------------------------------------------------------------
		// Destruct
		// closes the connection, cancels saving and updates the checksum (if required)
		// for a multiplayer server based game, this should only be called on the server
		// -------------------------------------------------------------------------------
		public void Destruct()
		{
			CancelInvoke(nameof(SavePlayers));
			CancelInvoke(nameof(DeletePlayers));
			CloseConnection();
			this.InvokeInstanceDevExtMethods("Destruct");
		}
		
		// -------------------------------------------------------------------------------
		// OpenConnection
		// -------------------------------------------------------------------------------
		public void OpenConnection()
		{
			databaseLayer.OpenConnection();
		}
		
		// -------------------------------------------------------------------------------
		// CloseConnection
		// -------------------------------------------------------------------------------
		public void CloseConnection()
		{
			databaseLayer.CloseConnection();
		}
		
		// -------------------------------------------------------------------------------
		// CreateTable
		// -------------------------------------------------------------------------------
		public void CreateTable<T>()
		{
			databaseLayer.CreateTable<T>();
		}
		
		// -------------------------------------------------------------------------------
		// CreateIndex
		// -------------------------------------------------------------------------------
		public void CreateIndex(string tableName, string[] columnNames, bool unique = false)
		{
			databaseLayer.CreateIndex(tableName, columnNames, unique);
		}
		
		// -------------------------------------------------------------------------------
		// Query
		// -------------------------------------------------------------------------------
		public List<T> Query<T>(string query, params object[] args) where T : new()
		{
			return databaseLayer.Query<T>(query, args);
		}
		
		// -------------------------------------------------------------------------------
		// Execute
		// -------------------------------------------------------------------------------
		public void Execute(string query, params object[] args)
		{
			databaseLayer.Execute(query, args);
		}
		
		// -------------------------------------------------------------------------------
		// FindWithQuery
		// -------------------------------------------------------------------------------
		public T FindWithQuery<T>(string query, params object[] args) where T : new()
		{
			return databaseLayer.FindWithQuery<T>(query, args);
		}
		
		// -------------------------------------------------------------------------------
		// Insert
		// -------------------------------------------------------------------------------
		public void Insert(object obj)
		{
			databaseLayer.Insert(obj);
		}
		
		// -------------------------------------------------------------------------------
		// InsertOrReplace
		// -------------------------------------------------------------------------------
		public void InsertOrReplace(object obj)
		{
			databaseLayer.InsertOrReplace(obj);
		}
		
		// -------------------------------------------------------------------------------
		// BeginTransaction
		// -------------------------------------------------------------------------------
		public void BeginTransaction()
		{
			databaseLayer.BeginTransaction();
		}
		
		// -------------------------------------------------------------------------------
		// Commit
		// -------------------------------------------------------------------------------
		public void Commit()
		{
			databaseLayer.Commit();
		}
		
		// -------------------------------------------------------------------------------

	}

}

// =======================================================================================