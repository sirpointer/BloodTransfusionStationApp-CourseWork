//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BloodTransfusionStationApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Прием_крови
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Прием_крови()
        {
            this.Медицинское_оборудование = new HashSet<Медицинское_оборудование>();
        }
    
        [Range(0, int.MaxValue)]
        [Display(Name = "Номер посещения")]
        public int Номер_посещения { get; set; }

        [Display(Name = "Дата приема")]
        public System.DateTime Дата_приема { get; set; }

        [Required]
        public string Вознаграждение { get; set; }

        [Required]
        [Display(Name = "Вид донорства")]
        public string Вид_донорства { get; set; }

        public int Врач { get; set; }
        public int Донор { get; set; }
        public int Хранилище { get; set; }

        [Range(0, 100000)]
        public float Объем { get; set; }
    
        public virtual Врачи Врачи { get; set; }
        public virtual Доноры Доноры { get; set; }
        public virtual Хранилища_крови Хранилища_крови { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Медицинское_оборудование> Медицинское_оборудование { get; set; }
    }
}
