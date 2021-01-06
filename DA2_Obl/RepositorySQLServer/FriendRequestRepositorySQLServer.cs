using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RepositorySQLServer
{
    public class FriendRequestRepositorySQLServer : IFriendRequestRepository
    {
        private static FriendRequestRepositorySQLServer instance;

        private FriendRequestRepositorySQLServer()
        {

        }

        public static FriendRequestRepositorySQLServer GetInstance()
        {
            if (instance == null)
            {
                instance = new FriendRequestRepositorySQLServer();
            }

            return instance;
        }

        public void Accept(Guid id)
        {
            FriendRequest friendRequest;

            using (DatabaseContext c = new DatabaseContext())
            {
                friendRequest = c.FriendRequests
                    .Where(f => f.Id == id)
                    .FirstOrDefault();

                friendRequest.Accepted = true;

                if (friendRequest.Sender != null)
                {
                    c.Entry(friendRequest.Sender)
                        .State = EntityState.Unchanged;
                }

                if (friendRequest.Receiver != null)
                {
                    c.Entry(friendRequest.Receiver)
                        .State = EntityState.Unchanged;
                }

                c.SaveChanges();
            }
        }

        public void Add(FriendRequest friendRequest)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                if (friendRequest.Sender != null)
                {
                    c.Entry(friendRequest.Sender)
                        .State = EntityState.Unchanged;
                }

                if (friendRequest.Receiver != null)
                {
                    c.Entry(friendRequest.Receiver)
                        .State = EntityState.Unchanged;
                }

                c.FriendRequests.Add(friendRequest);

                c.SaveChanges();
            }
        }

        public void Delete(FriendRequest friendRequest)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                FriendRequest toDelete = c.FriendRequests
                    .Where(d => d.Id == friendRequest.Id)
                    .FirstOrDefault();

                c.FriendRequests.Remove(toDelete);

                c.SaveChanges();
            }
        }

        public bool Exists(string senderEmail, string receiverEmail)
        {
            bool exists = false;

            using (DatabaseContext c = new DatabaseContext())
            {
                exists = c.FriendRequests.Any(f => ((f.Sender.Email == senderEmail && f.Receiver.Email == receiverEmail)
                || (f.Sender.Email == receiverEmail && f.Receiver.Email == senderEmail)));
            }

            return exists;
        }

        public IEnumerable<FriendRequest> GetAllAcceptedByEmail(string userEmail)
        {
            List<FriendRequest> friendRequests = new List<FriendRequest>();

            using (DatabaseContext c = new DatabaseContext())
            {
                friendRequests = c.FriendRequests
                    .Include(s => s.Sender)
                    .Include(r => r.Receiver)
                    .Where(f => ((f.Accepted == true) && (f.Sender.Email == userEmail || f.Receiver.Email == userEmail)))
                    .ToList();

            }

            return friendRequests.AsEnumerable();
        }

        public IEnumerable<FriendRequest> GetAllReceivedByEmail(string userEmail)
        {
            List<FriendRequest> friendRequests = new List<FriendRequest>();

            using (DatabaseContext c = new DatabaseContext())
            {
                friendRequests = c.FriendRequests
                    .Include(s => s.Sender)
                    .Include(r => r.Receiver)
                    .Where(f => f.Receiver.Email == userEmail)
                    .ToList();
            }

            return friendRequests.AsEnumerable();
        }

        public FriendRequest GetByEmails(string receiverEmail, string senderEmail)
        {
            FriendRequest friendRequest = null;

            using (DatabaseContext c = new DatabaseContext())
            {
                friendRequest = c.FriendRequests
                    .Include(s => s.Sender)
                    .Include(r => r.Receiver)
                    .Where(f => ((f.Sender.Email == senderEmail && f.Receiver.Email == receiverEmail)
                || (f.Sender.Email == receiverEmail && f.Receiver.Email == senderEmail)))
                    .FirstOrDefault();
            }

            return friendRequest;
        }
    }
}
