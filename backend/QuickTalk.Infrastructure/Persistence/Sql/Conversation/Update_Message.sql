UPDATE [dbo].[Messages]
   SET
    [IsRead] = @IsRead, 
    [LastModifiedDateTime] = @LastModifiedDateTime
 WHERE [Id] = @Id;