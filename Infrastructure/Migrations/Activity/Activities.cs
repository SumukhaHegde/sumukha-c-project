using FluentMigrator;

namespace Infrastructure.Migrations.Activity
{
    public class Activities : Migration
    {
        private const string activities = "Activities";
        public override void Down()
        {
            Delete.Table(activities);
        }

        public override void Up()
        {
            if (!Schema.Schema("public").Table(activities).Exists())
            {
                Create.Table(activities).WithColumn("Id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("ActivityName").AsString()
                    .WithColumn("ActivityDescription").AsString()
                    .WithColumn("ActivityDate").AsDateTime()
                    .WithColumn("Category").AsString()
                    .WithColumn("City").AsString()
                    .WithColumn("Venue").AsString();
            }
        }
    }
}
