// =======================================================================================
// Wovencore
// by Weaver (Fhiz)
// MIT licensed
// =======================================================================================

using System;
using System.Text;
using UnityEngine;
using wovencode;

namespace wovencode {

	// ===================================================================================
	// TableMap
	// ===================================================================================
	public class TableMap
	{
		
		public TableRow[] rows;
		
		// -------------------------------------------------------------------------------
		// TableMap (Constructor)
		// -------------------------------------------------------------------------------
		public TableMap(int rowCount)
		{
			rows = new TableRow[rowCount];
		}
		
	}
	
	// ===================================================================================
	// TableRow
	// ===================================================================================
	public class TableRow
	{
		public string name;
		public Type type;
		public bool primary;
		
		const string typeInt 		= " INT";
		const string typeBool		= " BOOLEAN";
		const string typeLong 		= " BIGINT";
		const string typeString 	= " VARCHAR(64)";
		const string typeDateTime 	= " DATETIME";
		
		// -------------------------------------------------------------------------------
		// ToString
		// -------------------------------------------------------------------------------
		public string ToString
		{
			get
			{
				if (type == typeof(int))
				{
					return name + typeInt;
				}
				else if (type == typeof(bool))
				{
					return name + typeBool;
				}		
				else if (type == typeof(long))
				{
					return name + typeLong;
				}		
				else if (type == typeof(string))
				{
					return name + typeString;
				}		
				else if (type == typeof(DateTime))
				{
					return name + typeDateTime;
				}
				
				return "";
			}
		}
		
		// -------------------------------------------------------------------------------
		
	}
		
}

// =======================================================================================