using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace GIBS.Module.Recipe.Migrations.EntityBuilders
{
    public class RatingsReviewEntityBuilder : AuditableBaseEntityBuilder<RatingsReviewEntityBuilder>
    {
        private const string _entityTableName = "GIBSRecipe_RatingsReview";
        private readonly PrimaryKey<RatingsReviewEntityBuilder> _primaryKey = new("PK_GIBSRecipe_RatingsReview", x => x.RatingsReviewId);
        private readonly ForeignKey<RatingsReviewEntityBuilder> _moduleForeignKey = new("FK_GIBSRecipe_RatingsReview_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.NoAction);
        private readonly ForeignKey<RatingsReviewEntityBuilder> _userForeignKey = new("FK_GIBSRecipe_RatingsReview_User", x => x.UserId, "User", "UserId", ReferentialAction.NoAction);
        private readonly ForeignKey<RatingsReviewEntityBuilder> _recipeForeignKey = new("FK_GIBSRecipe_RatingsReview_GIBSRecipe", x => x.RecipeId, "GIBSRecipe", "RecipeId", ReferentialAction.Cascade);
        private readonly MigrationBuilder _migrationBuilder;

        public RatingsReviewEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            _migrationBuilder = migrationBuilder;
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
            ForeignKeys.Add(_userForeignKey);
            ForeignKeys.Add(_recipeForeignKey);
        }

        protected override RatingsReviewEntityBuilder BuildTable(ColumnsBuilder table)
        {
            RatingsReviewId = AddAutoIncrementColumn(table, "RatingsReviewId");
            ModuleId = AddIntegerColumn(table, "ModuleId", true);
            UserId = AddIntegerColumn(table, "UserId");
            RecipeId = AddIntegerColumn(table, "RecipeId");
            Rating = AddIntegerColumn(table, "Rating", true);
            ReviewText = AddMaxStringColumn(table, "ReviewText", true);
            AddAuditableColumns(table);
            return this;
        }

        public new void Create()
        {
            base.Create();
            _migrationBuilder.AddCheckConstraint("CK_GIBSRecipe_RatingsReview_Rating", EntityTableName, "[Rating] >= 1 AND [Rating] <= 5");
        }

        public OperationBuilder<AddColumnOperation> RatingsReviewId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> UserId { get; set; }
        public OperationBuilder<AddColumnOperation> RecipeId { get; set; }
        public OperationBuilder<AddColumnOperation> Rating { get; set; }
        public OperationBuilder<AddColumnOperation> ReviewText { get; set; }
    }
}