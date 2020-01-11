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
	[DisallowMultipleComponent]
	public partial class Database : BaseDatabase, IAbstractableDatabase
	{
		
		[Header("Settings")]
		public DatabaseType databaseType = DatabaseType.SQLite;
		[Tooltip("Player data save interval in seconds (0 to disable).")]
		public float saveInterval = 60f;
		[Tooltip("Deleted Player erease interval in seconds (0 to disable).")]
		public float deleteInterval = 60f;
		
		public static Database singleton;
		
		protected DatabaseType _databaseType = DatabaseType.SQLite;
		
#if WOCO_MYSQL
		[Header("Database Layer - mySQL")]
		public DatabaseLayerMySQL databaseLayer;
#else
		[Header("Database Layer - SQLite")]
		public DatabaseLayerSQLite databaseLayer;
#endif
		
		protected const string _defineSQLite 	= "WOCO_SQLITE";
		protected const string _defineMySQL 	= "WOCO_MYSQL";
		
		// -------------------------------------------------------------------------------
		// OnValidate
		// updates the define to set the database layer depending of chosen database type
		// -------------------------------------------------------------------------------
		void OnValidate()
		{
			
			if (databaseType == DatabaseType.mySQL && databaseType != _databaseType)
			{
				EditorTools.RemoveScriptingDefine(_defineSQLite);
				EditorTools.AddScriptingDefine(_defineMySQL);
				_databaseType = databaseType;
			}
			else if (databaseType == DatabaseType.SQLite && databaseType != _databaseType)
			{
				EditorTools.RemoveScriptingDefine(_defineMySQL);
				EditorTools.AddScriptingDefine(_defineSQLite);
				_databaseType = databaseType;
			}
			
			this.InvokeInstanceDevExtMethods("OnValidate");
			
		}
		
		// -------------------------------------------------------------------------------
		// DeletePlayers
		// hard deletes all players that have been soft deleted before
		// -------------------------------------------------------------------------------
		void DeletePlayers()
		{
#if WOCO_PLAYER	
			List<TablePlayer> players = Query<TablePlayer>("SELECT * FROM TablePlayer WHERE deleted=1");

			foreach (TablePlayer player in players)
				this.InvokeInstanceDevExtMethods("DeleteData", player.name);
		
			Debug.Log("[Database] Deleted " + players.Count + " player(s)");
#endif
			
			this.InvokeInstanceDevExtMethods("DeletePlayers");
		}
		
		// -------------------------------------------------------------------------------
		// SavePlayers
		// same function as below but without parameters (for Invoke)
		// -------------------------------------------------------------------------------
		void SavePlayers()
		{
			SavePlayers(true);
		}
		
		// -------------------------------------------------------------------------------
		// SavePlayers
		// -------------------------------------------------------------------------------
		void SavePlayers(bool online = true)
    	{
#if WOCO_NETWORK
			
			/*
				When using "Wovencode.Network", the database will automatically save
				all online players. If you use any other solution, you will have to
				replace the code below with your own.
			*/
			
    		if (wovencode.NetworkManager.onlinePlayers.Count == 0)
    			return; 
    		
        	databaseLayer.BeginTransaction();
        	
        	foreach (GameObject player in wovencode.NetworkManager.onlinePlayers.Values)
            	SaveData(player, online, false);
            	
        	databaseLayer.Commit();
        	
        	Debug.Log("[Database] Saved " + wovencode.NetworkManager.onlinePlayers.Count + " player(s)");
#else
			
			/*
				In case of a single-player game, you will have to provide your own
				code in order to save the current player to the database. You can
				use the hook below to move the save process to another file, or
				add your own code right here if preferred.
			
			*/
			
#endif

			this.InvokeInstanceDevExtMethods("SavePlayers");
			
    	}
    	
    	// =========================== DEFAULT EVENTS ====================================
		
		// -------------------------------------------------------------------------------
		// OnApplicationQuit
		// -------------------------------------------------------------------------------
		void OnApplicationQuit()
		{
			Destruct();
		}
    	
		// -------------------------------------------------------------------------------

	}

}

// =======================================================================================