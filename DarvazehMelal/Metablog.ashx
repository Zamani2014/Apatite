<%@ WebHandler Language="C#" Class="Metablog" %>

//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System;
using System.IO;
using System.Web;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using MyWebPagesStarterKit;
using CookComputing.XmlRpc;
using CookComputing.MetaWeblog;

/// <summary>
/// Provides a Metaweblog API (see http://www.xmlrpc.com/metaWeblogApi) for the Blog-sections of the starter kit.
/// For all the XML-RPC related things, a library from Charles Cook is used (http://www.xml-rpc.net/)
/// Note: to ensure proper blog-post addressing, the blogpost ID is always a combination of the blog's GUID and the blog post's GUID chained together with a hash-sign (blogGUID#blogPostGuid)
/// </summary>
public class Metablog : XmlRpcService
{
	/// <summary> 
	/// Returns the most recent draft and non-draft blog posts sorted in descending order by publish date. 
	/// </summary> 
	/// <param name="blogid"> Unique identifier that identifies specific user blog as account. </param> 
	/// <param name="username"> User's username. </param> 
	/// <param name="password"> User's password. </param> 
	/// <param name="numberOfPosts"> The number of posts to return. </param> 
	/// <returns>Array of Posts</returns> 
	[XmlRpcMethod("metaWeblog.getRecentPosts")]
	public Post[] getRecentPosts(string blogid, string username, string password, int numberOfPosts)
	{
		if (checkUser(username, password))
		{
			if (!userHasAccessToBlogAdmin(blogid, username))
				throw new XmlRpcFaultException(0, "access denied");

			List<Post> recentPosts = new List<Post>();
			Blog blog = new Blog(blogid);

			foreach (DataRowView entry in blog.GetBlogEntriesSorted("Created desc"))
			{
				Post post = new Post();
				post.postid = blogid + "#" + ((Guid)entry["Guid"]).ToString();
				post.title = (string)entry["Title"];
				post.description = (string)entry["Content"];
				post.dateCreated = (DateTime)entry["Created"];
				post.categories = blog.GetTagsAsTextForBlogPostId(post.postid.ToString());
				recentPosts.Add(post);

				if (recentPosts.Count >= numberOfPosts)
					break;
			}

			return recentPosts.ToArray();
		}
		else
		{
			throw new XmlRpcFaultException(0, "invalid user credentials");
		}
	}

	/// <summary> 
	/// Posts a new post(entry) to a blog.
	/// </summary>
	/// <param name="blogid"> Unique identifier that identifies specific user blog as account. </param> 
	/// <param name="username"> User's username. </param> 
	/// <param name="password"> User's password. </param> 
	/// <param name="post"> A struct representing the content(entry) to post to blog (upload). </param> 
	/// <param name="publish"> True if post should be published immediately, else this post will be uploaded and marked as draft.</param> 
	/// <returns> Postid of the created post entry. </returns> 
	[XmlRpcMethod("metaWeblog.newPost")]
	public string newPost(string blogid, string username, string password, Post content, bool publish)
	{
		if (checkUser(username, password))
		{
			if (!userHasAccessToBlogAdmin(blogid, username))
				throw new XmlRpcFaultException(0, "access denied");

			Blog blog = new Blog(blogid);
			Guid newPostId = Guid.NewGuid();
			DataRow row = blog.BlogEntries.NewRow();

			row["Guid"] = newPostId;
			row["Created"] = DateTime.Now;
			row["Updated"] = DateTime.Now;
			row["Comments"] = blog.CommentsStructure();

			row["Title"] = content.title;
			row["Content"] = content.description;

			assignTags(blog, row, content.categories);

			blog.BlogEntries.Rows.Add(row);
			blog.BlogEntries.AcceptChanges();

			blog.ConfigurationData.Rows[0]["Updated"] = DateTime.Now;
			blog.ConfigurationData.AcceptChanges();

			blog.SaveData();

			return blogid + "#" + newPostId.ToString();
		}
		else
		{
			throw new XmlRpcFaultException(0, "invalid user credentials");
		}
	}

