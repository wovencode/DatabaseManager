// =======================================================================================
// Wovencore
// by Weaver (Fhiz)
// MIT licensed
// =======================================================================================

using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using MySql.Data;
using MySql.Data.MySqlClient;
using Wovencode;
using Wovencode.Database;

namespace Wovencode.Database {
	
	// ===================================================================================
	// PrimaryKeyAttribute
	// ===================================================================================
	[AttributeUsage (AttributeTargets.Property)]
	public class PrimaryKeyAttribute : Attribute
	{
	}
	
	// ===================================================================================
	// AutoIncrementAttribute
	// ===================================================================================
	[AttributeUsage (AttributeTargets.Property)]
	public class AutoIncrementAttribute : Attribute
	{
	}
	
	// ===================================================================================
	// TableMap
	// ===================================================================================
	public class TableMap
	{
		
		public Type type;
		public string name;
		public TableRow[] rows;
		
		// Caches
		protected string mySQLString			= "";
		protected string mySQLString_Prefixed 	= "";
		protected MySqlParameter[] parameters;
		
		// -------------------------------------------------------------------------------
		// TableMap (Constructor)
		// -------------------------------------------------------------------------------
		public TableMap(Type _type, string _name, int rowCount)
		{
			type = _type;
			name = _name;
			rows = new TableRow[rowCount];
		}
		
		// -------------------------------------------------------------------------------
		// RowsToMySQLInsertString
		// -------------------------------------------------------------------------------
		public string RowsToMySQLInsertString
		{
			get
			{
				
				string tableParameters = "";
			
				foreach (TableRow row in rows)
				{
			
					tableParameters += row.ToMySQLString;
					tableParameters += " NOT NULL";
				
					if (row != rows.Last())
						tableParameters += ",";
					
				}
				
				return tableParameters;
			}
		}
		
		// -------------------------------------------------------------------------------
		// RowsToMySQLString
		// -------------------------------------------------------------------------------
		public string RowsToMySQLString(string prefix="")
		{
		
			string convertedString = "";
			
			if (!String.IsNullOrWhiteSpace(prefix))
				convertedString = mySQLString_Prefixed;
			else
				convertedString = mySQLString;
						
			if (String.IsNullOrWhiteSpace(convertedString))
			{
			
				foreach (TableRow row in rows)
				{
					
					if (!String.IsNullOrWhiteSpace(prefix))
						convertedString += prefix;
						
					convertedString += row.name;
			
					if (row != rows.Last())
						convertedString += ",";
				
				}
				
				if (!String.IsNullOrWhiteSpace(prefix))
					mySQLString_Prefixed = convertedString;
				else
					mySQLString = convertedString;
				
			}
			
			return convertedString;
			
		}
		
		// -------------------------------------------------------------------------------
		// RowsToMySQLParameters
		// -------------------------------------------------------------------------------
		public MySqlParameter[] RowsToMySQLParameters
		{
			get
			{
				if (parameters == null)
				{
					parameters = new MySqlParameter[rows.Length];
					int i = 0;
					
					foreach (TableRow row in rows)
					{
						parameters[i] = new MySqlParameter("@"+row.name, row.value);
						i++;
					}
				}
				
				return parameters;
				
			}
		}
		
		// -------------------------------------------------------------------------------
		// HasPrimaryKey
		// -------------------------------------------------------------------------------
		public bool HasPrimaryKey
		{
			get
			{
				foreach (TableRow row in rows)
					if (row.primary) return true;
				return false;
			}
		}
		
		// -------------------------------------------------------------------------------
		// GetPrimaryKey
		// -------------------------------------------------------------------------------
		public string GetPrimaryKey
		{
			get
			{
				foreach (TableRow row in rows)
					if (row.primary) return row.name;
				return "";
			}
		}
		
		// -------------------------------------------------------------------------------
		// UpdateValue
		// -------------------------------------------------------------------------------
		public void UpdateValue(object obj)
		{
			foreach (TableRow row in rows)
			{
				if (row.name == name)
					row.value = obj;
			}
		}
		
		// -------------------------------------------------------------------------------
		// UpdateValues
		// -------------------------------------------------------------------------------
		public void UpdateValues(object obj)
		{
		
			PropertyInfo[] pInfo;
			Type t = obj.GetType();
			pInfo = t.GetProperties();
			
			for (int i = 0; i < pInfo.Length; i++)
				rows[i].value = pInfo[i].GetValue(obj);
			
		}
		
		// -------------------------------------------------------------------------------
		// ToList
		// -------------------------------------------------------------------------------
		public List<T> ToList<T>()
		{
			
			List<T> list = new List<T>();
			
			PropertyInfo[] pInfo;
			pInfo = type.GetProperties();
		
			for (int i = 0; i < pInfo.Length; i++)
			{
				T obj = (T)Activator.CreateInstance(typeof(T));
				pInfo[i].SetValue(obj, rows[i].value);
				list.Add(obj);
			}
			
			return list;
			
		}
		
		// -------------------------------------------------------------------------------
		
	}
	
	// ===================================================================================
	// TableRow
	// ===================================================================================
	public class TableRow
	{
		public string name;
		public Type type;
		public object value;
		public bool primary;
				
		const string typeInt 		= " INT";
		const string typeBool		= " BOOLEAN";
		const string typeLong 		= " BIGINT";
		const string typeString 	= " VARCHAR(64)";
		const string typeDateTime 	= " DATETIME";
		
		// -------------------------------------------------------------------------------
		// ToMySQLString
		// -------------------------------------------------------------------------------
		public string ToMySQLString
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