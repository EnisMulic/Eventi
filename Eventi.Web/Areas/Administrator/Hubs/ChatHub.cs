using Eventi.Data.EF;
using Eventi.Data.Models;
using Eventi.Data.Repository;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly MojContext ctx;
        private readonly EventAttenderUnitOfWork uow;

        
        public ChatHub(MojContext context)
        {
            ctx = context;
            uow = new EventAttenderUnitOfWork(ctx);
        }
        public async Task SendMessage(string userId, string user, string message)
        {
            var Poruka = new ChatPoruke
            {
                OsobaId = int.Parse(userId),
                Poruka = message,
                Kreirana = DateTime.Now
            };
            uow.ChatPorukeRepository.Add(Poruka);

            
            await Clients.All.SendAsync("ReceiveMessage", Poruka.Id, user, message, DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
        }
    }
}