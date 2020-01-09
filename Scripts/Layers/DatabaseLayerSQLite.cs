// =======================================================================================
// DatabaseLayerSQLite
// by Weaver (Fhiz)
// MIT licensed
// =======================================================================================

using wovencode;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using SQLite;

namespace wovencode
{
	
	// ===================================================================================
	// DatabaseLayerSQLite
	// ===================================================================================
	[System.Serializable]
	public partial class DatabaseLayerSQLite : DatabaseAbstractionLayer
	{
		
		[Header("Options")]
		public string databaseName 	= "Database.sqlite";
		
		
		[Tooltip("Launch automatically when the game is started (recommended for Single-Player games).")]
		public bool initOnAwake;
		[Tooltip("Compares the hash of the database with player prefs to prevent cheating.")]
		public bool checkIntegrity;
		
		
		protected 			SQLiteConnection 	connection;
		protected static 	string 				_dbPath = "";
		
		
		
		// -------------------------------------------------------------------------------

	}

}

// =======================================================================================