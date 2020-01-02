// Copyright (c) 2017 Leacme (http://leac.me). View LICENSE.md for more information.
using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;
using Leacme.Lib.Notables;

namespace Leacme.App.Notables {

	public class AppUI {

		private StackPanel rootPan = (StackPanel)Application.Current.MainWindow.Content;
		private Library lib = new Library();
		private DataGrid storedNblsGr = App.DataGrid;

		public AppUI() {
			lib.AddInitialExampleNotable();

			var blb1 = App.TextBlock;
			blb1.TextAlignment = TextAlignment.Center;
			blb1.Text = "My Current Notables:";
			storedNblsGr.CanUserResizeColumns = true;

			RefreshItems();
			storedNblsGr.AutoGeneratingColumn += (z, zz) => {
				if (zz.PropertyName.Equals("Id")) { zz.Cancel = true; }
				if (zz.PropertyName.Equals("Title")) { zz.Column.MinWidth = 150; }
				if (zz.PropertyName.Equals("Note")) { zz.Column.MinWidth = 400; }
			};

			storedNblsGr.CellPointerPressed += (z, zz) => {
				if (zz.Column?.DisplayIndex.Equals(3) == true) {
					var entryToRemove = storedNblsGr.Items.Cast<dynamic>().ToList().ElementAt(zz.Row.GetIndex());
					lib.DeleteNotable(new Notable() { Id = entryToRemove.Id });
					Dispatcher.UIThread.InvokeAsync(() => {
						RefreshItems();
					});
				}
			};

			var blb2 = App.TextBlock;
			blb2.TextAlignment = TextAlignment.Center;
			blb2.Text = "Add a new Notable:";

			var addNblH = App.HorizontalStackPanel;
			addNblH.HorizontalAlignment = HorizontalAlignment.Center;

			var titleNblBlb = App.TextBlock;
			titleNblBlb.Text = "Title:";
			var titleNblBox = App.TextBox;
			titleNblBox.Width = 150;
			titleNblBox.Watermark = "New Title";

			var textNblBlb = App.TextBlock;
			textNblBlb.Text = "Notable:";
			var textNblBox = App.TextBox;
			textNblBox.Width = 450;
			textNblBox.Watermark = "New Notable";

			var addNbl = App.Button;
			addNbl.Content = "Add";
			addNbl.Width = 50;
			addNbl.Click += (z, zz) => {
				lib.StoreNotable(new Notable(DateTime.Now, titleNblBox.Text, textNblBox.Text));
				RefreshItems();
				titleNblBox.Text = String.Empty; textNblBox.Text = String.Empty;
			};

			addNblH.Children.AddRange(new List<IControl> { titleNblBlb, titleNblBox, textNblBlb, textNblBox, addNbl });

			rootPan.Children.AddRange(new List<IControl> { blb1, storedNblsGr, new Control { Height = 20 }, blb2, addNblH });
		}

		private void RefreshItems() {
			storedNblsGr.Items = lib.GetStoredNotables().Select(z => new { z.Id, Date = z.Date.ToShortTimeString() + " on " + z.Date.ToString("d MMMM, yyyy"), z.Title, Note = z.Text, Delete = "Delete" });
		}
	}
}