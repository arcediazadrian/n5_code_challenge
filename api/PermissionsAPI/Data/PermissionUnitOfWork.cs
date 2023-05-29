using Domain.Interfaces;
using Nest;

namespace Data
{
    public class PermissionUnitOfWork : IPermissionUnitOfWork
    {
        public IPermissionRepository PermissionRepository { get; set; }
        public IPermissionTypeRepository PermissionTypeRepository { get; set; }

        public PermissionUnitOfWork(PermissionsContext context, IElasticClient elasticClient)
        {
            this.PermissionRepository = new PermissionRepository(context, elasticClient);
            this.PermissionTypeRepository = new PermissionTypeRepository(context);
        }
    }
}
