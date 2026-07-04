UPDATE [dbo].[UserPrivacySettings]
SET 
    [ShowProfilePicture] = @ShowProfilePicture,
    [ShowOnlineStatus] = @ShowOnlineStatus,
    [ShowLastSeen] = @ShowLastSeen,
    [ShowBio] = @ShowBio,
    [LastModifiedDateTime] = GETDATE()
WHERE [Id] = @Id;
