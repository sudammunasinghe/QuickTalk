export interface ChangePasswordRequest {
    currentPassword: string,
    newPassword: string,
    confirmedNewPassword: string
}