using ServiceLayer.Enumerations;
using ServiceLayer.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using PresentationLayer.MVVM.Models;
using System.Collections.ObjectModel;

namespace PresentationLayer.Seeding
{
    public static class InviteSeeds
    {
        public static async Task<ObservableCollection<InviteModel>> GetAllInvitesSeedsAsyncTask()
        {
            var result = await Task.Factory.StartNew(() => {
                ObservableCollection<InviteModel> AllInvites = new ObservableCollection<InviteModel>();
                List<ControlInvite> allInvitesList = GetAllControlInvitesSeeds();
                foreach (ControlInvite controlInvite in allInvitesList)
                {
                    InviteModel model = new InviteModel()
                    {
                        InviteId = controlInvite.InviteObject.InviteId,
                        ControlActionType = controlInvite.ControlActionType,
                        ChatRoomName = controlInvite.InviteObject.ChatRoomName,
                        ChatRoomId = controlInvite.InviteObject.ChatRoomId,
                        ChatRoomIdentifierNameAndID = "" + controlInvite.InviteObject.ChatRoomName + "_" + controlInvite.InviteObject.ChatRoomId,
                        ChatRoomCreatorUsername = controlInvite.InviteObject.ChatRoomCreator.Username,
                    };
                    AllInvites.Add(model);
                }
                return AllInvites;
            });
            return result;
        }

        public static int GetAllInviteSeedsCount()
        {
            return GetAllControlInvitesSeeds().Count;
        }

        private static List<ControlInvite> GetAllControlInvitesSeeds()
        {
            List<ControlInvite> allControlInvites = new List<ControlInvite>();

            ControlInvite invite1 = new ControlInvite()
            {
                ControlActionType = ControlActionType.Create,
                InviteObject = new Invite()
                {
                    InviteId = Guid.NewGuid(),
                    ChatRoomCreator = new ServerUser()
                    {
                        ServerUserID = Guid.NewGuid(),
                        Username = "USER Joe"
                    },
                    ChatRoomId = Guid.NewGuid(),
                    ChatRoomName = "Test 1",
                    GuestServerUser = new ServerUser()
                    {
                        ServerUserID = Guid.NewGuid(),
                        Username = "Main User"
                    },
                    InviteStatus = InviteStatus.SentAndPendingResponse

                }
            };

            ControlInvite invite2 = new ControlInvite()
            {
                ControlActionType = ControlActionType.Create,
                InviteObject = new Invite()
                {
                    InviteId = Guid.NewGuid(),
                    ChatRoomCreator = new ServerUser()
                    {
                        ServerUserID = Guid.NewGuid(),
                        Username = "USER Joe"
                    },
                    ChatRoomId = Guid.NewGuid(),
                    ChatRoomName = "Test 1",
                    GuestServerUser = new ServerUser()
                    {
                        ServerUserID = Guid.NewGuid(),
                        Username = "Main User"
                    },
                    InviteStatus = InviteStatus.SentAndPendingResponse

                }
            };

            ControlInvite invite3 = new ControlInvite()
            {
                ControlActionType = ControlActionType.Create,
                InviteObject = new Invite()
                {
                    InviteId = Guid.NewGuid(),
                    ChatRoomCreator = new ServerUser()
                    {
                        ServerUserID = Guid.NewGuid(),
                        Username = "USER Joe"
                    },
                    ChatRoomId = Guid.NewGuid(),
                    ChatRoomName = "Test 1",
                    GuestServerUser = new ServerUser()
                    {
                        ServerUserID = Guid.NewGuid(),
                        Username = "Main User"
                    },
                    InviteStatus = InviteStatus.SentAndPendingResponse

                }
            };

            ControlInvite invite4 = new ControlInvite()
            {
                ControlActionType = ControlActionType.Create,
                InviteObject = new Invite()
                {
                    InviteId = Guid.NewGuid(),
                    ChatRoomCreator = new ServerUser()
                    {
                        ServerUserID = Guid.NewGuid(),
                        Username = "USER Joe"
                    },
                    ChatRoomId = Guid.NewGuid(),
                    ChatRoomName = "Test 1",
                    GuestServerUser = new ServerUser()
                    {
                        ServerUserID = Guid.NewGuid(),
                        Username = "Main User"
                    },
                    InviteStatus = InviteStatus.SentAndPendingResponse

                }
            };
            allControlInvites.Add(invite1);
            allControlInvites.Add(invite2);
            allControlInvites.Add(invite3);
            allControlInvites.Add(invite4);

            return allControlInvites;
        }

    }
}
