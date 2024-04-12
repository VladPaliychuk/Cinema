namespace BuildingBlocks.Messaging.Events
{
    public class UserLoggedOutIntegrationEvent : IntegrationBaseEvent
    {
        public string Username { get; set; }

        public UserLoggedOutIntegrationEvent(string username)
        {
            Username = username;
        }
    }
}