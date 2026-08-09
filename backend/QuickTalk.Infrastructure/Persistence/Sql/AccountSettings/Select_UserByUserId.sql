SELECT 
	[Id],
    [FirstName],
    [LastName],
    [Email],
    [PasswordHash],
    [DateOfBirth],
    [Bio],
    [ProfileImageUrl],
    [IsOnline],
    [Otp],
    [OtpExpiry],
    [IsUsed],
    [LastSeen],
    [IsActive],
    [CreatedDateTime],
    [LastModifiedDateTime]
FROM [dbo].[Users]
WHERE [Id] = @UserId AND
	[IsActive] = 1;