USE [Skjatexti]
GO

/****** Object:  Table [dbo].[Request]    Script Date: 8.5.2014 09:05:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Request](
	[reqId] [int] IDENTITY(1,1) NOT NULL,
	[requestText] [nvarchar](100) NOT NULL,
	[reqDate] [datetime] NULL,
	[reqLike] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[reqId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

