using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EstadiasUTTN.Models.ViewModels
{
    public class OrganigramaViewModel
    {
        public int Id { get; set; }

        //[Required]
        [StringLength(50)]
        [Display(Name = "Titulo")]
        public string Title { get; set; }

        //[Required]
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        //[Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Fecha")]
        public Nullable<System.DateTime> Datetime { get; set; }

        public string Idusuario { get; set; }
    }
}