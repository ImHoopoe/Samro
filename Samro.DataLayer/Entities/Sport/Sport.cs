using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using WinWin.DataLayer.Entities.EventModels;

namespace WinWin.DataLayer.Entities.Sport
{
    public class Sport
    {
        [Key]
        public int SportId { get; set; }

        [Required]
        [MaxLength(100)]
        public string SportName { get; set; }

        [ValidateNever]
        public int? ParentId { get; set; }

        #region Relations

        [ForeignKey(nameof(ParentId))]
        [ValidateNever]
        public Sport? Parent { get; set; }
        public ICollection<Sport> SubGroups { get; set; }
        public ICollection<Tournament>? SportToTournaments { get; set; }
        public ICollection<SportToMatch>? SportToMatches { get; set; }

        #endregion

        public Sport()
        {
            SubGroups = new List<Sport>();
        }
    }
}
