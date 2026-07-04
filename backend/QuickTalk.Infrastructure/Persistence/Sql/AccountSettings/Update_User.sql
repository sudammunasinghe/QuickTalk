UPDATE [dbo].[Users]
   SET 
      [PasswordHash] = @PasswordHash,
      [LastModifiedDateTime] = @LastModifiedDateTime
WHERE [Id] = @UserId;