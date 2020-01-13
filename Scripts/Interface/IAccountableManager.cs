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
		
		// User
		bool TryLoginUser(string name, string password);
		bool TryRegisterUser(string name, string password);
		bool TrySoftDeleteUser(string name, string password, int action=1);
		bool TryBanUser(string name, string password, int action=1);
		bool TryChangePasswordUser(string name, string oldpassword, string newpassword);
		bool TryConfirmUser(string name, string password, int action=1);
		
		// Player
		bool TryLoginPlayer(string name, string _username);
		bool TryRegisterPlayer(string name, string _username);
		bool TrySoftDeletePlayer(string name, string _username, int action=1);
		bool TryBanPlayer(string name, string _username, int action=1);
		bool TrySwitchServerPlayer(string name, int _token=0);
		
	}
		
}

// =======================================================================================