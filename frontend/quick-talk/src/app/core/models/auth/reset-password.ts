export interface ResetPasswordRequest{
    otp: string,
    newPassword: string,
    confirmNewPassword: string
}