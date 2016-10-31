//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System;
using System.Collections.Generic;
using System.Web;

using MyWebPagesStarterKit;
using MyWebPagesStarterKit.Controls;
using System.Data;

namespace MyWebPagesStarterKit
{
	/// <summary>
	/// Forum section
	/// This is the section class for the forum.
	/// </summary>
	public class Forum : Section<Forum.ForumData>, ISidebarObject
	{
		public Forum() : base() { }

		public Forum(string id) : base(id) { }

		#region ISidebarObject Members

		/// <summary>
		/// The forum shows the five most recently updated threads and has a deeplink to their posts
		/// </summary>
		public ChannelData GetSidebarRss(string PageId)
		{
			ChannelData elements = new ChannelData();
			DataRow[] latestThreads = Threads.Select(string.Empty, "lastEntry DESC", DataViewRowState.CurrentRows);

			for (int i = 0; i < latestThreads.Length; i++)
			{
				WebPage page = new WebPage(PageId);
				string url = string.Empty;
				if (page.VirtualPath != string.Empty)
					url = page.VirtualPath.ToLower() + "?thread={1}#{2}";
				else
					url = "~/default.aspx?pg={0}&thread={1}#{2}";

				Dictionary<string, string> item = new Dictionary<string, string>();

				item.Add("title", latestThreads[i]["Subject"].ToString());
				item.Add("link", string.Format(url, PageId, latestThreads[i]["Permalink"], SectionId));
				item.Add("description", string.Empty);

				elements.ChannelItems.Add(item);

				if (i >= 4)
					break;
			}
			return elements;
		}

		#endregion

		#region Overridden methods of Section<>
		public override string GetLocalizedSectionName()
		{
			return Resources.Forum.ctl_Forum_RssTitle;
		}

		public override List<SearchResult> Search(string searchString, WebPage page)
		{
			string url = string.Empty;
			if (page.VirtualPath != string.Empty)
				url = page.VirtualPath.ToLower() + "?thread={1}&post={2}#{2}";
			else
				url = "~/default.aspx?pg={0}&thread={1}&post={2}#{2}";

			List<SearchResult> foundResults = new List<SearchResult>();
			DataRow[] foundPosts = Posts.Select(string.Format("Body LIKE '%{0}%' AND IsApproved='true'", searchString.Replace("'", "''")));

			foreach (DataRow post in foundPosts)
			{
				DataRow thread = GetThread((Guid)post["ThreadID"]);

				foundResults.Add(
					new SearchResult(
						string.Format(url, page.PageId, thread["Permalink"], post["PostID"]),
						(string)thread["Subject"],
						SearchResult.CreateExcerpt(SearchResult.RemoveHtml((string)post["Body"]), searchString)
					)
				);
			}

			return foundResults;
		}
		#endregion
		
		#region Thread related methods
		/// <summary>
		/// Adds a new thread to the forum. A thread consists of a subject and the first post (body).
		/// </summary>
		public Guid AddNewThread(string subject, string body, string author)
		{
			DataRow _rowThread = Threads.NewRow();
			_rowThread["ThreadID"] = Guid.NewGuid();
			_rowThread["Subject"] = subject.Trim();
			_rowThread["CreatedBy"] = author;
			_rowThread["lastEntry"] = DateTime.Now;
			_rowThread["MessageCount"] = 0;
			_rowThread["HitCount"] = 0;
			_rowThread["lastAuthor"] = author;
			_rowThread["Permalink"] = TextFormatter.GeneratePermalinkUrl(DateTime.Now.ToString("yyyy.MM.dd") + "_" + subject.Trim());
			_rowThread["IsApproved"] = !EnableModeration;
			Threads.Rows.Add(_rowThread);

			DataRow _rowPost = Posts.NewRow();
			_rowPost["PostID"] = Guid.NewGuid();
			_rowPost["ThreadID"] = _rowThread["ThreadID"];
			_rowPost["Author"] = author;
			_rowPost["Body"] = body.Trim();
			_rowPost["Created"] = DateTime.Now;
			_rowPost["IsApproved"] = !EnableModeration;
			Posts.Rows.Add(_rowPost);

			_data.Entries.AcceptChanges();
			SaveData();

			return (Guid)_rowThread["ThreadID"];
		}

