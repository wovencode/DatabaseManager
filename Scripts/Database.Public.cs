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

namespace wovencode
{
	
	// ===================================================================================
	// Database
	// ===================================================================================
	public partial class Database
	{
		
		// ============================= PUBLIC METHODS ==================================
		
    	// -------------------------------------------------------------------------------
		// CreateDefaultData
		// called when a new player is registered, the hook is executed on all modules and
		// used to parse default data onto the player (like starting Equipment etc.).
		// -------------------------------------------------------------------------------
		public void CreateDefaultData(GameObject player)
		{
			this.InvokeInstanceDevExtMethods("CreateDefaultData", player);
		}
		
		// -------------------------------------------------------------------------------
		// LoadData
		// called when a player is loaded from the database, the hooks are executed on
		// all modules and used to load additional player data.
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
		// called when a player is saved to the database, the hook is executed on all
		// modules and used to save additional player data.
		// -------------------------------------------------------------------------------
		public void SaveData(GameObject player, bool online, bool useTransaction = true)
		{
			if (useTransaction)
				databaseLayer.BeginTransaction();
				
			this.InvokeInstanceDevExtMethods("SaveData", player);
			
			if (useTransaction)
				databaseLayer.Commit();
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