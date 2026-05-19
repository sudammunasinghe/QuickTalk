INSERT INTO [dbo].[Messages]
(
	[SenderId],
    [ReceiverId],
    [MessageText],
    [IsRead]
)
VALUES
(
	@SenderId,
    @ReceiverId, 
    @MessageText, 
    @IsRead
);