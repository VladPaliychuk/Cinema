namespace BuildingBlocks.Messaging.Events
{
    public class UserLoggedInIntegrationEvent : IntegrationBaseEvent
    {
        public string Username { get; set; }

        public UserLoggedInIntegrationEvent(string username)
        {
            Username = username;
        }
    }
}