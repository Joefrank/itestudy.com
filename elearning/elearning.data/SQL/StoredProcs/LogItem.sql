 
    CREATE PROC LogItem
	    @message NVARCHAR(MAX),
	    @sessionid NVARCHAR(100) = NULL,
	    @createdate DATETIME
    AS
    BEGIN

        INSERT INTO Logs([Message], SessionId, CreatedOn)
        VALUES(@message, @sessionid, @createdate)

    END
        