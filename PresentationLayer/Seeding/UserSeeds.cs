using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace PresentationLayer.Seeding
{
    public static class UserSeeds
    {
        public static async Task<ObservableCollection<ServerUser>> GetAllUsersSeedsAsyncTask()
        {
           var result = await  Task.Factory.StartNew(() => {
               ObservableCollection<ServerUser> allUsers = new ObservableCollection<ServerUser>();
               List<ServerUser> users = GetAllUsersSeeds();
               foreach(var user in users)
               {
                   allUsers.Add(user);
               }
               return allUsers;
            });

            return result;
        }


        public static int GetAllUserSeedsCount()
        {
            return GetAllUsersSeeds().Count;
        }
        private static List<ServerUser> GetAllUsersSeeds()
        {
            string strUri = "pack://application:,,,/Icons/user.jpg";
            string strUri1 = "../../Icons/user.png";
            var uri = new Uri(strUri);
            List<ServerUser> users = new List<ServerUser>() {

                new ServerUser(){
                    Username="tom",
                    ImageSource= strUri1
                },
                new ServerUser(){
                    Username="tom1",
                    ImageSource= strUri1
                },
                new ServerUser(){
                    Username="tom2",
                    ImageSource= strUri1
                },
                new ServerUser(){
                    Username="tom3",
                    ImageSource= strUri1
                },
            };

            return users;
        }

    }
}
