

CREATE TABLE [dbo].[Logs](
	[LogId]				[bigint] IDENTITY(1,1) NOT NULL,
	[Date]				[datetime]		NOT NULL,
	[Level]				[tinyint]		NOT NULL,
	[Context]			[nvarchar](max)		NULL,
	[Class]				[nvarchar](255)		NULL,
	[Method]			[nvarchar](255)		NULL,
	[FilePath]			[nvarchar](max)		NULL,
	[LineNumber]		[int]				NULL,
	[Message]			[nvarchar](max)	NOT NULL,
	[Exception]			[nvarchar](max)		NULL,
	[StackSources]		[nvarchar](max)		NULL,
	[Namespace]			[nvarchar](max)		NULL,
	[Assembly]			[nvarchar](max)		NULL,
	[MachineName]		[nvarchar](255)		NULL,
	[MachineIp]			[nvarchar](64)		NULL,
	[ProcessName]		[nvarchar](max)		NULL,
	[ProcessId] 		[int]				NULL,

	CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED ( [LogId] ASC )
)

GO


