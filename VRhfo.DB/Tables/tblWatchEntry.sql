CREATE TABLE [dbo].[tblWatchEntry]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [VideoId] INT NOT NULL, 
    [LastDateWatched] DATETIME NOT NULL, 
    [FirstViewed] DATETIME NOT NULL, 
    [WatchDurationTicks] BIGINT NOT NULL, 
    [TimesViewed] INT NOT NULL, 
    [Completed] BIT NOT NULL
)
