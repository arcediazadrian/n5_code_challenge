﻿using Domain.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class PermissionTypeRepository : IPermissionTypeRepository
    {
        private PermissionsContext context;

        public PermissionTypeRepository(PermissionsContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<PermissionType>> GetPermissionTypes()
        {
            await context.PermissionTypes.LoadAsync();
            return context.PermissionTypes.OrderBy(b => b.Id).ToList();
        }

        public async Task<PermissionType> GetPermissionTypeById(int id)
        {
            var permissionType = await context.PermissionTypes.Where(p => p.Id == id).FirstOrDefaultAsync();
            return permissionType;
        }

        public void InsertPermissionType(PermissionType permissionType)
        {
            context.PermissionTypes.Add(permissionType);
        }

        public async Task DeletePermissionType(int id)
        {
            PermissionType permissionType = await context.PermissionTypes.Where(p => p.Id == id).FirstOrDefaultAsync();
            context.PermissionTypes.Remove(permissionType);
        }

        public async Task UpdatePermissionType(int id, PermissionType permissionTypeToUpdate)
        {
            var permissionType = await context.PermissionTypes.Where(p => p.Id == id).FirstOrDefaultAsync();

            permissionType.Description = permissionTypeToUpdate.Description;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
            return;
        }
    }
}
