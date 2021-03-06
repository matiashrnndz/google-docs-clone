USE [master]
GO
/****** Object:  Database [169236-199394]    Script Date: 21-Jun-18 10:21:02 PM ******/
CREATE DATABASE [169236-199394]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'169236-199394', FILENAME = N'D:\Programs\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\169236-199394.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'169236-199394_log', FILENAME = N'D:\Programs\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\169236-199394_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [169236-199394] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [169236-199394].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [169236-199394] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [169236-199394] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [169236-199394] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [169236-199394] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [169236-199394] SET ARITHABORT OFF 
GO
ALTER DATABASE [169236-199394] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [169236-199394] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [169236-199394] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [169236-199394] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [169236-199394] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [169236-199394] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [169236-199394] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [169236-199394] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [169236-199394] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [169236-199394] SET  ENABLE_BROKER 
GO
ALTER DATABASE [169236-199394] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [169236-199394] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [169236-199394] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [169236-199394] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [169236-199394] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [169236-199394] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [169236-199394] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [169236-199394] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [169236-199394] SET  MULTI_USER 
GO
ALTER DATABASE [169236-199394] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [169236-199394] SET DB_CHAINING OFF 
GO
ALTER DATABASE [169236-199394] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [169236-199394] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [169236-199394] SET DELAYED_DURABILITY = DISABLED 
GO
USE [169236-199394]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 21-Jun-18 10:21:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 21-Jun-18 10:21:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[Id] [uniqueidentifier] NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[Rating] [int] NOT NULL,
	[Commenter_Email] [nvarchar](128) NULL,
	[Document_Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.Comment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Content]    Script Date: 21-Jun-18 10:21:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Content](
	[Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.Content] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Document]    Script Date: 21-Jun-18 10:21:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Document](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModification] [datetime] NOT NULL,
	[Creator_Email] [nvarchar](128) NOT NULL,
	[StyleClass_Name] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.Document] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DocumentModificationLog]    Script Date: 21-Jun-18 10:21:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentModificationLog](
	[Id] [uniqueidentifier] NOT NULL,
	[DateOfModification] [datetime] NOT NULL,
	[Document_Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.DocumentModificationLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Footer]    Script Date: 21-Jun-18 10:21:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Footer](
	[Id] [uniqueidentifier] NOT NULL,
	[Content_Id] [uniqueidentifier] NULL,
	[DocumentThatBelongs_Id] [uniqueidentifier] NOT NULL,
	[StyleClass_Name] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.Footer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Format]    Script Date: 21-Jun-18 10:21:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Format](
	[Name] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.Format] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FriendRequest]    Script Date: 21-Jun-18 10:21:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FriendRequest](
	[Id] [uniqueidentifier] NOT NULL,
	[Accepted] [bit] NOT NULL,
	[Receiver_Email] [nvarchar](128) NULL,
	[Sender_Email] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.FriendRequest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Header]    Script Date: 21-Jun-18 10:21:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Header](
	[Id] [uniqueidentifier] NOT NULL,
	[Content_Id] [uniqueidentifier] NULL,
	[DocumentThatBelongs_Id] [uniqueidentifier] NOT NULL,
	[StyleClass_Name] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.Header] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoggedEntry]    Script Date: 21-Jun-18 10:21:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoggedEntry](
	[Id] [uniqueidentifier] NOT NULL,
	[TypeOfEntry] [nvarchar](max) NOT NULL,
	[loggedUser] [nvarchar](max) NOT NULL,
	[dateOfRegistration] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.LoggedEntry] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Paragraph]    Script Date: 21-Jun-18 10:21:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paragraph](
	[Id] [uniqueidentifier] NOT NULL,
	[Position] [int] NOT NULL,
	[Content_Id] [uniqueidentifier] NULL,
	[DocumentThatBelongs_Id] [uniqueidentifier] NOT NULL,
	[StyleClass_Name] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.Paragraph] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Session]    Script Date: 21-Jun-18 10:21:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[Token] [uniqueidentifier] NOT NULL,
	[User_Email] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.Session] PRIMARY KEY CLUSTERED 
