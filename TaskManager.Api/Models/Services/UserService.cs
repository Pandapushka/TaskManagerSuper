using System.Security.Claims;
using System.Text;
using TaskManager.Api.Models.Abstractions;
using TaskManager.Api.Models.Data;
using TaskManager.Common.Models;

namespace TaskManager.Api.Models.Services
{
    public class UserService : ICommonService<UserModel>
    {
        private readonly ApplicationContext _db;

        public UserService(ApplicationContext db)
        {
                _db = db;
        }

        public Tuple<string, string> GetUserLoginPassFromBasicAuth(HttpRequest request)
        {
            string userLogin = "";
            string userPassword = "";
            string authHeader = request.Headers["Authorization"].ToString();
            if (authHeader != null && authHeader.StartsWith("Basic")) 
            {
                string encodedUserNamePass = authHeader.Replace("Basic", "");
                var encoding = Encoding.GetEncoding("iso-8859-1");

                string[] namePassArray = encoding.GetString(Convert.FromBase64String(encodedUserNamePass)).Split(':');
                userLogin = namePassArray[0];
                userPassword = namePassArray[1];
            }
            return new Tuple<string, string>(userLogin, userPassword);
        }

        public User GetUser(string login, string password)
        {
            return _db.Users.FirstOrDefault(u => u.Email == login && u.Password == password);
        }

        public ClaimsIdentity GetIdentity(string username, string password) 
        {
            User currentUser = GetUser(username, password);
            if (currentUser != null)
            {
                currentUser.LastLoginDate = DateTime.Now;
                _db.Users.Update(currentUser);
                _db.SaveChanges();

                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, currentUser.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, currentUser.Status.ToString())
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                                   ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }

        public bool Create(UserModel model)
        {
            try 
            {
                User newUser = new User(model.FirstName, model.LastName, model.Email,
                                       model.Password, model.Status, model.Phone);
                _db.Users.Add(newUser);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

        public bool Update(int id, UserModel model)
        {
            User userForUpdate = _db.Users.FirstOrDefault(x => x.Id == id);
            if (userForUpdate != null)
            {
                try
                {
                    userForUpdate.FirstName = model.FirstName;
                    userForUpdate.LastName = model.LastName;
                    userForUpdate.Password = model.Password;
                    userForUpdate.Phone = model.Phone;
                    userForUpdate.Status = model.Status;
                    userForUpdate.Email = model.Email;
                    _db.Users.Update(userForUpdate);
                    _db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
            return false;
        }

        public bool Delete(int id)
        {
              User user = _db.Users.FirstOrDefault(x => x.Id == id);
              if (user != null)
              {
                  try
                  {
                      _db.Users.Remove(user);
                      _db.SaveChanges();
                      return true;
                  }
                  catch (Exception ex)
                  {
                       return false;
                  }
              }
              return false;
        }
    }
}