	/// <summary> 
	/// Edits an existing entry on a blog. 
	/// </summary> 
	/// <param name="postid"> Unique identifier of the post to update. </param> 
	/// <param name="username"> User's username. </param> 
	/// <param name="password"> User's password. </param> 
	/// <param name="post"> A struct representing the content(entry) to post to blog (upload). </param> 
	/// <param name="publish"> True if post should be published immediately, else this post will be uploaded and marked as draft.</param> 
	/// <returns> Always returns true. </returns> 
	[XmlRpcMethod("metaWeblog.editPost")]
	public bool editPost(string postid, string username, string password, Post content, bool publish)
	{
		if (checkUser(username, password))
		{
			string[] ids = postid.Split('#'); //first is blog-id, second is post-id

			if (!userHasAccessToBlogAdmin(ids[0], username))
				throw new XmlRpcFaultException(0, "access denied");

			Blog blog = new Blog(ids[0]);
			DataRow row = blog.GetBlogEntry(ids[1]);

			row["Updated"] = DateTime.Now;
			row["Title"] = content.title;
			row["Content"] = content.description;
			//unassign the current tags, as they might have changed
			unassignTags(blog, row);
			//reassign the tags and increment number
			assignTags(blog, row, content.categories);
			row.AcceptChanges();

			blog.ConfigurationData.Rows[0]["Updated"] = DateTime.Now;
			blog.ConfigurationData.AcceptChanges();

			blog.SaveData();

			return true;
		}
		else
		{
			throw new XmlRpcFaultException(0, "invalid user credentials");
		}
	}

	/// <summary> 
	/// Deletes a post from the blog. 
	/// </summary> 
	/// <param name="appKey"> This parameter is ignored. </param> 
	/// <param name="postid"> Unique identifier of the post to update. </param> 
	/// <param name="username"> User's username. </param> 
	/// <param name="password"> User's password. </param> 
	/// <param name="post"> A struct representing the content(entry) to post to blog (upload). </param> 
	/// <param name="publish"> This parameter is ignored. </param> 
	/// <returns> Always returns true. </returns> 
	[XmlRpcMethod("blogger.deletePost")]
	public bool deletePost(string appKey, string postid, string username, string password, bool publish)
	{
		if (checkUser(username, password))
		{
			string[] ids = postid.Split('#'); //first is blog-id, second is post-id

			if (!userHasAccessToBlogAdmin(ids[0], username))
				throw new XmlRpcFaultException(0, "access denied");

			Blog blog = new Blog(ids[0]);
			DataRow row = blog.GetBlogEntry(ids[1]);

			//unassign the tags
			unassignTags(blog, row);

			blog.BlogEntries.Rows.Remove(row);
			blog.BlogEntries.AcceptChanges();

			///blog updated
			blog.ConfigurationData.Rows[0]["Updated"] = DateTime.Now;
			blog.ConfigurationData.AcceptChanges();

			blog.SaveData();

			return true;
		}
		else
		{
			throw new XmlRpcFaultException(0, "invalid user credentials");
		}
	}

	/// <summary> 
	/// Returns information about the user’s weblog account. User can have many blogs or he can have none. 
	/// Note that one user can have many blogs and single blog can have many posts.
	/// </summary> 
	/// <param name="appKey"> This parameter is ignored. </param> 
	/// <param name="username"> User's username. </param>
	/// <param name="password"> User's password. </param> 
	/// <returns> Array of structs that represents user’s blogs. Using WLW user can select blog on which he wants to post</returns> 
	[XmlRpcMethod("blogger.getUsersBlogs")]
	public UserBlog[] getUsersBlogs(string appKey, string username, string password)
	{
		if (checkUser(username, password))
		{
			List<UserBlog> usersBlogs = new List<UserBlog>();
			foreach (Blog blog in SectionLoader.GetInstance().LoadAllSectionsOfType<Blog>())
			{
				if (userHasAccessToBlogAdmin(blog.SectionId, username))
				{
					UserBlog userBlog = new UserBlog();
					userBlog.blogid = blog.SectionId;
					userBlog.blogName = (string)blog.ConfigurationData.Rows[0]["BlogName"];
					userBlog.url = getAbsoluteUrlToBlog(blog.SectionId);
					usersBlogs.Add(userBlog);
				}
			}

			return usersBlogs.ToArray();
		}
		else
		{
			throw new XmlRpcFaultException(0, "invalid user credentials");
		}
	}

