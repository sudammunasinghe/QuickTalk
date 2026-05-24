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
WHERE (([SenderId] = @SenderId AND [ReceiverId] = @ReceiverId) OR
	([SenderId] = @ReceiverId AND [ReceiverId] = @SenderId)) AND
	[IsActive] = 1;