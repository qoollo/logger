USE [TestLog]
GO

/****** Object:  Table [dbo].[Log]    Script Date: 02.10.2012 11:22:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Logs](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[Time] [datetime] NOT NULL,
	[Level] [tinyint] NOT NULL,
	[Modul] [varchar](50) NOT NULL,
	[Namespace] [varchar](max) NULL,
	[Class] [varchar](50) NULL,
	[Method] [varchar](50) NULL,
	[Message] [varchar](max) NOT NULL,
	[Exception] [varchar](max) NULL,
	[Stacksources] [varchar](max) NULL,
	[Context] [xml] NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

