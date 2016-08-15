using System;
using SQLite; 

namespace LetsPlay
{
	public class GamesActivity
	{
		public GamesActivity ()
		{
			[PrimaryKey, AutoIncrement]
			public int BookId { get; set; }
			public string BookTitle { get; set; }
			public string ISBN { get; set; }
		}
	}
}

