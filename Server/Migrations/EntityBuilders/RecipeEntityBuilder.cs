using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace GIBS.Module.Recipe.Migrations.EntityBuilders
{
    public class RecipeEntityBuilder : AuditableBaseEntityBuilder<RecipeEntityBuilder>
    {
        private const string _entityTableName = "GIBSRecipe";
        private readonly PrimaryKey<RecipeEntityBuilder> _primaryKey = new("PK_GIBSRecipe", x => x.RecipeId);
        private readonly ForeignKey<RecipeEntityBuilder> _moduleForeignKey = new("FK_GIBSRecipe_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public RecipeEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override RecipeEntityBuilder BuildTable(ColumnsBuilder table)
        {
            RecipeId = AddAutoIncrementColumn(table, "RecipeId");
            ModuleId = AddIntegerColumn(table, "ModuleId", false);
            Name = AddStringColumn(table, "Name", 255, false);
            Description = AddMaxStringColumn(table, "Description", true);
            Instructions = AddMaxStringColumn(table, "Instructions", true);
            Servings = AddIntegerColumn(table, "Servings", true);
            PrepTime = AddStringColumn(table, "PrepTime", 50, true);
            CookTime = AddStringColumn(table, "CookTime", 50, true);
            ImageURL = AddMaxStringColumn(table, "ImageURL", true);
            IsFeatured = AddBooleanColumn(table, "IsFeatured", false);
            IsActive = AddBooleanColumn(table, "IsActive", false);
            Slug = AddStringColumn(table, "Slug", 250, true);
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> RecipeId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
        public OperationBuilder<AddColumnOperation> Description { get; set; }
        public OperationBuilder<AddColumnOperation> Instructions { get; set; }
        public OperationBuilder<AddColumnOperation> Servings { get; set; }
        public OperationBuilder<AddColumnOperation> PrepTime { get; set; }
        public OperationBuilder<AddColumnOperation> CookTime { get; set; }
        public OperationBuilder<AddColumnOperation> ImageURL { get; set; }
        public OperationBuilder<AddColumnOperation> IsFeatured { get; set; }
        public OperationBuilder<AddColumnOperation> IsActive { get; set; }
        public OperationBuilder<AddColumnOperation> Slug { get; set; }
    }
}