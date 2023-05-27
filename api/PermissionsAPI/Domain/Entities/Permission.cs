using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public partial class Permission
    {
        public int Id { get; set; }
        [Column(TypeName = "Text")]
        public string EmployeeFirstName { get; set; } = null!;
        [Column(TypeName = "Text")]
        public string EmployeeLastName { get; set; } = null!;
        public int PermissionTypeId { get; set; }
        public DateTime GrantedDate { get; set; }

        public virtual PermissionType PermissionType { get; set; } = null!;
    }
}
