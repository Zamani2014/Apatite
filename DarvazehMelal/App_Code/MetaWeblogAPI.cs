//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================
                                                                                                                                                                                                                                                               
using System;
using CookComputing.XmlRpc;

namespace CookComputing.MetaWeblog
{
  	/// <summary>
	/// This struct represents enclosure.
	/// </summary>
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct Enclosure
	{
		public int length;
		public string type;
		public string url;
	}
	/// <summary>
	/// This struct represents source.
	/// </summary>
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct Source
	{
		public string name;
		public string url;
	}

	/// <summary> 
	/// This struct represents user blog.
	/// </summary> 
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct UserBlog
	{
		/// <summary>
		/// Url to blog.
		/// </summary>
		public string url;
		/// <summary>
		/// BlogID, as defined in rsd file - possible collision.
		/// </summary>
		public string blogid;
		/// <summary>
		/// Name of blog.
		/// </summary>
		public string blogName;
	}
	/// <summary> 
	/// This struct represents information about a user. 
	/// </summary> 
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct UserInfo
	{
		/// <summary>
		/// Url to user's blog.
		/// </summary>
		public string url;
		/// <summary>
		/// Blog's unique identifier.
		/// </summary>
		public string blogid;
		/// <summary>
		/// Name of a blog
		/// </summary>
		public string blogName;
		/// <summary>
		/// User's first name.
		/// </summary>
		public string firstname;
		/// <summary>
		/// User's last name.
		/// </summary>
		public string lastname;
		/// <summary>
		/// User's email.
		/// </summary>
		public string email;
		/// <summary>
		/// User's nick name.
		/// </summary>
		public string nickname;
		/// <summary>
		/// User's unique identifier.
		/// </summary>
		public string userid;
	}

	/// <summary> 
	/// This struct represents the information about a category.
	/// </summary> 
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct Category
	{
		public string categoryid;
		public string title;
		public string description;
		public string htmlUrl;
		public string rssUrl;

	}
	/// <summary>
	/// This struct represents a post.
	/// </summary>
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct Post
	{
		/// <summary>
		/// date of creation of current post
		/// </summary>
		public DateTime dateCreated;
		/// <summary>
		/// Description of the post- this is content of post.
		/// </summary>
		public string description;
		/// <summary>
		/// Post's title
		/// </summary>
		public string title;
		/// <summary>
		/// Unique identifier of a post.
		/// </summary>
		public string postid;
		/// <summary>
		/// Array of assigned categories.
		/// </summary>
		public string[] categories;
		/// <summary>
		/// Link (url) to post.
		/// </summary>
		public string link;
		/// <summary>
		/// Permalink to post.
		/// </summary>
		public string permaLink;
		/// <summary>
		/// Unique identifier of a user that created that post (usually that is author).
		/// </summary>
		public string userid;
		/// <summary>
		/// Post's enclosure.
		/// </summary>
		public Enclosure enclosure;
		/// <summary>
		/// Post's source.
		/// </summary>
		public Source source;
	}
	/// <summary>
	/// Struct that represents a certain media object for example image, video or sound.
	/// Note that this stucture can represent any file type (even an exe)
	/// but handling of such files is never intended (maybe there is a wlw plugin that can 'attach' other files to post).
	/// </summary>
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct MediaObject
	{
		/// <summary>
		/// Filename of a file that user selected.
		/// </summary>
		public string name;
		/// <summary>
		/// Type of a file in MIME type format.
		/// </summary>
		public string type;
		/// <summary>
		/// Bits field contains array of bites that file(Media Object) contains.
		/// </summary>
		public byte[] bits;
	}
	/// <summary>
	/// Struct that represents a Media Object informations.
	/// </summary>
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct MediaObjectInfo
	{
		/// <summary>
		/// File name of stored file.
		/// </summary>
		public string file;
		/// <summary>
		/// Url from which file can be retrieved.
		/// </summary>
		public string url;
		/// <summary>
		/// Type of a file in MIME type format.
		/// </summary>
		public string type;
	}
}


