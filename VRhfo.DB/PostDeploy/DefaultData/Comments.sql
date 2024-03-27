BEGIN 
	INSERT INTO tblComment( Content, DatePosted, UserId, VideoId)
	VALUES('This is comment 1 which belongs to user 1', '2023-09-03', 1, 3),
			('This is comment 2 which belongs to user 2', '2023-09-03', 2, 2),
			('This is a comment 3 which belongs to user 3', '2023-09-03', 3, 1),
			('This is a comment 4 which belongs to user 3', '2023-09-03', 3, 4),
			('This is a comment 5 which belongs to user 1', '2023-09-03', 1, 2),
			('No way', '2023-09-03', 3, 2),
			('Amazinggg', '2023-09-03', 3, 1),
			('omg', '2023-09-03', 1, 8),
			('so fucking hot', '2023-09-03', 1, 10),
			('yesss', '2023-09-03', 2, 9),
			('wow', '2023-09-03', 3, 9),
			('awesome', '2023-09-03', 3, 1),
			('Lmao', '2023-09-03', 1, 1)
END