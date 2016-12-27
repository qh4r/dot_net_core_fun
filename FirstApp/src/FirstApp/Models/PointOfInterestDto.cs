using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApp.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PointOfInterestDto : PointOfIntrestCreationDto
    {
        public int Id { get; set; }
    }
}
