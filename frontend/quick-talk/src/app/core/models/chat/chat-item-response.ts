export interface ChatItemResponse {
    userId: number,
    firstName: string,
    lastName: string,
    lastMessage: string,
    lastMessageDisplayTime: string,
    showProfilePicture: boolean,
    showOnlineStatus: boolean,
    showLastSeen: boolean,
    showBio: boolean,
    profileImageUrl: string | null,
    unreadCount: number,
    isOnline: boolean
}