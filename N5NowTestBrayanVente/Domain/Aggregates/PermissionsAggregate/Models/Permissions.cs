using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N5NowTestBrayanVente.Domain.Aggregates.PermissionsAggregate.Models
{
    public class Permissions
    {
        [Key]
        public int Id { get; set; }

        public string NombreEmpleado { get; set; }

        public string ApellidoEmpleado { get; set; }


        public int TipoPermiso { get; set; }
        [ForeignKey("TipoPermiso")]
        public virtual PermissionTypes PermissionType { get; set; }


        public DateTime FechaPermiso { get; set; }
    }
}