(
	[Token] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Style]    Script Date: 21-Jun-18 10:21:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Style](
	[Id] [uniqueidentifier] NOT NULL,
	[Key] [nvarchar](max) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[Format_Name] [nvarchar](128) NOT NULL,
	[StyleClass_Name] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.Style] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StyleClass]    Script Date: 21-Jun-18 10:21:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StyleClass](
	[Name] [nvarchar](128) NOT NULL,
	[BasedOn_Name] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.StyleClass] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Text]    Script Date: 21-Jun-18 10:21:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Text](
	[Id] [uniqueidentifier] NOT NULL,
	[TextContent] [nvarchar](max) NOT NULL,
	[Position] [int] NOT NULL,
	[ContentThatBelongs_Id] [uniqueidentifier] NOT NULL,
	[StyleClass_Name] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.Text] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 21-Jun-18 10:21:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Email] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Administrator] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Commenter_Email]    Script Date: 21-Jun-18 10:21:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_Commenter_Email] ON [dbo].[Comment]
(
	[Commenter_Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Document_Id]    Script Date: 21-Jun-18 10:21:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_Document_Id] ON [dbo].[Comment]
(
	[Document_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Creator_Email]    Script Date: 21-Jun-18 10:21:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_Creator_Email] ON [dbo].[Document]
(
	[Creator_Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_StyleClass_Name]    Script Date: 21-Jun-18 10:21:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_StyleClass_Name] ON [dbo].[Document]
(
	[StyleClass_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Document_Id]    Script Date: 21-Jun-18 10:21:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_Document_Id] ON [dbo].[DocumentModificationLog]
(
	[Document_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Content_Id]    Script Date: 21-Jun-18 10:21:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_Content_Id] ON [dbo].[Footer]
(
	[Content_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DocumentThatBelongs_Id]    Script Date: 21-Jun-18 10:21:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_DocumentThatBelongs_Id] ON [dbo].[Footer]
(
	[DocumentThatBelongs_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_StyleClass_Name]    Script Date: 21-Jun-18 10:21:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_StyleClass_Name] ON [dbo].[Footer]
(
	[StyleClass_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Receiver_Email]    Script Date: 21-Jun-18 10:21:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_Receiver_Email] ON [dbo].[FriendRequest]
(
	[Receiver_Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Sender_Email]    Script Date: 21-Jun-18 10:21:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_Sender_Email] ON [dbo].[FriendRequest]
(
	[Sender_Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Content_Id]    Script Date: 21-Jun-18 10:21:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_Content_Id] ON [dbo].[Header]
(
	[Content_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DocumentThatBelongs_Id]    Script Date: 21-Jun-18 10:21:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_DocumentThatBelongs_Id] ON [dbo].[Header]
(
	[DocumentThatBelongs_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_StyleClass_Name]    Script Date: 21-Jun-18 10:21:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_StyleClass_Name] ON [dbo].[Header]
(
	[StyleClass_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Content_Id]    Script Date: 21-Jun-18 10:21:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_Content_Id] ON [dbo].[Paragraph]
(
	[Content_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DocumentThatBelongs_Id]    Script Date: 21-Jun-18 10:21:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_DocumentThatBelongs_Id] ON [dbo].[Paragraph]
(
	[DocumentThatBelongs_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_StyleClass_Name]    Script Date: 21-Jun-18 10:21:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_StyleClass_Name] ON [dbo].[Paragraph]
(
	[StyleClass_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_User_Email]    Script Date: 21-Jun-18 10:21:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_User_Email] ON [dbo].[Session]
(
	[User_Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Format_Name]    Script Date: 21-Jun-18 10:21:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_Format_Name] ON [dbo].[Style]
(
	[Format_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_StyleClass_Name]    Script Date: 21-Jun-18 10:21:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_StyleClass_Name] ON [dbo].[Style]
