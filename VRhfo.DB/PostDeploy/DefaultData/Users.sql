BEGIN 
	INSERT INTO tblUser (Id, Username, Password, Email, Auth0UserId, RegistrationDate, IsSubscribed, SubscribedDate, SubscriptionTier, NextRenewalDueDate)
	VALUES 
		('8a844bae-880c-437d-a571-4b94b66c1fe3', 'JmoneySH', '1234', 'user1@email.com', 'auth01', '2023-01-01', 1, '2023-01-01 00:00:00', 'Monthly', '2023-02-01'),
		('cd8093d3-6537-44fe-8e1c-dc80af075ada', 'HopelessGooner6969', '123', 'user2@email.com', 'auth02', '2023-02-01', 0, '2023-02-01 00:00:00', 'Yearly', '2023-03-01'),
		('8085cb51-2b50-4a5c-9651-6f48461b0091', 'jack3d', '123', 'user3@email.com', 'auth03', '2023-03-01', 1, '2023-03-01 00:00:00', 'Lifetime', '2023-01-01')
END
