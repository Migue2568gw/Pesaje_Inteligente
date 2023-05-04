using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
    public class TbEmpresa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpresaID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        public int Codigo { get; set; }

        [StringLength(80)]
        public string Direccion { get; set; }


        [StringLength(10)]
        public string Telefono { get; set; }

        [StringLength(50)]
        public string Ciudad { get; set; }

        [StringLength(50)]
        public string Departamento { get; set; }

        [StringLength(50)]
        public string Pais { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
