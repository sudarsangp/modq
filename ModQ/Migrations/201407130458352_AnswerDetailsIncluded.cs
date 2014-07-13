namespace ModQ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnswerDetailsIncluded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuizModels", "AnswerDetails", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuizModels", "AnswerDetails");
        }
    }
}