	/// <summary> 
	/// Returns user info.
	/// </summary> 
	/// <param name="appKey"> This parameter is ignored. </param> 
	/// <param name="username"> User's username. </param>
	/// <param name="password"> User's password. </param> 
	/// <returns> Struct that contains profile information about the user.</returns> 
	[XmlRpcMethod("blogger.getUserInfo")]
	public UserInfo getUserInfo(string appKey, string username, string password)
	{
		if (checkUser(username, password))
		{
			MembershipUser user = Membership.GetUser(username);
			UserInfo info = new UserInfo();
			info.userid = user.ProviderUserKey.ToString();
			info.nickname = username;
			info.email = user.Email;
			return info;
		}
		else
		{
			throw new XmlRpcFaultException(0, "invalid user credentials");
		}
	}

	/// <summary> 
	/// Returns a specific post(entry) from a blog. 
	/// </summary> 
	/// <param name="postid"> The ID of the post to update. </param> 
	/// <param name="username"> User's username. </param>
	/// <param name="password"> User's password. </param> 
	/// <returns> Always returns true. </returns> 
	[XmlRpcMethod("metaWeblog.getPost")]
	public Post getPost(string postid, string username, string password)
	{
		if (checkUser(username, password))
		{
			string[] ids = postid.Split('#'); //first is blog-id, second is post-id

			if (!userHasAccessToBlogAdmin(ids[0], username))
				throw new XmlRpcFaultException(0, "access denied");

			Blog blog = new Blog(ids[0]);
			DataRow row = blog.GetBlogEntry(ids[1]);

			Post p = new Post();
			p.postid = postid;
			p.title = (string)row["Title"];
			p.description = (string)row["Content"];
			p.dateCreated = (DateTime)row["Created"];
			p.link = getAbsoluteUrlToBlogPost(ids[0], ids[1]);
			List<string> tags = new List<string>();
			foreach (string tagId in (ArrayList)row["Tags"])
			{
				tags.Add((string)blog.GetBlogTag(tagId)["Name"]);
			}
			p.categories = tags.ToArray();
				
			return p;
		}
		else
		{
			throw new XmlRpcFaultException(0, "invalid user credentials");
		}
	}

	/// <summary> 
	/// Get the array of categories that have been used in the blog. 
	/// </summary> 
	/// <param name="blogid"> Unique identifier that identifies specific user blog as account. </param> 
	/// <param name="username"> User's username. </param>
	/// <param name="password"> User's password. </param>
	/// <returns> An array of structs that represents each category.</returns> 
	[XmlRpcMethod("metaWeblog.getCategories")]
	public Category[] getCategories(string blogid, string username, string password)
	{
		if (checkUser(username, password))
		{
			if (!userHasAccessToBlogAdmin(blogid, username))
				throw new XmlRpcFaultException(0, "access denied");

			Blog blog = new Blog(blogid);
			List<Category> categories = new List<Category>();
			foreach (DataRowView tag in blog.GetBlogTagsSorted())
			{
				Category cat = new Category();
				cat.categoryid = ((Guid)tag["Guid"]).ToString();
				cat.title = (string)tag["Name"];
				categories.Add(cat);
			}
			return categories.ToArray();
		}
		else
		{
			throw new XmlRpcFaultException(0, "invalid user credentials");
		}
	}
	/// <summary> 
	/// Method that accepts remote file and saves it on the server.
	/// </summary>
	/// <param name="blogid"> Unique identifier that identifies specific user blog as account. </param>
	/// <param name="username"> User's username. </param>
	/// <param name="password"> User's password. </param>
	/// <param name="mediaObject"> Struct that represents media object that needs to be uploades</param>
	/// <returns>Struct that contains information about newly uploaded file</returns>
	[XmlRpcMethod("metaWeblog.newMediaObject")]
	public MediaObjectInfo newMediaObject(string blogid, string username, string password, MediaObject mediaObject)
	{
		if (checkUser(username, password))
		{
			if(!userHasAccessToBlogAdmin(blogid, username))
				throw new XmlRpcFaultException(0, "access denied");

			string serverPath = Path.Combine(
				Context.Server.MapPath("~/App_Data/UserImages/Image/BlogImages/"),
				Path.GetFileName(mediaObject.name)
			);
			
			if(!Directory.Exists(Path.GetDirectoryName(serverPath)))
				Directory.CreateDirectory(Path.GetDirectoryName(serverPath));
			File.WriteAllBytes(serverPath, mediaObject.bits);

			MediaObjectInfo media = new MediaObjectInfo();
			media.file = Path.GetFileName(serverPath);
			media.type = mediaObject.type;
			media.url = string.Format("{0}/ImageHandler.ashx?UploadedFile=true&image=~/App_Data/UserImages/Image/BlogImages/{1}", WebSite.GetInstance().GetFullWebsiteUrl(Context), Path.GetFileName(serverPath));
			
			return media;
		}
		else
		{
			throw new XmlRpcFaultException(0, "invalid user credentials");
		}
	}

