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
	public partial class Database : MonoBehaviour
	{
		
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