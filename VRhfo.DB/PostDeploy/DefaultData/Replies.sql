BEGIN 
	INSERT INTO tblReply(Id, Content, DatePosted, UserId, CommentId, LikesCount, DislikesCount)
	VALUES 
		('4ad5410a-7aa2-416f-8416-86fbcb0c5421', 'I totally agree with this point!', '2024-03-12 21:52:24', '8a844bae-880c-437d-a571-4b94b66c1fe3', 3, 15, 2),
		('d5ca29f7-fab7-4f53-b4d2-37a787880158','Interesting take, but I have some doubts.', '2024-08-02 20:15:48', '8a844bae-880c-437d-a571-4b94b66c1fe3', 1, 10, 5),
		('2e3d44c1-3dd4-4119-9cb4-5e1c3721db1e','Can you provide more context here?', '2024-09-18 18:35:12', '8a844bae-880c-437d-a571-4b94b66c1fe3', 2, 8, 1),
		('09c6ba68-9dfe-4da4-966d-48e091230a16','Thanks for sharing this info!', '2024-04-13 10:14:05', '8a844bae-880c-437d-a571-4b94b66c1fe3', 4, 22, 4),
		('cbe87884-c69d-4a7b-aa5e-6d2e5b5b82ce','Not sure I follow your logic.', '2024-01-08 17:50:33', 'cd8093d3-6537-44fe-8e1c-dc80af075ada', 5, 5, 9),
		('acf8e287-5c7c-4043-892b-06dd8eb118ef','This is hilarious!', '2024-07-12 22:03:59', 'cd8093d3-6537-44fe-8e1c-dc80af075ada', 10, 30, 2),
		('152af4d4-ae07-41b6-9014-4026012e58b2','I disagree, here\''s why...', '2024-07-12 12:47:11', 'cd8093d3-6537-44fe-8e1c-dc80af075ada', 10, 3, 12),
		('de2d914f-c8cf-4874-8e64-f59e8689391c','Great perspective! I hadn\''t thought of it like that.', '2024-08-22 15:23:47', 'cd8093d3-6537-44fe-8e1c-dc80af075ada', 9, 18, 3)
END