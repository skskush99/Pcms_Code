using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EcourtDto.Ecourt
{
    public class GetDetailByCNR
    {
        [Display(Name = "Cnr Number")]
        [Required(ErrorMessage = "Please enter Cnr Number")]
        public required string CinNo { get; set; }
    }

    
}
