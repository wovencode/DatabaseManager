// =======================================================================================
// Database
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
	public partial class Database : MonoBehaviour
	{
		
		[Header("Options")]
		public string databaseName 	= "Database.sqlite";
		
		[Tooltip("Player data save interval in seconds (0 to disable).")]
		public float saveInterval = 60f;
		[Tooltip("Launch Automatically when the game is started (recommended for Single-Player games).")]
		public bool initOnAwake;
		[Tooltip("Compares the hash of the database with player prefs to prevent cheating.")]
		public bool checkIntegrity;
		
		public 				SQLiteConnection 	connection;
		public static 		Database 			singleton;
		protected static 	string 				_dbPath = "";
		
		// -------------------------------------------------------------------------------
		// Awake
		// Sets the singleton on awake, database can be accessed from anywhere by using it
		// -------------------------------------------------------------------------------
		void Awake()
		{
			if (singleton == null) singleton = this;
			
			if (initOnAwake)
				Init();
		}
			
		// -------------------------------------------------------------------------------
		// Init
		// creates/connects to the database and creates all tables
		// for a multiplayer server based game, this should only be called on the server
		// -------------------------------------------------------------------------------
		void Init()
		{
			
			_dbPath = Tools.GetPath(databaseName);
			
			// checks if the database file has been manipulated outside of the game
			// recommended for single-player games only, not recommended on very large files
			if (File.Exists(_dbPath) &&
				checkIntegrity &&
				Tools.GetChecksum(_dbPath) == false)
			{
				Debug.LogError("Database file is corrupted!");
				// deletes the file, a fresh database file is re-created thereafter
				File.Delete(_dbPath);
			}

			connection = new SQLiteConnection(_dbPath);

			if (saveInterval > 0)
				InvokeRepeating(nameof(SavePlayers), saveInterval, saveInterval);
			
			this.InvokeInstanceDevExtMethods("Init");
			
			Debug.Log("[Database] initialized.");
		}
		
		// -------------------------------------------------------------------------------
		// Destruct
		// closes the connection, cancels saving and updates the checksum (if required)
		// for a multiplayer server based game, this should only be called on the server
		// -------------------------------------------------------------------------------
		void Destruct()
		{
		
			CancelInvoke(nameof(SavePlayers));
			
			connection?.Close();
		
			if (checkIntegrity)
				Tools.SetChecksum(_dbPath);
			
			//this.InvokeInstanceDevExtMethods("Destruct"); // enable if required
			
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
    		
        	connection.BeginTransaction();
        	
        	foreach (GameObject player in wovencode.NetworkManager.onlinePlayers.Values)
            	SaveData(player, online, false);
            	
        	connection.Commit();
        	
        	Debug.Log("[Database] Saved " + wovencode.NetworkManager.onlinePlayers.Count + " player(s)");
#else
			
			/*
				In case of a single-player game, you will have to provide your own
				code in order to save the current player to the database. You can
				use the hook below to move the save process to another file, or
				add your own code right here if preferred.
			
			*/
			
			//this.InvokeInstanceDevExtMethods("SavePlayers"); // enable if required
#endif
    	}
    	
    	// =========================== PUBLIC METHODS ====================================
    	
    	// -------------------------------------------------------------------------------
		// CreateDefaultData
		// -------------------------------------------------------------------------------
		public void CreateDefaultData(GameObject player)
		{
			this.InvokeInstanceDevExtMethods("CreateDefaultData", player);
		}
		
		// -------------------------------------------------------------------------------
		// LoadData
		// -------------------------------------------------------------------------------
		public GameObject LoadData(GameObject prefab, string _name)
		{
			GameObject player = Instantiate(prefab);
			player.name = _name;
			this.InvokeInstanceDevExtMethods("LoadDataWithPriority", player);
			this.InvokeInstanceDevExtMethods("LoadData", player);
			return player;
		}
		
		// -------------------------------------------------------------------------------
		// SaveData
		// -------------------------------------------------------------------------------
		public void SaveData(GameObject player, bool online, bool useTransaction = true)
		{
			if (useTransaction)
				connection.BeginTransaction();
				
			this.InvokeInstanceDevExtMethods("SaveData", player);
			
			if (useTransaction)
				connection.Commit();
		}
		
		// =========================== DEFAULT EVENTS ====================================
		
		// -------------------------------------------------------------------------------
		// OnApplicationQuit
		// -------------------------------------------------------------------------------
		void OnApplicationQuit()
		{
			Destruct();
		}
		
		// ======================== PUBLIC EVENT METHODS =================================
		
		// -------------------------------------------------------------------------------
		// EventStartServer
		// -------------------------------------------------------------------------------
		public void EventStartServer()
		{
			Init();
		}
		
		// -------------------------------------------------------------------------------
		// EventStopServer
		// -------------------------------------------------------------------------------
		public void EventStopServer()
		{
			Destruct();
		}
		
		// -------------------------------------------------------------------------------

	}

}

// =======================================================================================