using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models.Contacts
{

    public class Contact
    {
        [Key]
        public int Id { set; get; }
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        [Required(ErrorMessage ="Phai nhap ho ten{0}")]
        [Display(Name = "Ho ten")]
        public string? Fullname { set; get; }

        [Required(ErrorMessage = "Phai nhap Email{0}")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Dinh dang Email ko dung")]
        [Display(Name = "Dia chi Email")]
        public string? Email { set; get; }
        public DateTime DateSent { set; get; }
        
        [Display(Name = "Noi dung")]
        public string? Message { set; get; }
        [StringLength(50)]
        [Phone(ErrorMessage = "So dien thoai ko dung")]
        [Display(Name ="so dien thoai")]
        public string? Phone { set; get; }
    }
    

}