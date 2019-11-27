namespace ESearch.DAL.Domain
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRecordtimestamp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QueryResults", "RecordTimeStamp", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QueryResults", "RecordTimeStamp");
        }
    }
}
