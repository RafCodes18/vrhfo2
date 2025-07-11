﻿/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
DROP TABLE IF EXISTS tblVideosLiked
DROP TABLE IF EXISTS tblUser
DROP TABLE IF EXISTS tblVideo
DROP TABLE IF EXISTS tblComment
DROP TABLE IF EXISTS tblWatchEntry
DROP TABLE IF EXISTS tblReply
DROP TABLE IF EXISTS tblCommentLikes
DROP TABLE IF EXISTS tblVideoViews
