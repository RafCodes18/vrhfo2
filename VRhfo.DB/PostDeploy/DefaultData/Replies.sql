BEGIN 
	INSERT INTO tblReply(Content, DatePosted, UserId, CommentId, LikesCount, DislikesCount)
	VALUES 
		('Blah blah blah nigga', '3/12/2024 9:52:24 PM', '8a844bae-880c-437d-a571-4b94b66c1fe3', 3, 12, 8),
		('Blah blah blah nigga', '8/2/2024 9:52:24 PM', '8a844bae-880c-437d-a571-4b94b66c1fe3', 1, 12, 8),
		('Blah blah blah nigga', '9/18/2024 9:52:24 PM', '8a844bae-880c-437d-a571-4b94b66c1fe3', 2, 12, 8),
		('Blah blah blah nigga', '4/13/2024 9:52:24 PM', '8a844bae-880c-437d-a571-4b94b66c1fe3', 4, 12, 8),
		('Blah blah blah nigga', '1/8/2024 9:52:24 PM', 'cd8093d3-6537-44fe-8e1c-dc80af075ada', 5, 12, 8),
		('Blah blah blah nigga', '7/12/2024 9:52:24 PM', 'cd8093d3-6537-44fe-8e1c-dc80af075ada', 10, 12, 8),
		('Blah blah blah nigga', '7/12/2024 9:52:24 PM', 'cd8093d3-6537-44fe-8e1c-dc80af075ada', 10, 1, 3),
		('Blah blah blah nigga', '8/22/2024 9:52:24 PM', 'cd8093d3-6537-44fe-8e1c-dc80af075ada', 9, 12, 8)
END
