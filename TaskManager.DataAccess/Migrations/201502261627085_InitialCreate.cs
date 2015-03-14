using System.Data.Entity.Migrations;

namespace TaskManager.DataAccess.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        CommentText = c.String(nullable: false, maxLength: 1000),
                        CommentDate = c.DateTime(nullable: false),
                        TaskId = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.UserProfile", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Task", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.TaskId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        UserFullName = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.TaskEventLog",
                c => new
                    {
                        TaskEventLogId = c.Int(nullable: false, identity: true),
                        EventDateTime = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        TaskId = c.Int(nullable: false),
                        PropertyName = c.String(nullable: false),
                        OldValue = c.String(),
                        NewValue = c.String(),
                    })
                .PrimaryKey(t => t.TaskEventLogId)
                .ForeignKey("dbo.Task", t => t.TaskId, cascadeDelete: true)
                .ForeignKey("dbo.UserProfile", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.Task",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        TaskText = c.String(nullable: false, maxLength: 1000),
                        SenderId = c.Int(nullable: false),
                        RecipientId = c.Int(),
                        AssignDateTime = c.DateTime(),
                        Deadline = c.DateTime(),
                        CreateDate = c.DateTime(nullable: false),
                        CompleteDate = c.DateTime(),
                        AcceptCpmpleteDate = c.DateTime(),
                        IsRecipientViewed = c.Boolean(nullable: false),
                        PriorityId = c.Int(),
                        ResultComment = c.String(),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.Priority", t => t.PriorityId)
                .ForeignKey("dbo.UserProfile", t => t.RecipientId)
                .ForeignKey("dbo.UserProfile", t => t.SenderId)
                .Index(t => t.SenderId)
                .Index(t => t.RecipientId)
                .Index(t => t.PriorityId);
            
            CreateTable(
                "dbo.Priority",
                c => new
                    {
                        PriorityId = c.Int(nullable: false, identity: true),
                        PriorityName = c.String(),
                    })
                .PrimaryKey(t => t.PriorityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "TaskId", "dbo.Task");
            DropForeignKey("dbo.TaskEventLog", "UserId", "dbo.UserProfile");
            DropForeignKey("dbo.TaskEventLog", "TaskId", "dbo.Task");
            DropForeignKey("dbo.Task", "SenderId", "dbo.UserProfile");
            DropForeignKey("dbo.Task", "RecipientId", "dbo.UserProfile");
            DropForeignKey("dbo.Task", "PriorityId", "dbo.Priority");
            DropForeignKey("dbo.Comment", "AuthorId", "dbo.UserProfile");
            DropIndex("dbo.Task", new[] { "PriorityId" });
            DropIndex("dbo.Task", new[] { "RecipientId" });
            DropIndex("dbo.Task", new[] { "SenderId" });
            DropIndex("dbo.TaskEventLog", new[] { "TaskId" });
            DropIndex("dbo.TaskEventLog", new[] { "UserId" });
            DropIndex("dbo.Comment", new[] { "AuthorId" });
            DropIndex("dbo.Comment", new[] { "TaskId" });
            DropTable("dbo.Priority");
            DropTable("dbo.Task");
            DropTable("dbo.TaskEventLog");
            DropTable("dbo.UserProfile");
            DropTable("dbo.Comment");
        }
    }
}
