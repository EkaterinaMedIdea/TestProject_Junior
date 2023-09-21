namespace ClinicApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAgeColumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PatientCards", "Age", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PatientCards", "Age", c => c.Int(nullable: false));
        }
    }
}
