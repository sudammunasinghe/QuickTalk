INSERT INTO [dbo].[Users]
(
	[FirstName],
    [LastName],
    [Email],
    [PasswordHash],
    [DateOfBirth]
)
VALUES
(
	@FirstName, 
    @LastName, 
    @Email, 
    @PasswordHash,
    @DateOfBirth
);

SELECT CAST(SCOPE_IDENTITY() AS INT);


