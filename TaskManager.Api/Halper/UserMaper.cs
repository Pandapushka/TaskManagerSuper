using TaskManager.Api.Models;
using TaskManager.Common.Models;

namespace TaskManager.Api.Halper
{
    public  class UserMaper
    {
        public static List<User> ToUsersList(List<UserModel> usersModel) 
        {
            List<User> users = new List<User>();
            foreach (var user in usersModel)
            {
                var i = new User(user);
                users.Add(i);
            }
            return users;
                
        }
    }
}
