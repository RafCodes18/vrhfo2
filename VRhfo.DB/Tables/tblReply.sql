CREATE TABLE [dbo].[tblReply]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Content] VARCHAR(200) NOT NULL, 
    [DatePosted] DATETIME NOT NULL, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [CommentId] INT NOT NULL, 
    [LikesCount] INT NOT NULL, 
    [DislikesCount] INT NOT NULL
)
