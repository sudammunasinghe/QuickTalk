SELECT 
	[Id],
    [FirstName],
    [LastName],
    [Email],
    [PasswordHash],
    [DateOfBirth],
    [IsOnline],
    [Otp],
    [ProfileImageUrl],
    [OtpExpiry],
    [IsActive],
    [CreatedDateTime],
    [LastModifiedDateTime],
    [IsUsed],
    [LastSeen]
FROM [dbo].[Users] US
WHERE US.[Id] <> @UserId 
AND US.[Id] NOT IN(
	SELECT	
		[SenderId]
	FROM [dbo].[Messages]
	WHERE [SenderId] = @UserId

	UNION

	SELECT	
		[ReceiverId]
	FROM [dbo].[Messages]
	WHERE [SenderId] = @UserId
)
ORDER BY [FirstName],[LastName];
