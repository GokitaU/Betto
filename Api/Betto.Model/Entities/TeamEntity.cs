﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Betto.Model.Entities
{
    public class TeamEntity
    {
        [Key]
        public int TeamId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string Code { get; set; }
        [Required]
        [MaxLength(300)]
        public string Logo { get; set; }
        [Required]
        [MaxLength(100)]
        public string Country { get; set; }
        [Required]
        [JsonProperty("is_national", NullValueHandling = NullValueHandling.Ignore)]
        public string IsNational { get; set; }
        [Required]
        public int? Founded { get; set; }
        [Required]
        public VenueEntity Venue { get; set; }
        public LeagueEntity League { get; set; }
        [ForeignKey("League")]
        public int LeagueId { get; set; }
    }
}
