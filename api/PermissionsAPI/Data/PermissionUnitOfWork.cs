using Domain.Interfaces;

namespace Data
{
    public class PermissionUnitOfWork : IPermissionUnitOfWork
    {
        public IPermissionRepository PermissionRepository { get; set; }
        public IPermissionTypeRepository PermissionTypeRepository { get; set; }

        private readonly PermissionsContext context;

        public PermissionUnitOfWork(PermissionsContext context)
        {
            this.context = context;
            this.PermissionRepository = new PermissionRepository(context);
            this.PermissionTypeRepository = new PermissionTypeRepository(context);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
            return;
        }
    }
}