(
	[StyleClass_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_BasedOn_Name]    Script Date: 21-Jun-18 10:21:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_BasedOn_Name] ON [dbo].[StyleClass]
(
	[BasedOn_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ContentThatBelongs_Id]    Script Date: 21-Jun-18 10:21:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_ContentThatBelongs_Id] ON [dbo].[Text]
(
	[ContentThatBelongs_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_StyleClass_Name]    Script Date: 21-Jun-18 10:21:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_StyleClass_Name] ON [dbo].[Text]
(
	[StyleClass_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Comment_dbo.Document_Document_Id] FOREIGN KEY([Document_Id])
REFERENCES [dbo].[Document] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_dbo.Comment_dbo.Document_Document_Id]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Comment_dbo.User_Commenter_Email] FOREIGN KEY([Commenter_Email])
REFERENCES [dbo].[User] ([Email])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_dbo.Comment_dbo.User_Commenter_Email]
GO
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Document_dbo.StyleClass_StyleClass_Name] FOREIGN KEY([StyleClass_Name])
REFERENCES [dbo].[StyleClass] ([Name])
GO
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_dbo.Document_dbo.StyleClass_StyleClass_Name]
GO
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Document_dbo.User_Creator_Email] FOREIGN KEY([Creator_Email])
REFERENCES [dbo].[User] ([Email])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_dbo.Document_dbo.User_Creator_Email]
GO
ALTER TABLE [dbo].[DocumentModificationLog]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DocumentModificationLog_dbo.Document_Document_Id] FOREIGN KEY([Document_Id])
REFERENCES [dbo].[Document] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DocumentModificationLog] CHECK CONSTRAINT [FK_dbo.DocumentModificationLog_dbo.Document_Document_Id]
GO
ALTER TABLE [dbo].[Footer]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Footer_dbo.Content_Content_Id] FOREIGN KEY([Content_Id])
REFERENCES [dbo].[Content] ([Id])
GO
ALTER TABLE [dbo].[Footer] CHECK CONSTRAINT [FK_dbo.Footer_dbo.Content_Content_Id]
GO
ALTER TABLE [dbo].[Footer]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Footer_dbo.Document_DocumentThatBelongs_Id] FOREIGN KEY([DocumentThatBelongs_Id])
REFERENCES [dbo].[Document] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Footer] CHECK CONSTRAINT [FK_dbo.Footer_dbo.Document_DocumentThatBelongs_Id]
GO
ALTER TABLE [dbo].[Footer]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Footer_dbo.StyleClass_StyleClass_Name] FOREIGN KEY([StyleClass_Name])
REFERENCES [dbo].[StyleClass] ([Name])
GO
ALTER TABLE [dbo].[Footer] CHECK CONSTRAINT [FK_dbo.Footer_dbo.StyleClass_StyleClass_Name]
GO
ALTER TABLE [dbo].[FriendRequest]  WITH CHECK ADD  CONSTRAINT [FK_dbo.FriendRequest_dbo.User_Receiver_Email] FOREIGN KEY([Receiver_Email])
REFERENCES [dbo].[User] ([Email])
GO
ALTER TABLE [dbo].[FriendRequest] CHECK CONSTRAINT [FK_dbo.FriendRequest_dbo.User_Receiver_Email]
GO
ALTER TABLE [dbo].[FriendRequest]  WITH CHECK ADD  CONSTRAINT [FK_dbo.FriendRequest_dbo.User_Sender_Email] FOREIGN KEY([Sender_Email])
REFERENCES [dbo].[User] ([Email])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FriendRequest] CHECK CONSTRAINT [FK_dbo.FriendRequest_dbo.User_Sender_Email]
GO
ALTER TABLE [dbo].[Header]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Header_dbo.Content_Content_Id] FOREIGN KEY([Content_Id])
REFERENCES [dbo].[Content] ([Id])
GO
ALTER TABLE [dbo].[Header] CHECK CONSTRAINT [FK_dbo.Header_dbo.Content_Content_Id]
GO
ALTER TABLE [dbo].[Header]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Header_dbo.Document_DocumentThatBelongs_Id] FOREIGN KEY([DocumentThatBelongs_Id])
REFERENCES [dbo].[Document] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Header] CHECK CONSTRAINT [FK_dbo.Header_dbo.Document_DocumentThatBelongs_Id]
GO
ALTER TABLE [dbo].[Header]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Header_dbo.StyleClass_StyleClass_Name] FOREIGN KEY([StyleClass_Name])
REFERENCES [dbo].[StyleClass] ([Name])
GO
ALTER TABLE [dbo].[Header] CHECK CONSTRAINT [FK_dbo.Header_dbo.StyleClass_StyleClass_Name]
GO
ALTER TABLE [dbo].[Paragraph]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Paragraph_dbo.Content_Content_Id] FOREIGN KEY([Content_Id])
REFERENCES [dbo].[Content] ([Id])
GO
ALTER TABLE [dbo].[Paragraph] CHECK CONSTRAINT [FK_dbo.Paragraph_dbo.Content_Content_Id]
GO
ALTER TABLE [dbo].[Paragraph]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Paragraph_dbo.Document_DocumentThatBelongs_Id] FOREIGN KEY([DocumentThatBelongs_Id])
REFERENCES [dbo].[Document] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Paragraph] CHECK CONSTRAINT [FK_dbo.Paragraph_dbo.Document_DocumentThatBelongs_Id]
GO
ALTER TABLE [dbo].[Paragraph]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Paragraph_dbo.StyleClass_StyleClass_Name] FOREIGN KEY([StyleClass_Name])
REFERENCES [dbo].[StyleClass] ([Name])
GO
ALTER TABLE [dbo].[Paragraph] CHECK CONSTRAINT [FK_dbo.Paragraph_dbo.StyleClass_StyleClass_Name]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Session_dbo.User_User_Email] FOREIGN KEY([User_Email])
REFERENCES [dbo].[User] ([Email])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_dbo.Session_dbo.User_User_Email]
GO
ALTER TABLE [dbo].[Style]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Style_dbo.Format_Format_Name] FOREIGN KEY([Format_Name])
REFERENCES [dbo].[Format] ([Name])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Style] CHECK CONSTRAINT [FK_dbo.Style_dbo.Format_Format_Name]
GO
ALTER TABLE [dbo].[Style]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Style_dbo.StyleClass_StyleClass_Name] FOREIGN KEY([StyleClass_Name])
REFERENCES [dbo].[StyleClass] ([Name])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Style] CHECK CONSTRAINT [FK_dbo.Style_dbo.StyleClass_StyleClass_Name]
GO
ALTER TABLE [dbo].[StyleClass]  WITH CHECK ADD  CONSTRAINT [FK_dbo.StyleClass_dbo.StyleClass_BasedOn_Name] FOREIGN KEY([BasedOn_Name])
REFERENCES [dbo].[StyleClass] ([Name])
GO
ALTER TABLE [dbo].[StyleClass] CHECK CONSTRAINT [FK_dbo.StyleClass_dbo.StyleClass_BasedOn_Name]
GO
ALTER TABLE [dbo].[Text]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Text_dbo.Content_ContentThatBelongs_Id] FOREIGN KEY([ContentThatBelongs_Id])
REFERENCES [dbo].[Content] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Text] CHECK CONSTRAINT [FK_dbo.Text_dbo.Content_ContentThatBelongs_Id]
GO
ALTER TABLE [dbo].[Text]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Text_dbo.StyleClass_StyleClass_Name] FOREIGN KEY([StyleClass_Name])
REFERENCES [dbo].[StyleClass] ([Name])
GO
ALTER TABLE [dbo].[Text] CHECK CONSTRAINT [FK_dbo.Text_dbo.StyleClass_StyleClass_Name]
GO
USE [master]
GO
ALTER DATABASE [169236-199394] SET  READ_WRITE 
GO
