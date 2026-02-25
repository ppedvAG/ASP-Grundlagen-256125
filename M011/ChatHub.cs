using Microsoft.AspNetCore.SignalR;

namespace M011;

public class ChatHub : Hub
{
	/// <summary>
	/// Wenn der Client (JS) diesen Befehl ausführen möchte, sendet er ein JavaScript Signal namens SendMessage an den Server
	/// Der Server führt hier diese C# Methode aus
	/// </summary>
	public void SendMessage(string msg, string user)
	{
		Clients.All.SendAsync("ReceiveMessageFromServer", msg, user);
	}
}