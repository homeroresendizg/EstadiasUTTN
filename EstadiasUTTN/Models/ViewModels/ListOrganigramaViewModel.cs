using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EstadiasUTTN.Models.ViewModels
{
    public class ListOrganigramaViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> Datetime { get; set; }
        public string Idusuario { get; set; }

    }
}