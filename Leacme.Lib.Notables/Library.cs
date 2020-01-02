// Copyright (c) 2017 Leacme (http://leac.me). View LICENSE.md for more information.
using System;
using System.Collections.Generic;
using LiteDB;

namespace Leacme.Lib.Notables {

	public class Library {

		private LiteDatabase db = new LiteDatabase(typeof(Library).Namespace + ".Settings.db");
		private LiteCollection<Notable> notableCollection;

		public Library() {
			notableCollection = db.GetCollection<Notable>(nameof(notableCollection));
		}

		/// <summary>
		/// Add an example notable to an unitialized database.
		/// /// </summary>
		public void AddInitialExampleNotable() {
			if (!db.CollectionExists(nameof(notableCollection))) {
				notableCollection.Insert(new Notable(DateTime.Now, "Example Notable Title", "Example Notable Text"));
			}
		}

		/// <summary>
		///	Store the <c>Notable</c> entity in the database.
		/// /// </summary>
		/// <param name="notable"></param>
		/// <returns>The generated id of the stored entity.</returns>
		public int StoreNotable(Notable notable) {
			return notableCollection.Insert(notable);
		}

		/// <summary>
		///	Retrieve all stored <c>Notable</c> entities from the database.
		/// /// </summary>
		/// <returns>The <c>Notable</c> entities.</returns>
		public IEnumerable<Notable> GetStoredNotables() {
			return notableCollection.FindAll();
		}

		/// <summary>
		///	Delete the <c>Notable</c> entity from the database via its id.
		/// /// </summary>
		/// <param name="notable"></param>
		/// <returns>If the entity was deleted.</returns>
		public bool DeleteNotable(Notable notable) {
			return notableCollection.Delete(notable.Id);
		}
	}

}