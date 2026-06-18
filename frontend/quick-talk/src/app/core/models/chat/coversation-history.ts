export interface CoversationHistory{
    id: number,
    senderId: number,
    message: string,
    sendAt: Date,
    isMine: boolean
}