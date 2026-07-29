SELECT 
	US.[Id] [UserId],
    US.[FirstName],
    US.[LastName],
	'' [LastMessage],
	'' [LastMessageDisplayTime],
	PS.[ShowProfilePicture],
	PS.[ShowOnlineStatus],
	PS.[ShowLastSeen],
	PS.[ShowBio],
	US.[ProfileImageUrl],
	US.[IsOnline],
	US.[Bio],
	US.[CreatedDateTime] [RegisteredDateTime]
FROM [dbo].[Users] US
	INNER JOIN [dbo].[UserPrivacySettings] PS ON US.[Id] = PS.[UserId]
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
