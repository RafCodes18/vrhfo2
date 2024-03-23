CREATE TABLE [dbo].[tblComment]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Content] VARCHAR(100) NOT NULL, 
    [DatePosted] NCHAR(10) NULL, 
    [UserId] INT NOT NULL, 
    [VideoId] INT NOT NULL
)
