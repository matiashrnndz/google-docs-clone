using Domain;
using Exception;
using Repository;
using Service;
using System;
using System.Collections.Generic;

namespace ServiceImp
{
    public class FriendRequestManagementService : IFriendRequestManagementService
    {
        internal IUserRepository UserRepository { get; set; }
        internal IFriendRequestRepository FriendRequestRepository { get; set; }

        public void AddFriendRequest(string senderEmail, string receiverEmail)
        {
            if (!UserRepository.Exists(senderEmail))
            {
                throw new MissingUserException("The user with that mail was not found.");
            }

            if (!UserRepository.Exists(receiverEmail))
            {
                throw new MissingUserException("The user with that mail was not found.");
            }

            if (senderEmail == receiverEmail)
            {
                throw new InvalidFriendRequestException("You can not add yourself to your friendlist.");
            }

            if (FriendRequestRepository.Exists(senderEmail, receiverEmail))
            {
                throw new InvalidFriendRequestException("User : " + receiverEmail + " is already on your friendlist.");
            }

            FriendRequest friendRequest = new FriendRequest()
            {
                Id = Guid.NewGuid(),
                Accepted = false,
                Sender = new User { Email = senderEmail },
                Receiver = new User { Email = receiverEmail }
            };

            FriendRequestRepository.Add(friendRequest);
        }

        public IEnumerable<User> ListFriendsByEmail(string userEmail)
        {
            if (!UserRepository.Exists(userEmail))
            {
                throw new MissingUserException("The user with that mail was not found.");
            }

            IEnumerable<FriendRequest> friendRequestsOfUser = FriendRequestRepository.GetAllAcceptedByEmail(userEmail);

            List<User> friends = new List<User>();

            foreach (FriendRequest friendRequest in friendRequestsOfUser)
            {
                User sender = friendRequest.Sender;
                User receiver = friendRequest.Receiver;

                if (sender.Email == userEmail)
                {
                    friends.Add(receiver);
                }
                else
                {
                    friends.Add(sender);
                }
            }

            return friends;
        }

        public void RespondFriendRequest(string receiverEmail, string senderEmail, bool answer)
        {
            if (!UserRepository.Exists(senderEmail))
            {
                throw new MissingUserException("The user with that mail was not found.");
            }

            if (!UserRepository.Exists(receiverEmail))
            {
                throw new MissingUserException("The user with that mail was not found.");
            }

            if (senderEmail == receiverEmail)
            {
                throw new InvalidFriendRequestException("You can not add yourself to your friendlist.");
            }

            if (!FriendRequestRepository.Exists(senderEmail, receiverEmail))
            {
                throw new InvalidFriendRequestException("The user " + receiverEmail + " is already on your friendlist.");
            }

            FriendRequest friendRequest = FriendRequestRepository.GetByEmails(receiverEmail, senderEmail);

            if (answer == false)
            {
                FriendRequestRepository.Delete(friendRequest);
            }
            else
            {
                FriendRequestRepository.Accept(friendRequest.Id);
            }
        }

        public IEnumerable<User> ListReceivedFriendRequestsByEmail(string userEmail)
        {
            if (!UserRepository.Exists(userEmail))
            {
                throw new MissingUserException("The user with that mail was not found.");
            }

            IEnumerable<FriendRequest> friendRequests = FriendRequestRepository.GetAllReceivedByEmail(userEmail);
            IList<User> senders = new List<User>();

            foreach (FriendRequest friendRequest in friendRequests)
            {
                if (friendRequest.Accepted != true)
                {
                    senders.Add(friendRequest.Sender);
                }
            }

            return senders;
        }

        public FriendRequest GetFriendRequest(string senderEmail, string receiverEmail)
        {
            if (!UserRepository.Exists(senderEmail))
            {
                throw new MissingUserException("The user with that mail was not found.");
            }

            if (senderEmail == receiverEmail)
            {
                throw new InvalidFriendRequestException("You can not add yourself to your friendlist.");
            }

            if (!UserRepository.Exists(receiverEmail))
            {
                return new FriendRequest();
            }

            FriendRequest friendRequest = FriendRequestRepository.GetByEmails(receiverEmail, senderEmail);

            return friendRequest;
        }
    }
}
