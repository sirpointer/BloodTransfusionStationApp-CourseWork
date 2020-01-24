namespace BloodTransfusionStationApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DataOfUser")]
    public partial class DataOfUser
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "�����")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "������")]
        public string Password { get; set; }
    }
}
