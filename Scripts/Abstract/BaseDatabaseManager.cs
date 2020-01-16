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
		
		[Header("Debug Helper")]
		public DebugHelper debug;
		
    	// ======================= PUBLIC METHODS - USER =================================
    	
		// -------------------------------------------------------------------------------
		public virtual bool TryUserLogin(string name, string password)
		{
			return (Tools.IsAllowedName(name) && Tools.IsAllowedPassword(password));
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TryUserRegister(string name, string password)
		{
			return (Tools.IsAllowedName(name) && Tools.IsAllowedPassword(password));
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TryUserDelete(string name, string password, int _action=1)
		{
			return (Tools.IsAllowedName(name) && Tools.IsAllowedPassword(password));
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TryUserBan(string name, string password, int _action=1)
		{
			return (Tools.IsAllowedName(name) && Tools.IsAllowedPassword(password));
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TryUserChangePassword(string name, string oldpassword, string newpassword)
		{
			return (Tools.IsAllowedName(name) && Tools.IsAllowedPassword(oldpassword) && Tools.IsAllowedPassword(newpassword) && oldpassword != newpassword);
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TryUserConfirm(string name, string password, int _action=1)
		{
			return (Tools.IsAllowedName(name) && Tools.IsAllowedPassword(password));
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TryUserGetValid(string name, string password)
		{
			return (Tools.IsAllowedName(name) && Tools.IsAllowedPassword(password));
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TryUserGetExists(string name)
		{
			return (Tools.IsAllowedName(name));
		}
		
		// -------------------------------------------------------------------------------
		public abstract int TryUserGetPlayerCount(string name);
				
		// ======================= PUBLIC METHODS - PLAYER ===============================
		
		// -------------------------------------------------------------------------------
		public virtual bool TryPlayerLogin(string name, string username)
		{
			return (Tools.IsAllowedName(name) && Tools.IsAllowedPassword(username));
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TryPlayerRegister(string name, string username)
		{
			return (Tools.IsAllowedName(name) && Tools.IsAllowedPassword(username));
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TryPlayerDeleteSoft(string name, string username, int _action=1)
		{
			return (Tools.IsAllowedName(name) && Tools.IsAllowedPassword(username));
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TryPlayerDeleteHard(string name)
		{
			return (Tools.IsAllowedName(name));
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TryPlayerBan(string name, string username, int _action=1)
		{
			return (Tools.IsAllowedName(name) && Tools.IsAllowedPassword(username));
		}
		
		// -------------------------------------------------------------------------------
		public virtual bool TryPlayerSwitchServer(string name, int _token)
		{
			return (Tools.IsAllowedName(name) && Tools.IsAllowedToken(_token));
		}
		
		// -------------------------------------------------------------------------------

	}

}

// =======================================================================================