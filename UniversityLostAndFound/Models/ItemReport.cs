using System;
using System.ComponentModel.DataAnnotations;

namespace UniversityLostAndFound.Models
{
    public class ItemReport
    {
        [Key]
        public int ItemID { get; set; }

        [Required(ErrorMessage = "الرجاء إدخال عنوان الإعلان")]
        [StringLength(100)]
        [Display(Name = "العنوان")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "الرجاء إدخال وصف تفصيلي")]
        [Display(Name = "الوصف")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "الرجاء تحديد نوع البلاغ")]
        [Display(Name = "نوع البلاغ")]
        public string ReportType { get; set; } = "Lost"; // Lost or Found

        [Required(ErrorMessage = "الرجاء تحديد مكان الفقدان أو الوجود")]
        [Display(Name = "الموقع داخل الجامعة")]
        public string Location { get; set; } = string.Empty;

        [Display(Name = "تاريخ البلاغ")]
        public DateTime DateReported { get; set; } = DateTime.Now;

        [Display(Name = "حالة العنصر")]
        public string Status { get; set; } = "Active"; // Active, Claimed, Resolved
    }
}