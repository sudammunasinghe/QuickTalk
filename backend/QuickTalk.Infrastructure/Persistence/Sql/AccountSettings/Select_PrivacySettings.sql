SELECT 
	[Id],
    [UserId],
    [ShowProfilePicture],
    [ShowOnlineStatus],
    [ShowLastSeen],
    [ShowBio],
    [IsActive],
    [CreatedDateTime],
    [LastModifiedDateTime]
FROM [dbo].[UserPrivacySettings]
WHERE [UserId] = @UserId AND
	[IsActive] = 1;