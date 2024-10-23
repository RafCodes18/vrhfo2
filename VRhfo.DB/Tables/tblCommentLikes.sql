CREATE TABLE [dbo].[tblCommentLikes]
(
	Id INT PRIMARY KEY IDENTITY(1,1),
    UserId UNIQUEIDENTIFIER NOT NULL, -- GUID of the user
    CommentId INT NOT NULL,
    IsLike BIT NOT NULL, -- True for like, false for dislike
    CreatedAt DATETIME DEFAULT GETDATE()
)
