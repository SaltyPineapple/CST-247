using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BibleVerseSearchApp.Models {
    public class Verse {

        [DisplayName("Testament: ")]
        [Required(ErrorMessage = "The Testament is required for entry.")]
        [MaxLength(3, ErrorMessage = "Can only enter 'Old' or 'New'.")]
        public string Testament { get; set; }

        
        [DisplayName("Book: ")]
        [Required(ErrorMessage = "The Book is required for entry.")]
        public string Book { get; set; }

        [DisplayName("Chapter Number: ")]
        [Required(ErrorMessage = "The chapter number is required for entry.")]
        public int Chapter { get; set; }

        [DisplayName("Verse Number: ")]
        [Required(ErrorMessage = "The verse number is required for entry.")]
        public int VerseNumber { get; set; }

        [DisplayName("Bible Verse: ")]
        [Required(ErrorMessage = "The actual Verse is required for entry.")]
        [MinLength(9, ErrorMessage = "Verse has to be of an actual length.")]
        public string BibleVerse { get; set; }
    }
}