// =======================================================================================
// Wovencore
// by Weaver (Fhiz)
// MIT licensed
//
//
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
		bool TryLogin(string _name, string _password);
		bool TryRegister(string _name, string _password);
		bool TrySoftDelete(string _name, string _password, int _action=1);
		bool TryBan(string _name, string _password, int _action=1);
		bool TryConfirm(string _name, string _password, int _action=1);
		bool TrySwitchServer(string _name, int _token=0);
	}
		
}

// =======================================================================================