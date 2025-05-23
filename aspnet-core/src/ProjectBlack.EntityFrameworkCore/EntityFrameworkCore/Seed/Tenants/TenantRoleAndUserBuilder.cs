﻿using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using ProjectBlack.Authorization;
using ProjectBlack.Authorization.Roles;
using ProjectBlack.Authorization.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;

namespace ProjectBlack.EntityFrameworkCore.Seed.Tenants;

public class TenantRoleAndUserBuilder
{
    private readonly ProjectBlackDbContext _context;
    private readonly int _tenantId;

    public TenantRoleAndUserBuilder(ProjectBlackDbContext context, int tenantId)
    {
        _context = context;
        _tenantId = tenantId;
    }

    public void Create()
    {
        CreateRolesAndUsers();
    }

    private void CreateRolesAndUsers()
    {
        // Admin role

        var adminRole = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Admin);
        if (adminRole == null)
        {
            adminRole = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Admin, StaticRoleNames.Tenants.Admin) { IsStatic = true }).Entity;
            _context.SaveChanges();
        }
         // Analyst role
        var analystRole = _context.Roles.FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Analyst);
        if (analystRole == null)
        {
            analystRole = _context.Roles.Add(
                new Role(_tenantId, StaticRoleNames.Tenants.Analyst, StaticRoleNames.Tenants.Analyst)
                { IsStatic = true }
            ).Entity;
            _context.SaveChanges();
        }


        // Grant all permissions to admin role

        var grantedPermissions = _context.Permissions.IgnoreQueryFilters()
            .OfType<RolePermissionSetting>()
            .Where(p => p.TenantId == _tenantId && p.RoleId == adminRole.Id)
            .Select(p => p.Name)
            .ToList();

        var permissions = PermissionFinder
            .GetAllPermissions(new ProjectBlackAuthorizationProvider())
            .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant) &&
                        !grantedPermissions.Contains(p.Name))
            .ToList();

        if (permissions.Any())
        {
            _context.Permissions.AddRange(
                permissions.Select(permission => new RolePermissionSetting
                {
                    TenantId = _tenantId,
                    Name = permission.Name,
                    IsGranted = true,
                    RoleId = adminRole.Id
                })
            );
            _context.SaveChanges();
        }

        // Grant all permissions to analyst role

        var grantedAnalystPermissions = _context.Permissions.IgnoreQueryFilters()
            .OfType<RolePermissionSetting>()
            .Where(p => p.TenantId == _tenantId && p.RoleId == analystRole.Id)
            .Select(p => p.Name)
            .ToList();

        var analystPermissions = PermissionFinder
            .GetAllPermissions(new ProjectBlackAuthorizationProvider())
            .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant) &&
                        !grantedAnalystPermissions.Contains(p.Name))
            .ToList();

        if (analystPermissions.Any())
        {
            _context.Permissions.AddRange(
                analystPermissions.Select(permission => new RolePermissionSetting
                {
                    TenantId = _tenantId,
                    Name = permission.Name,
                    IsGranted = true,
                    RoleId = analystRole.Id
                })
            );
            _context.SaveChanges();
        }

        // Admin user

        var adminUser = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.TenantId == _tenantId && u.UserName == AbpUserBase.AdminUserName);
        if (adminUser == null)
        {
            adminUser = User.CreateTenantAdminUser(_tenantId, "admin@defaulttenant.com");
            adminUser.Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(adminUser, "123qwe");
            adminUser.IsEmailConfirmed = true;
            adminUser.IsActive = true;

            _context.Users.Add(adminUser);
            _context.SaveChanges();

            // Assign Admin role to admin user
            _context.UserRoles.Add(new UserRole(_tenantId, adminUser.Id, adminRole.Id));
            _context.SaveChanges();
        }
    }
}
