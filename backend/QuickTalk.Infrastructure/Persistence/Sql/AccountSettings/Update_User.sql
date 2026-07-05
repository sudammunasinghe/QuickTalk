UPDATE [dbo].[Users]
   SET 
      [PasswordHash] = @PasswordHash,
      [FirstName] = @FirstName,
      [LastName] = @LastName,
      [Bio] = @Bio,
      [DateOfBirth] = @DateOfBirth,
      [ProfileImageUrl] = @profileImageUrl,
      [LastModifiedDateTime] = @LastModifiedDateTime
WHERE [Id] = @UserId;