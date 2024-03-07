using Bloggie.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Auth1DbContext auth1DbContext;

        public UserRepository(Auth1DbContext auth1DbContext)
        {
            this.auth1DbContext = auth1DbContext;
        }
        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users = await auth1DbContext.Users.ToListAsync();

            var superAdmin = await auth1DbContext.Users
                .FirstOrDefaultAsync(x => x.Email == "superadmin@bloggie.com");

            if (superAdmin != null)
            {
                users.Remove(superAdmin);
            }
            return users;

        }
    }
}
