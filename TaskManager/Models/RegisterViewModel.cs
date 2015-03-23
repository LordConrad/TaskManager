using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TaskManager.Models
{
    public class RegisterViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите логин")]
        [Display(Name = "Логин (ivanov)")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите пароль")]
        [MinLength(3, ErrorMessage = "Минимальная длина пароля 3 символа")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите свои Ф.И.О.")]
        [Display(Name = "Ф.И.О. (Иванов И.И.)")]
        public string UserFullName { get; set; }
    }
}
