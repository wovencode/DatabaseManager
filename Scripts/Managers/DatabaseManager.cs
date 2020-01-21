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
	// Database
	// ===================================================================================
	[DisallowMultipleComponent]
	public partial class DatabaseManager : BaseDatabaseManager, IAbstractableDatabase
	{
		
		[Header("Settings")]
		public DatabaseType databaseType = DatabaseType.SQLite;
		[Tooltip("Player data save interval in seconds (0 to disable).")]
		public float saveInterval = 60f;
		[Tooltip("Deleted user prune interval in seconds (0 to disable).")]
		public float deleteInterval = 240f;
		
		public static DatabaseManager singleton;
		
		protected DatabaseType _databaseType = DatabaseType.SQLite;
		
#if wMYSQL
		[Header("Database Layer - mySQL")]
		public DatabaseLayerMySQL databaseLayer;
#else
		[Header("Database Layer - SQLite")]
		public DatabaseLayerSQLite databaseLayer;
#endif
		
		protected const string _defineSQLite 	= "wSQLITE";
		protected const string _defineMySQL 	= "wMYSQL";
		
		// -------------------------------------------------------------------------------
		// OnValidate
		// updates the define to set the database layer depending of chosen database type
		// -------------------------------------------------------------------------------
		void OnValidate()
		{
#if UNITY_EDITOR
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
			
			this.InvokeInstanceDevExtMethods(nameof(OnValidate));
#endif
		}
		
		// -------------------------------------------------------------------------------
		// DeleteUsers
		// hard deletes all users that have been soft deleted before
		// -------------------------------------------------------------------------------
		void DeleteUsers()
		{
#if wPLAYER	
			List<TableUser> users = Query<TableUser>("SELECT * FROM TableUser WHERE deleted=1");

			foreach (TableUser user in users)
				this.InvokeInstanceDevExtMethods("DeleteData", user.name);
			
			if (users.Count > 0)
				debug.Log("[Database] Pruned " + users.Count + " inactive user(s)");
#else
			debug.LogWarning("[Database] No users could be pruned (Define #wPLAYER missing)");
#endif
			
			this.InvokeInstanceDevExtMethods(nameof(DeleteUsers));
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
#if wNETWORK
			
			/*
				When using "Wovencode.Network", the database will automatically save
				all online players. If you use any other solution, you will have to
				replace the code below with your own.
			*/
			
    		if (Wovencode.Network.NetworkManager.onlinePlayers.Count == 0)
    			return; 
    		
        	databaseLayer.BeginTransaction();
        	
        	foreach (GameObject player in Wovencode.Network.NetworkManager.onlinePlayers.Values)
            	SaveDataPlayer(player, online, false);
            
        	databaseLayer.Commit();
        	
        	if (Wovencode.Network.NetworkManager.onlinePlayers.Count > 0)
        		debug.Log("[Database] Saved " + Wovencode.Network.NetworkManager.onlinePlayers.Count + " player(s)");
#else
			
			/*
				In case of a single-player game, you will have to provide your own
				code in order to save the current player to the database. You can
				use the hook below to move the save process to another file, or
				add your own code right here if preferred.
			
			*/
			
			debug.LogWarning("[Database] No players could be saved (Define #NETWORK missing)");
			
#endif

			this.InvokeInstanceDevExtMethods(nameof(SavePlayers));
			
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