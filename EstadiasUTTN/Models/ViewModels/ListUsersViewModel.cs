using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EstadiasUTTN.Models.ViewModels
{
    public class ListUsersViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public Nullable<System.DateTime> Datetime { get; set; }
        public string Idusuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}