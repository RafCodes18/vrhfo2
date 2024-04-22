BEGIN 
	INSERT INTO tblUser (Username, Password, Email, Auth0UserId, RegistrationDate, IsSubscribed, SubscribedDate)
	VALUES 
		('JmoneySH', '1234', 'user1@email.com', 'auth01', '2023-01-01', 1, '2023-01-01 00:00:00'),
		('HopelessGooner6969', '123', 'user2@email.com', 'auth02', '2023-02-01', 0, '2023-02-01 00:00:00'),
		('jack3d', '123', 'user3@email.com', 'auth03', '2023-03-01', 1, '2023-03-01 00:00:00')
END
