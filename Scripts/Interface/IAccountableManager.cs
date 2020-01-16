// =======================================================================================
// Wovencore
// by Weaver (Fhiz)
// MIT licensed
// =======================================================================================

using System;
using System.Collections.Generic;
using UnityEngine;
using wovencode;

namespace wovencode
{

	// ===================================================================================
	// IAccountableManager
	// ===================================================================================
	public interface IAccountableManager
	{
		
		// ---- User
		bool TryUserLogin(string name, string password);
		bool TryUserRegister(string name, string password);
		bool TryUserDelete(string name, string password, int action=1);
		bool TryUserBan(string name, string password, int action=1);
		bool TryUserChangePassword(string name, string oldpassword, string newpassword);
		bool TryUserConfirm(string name, string password, int action=1);
		bool TryUserGetValid(string name, string password);
		bool TryUserGetExists(string name);
		int TryUserGetPlayerCount(string name);
		
		// ---- Player
		bool TryPlayerLogin(string name, string _username);
		bool TryPlayerRegister(string name, string _username);
		bool TryPlayerDeleteSoft(string name, string _username, int action=1);
		bool TryPlayerDeleteHard(string name);
		bool TryPlayerBan(string name, string _username, int action=1);
		bool TryPlayerSwitchServer(string name, int _token=0);
		
	}
		
}

// =======================================================================================