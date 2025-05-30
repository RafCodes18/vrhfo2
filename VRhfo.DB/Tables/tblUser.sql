CREATE TABLE [dbo].[tblUser]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Username] NVARCHAR(120) NOT NULL, 
    [Email] NVARCHAR(50) NOT NULL, 
    [Auth0UserId] NCHAR(10) NOT NULL, 
    [FirstVisit] DATETIME NOT NULL, 
    [IsSubscribed] BIT NOT NULL, 
    [SubscribedDate] DATETIME NULL, 
    [PasswordHash] VARCHAR(50) NOT NULL, 
    [SubscriptionTier] VARCHAR(50) NOT NULL, 
    [NextRenewalDueDate] DATE NULL, 
    [GoonScore] INT NULL, 

    NormalizedEmail NVARCHAR(256) NULL, -- Identity
    NormalizedUsername NVARCHAR(256) NULL, -- Identity
    ConcurrencyStamp NVARCHAR(128) NULL, -- Identity
    LockoutEnabled BIT NULL DEFAULT 0, -- Identity, set to 0 to disable
    LockoutEnd DATETIME NULL, -- Identity
    AccessFailedCount INT NULL DEFAULT 0, -- Identity
    SecurityStamp NVARCHAR(128) NULL -- Identity
, 
    [PasswordResetToken] NVARCHAR(100) NULL, 
    [PasswordResetTokenExpiration] DATETIME NULL)
