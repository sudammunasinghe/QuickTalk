SELECT 
	[Id],
    [FirstName],
    [LastName],
    [Email],
    [PasswordHash],
    [DateOfBirth],
    [IsOnline],
    [PasswordResetTokenId],
    [PasswordResetTokenHash],
    [PasswordResetTokenExpiry],
    [IsActive],
    [CreatedDateTime],
    [LastModifiedDateTime]
FROM [dbo].[Users]
WHERE [Id] = @UserId AND
	[IsActive] = 1;