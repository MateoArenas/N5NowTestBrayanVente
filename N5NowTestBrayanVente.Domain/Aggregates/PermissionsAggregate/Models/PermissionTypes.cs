using System.ComponentModel.DataAnnotations;

namespace N5NowTestBrayanVente.Domain.Aggregates.PermissionsAggregate.Models
{
    public class PermissionTypes
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Permissions> Permissions { get; set; }
    }
}
