BEGIN

INSERT INTO tblVideoViews (  UserId, VideoId, IPAdress, ViewTime)
VALUES 
( '8a844bae-880c-437d-a571-4b94b66c1fe3', 1, '192.168.1.10', GETDATE()),
( '8a844bae-880c-437d-a571-4b94b66c1fe3', 1, '192.168.1.11', GETDATE()),
( '8a844bae-880c-437d-a571-4b94b66c1fe3', 1, '192.168.1.12', GETDATE()),
( '8a844bae-880c-437d-a571-4b94b66c1fe3', 1, '192.168.1.13', GETDATE()),
( '8a844bae-880c-437d-a571-4b94b66c1fe3', 1, '192.168.1.14', GETDATE()),
( '8a844bae-880c-437d-a571-4b94b66c1fe3', 1, '192.168.1.15', GETDATE());

END
