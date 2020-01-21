// =======================================================================================
// Wovencore
// by Weaver (Fhiz)
// MIT licensed
// =======================================================================================

#if UNITY_EDITOR

using Wovencode;
using UnityEditor;
using UnityEngine;

namespace Wovencode
{

	public partial class DefinesManager
	{

		[DevExtMethods("Constructor")]
		public static void Constructor_DatabaseSQLite()
		{
			EditorTools.AddScriptingDefine("wDB");
		}

	}

}

#endif