using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeRecord.Models.Employees
{
    public class Employee
    {
        public int id { get; set; }

        [Required]
        public string nombre { get; set; }

        [Required]
        public string apellidos { get; set; }


        [Required]
        public string puesto { get; set; } 

        [Required(ErrorMessage ="El campo {0}, es requerido.")]
        public string empresa { get; set; } 



        [Required(ErrorMessage = "El campo {0}, es requerido.")]
        [EmailAddress(ErrorMessage = "El {0}, debe tener formato correo@correo.com")]
        public string email { get; set; }


        public DateTime creation_date { get; set; }

        public string ToQueryUpdate()
        {
            return $"UPDATE `empleado` SET `nombre`='{nombre}',`apellidos`='{apellidos}',`puesto`='{puesto}',`email`='{email}',`empresa`='{empresa}' WHERE `id`='{id}'";
        }

        public override string ToString()
        {
            return $"{nombre} {apellidos}";
        }

        internal string ToQueryInsert()
        {
            return $"INSERT INTO `empleado`(`id`, `nombre`, `apellidos`, `puesto`, `email`, `empresa`, `creation_date`) VALUES ('{id}','{nombre}','{apellidos}','{puesto}','{email}','{empresa}','{creation_date.ToString("yyyy-MM-dd HH:mm:ss")}')";
        }

        public static implicit operator Employee(EmployeeModel v)
        {
            throw new NotImplementedException();
        }
    }
}
