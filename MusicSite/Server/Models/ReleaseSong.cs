using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MusicSite.Server.Models
{
    public class ReleaseSong
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ReleaseId { get; set; }

        //[Key, MaxLength(50)]
        //public string Language { get; set; }
        [Required]
        public int SongOrder { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        public int LengthSecs { get; set; }

        [MaxLength(2000)]
        public string Lyrics { get; set; }
    }
}
