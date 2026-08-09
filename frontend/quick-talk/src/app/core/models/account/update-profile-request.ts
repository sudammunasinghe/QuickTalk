import { profileDetails } from "./profile-details";

export interface UpdateProfileRequest extends profileDetails {
    profilePicture: File | null,
    removeProfileImage: boolean
}