// =======================================================================================
// Database - Player
// by Weaver (Fhiz)
// MIT licensed
// =======================================================================================

using wovencode;
using System;
using SQLite;

namespace wovencode
{

	// ===================================================================================
	// TablePlayer
	// ===================================================================================
	partial class TablePlayer
	{
		[PrimaryKey]
		public string name 			{ get; set; }
		public string password 		{ get; set; }
		public DateTime created 	{ get; set; }
		public DateTime lastlogin 	{ get; set; }
		public bool deleted 		{ get; set; }
		public bool banned 			{ get; set; }
		public bool online 			{ get; set; }
		public bool confirmed		{ get; set; }
		public DateTime lastsaved 	{ get; set; }
	}
		
	// -------------------------------------------------------------------------------
	
}