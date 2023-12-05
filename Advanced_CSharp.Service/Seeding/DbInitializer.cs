using Advanced_CSharp.Database.Constants;
using Advanced_CSharp.Database.EF;
using Advanced_CSharp.Database.Entities;
using Advanced_CSharp.Database.Enums;
using Advanced_CSharp.Service.Utilities;

namespace Advanced_CSharp.Service.Seeding
{
    public static class DbInitializer
    {

        public static async Task Initialize(AdvancedCSharpDbContext context)
        {
            if (context != null)
            {

                if (context.AppRoles != null && !context.AppRoles.Any())
                {

                    context.AppRoles.AddRange(new List<AppRole>
                    {
                        new() { Id = new Guid(ConstSystem.AdminRoleId), RoleName = ConstSystem.AdminRole},
                        new() { Id = new Guid(ConstSystem.CustomerRoleId), RoleName = ConstSystem.CustomerRole}
                    });
                }



                if (context.AppUsers != null && context.AppUsers.Any(t => t.Id == new Guid(ConstSystem.AdminUserId)))
                {

                    string passAdminHash = BCrypt.Net.BCrypt.HashPassword("Admin@12345");
                    string passTesterHash = BCrypt.Net.BCrypt.HashPassword("Admin@123");

                    context.AppUsers.AddRange(new List<AppUser>
                    {
                        new()
                        {
                            Id = new Guid(ConstSystem.AdminRoleId),
                            FirstName = "Anh",
                            LastName="Trần Thế",
                            FullName = "Trần Thế Anh",
                            UserName="trantheanh132",
                            Dob=CommonUtils.convertToDate("13/02/1997"),
                            Email="trantheanh132@gmail.com",
                            Gender=EGender.Male,
                            PhoneNumber="0968768959",
                            IsActive= true,
                            PasswordHash =passAdminHash

                        },
                         new()
                        {
                            Id = new Guid(ConstSystem.TesterUserId),
                            FirstName = "tester",
                            LastName="tester",
                            FullName = "tester",
                            UserName="tester",
                            Dob=CommonUtils.convertToDate("01/01/2000"),
                            Email="tester@gmail.com",
                            Gender=EGender.Male,
                            PhoneNumber="",
                            IsActive= true,
                            PasswordHash =passTesterHash

                        }

                    });
                }


                if (context.AppUserRoles != null && !context.AppUserRoles.Any())
                {
                    List<AppUserRole> appUserRoleList = new();
                    if (!context.AppUserRoles.Any(t => t.UserId == new Guid(ConstSystem.AdminUserId)))
                    {
                        appUserRoleList.Add(new AppUserRole { UserId = new Guid(ConstSystem.AdminUserId), RoleId = new Guid(ConstSystem.AdminRoleId) });
                    }

                    if (!context.AppUserRoles.Any(t => t.UserId == new Guid(ConstSystem.TesterUserId)))
                    {
                        appUserRoleList.Add(new AppUserRole { UserId = new Guid(ConstSystem.TesterUserId), RoleId = new Guid(ConstSystem.CustomerRoleId) });

                    }
                    context.AppUserRoles.AddRange(appUserRoleList);
                }

                _ = await context.SaveChangesAsync();

            }
        }

    }
}

