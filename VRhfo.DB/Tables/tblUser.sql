CREATE TABLE [dbo].[tblUser]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Username] NVARCHAR(120) NOT NULL, 
    [Email] NVARCHAR(50) NOT NULL, 
    [Auth0UserId] NCHAR(10) NOT NULL, 
    [RegistrationDate] DATE NOT NULL, 
    [IsSubscribed] TINYINT NOT NULL, 
    [SubscribedDate] DATETIME NOT NULL, 
    [Password] VARCHAR(50) NOT NULL, 
    [SubscriptionTier] VARCHAR(50) NOT NULL, 
    [NextRenewalDueDate] DATE NOT NULL, 
)
