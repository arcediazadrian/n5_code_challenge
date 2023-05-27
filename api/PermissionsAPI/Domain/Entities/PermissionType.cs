using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public partial class PermissionType
    {
        public int Id { get; set; }
        [Column(TypeName = "Text")]
        public string Description { get; set; } = null!;

    }
}
