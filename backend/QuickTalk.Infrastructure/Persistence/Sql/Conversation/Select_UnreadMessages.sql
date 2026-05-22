SELECT 
	[Id],
    [SenderId],
    [ReceiverId],
    [MessageText],
    [IsRead],
    [IsActive],
    [CreatedDateTime],
    [LastModifiedDateTime]
FROM [dbo].[Messages]
WHERE [ReceiverId] = @LoggedUserId AND
	[SenderId] = @SenderId AND [IsRead] = 0 AND
	[IsActive] = 1;