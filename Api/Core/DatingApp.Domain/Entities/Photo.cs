﻿
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DatingApp.Domain.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public bool IsMain { get; set; }
        public string? PublicId { get; set; }

        public int AppUserid { get; set; }
        public bool IsApproved { get; set; }
        [JsonIgnore]
        public AppUser AppUser { get; set; }
    }
}