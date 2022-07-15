using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GradBootcamp_Tolgahaninan.Models
{
    [Table("mytable")]
    public partial class Movie
    {
        [Column("adult")]
        [StringLength(126)]
        public string Adult { get; set; } = null!;
        [Column("belongs_to_collection")]
        [StringLength(184)]
        public string? BelongsToCollection { get; set; }
        [Column("budget")]
        [StringLength(32)]
        public string Budget { get; set; } = null!;
        [Column("genres")]
        [StringLength(264)]
        public string Genres { get; set; } = null!;
        [Column("homepage")]
        [StringLength(242)]
        public string? Homepage { get; set; }
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("imdb_id")]
        [StringLength(9)]
        public string? ImdbId { get; set; }
        [Column("original_language")]
        [StringLength(5)]
        public string? OriginalLanguage { get; set; }
        [Column("original_title")]
        [StringLength(109)]
        public string OriginalTitle { get; set; } = null!;
        [Column("overview")]
        [StringLength(1000)]
        public string? Overview { get; set; }
        [Column("popularity")]
        [StringLength(21)]
        public string? Popularity { get; set; }
        [Column("poster_path")]
        [StringLength(35)]
        public string? PosterPath { get; set; }
        [Column("production_companies")]
        [StringLength(1252)]
        public string? ProductionCompanies { get; set; }
        [Column("production_countries")]
        [StringLength(1039)]
        public string? ProductionCountries { get; set; }
        [Column("release_date")]
        [StringLength(10)]
        public string? ReleaseDate { get; set; }
        [Column("revenue")]
        public int? Revenue { get; set; }
        [Column("runtime")]
        [Precision(6, 1)]
        public decimal? Runtime { get; set; }
        [Column("spoken_languages")]
        [StringLength(765)]
        public string? SpokenLanguages { get; set; }
        [Column("status")]
        [StringLength(15)]
        public string? Status { get; set; }
        [Column("tagline")]
        [StringLength(297)]
        public string? Tagline { get; set; }
        [Column("title")]
        [StringLength(105)]
        public string? Title { get; set; }
        [Column("video")]
        [StringLength(5)]
        public string? Video { get; set; }
        [Column("vote_average")]
        [Precision(4, 1)]
        public decimal? VoteAverage { get; set; }
        [Column("vote_count")]
        public int? VoteCount { get; set; }
    }
}
