// =======================================================================================
// Wovencore
// by Weaver (Fhiz)
// MIT licensed
// =======================================================================================

using wovencode;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace wovencode
{
	
	// ===================================================================================
	// DatabaseAbstractionLayer
	// ===================================================================================
	[System.Serializable]
	public abstract partial class DatabaseAbstractionLayer : IAbstractableDatabase
	{

		public abstract void Init();
		public abstract void OpenConnection();
		public abstract void CloseConnection();
		public abstract void CreateTable<T>();
		public abstract void CreateIndex(string tableName, string[] columnNames, bool unique = false);
		public abstract List<T> Query<T>(string query, params object[] args) where T : new();
		public abstract void Execute(string query, params object[] args);
		public abstract T FindWithQuery<T>(string query, params object[] args) where T : new();
		public abstract void Insert(object obj);
		public abstract void InsertOrReplace(object obj);
		public abstract void BeginTransaction();
		public abstract void Commit();
		
		// -------------------------------------------------------------------------------
		// GetTableNameFromType
		// -------------------------------------------------------------------------------
		protected string GetTableNameFromType<T>()
		{
			return typeof(T).Name;
		}
		
		// -------------------------------------------------------------------------------
		// GetTableMapFromType
		// -------------------------------------------------------------------------------
		protected TableMap GetTableMapFromType<T>()
		{
		
			PropertyInfo[] pInfo;
			Type t = typeof(T);
			pInfo = t.GetProperties();
			
			TableMap tableMap = new TableMap(pInfo.Length);
			
			for (int i = 0; i < pInfo.Length; i++)
			{
				tableMap.rows[i].name = pInfo[i].Name;
				tableMap.rows[i].type = pInfo[i].PropertyType;
				
				/*
					TODO: Add Primary Key here
				*/
				
			}
			
			return tableMap;
		
		}
		
		// -------------------------------------------------------------------------------
		// GetTableNameFromObject
		// -------------------------------------------------------------------------------
		protected string GetTableNameFromObject(object obj)
		{
			return obj.GetType().Name;
		}
				
		// -------------------------------------------------------------------------------
		// GetTableMapFromObject
		// -------------------------------------------------------------------------------
		protected TableMap GetTableMapFromObject(object obj)
		{
						
			PropertyInfo[] pInfo;
			Type t = obj.GetType();
			pInfo = t.GetProperties();
			
			TableMap tableMap = new TableMap(pInfo.Length);
			
			for (int i = 0; i < pInfo.Length; i++)
			{
				tableMap.rows[i].name = pInfo[i].Name;
				tableMap.rows[i].type = pInfo[i].PropertyType;
			}
			
			return tableMap;
		
		}
		
		// -------------------------------------------------------------------------------
		
	}

}

// =======================================================================================