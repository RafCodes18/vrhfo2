BEGIN
    INSERT INTO tblComment(Content, DatePosted, UserId, VideoId)
    VALUES
        ('This is comment 1 which belongs to usefr 1', '2023-09-03', 1, 3),
        ('This is comment 2 which belongs to usefr 2', '2023-09-03', 2, 2),
        ('This is a comment 3 which belongs to user 3', '2023-09-03', 3, 1),
        ('This is a comment 4 which belongs to usefr 3', '2023-09-03', 3, 1),
        ('This is a comment 5 which belongs to user 1', '2023-09-03', 1, 1),
        ('No wfay', '2023-09-03', 3, 2),
        ('Amazinfggg', '2023-09-03', 3, 1),
        ('omg', '2023-09-03', 1, 8),
        ('so fucking hoft', '2023-09-03', 1, 1),
        ('yefsss', '2023-09-03', 2, 1),
        ('wow', '2023-09-03', 3, 1),
        ('awfesome', '2023-09-03', 3, 1),
        ('Lmafo', '2023-09-03', 1, 1);
END