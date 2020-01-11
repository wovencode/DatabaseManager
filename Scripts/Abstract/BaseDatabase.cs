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
	// BaseDatabase
	// ===================================================================================
	public abstract partial class BaseDatabase : MonoBehaviour, IAccountableManager
	{
		
    	// =========================== PUBLIC METHODS ====================================
    	
		// -------------------------------------------------------------------------------
		public virtual bool TryLogin(string _name, string _password)
		{
			return (Tools.IsAllowedName(_name) && Tools.IsAllowedPassword(_password));
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TryRegister(string _name, string _password)
		{
			return (Tools.IsAllowedName(_name) && Tools.IsAllowedPassword(_password));
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TrySoftDelete(string _name, string _password, int _action=1)
		{
			return (Tools.IsAllowedName(_name) && Tools.IsAllowedPassword(_password));
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TryBan(string _name, string _password, int _action=1)
		{
			return (Tools.IsAllowedName(_name) && Tools.IsAllowedPassword(_password));
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TryConfirm(string _name, string _password, int _action=1)
		{
			return (Tools.IsAllowedName(_name) && Tools.IsAllowedPassword(_password));
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TrySwitchServer(string _name, int _token)
		{
			return (Tools.IsAllowedName(_name) && Tools.IsAllowedToken(_token));
		}
		
		// -------------------------------------------------------------------------------

	}

}

// =======================================================================================