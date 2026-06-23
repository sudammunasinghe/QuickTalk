UPDATE [dbo].[Users]
   SET 
      [Otp] = @Otp,
      [OtpExpiry] = @OtpExpiry,
      [LastModifiedDateTime] = @LastModifiedDateTime,
      [IsUsed] = @IsUsed,
      [LastSeen] = @LastSeen
WHERE [Id] = @Id;