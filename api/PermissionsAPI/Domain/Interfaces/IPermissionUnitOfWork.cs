namespace Domain.Interfaces
{
    public interface IPermissionUnitOfWork
    {
        IPermissionRepository PermissionRepository { get; }
        IPermissionTypeRepository PermissionTypeRepository { get; }
    }
}
