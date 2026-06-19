export interface ChatItemResponse {
    userId: number,
    firstName: string,
    lastName: string,
    lastMessage: string,
    lastMessageDisplayTime: string,
    unreadCount: number,
    isOnline: boolean
}