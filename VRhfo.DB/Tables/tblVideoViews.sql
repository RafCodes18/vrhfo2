CREATE TABLE [dbo].[tblVideoViews]
(
    Id INT PRIMARY KEY IDENTITY(1,1), 
    [VideoId] INT NOT NULL, 
    [UserId] UNIQUEIDENTIFIER NULL, 
    [IPAdress] NVARCHAR(50) NULL, 
    [ViewTime] DATETIME NOT NULL
)
