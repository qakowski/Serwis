using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
namespace Serwis.Models
{
    public class Customers 
    {
        [Key]
        [Required]
        [Display(Name="Numer seryjny")]
        public string SerialNumber { get; set; }

        [Required]
        [Display(Name="Imię")]
        public string ClientForeName { get; set; }

        [Required]
        [Display(Name="Nazwisko")]
        public string ClientSureName { get; set; }

        [Required]
        [Display(Name="Data przyjęcia")]
        public DateTime AcceptanceDate { get; set; }

        [Required]
        [Display(Name="Opis usterki")]
        public string IssueDescription { get; set; }

        [Required]
        [Display(Name="Stan")]
        public string State { get; set; }

        
        [Display(Name="Zdjęcie")]
        public string Photo { get; set; }

        
        

    }
}
