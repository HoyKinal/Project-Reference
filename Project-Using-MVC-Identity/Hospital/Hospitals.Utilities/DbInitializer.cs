using Hospital.Models;
using Hospital.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Hospitals.Utilities
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DbInitializer> _logger;


        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            ILogger<DbInitializer> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            try
            {
                if (_context.Database.GetAppliedMigrations().Count() > 0)
                {
                    await _context.Database.MigrateAsync();
                }

                if (!await _roleManager.RoleExistsAsync(WebSiteRoles.WebSite_Admin))
                {
                    await _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_Admin));
                    await _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_Patient));
                    await _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_Doctor));

                    var user = new ApplicationUser
                    {
                        UserName = "hoykinal",
                        Email = "hoykinal2001@gmail.com"
                    };

                    await _userManager.CreateAsync(user, "hoykinal@123");

                    var appUser = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Email == "hoykinal2001@gmail.com");

                    if (appUser != null)
                    {
                        await _userManager.AddToRoleAsync(appUser, WebSiteRoles.WebSite_Admin);
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Database update error during initialization: {ErrorMessage}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred during initialization: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}

//way2 
/*
 using Hospital.Models;
using Hospital.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Hospitals.Utilities
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            ILogger<DbInitializer> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _logger = logger;
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetAppliedMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }

                if (!_roleManager.RoleExists(WebSiteRoles.WebSite_Admin))
                {
                    _roleManager.Create(new IdentityRole(WebSiteRoles.WebSite_Admin));
                    _roleManager.Create(new IdentityRole(WebSiteRoles.WebSite_Patient));
                    _roleManager.Create(new IdentityRole(WebSiteRoles.WebSite_Doctor));

                    var user = new ApplicationUser
                    {
                        UserName = "hoykinal",
                        Email = "hoykinal2001@gmail.com"
                    };

                    _userManager.Create(user, "hoykinal@123");

                    var appUser = _context.ApplicationUsers.FirstOrDefault(x => x.Email == "hoykinal2001@gmail.com");

                    if (appUser != null)
                    {
                        _userManager.AddToRole(appUser, WebSiteRoles.WebSite_Admin);
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Database update error during initialization: {ErrorMessage}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred during initialization: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}

 
 */
