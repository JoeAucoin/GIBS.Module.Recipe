using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace GIBS.Module.Recipe.Migrations.EntityBuilders
{
    public class RecipeCategoryEntityBuilder : AuditableBaseEntityBuilder<RecipeCategoryEntityBuilder>
    {
        private const string _entityTableName = "GIBSRecipeCategory";
        private readonly PrimaryKey<RecipeCategoryEntityBuilder> _primaryKey = new("PK_GIBSRecipeCategory", x => x.RecipeCategoryId);
        private readonly ForeignKey<RecipeCategoryEntityBuilder> _moduleForeignKey = new("FK_GIBSRecipeCategory_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.NoAction);
        private readonly ForeignKey<RecipeCategoryEntityBuilder> _recipeForeignKey = new("FK_GIBSRecipeCategory_GIBSRecipe", x => x.RecipeId, "GIBSRecipe", "RecipeId", ReferentialAction.Cascade);
        private readonly ForeignKey<RecipeCategoryEntityBuilder> _categoryForeignKey = new("FK_GIBSRecipeCategory_GIBSCategory", x => x.CategoryId, "GIBSRecipe_Category", "CategoryId", ReferentialAction.NoAction);

        public RecipeCategoryEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
            ForeignKeys.Add(_recipeForeignKey);
            ForeignKeys.Add(_categoryForeignKey);
        }

        protected override RecipeCategoryEntityBuilder BuildTable(ColumnsBuilder table)
        {
            RecipeCategoryId = AddAutoIncrementColumn(table, "RecipeCategoryId");
            ModuleId = AddIntegerColumn(table, "ModuleId", false);
            RecipeId = AddIntegerColumn(table, "RecipeId");
            CategoryId = AddIntegerColumn(table, "CategoryId");
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> RecipeCategoryId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> RecipeId { get; set; }
        public OperationBuilder<AddColumnOperation> CategoryId { get; set; }
    }
}