BEGIN 
	INSERT INTO dbo.tblVideo (Title, Studio, ThumbnailUrl, VideoUrl, Description, Genre, UploadDate, Duration, Views, RatingCount, IsPreview, IsPublic, ContentWarning, Likes, Dislikes )
VALUES ('Title', 'Studio', 'lana.jpg', 'VideoUrl', 'desc', 'genre', '2021-11-01', '01:00:00', 1000, 500, 0, 1, 'No content warning', 400, 100),
       ('Title2', 'Studio2', 'vrpic.jpg', 'VideoUrl2', 'desc2', 'genre2', '2021-12-01', '02:00:00', 2000, 1000, 0, 1, 'No content warning', 800, 200),
       ('Title2', 'Studio2', 'azz.jpg', 'VideoUrl2', 'desc2', 'genre2', '2021-12-01', '02:00:00', 2000, 1000, 0, 1, 'No content warning', 800, 200),
       ('Title2', 'Studio2', 'reily.jpg', 'VideoUrl2', 'desc2', 'genre2', '2021-12-01', '02:00:00', 2000, 1000, 0, 1, 'No content warning', 800, 200),
       ('Title2', 'Studio2', 'ang.jpg', 'VideoUrl2', 'desc2', 'genre2', '2021-12-01', '02:00:00', 2000, 1000, 0, 1, 'No content warning', 800, 200),
       ('Title2', 'Studio2', 'squat.jpg', 'VideoUrl2', 'desc2', 'genre2', '2021-12-01', '02:00:00', 2000, 1000, 0, 1, 'No content warning', 800, 200),
       ('Title2', 'Studio2', 'puss.jpg', 'VideoUrl2', 'desc2', 'genre2', '2021-12-01', '02:00:00', 2000, 1000, 0, 1, 'No content warning', 800, 200),
       ('Title2', 'Studio2', 'squat.jpg', 'VideoUrl2', 'desc2', 'genre2', '2021-12-01', '02:00:00', 2000, 1000, 0, 1, 'No content warning', 800, 200),
       ('Title2', 'Studio2', 'puss.jpg', 'VideoUrl2', 'desc2', 'genre2', '2021-12-01', '02:00:00', 2000, 1000, 0, 1, 'No content warning', 800, 200),
       ('Title3', 'Studio3', 'nicole.jpg', 'VideoUrl3', 'desc3', 'genre3', '2022-01-01', '03:00:00', 3000, 1500, 0, 1, 'No content warning', 1200, 300);

END 