	#region Helper Methods
	/// <summary>
	/// This method verifies given username and password
	/// </summary>
	/// <param name="username"> User's username. </param>
	/// <param name="password"> User's password. </param>
	/// <returns> true if user authentication is successful</returns>
	private bool checkUser(string username, string password)
	{
		return System.Web.Security.Membership.ValidateUser(username, password);
	}

	private bool userHasAccessToBlogAdmin(string blogId, string username)
	{
		if (Roles.IsUserInRole(username, RoleNames.Administrators.ToString()))
			return true;

		if (Roles.IsUserInRole(username, RoleNames.PowerUsers.ToString()))
		{
			string pageId = WebSite.GetInstance().getPageId(blogId);
			if (string.IsNullOrEmpty(pageId))
				return false;

			WebPage page = new WebPage(pageId);
			return page.EditPowerUser;
		}

		return false;
	}

	private void assignTags(Blog blog, DataRow blogPostRow, string[] tagNames)
	{
		ArrayList tags = new ArrayList();
		foreach (string tagName in tagNames)
		{
			DataRow tag = blog.GetBlogTagByName(tagName);
			if (tag != null)
			{
				tags.Add(tag["Guid"]);

				//increment the "Number" of the tag (how often the tag is used)
				int number = (int)tag["Number"];
				number++;
				tag["Number"] = number;
				tag.AcceptChanges();
			}
		}
		blogPostRow["Tags"] = tags;
	}

	private void unassignTags(Blog blog, DataRow blogPostRow)
	{
		ArrayList assignedTags = blogPostRow["Tags"] as ArrayList;
		foreach (Guid tag in assignedTags)
		{
			DataRow tagRow = blog.GetBlogTag(tag.ToString());
			int number = (int)tagRow["Number"];
			if (number > 0)
				number--;
			tagRow["Number"] = number;
			tagRow.AcceptChanges();
		}
		blogPostRow["Tags"] = new ArrayList();
	}

	private string getAbsoluteUrlToBlog(string blogId)
	{
		WebPage page = new WebPage(WebSite.GetInstance().getPageId(blogId));
		if (page.VirtualPath != string.Empty)
		{
			return
				string.Format("{0}/{1}#{2}",
					WebSite.GetInstance().GetFullWebsiteUrl(Context),
					page.VirtualPath.ToLower().Substring(1), //remove ~ at the beginning
					blogId
				);
		}
		else
		{
			return
				string.Format("{0}/default.aspx?pg={1}#{2}",
					WebSite.GetInstance().GetFullWebsiteUrl(Context),
					page.PageId,
					blogId
				);
		}
	}

	private string getAbsoluteUrlToBlogPost(string blogId, string blogPostId)
	{
		string blogUrl = getAbsoluteUrlToBlog(blogId);
		if (blogUrl.Contains("?"))
			return blogUrl.Insert(blogUrl.LastIndexOf('#') - 1, "&details=" + blogPostId);
		else
			return blogUrl.Insert(blogUrl.LastIndexOf('#') - 1, "?details=" + blogPostId);
	}
	#endregion
}