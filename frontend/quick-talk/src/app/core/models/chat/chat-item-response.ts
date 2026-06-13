export interface ChatItemResponse{
    userId: number,
    name: string,
    lastMessage: string,
    lastMessageTime: Date,
    unreadCount: number,
    isOnline: boolean
}