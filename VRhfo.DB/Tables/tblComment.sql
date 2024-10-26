CREATE TABLE [dbo].[tblComment]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Content] VARCHAR(100) NOT NULL, 
    [DatePosted] DATETIME NOT NULL, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [VideoId] INT NOT NULL, 
    [LikesCount] INT NOT NULL, 
    [DislikesCount] INT NOT NULL
)
