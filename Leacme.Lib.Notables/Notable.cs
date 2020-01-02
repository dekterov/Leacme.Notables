using System;

namespace Leacme.Lib.Notables {
	public class Notable {
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime Date { get; set; }
		public string Text { get; set; }

		public Notable() {
		}

		public Notable(DateTime date, String title = "", string text = "") {
			this.Title = title; this.Date = date; this.Text = text;
		}
	}
}