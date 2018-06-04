/*
** rename new database to itestudy2
*  copy from existing database

*/

DECLARE @LastId int, @TemplateType int

SELECT TOP 1 @LastId = Id, @TemplateType = [Type] FROM [itestudy].[dbo].[EmailTemplates] ORDER BY Id 

WHILE EXISTS(SELECT 1 FROM [itestudy].[dbo].[EmailTemplates] WHERE Id > @LastId)
BEGIN	

	IF NOT EXISTS(SELECT 1 FROM [itestudy].[dbo].[EmailTemplates] WHERE [Type] = @TemplateType)
	BEGIN

		INSERT INTO [itestudy2].[dbo].[EmailTemplates](	
			[Type],
			[Name] ,
			[Subject],
			[BodyText] ,
			[BodyHtml] ,
			[CreatedOn] ,
			[CreatedBy] ,
			[LastModified]
		) 
		SELECT 
			[Type],
			[Name] ,
			[Subject],
			[BodyText] ,
			[BodyHtml] ,
			[CreatedOn] ,
			[CreatedBy] ,
			[LastModified]
		FROM [itestudy].[dbo].[EmailTemplates]
		WHERE Id = @LastId

	END

	SELECT TOP 1 @LastId = Id FROM [itestudy].[dbo].[EmailTemplates] WHERE Id > @LastId

END