USE [Submitter]
GO
/****** Object:  Table [dbo].[tbl_User]    Script Date: 10/02/2011 16:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Rating] [int] NOT NULL,
	[Password] [varchar](64) NOT NULL,
	[RegisterDate] [datetime] NOT NULL,
 CONSTRAINT [PK_tbl_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Submission]    Script Date: 10/02/2011 16:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Submission](
	[SubmissionID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](150) NOT NULL,
	[Link] [varchar](400) NOT NULL,
	[Rating] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[PostTime] [datetime] NOT NULL,
 CONSTRAINT [PK_tbl_Submission] PRIMARY KEY CLUSTERED 
(
	[SubmissionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_User_Rating_Submission_xref]    Script Date: 10/23/2011 06:01:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_User_Rating_Submission_xref](
	[UserID] [int] NOT NULL,
	[SubmissionID] [int] NOT NULL,
	[VoteType] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Comment]    Script Date: 10/02/2011 16:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Comment](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[CommentContents] [varchar](8000) NOT NULL,
	[PostDate] [datetime] NOT NULL,
	[Rating] [int] NOT NULL,
	[SubmissionID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[ParentCommentID] [int] NOT NULL,
 CONSTRAINT [PK_tbl_Comment] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table[dbo].[tbl_User_Rating_Comment_xref] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_User_Rating_Comment_xref](
	[UserID] [int] NOT NULL,
	[CommentID] [int] NOT NULL,
	[VoteType] [int] NOT NULL,
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  ForeignKey [FK_tbl_Submission_tbl_User]    Script Date: 10/23/2011 06:01:41 ******/
ALTER TABLE [dbo].[tbl_Submission]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Submission_tbl_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[tbl_User] ([UserID])
GO
ALTER TABLE [dbo].[tbl_Submission] CHECK CONSTRAINT [FK_tbl_Submission_tbl_User]
GO
/****** Object:  ForeignKey [FK_tbl_User_Rating_Submission_xref_tbl_Submission]    Script Date: 10/23/2011 06:01:41 ******/
ALTER TABLE [dbo].[tbl_User_Rating_Submission_xref]  WITH CHECK ADD  CONSTRAINT [FK_tbl_User_Rating_Submission_xref_tbl_Submission] FOREIGN KEY([SubmissionID])
REFERENCES [dbo].[tbl_Submission] ([SubmissionID])
GO
ALTER TABLE [dbo].[tbl_User_Rating_Submission_xref] CHECK CONSTRAINT [FK_tbl_User_Rating_Submission_xref_tbl_Submission]
GO
/****** Object:  ForeignKey [FK_tbl_User_Rating_Submission_xref_tbl_User]    Script Date: 10/23/2011 06:01:41 ******/
ALTER TABLE [dbo].[tbl_User_Rating_Submission_xref]  WITH CHECK ADD  CONSTRAINT [FK_tbl_User_Rating_Submission_xref_tbl_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[tbl_User] ([UserID])
GO
ALTER TABLE [dbo].[tbl_User_Rating_Submission_xref] CHECK CONSTRAINT [FK_tbl_User_Rating_Submission_xref_tbl_User]
GO
/****** Object:  ForeignKey [FK_tbl_Comment_tbl_Submission]    Script Date: 10/23/2011 06:01:41 ******/
ALTER TABLE [dbo].[tbl_Comment]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Comment_tbl_Submission] FOREIGN KEY([SubmissionID])
REFERENCES [dbo].[tbl_Submission] ([SubmissionID])
GO
ALTER TABLE [dbo].[tbl_Comment] CHECK CONSTRAINT [FK_tbl_Comment_tbl_Submission]
GO
/****** Object:  ForeignKey [FK_tbl_Comment_tbl_User]    Script Date: 10/23/2011 06:01:41 ******/
ALTER TABLE [dbo].[tbl_Comment]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Comment_tbl_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[tbl_User] ([UserID])
GO
ALTER TABLE [dbo].[tbl_Comment] CHECK CONSTRAINT [FK_tbl_Comment_tbl_User]
GO