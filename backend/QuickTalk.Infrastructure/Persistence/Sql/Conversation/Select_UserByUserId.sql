SELECT 
	[Id],
    [FirstName],
    [LastName],
    [Email],
    [PasswordHash],
    [DateOfBirth],
    [IsOnline],
    [Otp],
    [OtpExpiry],
    [IsUsed],
    [IsActive],
    [CreatedDateTime],
    [LastModifiedDateTime]
FROM [dbo].[Users]
WHERE [Id] = @UserId AND
	[IsActive] = 1;