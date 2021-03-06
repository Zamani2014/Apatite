/****** Object:  Table [dbo].[AreaRange]    Script Date: 04/29/2013 14:05:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AreaRange]') AND type in (N'U'))
DROP TABLE [dbo].[AreaRange]
GO
/****** Object:  Table [dbo].[BuildingImages]    Script Date: 04/29/2013 14:05:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BuildingImages]') AND type in (N'U'))
DROP TABLE [dbo].[BuildingImages]
GO
/****** Object:  Table [dbo].[BuildingTbl]    Script Date: 04/29/2013 14:05:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BuildingTbl]') AND type in (N'U'))
DROP TABLE [dbo].[BuildingTbl]
GO
/****** Object:  Table [dbo].[BuildingTypes]    Script Date: 04/29/2013 14:05:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BuildingTypes]') AND type in (N'U'))
DROP TABLE [dbo].[BuildingTypes]
GO
/****** Object:  Table [dbo].[CabinetTypes]    Script Date: 04/29/2013 14:05:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CabinetTypes]') AND type in (N'U'))
DROP TABLE [dbo].[CabinetTypes]
GO
/****** Object:  Table [dbo].[CurrencyTypes]    Script Date: 04/29/2013 14:05:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CurrencyTypes]') AND type in (N'U'))
DROP TABLE [dbo].[CurrencyTypes]
GO
/****** Object:  Table [dbo].[DocTypes]    Script Date: 04/29/2013 14:05:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocTypes]') AND type in (N'U'))
DROP TABLE [dbo].[DocTypes]
GO
/****** Object:  Table [dbo].[FlooringTypes]    Script Date: 04/29/2013 14:05:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FlooringTypes]') AND type in (N'U'))
DROP TABLE [dbo].[FlooringTypes]
GO
/****** Object:  Table [dbo].[ImageSliderTbl]    Script Date: 04/29/2013 14:05:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImageSliderTbl]') AND type in (N'U'))
DROP TABLE [dbo].[ImageSliderTbl]
GO
/****** Object:  Table [dbo].[ImagesTbl]    Script Date: 04/29/2013 14:05:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImagesTbl]') AND type in (N'U'))
DROP TABLE [dbo].[ImagesTbl]
GO
/****** Object:  Table [dbo].[MapTbl]    Script Date: 04/29/2013 14:05:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MapTbl]') AND type in (N'U'))
DROP TABLE [dbo].[MapTbl]
GO
/****** Object:  Table [dbo].[NewsTbl]    Script Date: 04/29/2013 14:05:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NewsTbl]') AND type in (N'U'))
DROP TABLE [dbo].[NewsTbl]
GO
/****** Object:  Table [dbo].[OwnerTbl]    Script Date: 04/29/2013 14:05:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OwnerTbl]') AND type in (N'U'))
DROP TABLE [dbo].[OwnerTbl]
GO
/****** Object:  Table [dbo].[ProvinceTbl]    Script Date: 04/29/2013 14:05:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProvinceTbl]') AND type in (N'U'))
DROP TABLE [dbo].[ProvinceTbl]
GO
/****** Object:  Table [dbo].[RegisterTblUsers]    Script Date: 04/29/2013 14:05:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RegisterTblUsers]') AND type in (N'U'))
DROP TABLE [dbo].[RegisterTblUsers]
GO
/****** Object:  Table [dbo].[RequestTbl]    Script Date: 04/29/2013 14:05:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RequestTbl]') AND type in (N'U'))
DROP TABLE [dbo].[RequestTbl]
GO
/****** Object:  Table [dbo].[RequestTblUsers]    Script Date: 04/29/2013 14:05:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RequestTblUsers]') AND type in (N'U'))
DROP TABLE [dbo].[RequestTblUsers]
GO
/****** Object:  Table [dbo].[ResidenceTypes]    Script Date: 04/29/2013 14:05:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ResidenceTypes]') AND type in (N'U'))
DROP TABLE [dbo].[ResidenceTypes]
GO
/****** Object:  Table [dbo].[SanitaryServicesTypes]    Script Date: 04/29/2013 14:05:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SanitaryServicesTypes]') AND type in (N'U'))
DROP TABLE [dbo].[SanitaryServicesTypes]
GO
/****** Object:  Table [dbo].[TotPriceRange]    Script Date: 04/29/2013 14:05:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TotPriceRange]') AND type in (N'U'))
DROP TABLE [dbo].[TotPriceRange]
GO
/****** Object:  Table [dbo].[TransactionTypes]    Script Date: 04/29/2013 14:05:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TransactionTypes]') AND type in (N'U'))
DROP TABLE [dbo].[TransactionTypes]
GO
/****** Object:  Table [dbo].[ViewTypes]    Script Date: 04/29/2013 14:05:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ViewTypes]') AND type in (N'U'))
DROP TABLE [dbo].[ViewTypes]
GO
/****** Object:  Table [dbo].[ViewTypes]    Script Date: 04/29/2013 14:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ViewTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ViewTypes](
	[ID] [int] NOT NULL,
	[ViewName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
END
GO
INSERT [dbo].[ViewTypes] ([ID], [ViewName]) VALUES (1, N'آجر')
INSERT [dbo].[ViewTypes] ([ID], [ViewName]) VALUES (2, N'آجر انگلیسی')
INSERT [dbo].[ViewTypes] ([ID], [ViewName]) VALUES (3, N'آجر سه سانت')
INSERT [dbo].[ViewTypes] ([ID], [ViewName]) VALUES (4, N'آلومینیوم')
INSERT [dbo].[ViewTypes] ([ID], [ViewName]) VALUES (5, N'رفلکس')
INSERT [dbo].[ViewTypes] ([ID], [ViewName]) VALUES (6, N'رومی')
INSERT [dbo].[ViewTypes] ([ID], [ViewName]) VALUES (7, N'سرامیک')
INSERT [dbo].[ViewTypes] ([ID], [ViewName]) VALUES (8, N'سرامیک و شیشه')
INSERT [dbo].[ViewTypes] ([ID], [ViewName]) VALUES (9, N'سنگ')
INSERT [dbo].[ViewTypes] ([ID], [ViewName]) VALUES (10, N'سنگ و شیشه')
INSERT [dbo].[ViewTypes] ([ID], [ViewName]) VALUES (11, N'سیمان')
INSERT [dbo].[ViewTypes] ([ID], [ViewName]) VALUES (12, N'شیشه')
INSERT [dbo].[ViewTypes] ([ID], [ViewName]) VALUES (13, N'کنیتکس')
INSERT [dbo].[ViewTypes] ([ID], [ViewName]) VALUES (14, N'گرانیت')
INSERT [dbo].[ViewTypes] ([ID], [ViewName]) VALUES (15, N'کامپوزیت')
INSERT [dbo].[ViewTypes] ([ID], [ViewName]) VALUES (16, N'کلاسیک')
/****** Object:  Table [dbo].[TransactionTypes]    Script Date: 04/29/2013 14:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TransactionTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TransactionTypes](
	[ID] [int] NOT NULL,
	[TransactionName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_TransactionTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[TransactionTypes] ([ID], [TransactionName]) VALUES (1, N'فروش')
INSERT [dbo].[TransactionTypes] ([ID], [TransactionName]) VALUES (2, N'رهن و اجاره')
INSERT [dbo].[TransactionTypes] ([ID], [TransactionName]) VALUES (3, N'رهن')
INSERT [dbo].[TransactionTypes] ([ID], [TransactionName]) VALUES (4, N'اجاره')
INSERT [dbo].[TransactionTypes] ([ID], [TransactionName]) VALUES (5, N'سرقفلی')
INSERT [dbo].[TransactionTypes] ([ID], [TransactionName]) VALUES (6, N'پیش فروش')
/****** Object:  Table [dbo].[TotPriceRange]    Script Date: 04/29/2013 14:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TotPriceRange]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TotPriceRange](
	[ID] [int] NOT NULL,
	[TotPrice] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_TotPriceRange] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[TotPriceRange] ([ID], [TotPrice]) VALUES (1, N'قیمت روز')
INSERT [dbo].[TotPriceRange] ([ID], [TotPrice]) VALUES (2, N'تا 50 میلیون')
INSERT [dbo].[TotPriceRange] ([ID], [TotPrice]) VALUES (3, N'50 - 100 میلیون')
INSERT [dbo].[TotPriceRange] ([ID], [TotPrice]) VALUES (4, N'100 - 200 میلیون')
INSERT [dbo].[TotPriceRange] ([ID], [TotPrice]) VALUES (5, N'200 - 300 میلیون')
INSERT [dbo].[TotPriceRange] ([ID], [TotPrice]) VALUES (6, N'300 - 400 میلیون')
INSERT [dbo].[TotPriceRange] ([ID], [TotPrice]) VALUES (7, N'400 - 500 میلیون')
INSERT [dbo].[TotPriceRange] ([ID], [TotPrice]) VALUES (8, N'500 - 700 میلیون')
INSERT [dbo].[TotPriceRange] ([ID], [TotPrice]) VALUES (9, N'700 تا یک میلیارد ')
INSERT [dbo].[TotPriceRange] ([ID], [TotPrice]) VALUES (10, N'1 میلیارد به بالا')
/****** Object:  Table [dbo].[SanitaryServicesTypes]    Script Date: 04/29/2013 14:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SanitaryServicesTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SanitaryServicesTypes](
	[ID] [int] NOT NULL,
	[SanitaryName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_SanitaryServicesTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[SanitaryServicesTypes] ([ID], [SanitaryName]) VALUES (1, N'ایرانی')
INSERT [dbo].[SanitaryServicesTypes] ([ID], [SanitaryName]) VALUES (2, N'ایرانی - فرنگی')
INSERT [dbo].[SanitaryServicesTypes] ([ID], [SanitaryName]) VALUES (3, N'فرنگی')
/****** Object:  Table [dbo].[ResidenceTypes]    Script Date: 04/29/2013 14:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ResidenceTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ResidenceTypes](
	[ID] [int] NOT NULL,
	[ResidenceName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_ResidenceTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[ResidenceTypes] ([ID], [ResidenceName]) VALUES (1, N'مالک')
INSERT [dbo].[ResidenceTypes] ([ID], [ResidenceName]) VALUES (2, N'مستاجر')
INSERT [dbo].[ResidenceTypes] ([ID], [ResidenceName]) VALUES (3, N'تخلیه')
/****** Object:  Table [dbo].[RequestTblUsers]    Script Date: 04/29/2013 14:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RequestTblUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RequestTblUsers](
	[ID] [int] NOT NULL,
	[CompName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CompIP] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CompDate] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CompTime] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CompBrowser] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Referer] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_RequestTblUsers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[RequestTbl]    Script Date: 04/29/2013 14:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RequestTbl]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RequestTbl](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EMail] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Tel] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Mobile] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Province] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[State] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[City] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Region] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BuildingType] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DocType] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TransactionType] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Range] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Price] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Comments] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateTime] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_RequestTbl] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[RegisterTblUsers]    Script Date: 04/29/2013 14:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RegisterTblUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RegisterTblUsers](
	[ID] [int] NOT NULL,
	[CompName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CompIP] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CompDate] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CompTime] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CompBrowser] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Referer] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_RegisterTblUsers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[RegisterTblUsers] ([ID], [CompName], [CompIP], [CompDate], [CompTime], [CompBrowser], [Referer]) VALUES (0, N'::1', N'::1', N'1392/2/9', N'13:51:29:528', N'Mozilla FireFox', N'')
/****** Object:  Table [dbo].[ProvinceTbl]    Script Date: 04/29/2013 14:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProvinceTbl]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProvinceTbl](
	[ID] [int] NOT NULL,
	[ProvinceName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_ProvinceTbl] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (1, N'آذربایجان شرقی')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (2, N'آذربایجان غربی')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (3, N'اردبیل')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (4, N'اصفهان')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (5, N'البرز')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (6, N'ایلام')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (7, N'بوشهر')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (8, N'تهران')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (9, N'چهارمحال و بختیاری')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (10, N'خراسان جنوبی')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (11, N'خراسان رضوی')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (12, N'خراسان شمالی')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (13, N'خوزستان')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (14, N'زنجان')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (15, N'سمنان')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (16, N'سیستان و بلوچستان')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (17, N'فارس')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (18, N'قزوین')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (19, N'قم')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (20, N'کردستان')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (21, N'کرمان')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (22, N'کرمانشاه')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (23, N'کهگیلویه و بویراحمد')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (24, N'گلستان')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (25, N'گیلان')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (26, N'لرستان')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (27, N'مازندران')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (28, N'مرکزی')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (29, N'هرمزگان')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (30, N'همدان')
INSERT [dbo].[ProvinceTbl] ([ID], [ProvinceName]) VALUES (31, N'یزد')
/****** Object:  Table [dbo].[OwnerTbl]    Script Date: 04/29/2013 14:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OwnerTbl]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OwnerTbl](
	[ID] [int] NOT NULL,
	[Country] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Province] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[City] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Region] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TransactionType] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BuildingType] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DocType] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[OwnerName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EMail] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Address] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[No] [int] NOT NULL,
	[Tel1] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Tel2] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Mobile] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Comments] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateTime] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_OwnerTbl] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[OwnerTbl] ([ID], [Country], [Province], [City], [Region], [TransactionType], [BuildingType], [DocType], [OwnerName], [EMail], [Address], [No], [Tel1], [Tel2], [Mobile], [Comments], [DateTime]) VALUES (291, N'جمهوری اسلامی ایران', N'آذربایجان شرقی', N'نور', N'نور', N'فروش', N'آپارتمان', N'مسکونی', N'زمانی', N'ali_Zamani@live.com', N'مازندران نور نرسیده به هتل نارنجستان', 0, N'09111177870', N'', N'', N'توضیحات', N'12:53:38 - 1392/2/9')
INSERT [dbo].[OwnerTbl] ([ID], [Country], [Province], [City], [Region], [TransactionType], [BuildingType], [DocType], [OwnerName], [EMail], [Address], [No], [Tel1], [Tel2], [Mobile], [Comments], [DateTime]) VALUES (292, N'جمهوری اسلامی ایران', N'آذربایجان شرقی', N'نور', N'تست', N'فروش', N'آپارتمان', N'مسکونی', N'زمانی', N'ali_Zamani@live.com', N'مازندران نور نرسیده به هتل نارنجستان', 0, N'09359662011', N'', N'', N'توضیحات', N'13:50:23 - 1392/2/9')
/****** Object:  Table [dbo].[NewsTbl]    Script Date: 04/29/2013 14:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NewsTbl]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NewsTbl](
	[ID] [int] NOT NULL,
	[Subject] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[News] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateTime] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_NewsTbl] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[MapTbl]    Script Date: 04/29/2013 14:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MapTbl]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MapTbl](
	[ID] [int] NOT NULL,
	[BuildingID] [int] NOT NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
 CONSTRAINT [PK_MapTbl] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[ImagesTbl]    Script Date: 04/29/2013 14:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImagesTbl]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ImagesTbl](
	[ID] [int] NOT NULL,
	[BuildingID] [int] NOT NULL,
	[imageData] [image] NOT NULL,
	[imageName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[imageType] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_ImagesTbl] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[ImageSliderTbl]    Script Date: 04/29/2013 14:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImageSliderTbl]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ImageSliderTbl](
	[ID] [int] NOT NULL,
	[Subject] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ImageID] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Link] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Comments] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateTime] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_ImageSliderTbl] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[FlooringTypes]    Script Date: 04/29/2013 14:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FlooringTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FlooringTypes](
	[ID] [int] NOT NULL,
	[FlooringName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_FlooringTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[FlooringTypes] ([ID], [FlooringName]) VALUES (1, N'HDF')
INSERT [dbo].[FlooringTypes] ([ID], [FlooringName]) VALUES (2, N'برنز')
INSERT [dbo].[FlooringTypes] ([ID], [FlooringName]) VALUES (3, N'پارکت')
INSERT [dbo].[FlooringTypes] ([ID], [FlooringName]) VALUES (4, N'پارکت سرامیک')
INSERT [dbo].[FlooringTypes] ([ID], [FlooringName]) VALUES (5, N'پارکت سنگ')
INSERT [dbo].[FlooringTypes] ([ID], [FlooringName]) VALUES (6, N'تکسرام')
INSERT [dbo].[FlooringTypes] ([ID], [FlooringName]) VALUES (7, N'دلخواه')
INSERT [dbo].[FlooringTypes] ([ID], [FlooringName]) VALUES (8, N'سرامیک')
INSERT [dbo].[FlooringTypes] ([ID], [FlooringName]) VALUES (9, N'سرامیک برنز')
INSERT [dbo].[FlooringTypes] ([ID], [FlooringName]) VALUES (10, N'سنگ')
INSERT [dbo].[FlooringTypes] ([ID], [FlooringName]) VALUES (11, N'سنگ سرامیک')
INSERT [dbo].[FlooringTypes] ([ID], [FlooringName]) VALUES (12, N'سنگ موزائیک')
INSERT [dbo].[FlooringTypes] ([ID], [FlooringName]) VALUES (13, N'سیمان')
INSERT [dbo].[FlooringTypes] ([ID], [FlooringName]) VALUES (14, N'شیشه')
INSERT [dbo].[FlooringTypes] ([ID], [FlooringName]) VALUES (15, N'گرانیت')
INSERT [dbo].[FlooringTypes] ([ID], [FlooringName]) VALUES (16, N'لمینت')
INSERT [dbo].[FlooringTypes] ([ID], [FlooringName]) VALUES (17, N'موزائیک')
INSERT [dbo].[FlooringTypes] ([ID], [FlooringName]) VALUES (18, N'موکت')
INSERT [dbo].[FlooringTypes] ([ID], [FlooringName]) VALUES (19, N'موکت پارکت')
INSERT [dbo].[FlooringTypes] ([ID], [FlooringName]) VALUES (20, N'موکت سنگ')
INSERT [dbo].[FlooringTypes] ([ID], [FlooringName]) VALUES (21, N'موکت موزائیک')
INSERT [dbo].[FlooringTypes] ([ID], [FlooringName]) VALUES (22, N'موکت سرامیک')
INSERT [dbo].[FlooringTypes] ([ID], [FlooringName]) VALUES (23, N'مکالئوم')
INSERT [dbo].[FlooringTypes] ([ID], [FlooringName]) VALUES (24, N'کاشی')
/****** Object:  Table [dbo].[DocTypes]    Script Date: 04/29/2013 14:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DocTypes](
	[ID] [int] NOT NULL,
	[DocName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_DocTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[DocTypes] ([ID], [DocName]) VALUES (1, N'مسکونی')
INSERT [dbo].[DocTypes] ([ID], [DocName]) VALUES (2, N'اداری')
INSERT [dbo].[DocTypes] ([ID], [DocName]) VALUES (3, N'تجاری')
INSERT [dbo].[DocTypes] ([ID], [DocName]) VALUES (4, N'موقعیت اداری')
INSERT [dbo].[DocTypes] ([ID], [DocName]) VALUES (5, N'مسکونی - تجاری')
INSERT [dbo].[DocTypes] ([ID], [DocName]) VALUES (6, N'مزروعی')
INSERT [dbo].[DocTypes] ([ID], [DocName]) VALUES (7, N'سایر موارد')
/****** Object:  Table [dbo].[CurrencyTypes]    Script Date: 04/29/2013 14:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CurrencyTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CurrencyTypes](
	[ID] [int] NOT NULL,
	[CurrencyName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_CurrencyTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[CurrencyTypes] ([ID], [CurrencyName]) VALUES (1, N'تومان')
INSERT [dbo].[CurrencyTypes] ([ID], [CurrencyName]) VALUES (2, N'دلار')
INSERT [dbo].[CurrencyTypes] ([ID], [CurrencyName]) VALUES (3, N'یورو')
/****** Object:  Table [dbo].[CabinetTypes]    Script Date: 04/29/2013 14:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CabinetTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CabinetTypes](
	[ID] [int] NOT NULL,
	[CabinetName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_CabinetTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[CabinetTypes] ([ID], [CabinetName]) VALUES (1, N'MDF')
INSERT [dbo].[CabinetTypes] ([ID], [CabinetName]) VALUES (2, N'آبدارخانه')
INSERT [dbo].[CabinetTypes] ([ID], [CabinetName]) VALUES (3, N'چوبی')
INSERT [dbo].[CabinetTypes] ([ID], [CabinetName]) VALUES (4, N'چوبی خارجی')
INSERT [dbo].[CabinetTypes] ([ID], [CabinetName]) VALUES (5, N'چوبی فلزی')
INSERT [dbo].[CabinetTypes] ([ID], [CabinetName]) VALUES (6, N'چوبی گازر')
INSERT [dbo].[CabinetTypes] ([ID], [CabinetName]) VALUES (7, N'دلخواه')
INSERT [dbo].[CabinetTypes] ([ID], [CabinetName]) VALUES (8, N'فایبرگلاس')
INSERT [dbo].[CabinetTypes] ([ID], [CabinetName]) VALUES (9, N'فلزی')
INSERT [dbo].[CabinetTypes] ([ID], [CabinetName]) VALUES (10, N'فلزی طرح چوب')
INSERT [dbo].[CabinetTypes] ([ID], [CabinetName]) VALUES (11, N'فورمات')
INSERT [dbo].[CabinetTypes] ([ID], [CabinetName]) VALUES (12, N'گازر')
INSERT [dbo].[CabinetTypes] ([ID], [CabinetName]) VALUES (13, N'ماج نما')
INSERT [dbo].[CabinetTypes] ([ID], [CabinetName]) VALUES (14, N'مبله')
INSERT [dbo].[CabinetTypes] ([ID], [CabinetName]) VALUES (15, N'نف آلمان')
/****** Object:  Table [dbo].[BuildingTypes]    Script Date: 04/29/2013 14:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BuildingTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BuildingTypes](
	[ID] [int] NOT NULL,
	[BuildingName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_BuildingTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[BuildingTypes] ([ID], [BuildingName]) VALUES (1, N'آپارتمان')
INSERT [dbo].[BuildingTypes] ([ID], [BuildingName]) VALUES (2, N'ویلا - خانه')
INSERT [dbo].[BuildingTypes] ([ID], [BuildingName]) VALUES (3, N'مستغلات')
INSERT [dbo].[BuildingTypes] ([ID], [BuildingName]) VALUES (4, N'املاک اداری')
INSERT [dbo].[BuildingTypes] ([ID], [BuildingName]) VALUES (5, N'مغازه و املاک تجاری')
INSERT [dbo].[BuildingTypes] ([ID], [BuildingName]) VALUES (6, N'زمین، باغ و باغچه')
INSERT [dbo].[BuildingTypes] ([ID], [BuildingName]) VALUES (7, N'کارخانه - کارگاه')
INSERT [dbo].[BuildingTypes] ([ID], [BuildingName]) VALUES (8, N'برج و مجتمع مسکونی')
/****** Object:  Table [dbo].[BuildingTbl]    Script Date: 04/29/2013 14:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BuildingTbl]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BuildingTbl](
	[ID] [int] NOT NULL,
	[OwnerID] [int] NOT NULL,
	[Range] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Area] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RoomNo] [int] NOT NULL,
	[ClassNo] [int] NOT NULL,
	[ClassTot] [int] NOT NULL,
	[UnitsInClass] [int] NOT NULL,
	[UnitsTot] [int] NULL,
	[BuildingView] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ResidentType] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[YoursOld] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Old] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GeoPosition] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Comments] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Cabinet] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Sanitary] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Floor] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Price] [int] NOT NULL,
	[TotPrice] [int] NOT NULL,
	[Currency] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Parking] [int] NULL,
	[TelsNo] [int] NULL,
	[Other] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Facilities] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateTime] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_BuildingTbl] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[BuildingTbl] ([ID], [OwnerID], [Range], [Area], [RoomNo], [ClassNo], [ClassTot], [UnitsInClass], [UnitsTot], [BuildingView], [ResidentType], [YoursOld], [Old], [GeoPosition], [Comments], [Cabinet], [Sanitary], [Floor], [Price], [TotPrice], [Currency], [Parking], [TelsNo], [Other], [Facilities], [DateTime]) VALUES (291, 292, N'33', N'', 1, 2, 2, 2, 0, N'آجر', N'مالک', N'  نوساز', N'ساله', N'', N'', N'MDF', N'ایرانی', N'HDF', 1000000, 50000000, N'تومان', 0, 0, N'', N'', N'13:50:39 - 1392/2/9')
INSERT [dbo].[BuildingTbl] ([ID], [OwnerID], [Range], [Area], [RoomNo], [ClassNo], [ClassTot], [UnitsInClass], [UnitsTot], [BuildingView], [ResidentType], [YoursOld], [Old], [GeoPosition], [Comments], [Cabinet], [Sanitary], [Floor], [Price], [TotPrice], [Currency], [Parking], [TelsNo], [Other], [Facilities], [DateTime]) VALUES (292, 0, N'33', N'', 1, 2, 2, 2, 0, N'آجر', N'مالک', N'  نوساز', N'ساله', N'', N'', N'MDF', N'ایرانی', N'HDF', 1000000, 50000000, N'تومان', 0, 0, N'', N'', N'13:51:29 - 1392/2/9')
/****** Object:  Table [dbo].[BuildingImages]    Script Date: 04/29/2013 14:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BuildingImages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BuildingImages](
	[ID] [int] NOT NULL,
	[BuildingID] [int] NOT NULL,
	[Image1] [int] NULL,
	[Image2] [int] NULL,
	[Image3] [int] NULL,
	[Image4] [int] NULL,
	[Image5] [int] NULL,
	[Image6] [int] NULL,
	[Image7] [int] NULL,
	[Image8] [int] NULL,
	[Image9] [int] NULL,
 CONSTRAINT [PK_BuildingImages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[AreaRange]    Script Date: 04/29/2013 14:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AreaRange]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AreaRange](
	[ID] [int] NOT NULL,
	[Area] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_AreaRange] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[AreaRange] ([ID], [Area]) VALUES (1, N'تا 50 متر')
INSERT [dbo].[AreaRange] ([ID], [Area]) VALUES (2, N'50 - 75')
INSERT [dbo].[AreaRange] ([ID], [Area]) VALUES (3, N'75 - 100')
INSERT [dbo].[AreaRange] ([ID], [Area]) VALUES (4, N'100 - 150')
INSERT [dbo].[AreaRange] ([ID], [Area]) VALUES (5, N'150 - 200')
INSERT [dbo].[AreaRange] ([ID], [Area]) VALUES (6, N'200 - 250')
INSERT [dbo].[AreaRange] ([ID], [Area]) VALUES (7, N'250 - 300')
INSERT [dbo].[AreaRange] ([ID], [Area]) VALUES (8, N'بالای 300 متر')
