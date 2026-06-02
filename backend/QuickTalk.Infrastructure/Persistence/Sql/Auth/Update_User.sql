UPDATE [dbo].[Users]
   SET 
      [Otp] = @Otp,
      [OtpExpiry] = @OtpExpiry,
      [LastModifiedDateTime] = @LastModifiedDateTime,
      [IsUsed] = @IsUsed
WHERE [Id] = @Id;