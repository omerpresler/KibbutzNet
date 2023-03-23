using Backend.Business.Utils;
using System.Xml.Linq;

namespace Backend.Service
{
    public class UserHandler
    {
        {
            private List<User> _users;

            public UserHandler()
            {
                _users = new List<User>();
                LoadUsersFromFile();
            }

            private void LoadUsersFromFile()
            {
                try
             
                {
                    using (StreamReader sr = new StreamReader("../..users.txt"))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] userFields = line.Split(',');
                            User user = new User
                            (
                                Convert.ToInt32(userFields[0]),
                                userFields[1],
                                 Convert.ToInt32(userFields[2]),
                                userFields[3]
                            );
                            _users.Add(user);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading users from file: {ex.Message}");
                }
            }

            public bool AuthenticateUser(int userId, string email)
            {
                User user = _users.Find(u=>u.checkEquelUser(userId, email));
                if (user != null)
                {
                    Console.WriteLine($"User '{email}' has been authenticated successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Authentication failed for user '{email}'.");
                    return false;
                }
            }
        }
    }