		/// <summary>
		/// Deletes a thread from the forum, including all its associated posts
		/// </summary>
		public void DeleteThread(Guid threadID)
		{
			//delete posts of the thread
			foreach (DataRow post in Posts.Select(string.Format("ThreadID='{0}'", threadID)))
			{
				Posts.Rows.Remove(post);
			}
			Posts.AcceptChanges();

			//now delete the thread
			Threads.Rows.Remove(Threads.Rows.Find(threadID));
			Threads.AcceptChanges();

			SaveData();
		}

		/// <summary>
		/// Finds a thread by its permalink.
		/// The permalink is generated from the thread-subject to have SEO friendly URLs for the thread-detailviews
		/// </summary>
		public Guid FindThreadIDByPermalink(string permalink)
		{
			DataRow[] foundThreads = Threads.Select(string.Format("Permalink LIKE '{0}'", permalink));
			if (foundThreads.Length == 0)
				return Guid.Empty;
			else
				return (Guid)foundThreads[0]["ThreadID"];
		}

		/// <summary>
		/// Returns a thread by its ID
		/// </summary>
		public DataRow GetThread(Guid threadID)
		{
			return Threads.Rows.Find(threadID);
		}

		/// <summary>
		/// Returns the subject (title) of a thread
		/// </summary>
		public string GetThreadTitle(Guid threadID)
		{
			return GetThread(threadID)["Subject"].ToString();
		}

		/// <summary>
		/// Returns a DataView of all approved threads
		/// </summary>
		public DataView GetThreads()
		{
			return new DataView(Threads, "IsApproved = 'true'", "lastEntry desc", DataViewRowState.CurrentRows);
		}

		/// <summary>
		/// Increments the number of hits (views) of a thread
		/// </summary>
		public void IncrementHitCount(Guid threadID)
		{
			DataRow thread = GetThread(threadID);
			int hitCount = (int)thread["HitCount"];
			hitCount++;
			thread["HitCount"] = hitCount;
			thread.AcceptChanges();
			SaveData();
		}

		/// <summary>
		/// Returns a DataView of all threads that are not approved yet.
		/// </summary>
		public DataView GetThreadsToApprove()
		{
			return new DataView(Threads, "IsApproved <> 'true'", string.Empty, DataViewRowState.CurrentRows);
		}

		/// <summary>
		/// Iterates through the passed list of threadIDs and approves each thread.
		/// </summary>
		public void ApproveThreads(List<Guid> threadIDs)
		{
			foreach (Guid threadID in threadIDs)
			{
				DataRow thread = Threads.Rows.Find(threadID);
				thread["IsApproved"] = true;

				//also approve the first post of the thread (no "empty" threads should be shown in the frontend)
				foreach (DataRow post in Posts.Select(string.Format("ThreadID = '{0}'", thread["ThreadID"])))
				{
					post["IsApproved"] = true;
				}
			}
			_data.Entries.AcceptChanges();
			SaveData();

			updateThreadMetadata();
		}

		/// <summary>
		/// Updates the "metadata" of a thread (dates, counts etc.)
		/// </summary>
		private void updateThreadMetadata()
		{
			foreach (DataRowView thread in GetThreads())
			{
				DataView posts = GetPostsByThreadID((Guid)thread["ThreadID"]);
				thread["MessageCount"] = posts.Count - 1;
				thread["lastAuthor"] = posts[posts.Count-1]["Author"];
				thread["lastEntry"] = posts[posts.Count - 1]["Created"];
			}
			Threads.AcceptChanges();
			SaveData();
		}
		#endregion

		#region Post related methods
		/// <summary>
		/// Adds a new post to a thread.
		/// </summary>
		public Guid AddNewPost(Guid threadID, string body, string author)
		{
			DataRow _rowPost = Posts.NewRow();
			_rowPost["PostID"] = Guid.NewGuid();
			_rowPost["ThreadID"] = threadID;
			_rowPost["Author"] = author;
			_rowPost["Body"] = body.Trim();
			_rowPost["Created"] = DateTime.Now;
			_rowPost["IsApproved"] = !EnableModeration;
			Posts.Rows.Add(_rowPost);

			//if moderation is disabled, update the thread's metadata
			if (!EnableModeration)
			{
				DataRow thread = Threads.Rows.Find(threadID);
				thread["lastEntry"] = _rowPost["Created"];
				thread["MessageCount"] = ((int)thread["MessageCount"]) + 1;
				thread["lastAuthor"] = author;
			}

			_data.Entries.AcceptChanges();
			SaveData();

			return (Guid)_rowPost["PostID"];
		}

