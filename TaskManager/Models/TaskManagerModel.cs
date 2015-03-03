using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Configuration;
using TaskManager.Migrations;
using WebMatrix.WebData;

namespace TaskManager.Models
{
    public class TaskManagerContext : DbContext
    {
        public TaskManagerContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new DatabaseInitializer());
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .HasRequired(x => x.TaskSender)
                .WithMany(m => m.SendedTasks)
                .HasForeignKey(f => f.SenderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaskEeventLog>()
                .HasRequired(x => x.Task)
                .WithMany(x => x.TaskEeventLogs)
                .HasForeignKey(x => x.TaskId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<TaskEeventLog>()
                .HasRequired(x => x.User)
                .WithMany(x => x.Logs)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(true);

            //modelBuilder.Entity<Task>()
            //    .HasRequired(x => x.TaskChief)
            //    .WithMany(m => m.ChiefTasks)
            //    .HasForeignKey(f => f.TaskChiefId)
            //    .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserProfile> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Priority> Priorities { get; set; }
    }
    
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserFullName { get; set; }
        [Required]
        public bool IsActive { get; set; }

        //public int? ChiefId { get; set; }
        //[ForeignKey("ChiefId")]
        //public virtual UserProfile RecipChief { get; set; }
        
        //[ForeignKey("ChiefId")]
        //public virtual ICollection<UserProfile> ChiefRecipients { get; set; } 
        
        [ForeignKey("SenderId")]
        public virtual ICollection<Task> SendedTasks { get; set; }
        
        [ForeignKey("RecipientId")]
        public virtual ICollection<Task> RecipTasks { get; set; }

        [ForeignKey("LogUser")]
        public virtual ICollection<TaskEeventLog> Logs { get; set; }
        
        //[ForeignKey("TaskChiefId")]
        //public virtual ICollection<Task> ChiefTasks { get; set; }

        [ForeignKey("AuthorId")]
        public virtual ICollection<Comment> Comments { get; set; }
    }

    [Table("Task")]
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }
        [Required]
        [MaxLength(1000)]
        public string TaskText { get; set; }
        
        [Required]
        public int SenderId { get; set; }
        [ForeignKey("SenderId")]
        public virtual UserProfile TaskSender { get; set; }

        public int? RecipientId { get; set; }
        [ForeignKey("RecipientId")]
        public virtual UserProfile TaskRecipient { get; set; }

        public DateTime? AssignDateTime { get; set; }
        
        //[Required]
        //public int TaskChiefId { get; set; }
        //[Required]
        //[ForeignKey("TaskChiefId")]
        //public virtual UserProfile TaskChief { get; set; }
        
        public DateTime? Deadline { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public DateTime? AcceptCpmpleteDate { get; set; }

        public bool IsRecipientViewed { get; set; }

        public int? PriorityId { get; set; }
        [ForeignKey("PriorityId")]
        public virtual Priority TaskPriority { get; set; }
        
        public string ResultComment { get; set; }

        [ForeignKey("AuthorId")]
        public virtual ICollection<Comment> Comments { get; set; }
        [ForeignKey("TaskLog")]
        public virtual ICollection<TaskEeventLog> TaskEeventLogs { get; set; } 
    }

    [Table("Comment")]
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        [Required]
        [MaxLength(1000)]
        public string CommentText { get; set; }
        [Required]
        public DateTime CommentDate { get; set; }
        
        [Required]
        public int TaskId { get; set; }
        [Required]
        [ForeignKey("TaskId")]
        virtual public Task Task { get; set; }
        
        [Required]
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        virtual public UserProfile Author { get; set; }
    }

    [Table("Priority")]
    public class Priority
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PriorityId { get; set; }
        public string PriorityName { get; set; }
        [ForeignKey("PriorityId")]
        virtual public ICollection<Task> SamePriorityTasks { get; set; }
    }

    public enum PriorityEnum
    {
        High,
        Medium,
        Low
    }

    [Table("TaskEventLog")]
    public class TaskEeventLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskEventLogId { get; set; }
        [Required]
        public DateTime EventDateTime { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("LogUser")]
        public virtual UserProfile User { get; set; }
        [Required]
        public int TaskId { get; set; }
        [ForeignKey("TaskLog")]
        public virtual Task Task { get; set; }
        [Required]
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Введите логин")]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
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