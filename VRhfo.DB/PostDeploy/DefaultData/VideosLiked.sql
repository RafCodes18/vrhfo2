BEGIN 
	INSERT INTO tblVideosLiked(UserID, VideoID, LikedDate)
	VALUES (1, 2, '2023-01-01 00:00:00'),
		   (2, 1, '2023-01-01 00:00:00'),
		   (3, 3, '2023-01-01 00:00:00'),
		   (1, 3, '2023-01-01 00:00:00'),
		   (3, 1, '2023-01-01 00:00:00')
			
END 