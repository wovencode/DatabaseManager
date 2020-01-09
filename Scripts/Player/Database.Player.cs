// =======================================================================================
// Database - Player
// by Weaver (Fhiz)
// MIT licensed
// =======================================================================================

using wovencode;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using SQLite;
using UnityEngine.AI;

namespace wovencode
{

	// ===================================================================================
	// Database
	// ===================================================================================
	public partial class Database
	{
		
		// ============================= PRIVATE METHODS =================================
		
		// -------------------------------------------------------------------------------
		[DevExtMethods("Init")]
		void Init_Player()
		{
	   		connection.CreateTable<TablePlayer>();
		}
		
	   	// -------------------------------------------------------------------------------
	   	[DevExtMethods("CreateDefaultData")]
		void CreateDefaultData_Player(GameObject player)
		{
			/*
				players have no default data, feel free to add your own
				
				instead, player data is saved/loaded as part of the register/login process
			*/
		}
		
		// -------------------------------------------------------------------------------
		[DevExtMethods("LoadDataWithPriority")]
		void LoadDataWithPriority_Player(GameObject player)
		{
			/*
				players do not load priority data, feel free to add your own
				
				instead, player data is saved/loaded as part of the register/login process
			*/
		}
		
	   	// -------------------------------------------------------------------------------
		[DevExtMethods("LoadData")]
		void LoadData_Player(GameObject player)
		{
	   		/*
				players do not load any data, feel free to add your own
				
				instead, player data is saved/loaded as part of the register/login process
			*/
		}
		
	   	// -------------------------------------------------------------------------------
		[DevExtMethods("SaveData")]
		void SaveData_Player(GameObject player)
		{
	   		connection.Execute("UPDATE TablePlayer SET lastsaved=? WHERE name=?", DateTime.UtcNow, player.name);
		}
		
		// ============================ PROTECTED METHODS ================================
		
		// -------------------------------------------------------------------------------
		// PlayerSetOnline
		// Sets the player online (1) or offline (0) and updates last login time
		// -------------------------------------------------------------------------------
		protected void PlayerSetOnline(string _name, int _action=1)
		{
			connection.Execute("UPDATE TablePlayer SET online=?, lastlogin=? WHERE name=?", _action, DateTime.UtcNow, _name);
		}
		
		// -------------------------------------------------------------------------------
		// PlayerSetDeleted
		// Sets the player to deleted (1) or undeletes it (0)
		// -------------------------------------------------------------------------------
		protected void PlayerSetDeleted(string _name, int _action=1)
		{
			connection.Execute("UPDATE TablePlayer SET deleted=? WHERE name=?", _action, _name);
		}
		
		// -------------------------------------------------------------------------------
		// PlayerSetBanned
		// Bans (1) or unbans (0) the player
		// -------------------------------------------------------------------------------
		protected void PlayerSetBanned(string _name, int _action=1)
		{
			connection.Execute("UPDATE TablePlayer SET banned=? WHERE name=?", _action, _name);
		}
		
		// -------------------------------------------------------------------------------
		// PlayerSetConfirmed
		// Sets the player to confirmed (1) or unconfirms it (0)
		// -------------------------------------------------------------------------------
		protected void PlayerSetConfirmed(string _name, int _action=1)
		{
			connection.Execute("UPDATE TablePlayer SET confirmed=? WHERE name=?", _action, _name);
		}
		
		// ============================== PUBLIC METHODS =================================
		
		// -------------------------------------------------------------------------------
		public bool TryLogin(string _name, string _password)
		{
		
			if (Tools.IsAllowedName(_name) && Tools.IsAllowedPassword(_password))
			{
				
				if (!PlayerExists(_name))
					return false;

				if (PlayerValid(_name, _password))
				{
					PlayerSetOnline(_name);
					return true;
				}
			}
			return false;
		}
		
		// -------------------------------------------------------------------------------
		public bool TryRegister(string _name, string _password)
		{
		
			if (Tools.IsAllowedName(_name) && Tools.IsAllowedPassword(_password))
			{
				
				if (PlayerExists(_name))
					return false;

				PlayerCreate(_name, _password);
				return true;
				
			}
			return false;
		}
		
		// -------------------------------------------------------------------------------
		public bool TryDelete(string _name, string _password, int _action=1)
		{
		
			if (Tools.IsAllowedName(_name) && Tools.IsAllowedPassword(_password))
			{
				
				if (!PlayerExists(_name))
					return false;

				if (PlayerValid(_name, _password))
				{
					PlayerSetDeleted(_name, _action);
					return true;	
				}
			}
			return false;
		
		}
		
		// -------------------------------------------------------------------------------
		public bool TryBan(string _name, string _password, int _action=1)
		{
		
			if (Tools.IsAllowedName(_name) && Tools.IsAllowedPassword(_password))
			{
				
				if (!PlayerExists(_name))
					return false;

				if (PlayerValid(_name, _password))
				{
					PlayerSetBanned(_name, _action);
					return true;	
				}
			}
			return false;
		
		}
		
		// -------------------------------------------------------------------------------
		public bool TryConfirm(string _name, string _password, int _action=1)
		{
		
			if (Tools.IsAllowedName(_name) && Tools.IsAllowedPassword(_password))
			{
				
				if (!PlayerExists(_name))
					return false;

				if (PlayerValid(_name, _password))
				{
					PlayerSetConfirmed(_name, _action);
					return true;	
				}
			}
			return false;
		
		}
		
		// -------------------------------------------------------------------------------
		public bool PlayerValidate(string _name, string _password)
		{
			
			if (PlayerValid(_name, _password))
				return true;
				
			return false;
		}
		
		// -------------------------------------------------------------------------------
		public void PlayerCreate(string _name, string _password)
		{
			connection.Insert(new TablePlayer{ name=_name, password=_password, created=DateTime.UtcNow, lastlogin=DateTime.Now, banned=false});
		}
		
		// -------------------------------------------------------------------------------
		public bool PlayerValid(string _name, string _password)
		{
			return connection.FindWithQuery<TablePlayer>("SELECT * FROM TablePlayer WHERE name=? AND password=? and banned=0", _name, _password) != null;
		}
		
		// -------------------------------------------------------------------------------
		public bool PlayerExists(string _name)
		{
			return connection.FindWithQuery<TablePlayer>("SELECT * FROM TablePlayer WHERE name=?", _name) != null;
		}
		
		// -------------------------------------------------------------------------------
		
	}

}