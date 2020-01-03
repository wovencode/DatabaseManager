// =======================================================================================
// Database - Example
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
		// Init
		// Creates the table using the provided table structure, if it does not exist
		// -------------------------------------------------------------------------------
		[DevExtMethods("Init")]
		void Init_Example()
		{
	   		connection.CreateTable<TableExample>();
        	connection.CreateIndex(nameof(TableExample), new []{"owner", "name", "amount"});
		}
		
		// -------------------------------------------------------------------------------
		// CreateDefaultData
		// -------------------------------------------------------------------------------
		[DevExtMethods("CreateDefaultData")]
		void CreateDefaultData_Example(GameObject player)
		{
	 		/*
	 			Fills the table with default data (if any)
	 			You should use a GetComponent call (for example to your "Inventory")
	 			And then fill the inventory with "DefaultItems"
	 			No need to save them in the database right away
	 			As the playerSaving or next saveInterval will take care of it
	 		*/
	 		
		}
		
		// -------------------------------------------------------------------------------
		// LoadDataWithPriority
		// -------------------------------------------------------------------------------
		[DevExtMethods("LoadDataWithPriority")]
		void LoadDataWithPriority_Example(GameObject player)
		{
	   		/*
	   			The difference to "LoadData" is, that "LoadDataWithPriority" is executed
	   			first. This allows you to load data beforehand, that is required by other
	   			data (like the "level" of a player to set its "inventory size" before
	   			loading that players actual "inventory").
	   		*/
		}
		
		// -------------------------------------------------------------------------------
		// LoadData
		// -------------------------------------------------------------------------------
		[DevExtMethods("LoadData")]
		void LoadData_Example(GameObject player)
		{
			
			/*
				
				This function loads any kind of data from the database and adds it to the
				player GameObject.
							
				Usage Example:
			*/
			
	   		/*
	   		InventoryManager manager = player.GetComponent<InventoryManager>();
	   		
			foreach (TableExample row in connection.Query<TableExample>("SELECT * FROM TableExample WHERE owner=?", player.name))
			{
				if (ItemTemplate.dict.TryGetValue(row.name.GetDeterministicHashCode(), out ItemTemplate template))
				{
					manager.AddEntry(row.owner, row.name, row.amount);
				}
				else Debug.LogWarning("[Load] Skipped item " + row.name + " for " + player.name + " as it was not found in Resources.");
			}
			*/
		}
		
		// -------------------------------------------------------------------------------
		// SaveData
		// -------------------------------------------------------------------------------
		[DevExtMethods("SaveData")]
		void SaveData_Example(GameObject player)
		{
		
			// you should delete all data of this player first, to prevent duplicates
	   		connection.Execute("DELETE FROM TableExample WHERE owner=?", player.name);
	   		
	   		/*
	   			This function saves any kind of data from your player object to the
	   			database. You should use a GetComponent call (for example "Inventory")
	   			And then iterate all "Items" therein with a foreach and add them to
	   			the database.
	   			
	   			Usage Example:
	   		*/
	   		
	   		/*
	   		InventoryManager manager = player.GetComponent<InventoryManager>();
	   		
	   		List<ItemSyncStruct> list = manager.GetEntries(false);
	   		
	   		for (int i = 0; i < list.Count; i++)
	   		{
	   			
	   			ItemSyncStruct entry = list[i];
	   			
	   			connection.InsertOrReplace(new TableExample{
                	owner 			= player.name,
                	name 			= entry.name,
                	amount 			= entry.amount,
            	});
	   		}
	   		*/
		}
	   
		// -------------------------------------------------------------------------------

	}

}

// =======================================================================================