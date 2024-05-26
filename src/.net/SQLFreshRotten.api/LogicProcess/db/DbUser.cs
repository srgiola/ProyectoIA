using NLog;
using SQLFreshRotten.api.Abstracts;
using SQLFreshRotten.api.LogicProcess.implements;
using SQLFreshRotten.api.LogicProcess.managmentfiles;
using SQLFreshRotten.api.Models;
using SQLFreshRotten.api.ProviderContext;

namespace SQLFreshRotten.api.LogicProcess.db
{
    public class DbUser
    {
        private readonly DbCtx _context;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        
        public DbUser (DbCtx context)
        {
            _context = context;
        }

        public async Task SetDafaultUsers ()
        {
            List<User> defaultUsers = DefaultUsers();
            List<string> usersInSystem = _context.Users
                                              .Select(property => property.UserName)
                                              .ToList();


            List<User> usersAdd = defaultUsers.Where(property 
                                                => !usersInSystem.Contains(property.UserName)
                                              )
                                              .ToList();

            LoadFactory factory = new EfLoadFactory(_context);
            var addRange = factory.AddItemRange<User>();
            await addRange.InsertRange(usersAdd);
        }

        private List<User> DefaultUsers ()
        {
            List<User> users = new ();
            
            try
            {
                ManagmentFile managmentFile = new();
                List<string> contentInRows = managmentFile.GetContentFile("users.txt");
                contentInRows.ForEach(row =>
                {
                    string[] userInformation = row.Split(',');

                    User newUser = new User
                    {
                         FirstName = userInformation[0],
                         LastName = userInformation[1],
                         Email = userInformation[2],
                         UserName = userInformation[3],
                         Password = userInformation[4]
                    };
                    users.Add(newUser);
                });
            }
            catch (Exception ex)
            {
                _logger.Error("Problemas al obtener los usuarios default");
                _logger.Error($"Ms: {ex.Message}, St: {ex.StackTrace}");
            }

            return users;
        }
    
        public bool AuthenticateUser (string user, string password)
        {
            if (string.IsNullOrEmpty(user))
                throw new Exception("Campo user, requerido");

            if (string.IsNullOrEmpty(password))
                throw new Exception("Campo password, requerido");

            return _context.Users
                           .Where(property => property.UserName == user && property.Password == password)
                           .FirstOrDefault() != null;
        }    
    }
}
