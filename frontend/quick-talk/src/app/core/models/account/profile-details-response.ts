import { profileDetails } from "./profile-details";

export interface ProfileDetailsResponse extends profileDetails {
    profilePictureUrl: string | null
}