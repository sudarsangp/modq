namespace ModQ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuizModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Faculty = c.String(),
                        Module = c.String(),
                        Question = c.String(),
                        FirstOption = c.String(),
                        SecondOption = c.String(),
                        ThirdOption = c.String(),
                        FourthOption = c.String(),
                        Answer = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.QuizModels");
        }
    }
}
