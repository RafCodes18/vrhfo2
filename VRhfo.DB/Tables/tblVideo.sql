CREATE TABLE [dbo].[tblVideo]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Title] NVARCHAR(255) NOT NULL, 
    [Category] NVARCHAR(255) NOT NULL, 
    [ThumbnailUrl] NVARCHAR(MAX) NOT NULL, 
    [VideoUrl] NVARCHAR(MAX) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [Genre] NVARCHAR(50) NOT NULL, 
    [UploadDate] DATETIME NOT NULL, 
    [Duration] TIME NOT NULL, 
    [Views] INT NOT NULL, 
    [RatingCount] INT NOT NULL, 
    [IsPublic] TINYINT NOT NULL, 
    [IsPreview] TINYINT NOT NULL, 
    [ContentWarning] NVARCHAR(255) NOT NULL, 
    [Likes] INT NOT NULL, 
    [Dislikes] INT NOT NULL, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [PreviewVideoURL] VARCHAR(MAX) NOT NULL
)