		/// <summary>
		/// Delete a post from a thread
		/// </summary>
		public void DeletePost(Guid postID, Guid threadID)
		{
			Posts.Rows.Remove(Posts.Rows.Find(postID));
			Posts.AcceptChanges();

			DataView posts = GetPostsByThreadID(threadID);
			DataRowView lastPost = posts[posts.Count - 1];

			//update metadata, as the deleted post might have been the "newest" one
			DataRow thread = Threads.Rows.Find(threadID);
			thread["lastEntry"] = lastPost["Created"];
			thread["MessageCount"] = ((int)thread["MessageCount"]) - 1;
			thread["lastAuthor"] = lastPost["Author"];
			thread.AcceptChanges();

			SaveData();
		}

		/// <summary>
		/// Returns a DataView of all approved posts of a thread.
		/// </summary>
		public DataView GetPostsByThreadID(Guid threadID)
		{
			return new DataView(Posts, string.Format("ThreadID = '{0}' AND IsApproved='true'", threadID), "Created Asc", DataViewRowState.CurrentRows);
		}

		/// <summary>
		/// Returns a DataView of all posts that have not been approved yet.
		/// </summary>
		public DataView GetPostsToApprove()
		{
			return new DataView(Posts, "IsApproved <> 'true'", "Created Asc", DataViewRowState.CurrentRows);
		}

		/// <summary>
		/// Iterates through the list of postIDs and approves each post.
		/// </summary>
		public void ApprovePosts(List<Guid> postIDs)
		{
			foreach (Guid postID in postIDs)
			{
				Posts.Rows.Find(postID)["IsApproved"] = true;
			}
			//update the thread metadata
			updateThreadMetadata();

			_data.Entries.AcceptChanges();
			SaveData();
		}
		#endregion

		#region Data related methods
		private DataTable Posts
		{
			get { return _data.Entries.Tables["Posts"]; }
		}

		private DataTable Threads
		{
			get { return _data.Entries.Tables["Threads"]; }
		}

		public bool AllowAnonymousPosts
		{
			get { return _data.AllowAnonymousPosts; }
			set { _data.AllowAnonymousPosts = value; }
		}

		public bool EnableModeration
		{
			get { return _data.EnableModeration; }
			set { _data.EnableModeration = value; }
		}

		public class ForumData
		{
			public ForumData()
			{
				AllowAnonymousPosts = false;
				EnableModeration = true;

				Entries = new DataSet();
				Entries.Tables.Add(new DataTable("Posts"));
				Entries.Tables["Posts"].Columns.AddRange(
					new DataColumn[] 
                {
					new DataColumn("PostID", typeof(Guid)),
                    new DataColumn("ThreadID", typeof(Guid)),
                    new DataColumn("Author", typeof(string)),
                    new DataColumn("Body", typeof(string)),
                    new DataColumn("Created", typeof(DateTime)),
					new DataColumn("IsApproved", typeof(bool))
                }
			   );
				Entries.Tables["Posts"].PrimaryKey = new DataColumn[] { Entries.Tables["Posts"].Columns["PostID"] };
				Entries.Tables["Posts"].Columns["PostID"].Unique = true;

				Entries.Tables.Add(new DataTable("Threads"));
				Entries.Tables["Threads"].Columns.AddRange(
					new DataColumn[]
                {
                    new DataColumn("ThreadID", typeof(Guid)),
                    new DataColumn("Subject", typeof(string)),
                    new DataColumn("CreatedBy", typeof(string)),
                    new DataColumn("lastEntry", typeof(DateTime)),
                    new DataColumn("MessageCount", typeof(int)),
                    new DataColumn("HitCount", typeof(int)),
                    new DataColumn("lastAuthor", typeof(string)),
					new DataColumn("Permalink", typeof(string)),
					new DataColumn("IsApproved", typeof(bool))
                }
				);
				Entries.Tables["Threads"].PrimaryKey = new DataColumn[] { Entries.Tables["Threads"].Columns["ThreadID"] };
				Entries.Tables["Threads"].Columns["ThreadID"].Unique = true;
			}

			public DataSet Entries;
			public bool AllowAnonymousPosts;
			public bool EnableModeration;
		}
		#endregion
	}
}