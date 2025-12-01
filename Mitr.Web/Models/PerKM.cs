using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class PerKM
    {
        public long Id { set; get; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public long KM { set; get; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public long KMRate { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public long FromKm { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public long ToKm { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string  VehicleType { get; set; }
        public long? LoginID { get; set; }
        public string IPAddress { get; set; }
        public List<PerKM> objkmlist { get; set; }
    }
}