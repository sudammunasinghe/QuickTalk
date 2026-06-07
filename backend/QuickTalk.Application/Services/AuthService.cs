using QuickTalk.Application.DTOs.Auth;
using QuickTalk.Application.Exceptions;
using QuickTalk.Application.Interfaces.IRepositories;
using QuickTalk.Application.Interfaces.IServices;
using QuickTalk.Domain.Entities;

namespace QuickTalk.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IHashingService _hashingService;
        private readonly ITokenGenerateService _tokenGenerateService;
        private readonly IOtpService _otpService;
        private readonly IEmailService _emailService;
        public AuthService(
            IAuthRepository authRepository, 
            IHashingService hashingService, 
            ITokenGenerateService tokenGenerateService,
            IOtpService otpService,
            IEmailService emailService
            )
        {
            _authRepository = authRepository;
            _hashingService = hashingService;
            _tokenGenerateService = tokenGenerateService;
            _otpService = otpService;
            _emailService = emailService;
        }

        public async Task<string> RegisterUserAsync(RegisterDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
                throw new BadRequestException("Passwords do not match.");

            var existingUser =
                await _authRepository.GetUserByEmailAsync(dto.Email);

            if (existingUser != null)
                throw new ConflictException("Email already exists.");

            var passwordHash = _hashingService.HashPassword(dto.Password);
            var newUser = User.Create(
                dto.FirstName,
                dto.LastName,
                dto.Email,
                passwordHash,
                dto.DateOfBirth
            );
            var newUserId = await _authRepository.RegisterUserAsync(newUser);
            newUser.Id = newUserId;
            return _tokenGenerateService.GenerateJwtToken(newUser);
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user =
                await _authRepository.GetUserByEmailAsync(dto.Email);

            if (user == null || !_hashingService.VerifyPassword(dto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");
            return _tokenGenerateService.GenerateJwtToken(user);
        }

        public async Task<string> ForgotPassword(ForgotPasswordDto dto)
        {
            var user =
                await _authRepository.GetUserByEmailAsync(dto.Email);

            if (user == null)
                throw new BadRequestException("Invalid Email.");

            var otp =
                await _otpService.GenerateOtpAsync();

            await _otpService.SaveOtpAsync(user, otp);

            var subject = "Your QuickTalk OTP Code";
            var body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset=""UTF-8"">
                    <title>QuickTalk OTP</title>
                </head>
                <body style=""margin:0; padding:0; font-family:Arial, sans-serif; background-color:#f4f6f8;"">
                    <div style=""max-width:600px; margin:40px auto; background:#ffffff; border-radius:10px; overflow:hidden; box-shadow:0 2px 10px rgba(0,0,0,0.1);"">             
                        <!-- Header -->
                        <div style=""background:#4f46e5; padding:20px; text-align:center; color:white;"">
                            <h2 style=""margin:0;"">QuickTalk</h2>
                        </div>
                
                        <!-- Body -->
                        <div style=""padding:30px; text-align:center;"">
                            <h3 style=""color:#333;"">Your OTP Code</h3>                
                            <p style=""color:#666; font-size:15px;"">
                                Use the following OTP to verify your account. This code will expire in 3 minutes.
                            </p>
                
                            <!-- OTP Box -->
                            <div style=""margin:30px 0;"">
                                <span style=""display:inline-block; font-size:28px; letter-spacing:6px; font-weight:bold; background:#f1f5f9; padding:15px 25px; border-radius:8px; color:#111;"">
                                    {otp}
                                </span>
                            </div>
                            <p style=""color:#999; font-size:13px;"">
                                If you did not request this code, you can safely ignore this email.
                            </p>
                        </div>
                
                        <!-- Footer -->
                        <div style=""background:#f9fafb; padding:15px; text-align:center; font-size:12px; color:#888;"">
                            © 2026 QuickTalk. All rights reserved.
                        </div>
                    </div>
                </body>
                </html>
            ";

            await _emailService.SendEmailAsync(
                dto.Email,
                subject,
                body
            );
            return "If the email exists, a OTP has been sent.";
        }

        public async Task ResetPassword(ResetPasswordDto dto)
        {
            if (dto.NewPassword != dto.ConfirmNewPassword)
                throw new BadRequestException("Passwords do not match.");

            var user =
                await _authRepository.GetUserByEmailAsync(dto.Email);

            if (user == null)
                throw new NotFoundException("User not found.");

            if (user.Otp != dto.Otp)
                throw new BadRequestException("Invalid OTP.");

            if (user.OtpExpiry < DateTime.UtcNow)
                throw new BadRequestException("OTP has expired.");

            if (user.IsUsed == true)
                throw new BadRequestException("OTP has already been used.");

            var passwordHash = _hashingService.HashPassword(dto.NewPassword);

            user.Otp = null;
            user.OtpExpiry = null;
            user.IsUsed = true;
            user.LastModifiedDateTime = DateTime.UtcNow;
            user.ChangePassword(passwordHash);

            await _authRepository.UpdateUserAsync(user);
        }
    }
}
