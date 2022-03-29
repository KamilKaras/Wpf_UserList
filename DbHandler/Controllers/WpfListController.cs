using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbHandler
{
    public class WpfListController
    {
        private readonly WpfListDbContext _wpfListDbContext;

        public WpfListController(WpfListDbContext dbContext)
        {
            _wpfListDbContext = dbContext;
        }

        public async Task<List<AplicationUserModel>> GetAllUser()
        {
            var allUsers = await _wpfListDbContext.AplicationUser.ToListAsync();
            return allUsers;
        }

        public async Task<AplicationUserModel> AddUser(AplicationUserModel newUser)
        {
            _wpfListDbContext.Add(newUser);
            await _wpfListDbContext.SaveChangesAsync();
            return newUser;
        }

        public async void DeleteUsers(int id)
        {
            var userToRemofeFromDb = await _wpfListDbContext.AplicationUser.FirstOrDefaultAsync(userDb => userDb.Id == id);
            if (userToRemofeFromDb != null)
            {
                _wpfListDbContext.AplicationUser.Remove(userToRemofeFromDb);
            }
        }

        public async void SaveChanges()
        {
            await _wpfListDbContext.SaveChangesAsync();
        }
    }
}
