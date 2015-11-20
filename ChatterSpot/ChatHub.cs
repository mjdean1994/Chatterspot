using Microsoft.AspNet.SignalR;

namespace ChatterSpot
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message, string id)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message, id);
        }

        public void Connect(string name, string id)
        {
            Clients.All.userJoin(name, id);
        }

        public void Disconnect(string name, string id)
        {
            Clients.All.userLeave(name, id);
        }

        public void ReportMyPresence(string name, string id)
        {
            Clients.All.populateUser(name, id);
        }

        public void RequestPresence(string id)
        {
            Clients.All.reportPresence(id);
        }

        public void ClaimAdmin(string name, string id)
        {
            Clients.All.recognizeAdmin(name, id);
        }

        public void UpdateTitle(string title, string id)
        {
            Clients.All.recognizeTitle(title, id);
        }

        public void KickUser(string name, string id)
        {
            Clients.All.recognizeKick(name, id);
        }

        public void whisper(string from, string message, string to, string id)
        {
            Clients.All.getWhisper(from, message, to, id);
        }
    }
}