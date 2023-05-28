﻿using Domain.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class PermissionTypeRepository : IPermissionTypeRepository
    {
        private readonly PermissionsContext context;

        public PermissionTypeRepository(PermissionsContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<PermissionType>> GetPermissionTypes()
        {
            await context.PermissionTypes.LoadAsync();
            return context.PermissionTypes.OrderBy(b => b.Id).ToList();
        }

        public async Task<PermissionType?> GetPermissionTypeById(int id)
        {
            var permissionType = await context.PermissionTypes.FirstOrDefaultAsync(p => p.Id == id);
            return permissionType;
        }

        public void InsertPermissionType(PermissionType permissionType)
        {
            context.PermissionTypes.Add(permissionType);
        }

        public void DeletePermissionType(PermissionType permissionType)
        {
            context.PermissionTypes.Remove(permissionType);
        }

        public void UpdatePermissionType(PermissionType currentPermissionType, PermissionType permissionTypeToUpdate)
        {
            currentPermissionType.Description = permissionTypeToUpdate.Description;
        }
    }
}
