BEGIN 
	INSERT INTO tblUser (Username, Email, Auth0UserId, RegistrationDate, IsSubscribed, SubscribedDate)
	VALUES 
		('User1', 'user1@email.com', 'auth01', '2023-01-01', 1, '2023-01-01 00:00:00'),
		('User2', 'user2@email.com', 'auth02', '2023-02-01', 0, '2023-02-01 00:00:00'),
		('User3', 'user3@email.com', 'auth03', '2023-03-01', 1, '2023-03-01 00:00:00')
END
