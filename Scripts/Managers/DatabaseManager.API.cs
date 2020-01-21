// =======================================================================================
// Wovencore
// by Weaver (Fhiz)
// MIT licensed
// =======================================================================================

using Wovencode;
using Wovencode.Database;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using SQLite;

namespace Wovencode.Database
{
	
	// ===================================================================================
	// DatabaseManager
	// ===================================================================================
	public partial class DatabaseManager
	{
		
		// -------------------------------------------------------------------------------
		// Awake
		// Sets the singleton on awake, database can be accessed from anywhere by using it
		// also calls "Init" on databaseLayer to create database and open connection if
		// that is required
		// -------------------------------------------------------------------------------
		public void Awake()
		{
			singleton = this;
			databaseLayer.Init();
		}
		
		// -------------------------------------------------------------------------------
		// Init
		// creates/connects to the database and creates all tables
		// for a multiplayer server based game, this should only be called on the server
		// -------------------------------------------------------------------------------
		public void Init()
		{
			
			OpenConnection();
			
			this.InvokeInstanceDevExtMethods(nameof(Init));
			
			if (saveInterval > 0)
				InvokeRepeating(nameof(SavePlayers), saveInterval, saveInterval);
			
			if (deleteInterval > 0)
				InvokeRepeating(nameof(DeleteUsers), deleteInterval, deleteInterval);
			
			this.InvokeInstanceDevExtMethods(nameof(Init));
			
		}
		
		// -------------------------------------------------------------------------------
		// Destruct
		// closes the connection, cancels saving and updates the checksum (if required)
		// for a multiplayer server based game, this should only be called on the server
		// -------------------------------------------------------------------------------
		public void Destruct()
		{
			CancelInvoke();
			CloseConnection();
			this.InvokeInstanceDevExtMethods(nameof(Destruct));
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