// =======================================================================================
// Wovencore
// by Weaver (Fhiz)
// MIT licensed
// =======================================================================================

using wovencode;
using UnityEngine;
using System;

namespace wovencode
{
	
	// ===================================================================================
	// BaseDatabase
	// ===================================================================================
	public abstract partial class BaseDatabaseManager : MonoBehaviour, IAccountableManager
	{
		
    	// ======================= PUBLIC METHODS - USER =================================
    	
		// -------------------------------------------------------------------------------
		public virtual bool TryLoginUser(string name, string password)
		{
			return (Tools.IsAllowedName(name) && Tools.IsAllowedPassword(password));
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TryRegisterUser(string name, string password)
		{
			return (Tools.IsAllowedName(name) && Tools.IsAllowedPassword(password));
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TrySoftDeleteUser(string name, string password, int _action=1)
		{
			return (Tools.IsAllowedName(name) && Tools.IsAllowedPassword(password));
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TryBanUser(string name, string password, int _action=1)
		{
			return (Tools.IsAllowedName(name) && Tools.IsAllowedPassword(password));
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TryChangePasswordUser(string name, string oldpassword, string newpassword)
		{
			return (Tools.IsAllowedName(name) && Tools.IsAllowedPassword(oldpassword) && Tools.IsAllowedPassword(newpassword) && oldpassword != newpassword);
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TryConfirmUser(string name, string password, int _action=1)
		{
			return (Tools.IsAllowedName(name) && Tools.IsAllowedPassword(password));
		}
		
		// ======================= PUBLIC METHODS - PLAYER ===============================
		
		// -------------------------------------------------------------------------------
		public virtual bool TryLoginPlayer(string name, string username)
		{
			return (Tools.IsAllowedName(name) && Tools.IsAllowedPassword(username));
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TryRegisterPlayer(string name, string username)
		{
			return (Tools.IsAllowedName(name) && Tools.IsAllowedPassword(username));
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TrySoftDeletePlayer(string name, string username, int _action=1)
		{
			return (Tools.IsAllowedName(name) && Tools.IsAllowedPassword(username));
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TryBanPlayer(string name, string username, int _action=1)
		{
			return (Tools.IsAllowedName(name) && Tools.IsAllowedPassword(username));
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TrySwitchServerPlayer(string name, int _token)
		{
			return (Tools.IsAllowedName(name) && Tools.IsAllowedToken(_token));
		}
		
		// -------------------------------------------------------------------------------

	}

}

// =======================================================================================