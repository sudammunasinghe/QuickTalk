WITH ConversationMessages AS
(
	SELECT
		*,
		CASE 
			WHEN [SenderId] = @LoggedUserId THEN [ReceiverId]
			ELSE [SenderId]
		END AS [ConversationUserId]
	FROM [dbo].[Messages]
	WHERE [SenderId] = @LoggedUserId OR 
		[ReceiverId] = @LoggedUserId
),

LastMessages AS
(
	SELECT
		cm.[ConversationUserId],
		cm.[MessageText],
		cm.[CreatedDateTime],
		ROW_NUMBER() OVER
		(
			PARTITION BY cm.[ConversationUserId]
			ORDER BY cm.[CreatedDateTime] DESC
		)AS rn
	FROM ConversationMessages cm
)

SELECT 
	US.[Id] [UserId],
	US.[FirstName],
	US.[LastName],
	LM.[MessageText] [LastMessage],
	CASE	
		WHEN CAST(LM.[CreatedDateTime] AS DATE) = CAST(GETDATE() AS DATE)
			THEN FORMAT(LM.[CreatedDateTime],'hh:mm tt')
		ELSE FORMAT(LM.[CreatedDateTime], 'dd/MM/yyyy')
	END AS [LastMessageDisplayTime],
	PS.[ShowProfilePicture],
	PS.[ShowOnlineStatus],
	PS.[ShowLastSeen],
	PS.[ShowBio],
	US.[ProfileImageUrl],
	US.[IsOnline],
	(
        SELECT COUNT(*)
        FROM ConversationMessages CM
        WHERE CM.ConversationUserId = US.Id
          AND CM.ReceiverId = @LoggedUserId
          AND CM.IsRead = 0
    ) AS UnreadCount
FROM LastMessages LM
	INNER JOIN [dbo].[Users] US ON LM.[ConversationUserId] = US.[Id]
	INNER JOIN [dbo].[UserPrivacySettings] PS ON US.[Id] = PS.[UserId]
WHERE LM.[rn] = 1
ORDER BY LM.[CreatedDateTime] DESC;