namespace FirstApp.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PointOfInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [ForeignKey("CityId")]
        //tak utworzona properta automatycznie tworzy powiazanie
        public City City { get; set; }

        public int CityId { get; set; }
    }
}