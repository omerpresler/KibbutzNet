export default function ChatDisplay({ userId, userType }) {
    // ... useState and useEffect hooks ...
  
    // ... fetchChats, handleChatClick, handleNewMessageChange functions ...
  
    const handleSendMessage = async () => {
      if (selectedChat && newMessage) {
        const message = new Message(userId, newMessage);
        const response = await chatService.sendMessage(
          selectedChat.sessionId,
          message,
        );
        if (!response.wasExecption) {
          setMessages([...messages, response.value]);
          setNewMessage('');
        }
      }
    };
  
    return (
      <Box>
        {/* ... Typography and List components for displaying chats ... */}
        {selectedChat && (
          <Box>
            {/* ... Typography component for displaying selected chat ... */}
            <List>
              {messages.map((message, index) => (
                <ListItem key={index}>
                  <ListItemText
                    primary={`${message.sender}: ${message.message}`}
                  />
                </ListItem>
              ))}
            </List>
            {/* ... TextField and Button components for sending a new message ... */}
          </Box>
        )}
      </Box>
    );
  }