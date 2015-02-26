using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaskManager.Helpers;

namespace TaskManager.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Логин")]
        
        public string Login { get; set; }
        [Required]
        [Display(Name = "Ф.И.О.")]
        public string UserName { get; set; }
        [Display(Name = "Руководитель")]
        public string ChiefId { get; set; }
        [Required]
        [Display(Name = "Администратор")]
        public bool IsAdmin { get; set; }
        [Required]
        [Display(Name = "Отправитель")]
        public bool IsSender { get; set; }
        [Required]
        [Display(Name = "Исполнитель")]
        public bool IsRecipient { get; set; }
        [Required]
        [Display(Name = "Руководитель")]
        public bool IsChief { get; set; }
        [Required]
        [Display(Name = "Главврач")]
        public bool IsMasterChief { get; set; }
    }
}