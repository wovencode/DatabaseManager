// =======================================================================================
// Database - Account
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
		void Init_Account()
		{
	   		connection.CreateTable<TableAccount>();
		}
		
	   	// -------------------------------------------------------------------------------
	   	[DevExtMethods("CreateDefaultData")]
		void CreateDefaultData_Account(GameObject player)
		{
			/*
				accounts have no default data, feel free to add your own
				
				instead, account data is saved/loaded as part of the register/login process
			*/
		}
		
		// -------------------------------------------------------------------------------
		[DevExtMethods("LoadDataWithPriority")]
		void LoadDataWithPriority_Account(GameObject player)
		{
			/*
				accounts do not load priority data, feel free to add your own
				
				instead, account data is saved/loaded as part of the register/login process
			*/
		}
		
	   	// -------------------------------------------------------------------------------
		[DevExtMethods("LoadData")]
		void LoadData_Account(GameObject player)
		{
	   		/*
				accounts do not load any data, feel free to add your own
				
				instead, account data is saved/loaded as part of the register/login process
			*/
		}
		
	   	// -------------------------------------------------------------------------------
		[DevExtMethods("SaveData")]
		void SaveData_Account(GameObject player)
		{
	   		connection.Execute("UPDATE TableAccount SET lastsaved=? WHERE name=?", DateTime.UtcNow, player.name);
		}
		
		// ============================ PROTECTED METHODS ================================
		
		// -------------------------------------------------------------------------------
		// AccountSetOnline
		// Sets the account online (1) or offline (0) and updates last login time
		// -------------------------------------------------------------------------------
		protected void AccountSetOnline(string _name, int _action=1)
		{
			connection.Execute("UPDATE TableAccount SET online=?, lastlogin=? WHERE name=?", _action, DateTime.UtcNow, _name);
		}
		
		// -------------------------------------------------------------------------------
		// AccountSetDeleted
		// Sets the account to deleted (1) or undeletes it (0)
		// -------------------------------------------------------------------------------
		protected void AccountSetDeleted(string _name, int _action=1)
		{
			connection.Execute("UPDATE TableAccount SET deleted=? WHERE name=?", _action, _name);
		}
		
		// -------------------------------------------------------------------------------
		// AccountSetBanned
		// Bans (1) or unbans (0) the account
		// -------------------------------------------------------------------------------
		protected void AccountSetBanned(string _name, int _action=1)
		{
			connection.Execute("UPDATE TableAccount SET banned=? WHERE name=?", _action, _name);
		}
		
		// ============================== PUBLIC METHODS =================================
		
		// -------------------------------------------------------------------------------
		public bool TryLogin(string _name, string _password)
		{
		
			if (Tools.IsAllowedName(_name) && Tools.IsAllowedPassword(_password))
			{
				
				if (!AccountExists(_name))
					return false;

				if (AccountValid(_name, _password))
				{
					AccountSetOnline(_name);
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
				
				if (AccountExists(_name))
					return false;

				AccountCreate(_name, _password);
				AccountSetOnline(_name);
				return true;
				
			}
			return false;
		}
		
		// -------------------------------------------------------------------------------
		public bool TryDelete(string _name, string _password)
		{
		
			if (Tools.IsAllowedName(_name) && Tools.IsAllowedPassword(_password))
			{
				
				if (!AccountExists(_name))
					return false;

				if (AccountValid(_name, _password))
				{
					AccountSetDeleted(_name);
					return true;	
				}
			}
			return false;
		
		}
		
		// -------------------------------------------------------------------------------
		public bool AccountValidate(string _name, string _password)
		{
			
			if (AccountValid(_name, _password))
				return true;
				
			return false;
		}
		
		// -------------------------------------------------------------------------------
		public void AccountCreate(string _name, string _password)
		{
			connection.Insert(new TableAccount{ name=_name, password=_password, created=DateTime.UtcNow, lastlogin=DateTime.Now, banned=false});
		}
		
		// -------------------------------------------------------------------------------
		public bool AccountValid(string _name, string _password)
		{
			return connection.FindWithQuery<TableAccount>("SELECT * FROM TableAccount WHERE name=? AND password=? and banned=0", _name, _password) != null;
		}
		
		// -------------------------------------------------------------------------------
		public bool AccountExists(string _name)
		{
			return connection.FindWithQuery<TableAccount>("SELECT * FROM TableAccount WHERE name=?", _name) != null;
		}
		
		// -------------------------------------------------------------------------------
		
	}

}