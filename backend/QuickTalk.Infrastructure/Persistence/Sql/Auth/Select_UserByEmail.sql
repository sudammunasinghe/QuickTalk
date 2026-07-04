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
    [LastSeen],
    [IsActive],
    [CreatedDateTime],
    [LastModifiedDateTime]
FROM [dbo].[Users]
WHERE [Email] = @Email AND
	[IsActive] = 1;