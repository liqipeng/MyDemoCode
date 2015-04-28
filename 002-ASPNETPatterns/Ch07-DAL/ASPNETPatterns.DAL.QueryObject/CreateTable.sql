USE [QueryObject]
GO

/****** Object:  Table [dbo].[Order]    Script Date: 2015/4/28 23:44:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Order](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderDate] [date] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
DECLARE @CustId varchar(max) = 'bc03d937-e325-4c3a-956a-dfd49911ac6c';
INSERT INTO [Order](Id, OrderDate, CustomerId) VALUES(NEWID(), GETDATE(), @CustId)
INSERT INTO [Order](Id, OrderDate, CustomerId) VALUES(NEWID(), GETDATE(), @CustId)
INSERT INTO [Order](Id, OrderDate, CustomerId) VALUES(NEWID(), GETDATE(), @CustId)
INSERT INTO [Order](Id, OrderDate, CustomerId) VALUES(NEWID(), GETDATE(), @CustId)
INSERT INTO [Order](Id, OrderDate, CustomerId) VALUES(NEWID(), GETDATE(), @CustId)

