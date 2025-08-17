using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace GIBS.Module.Recipe.Migrations.EntityBuilders
{
    public class RecipeTagEntityBuilder : AuditableBaseEntityBuilder<RecipeTagEntityBuilder>
    {
        private const string _entityTableName = "GIBSRecipeTag";
        private readonly PrimaryKey<RecipeTagEntityBuilder> _primaryKey = new("PK_GIBSRecipeTag", x => x.RecipeTagId);
        private readonly ForeignKey<RecipeTagEntityBuilder> _moduleForeignKey = new("FK_GIBSRecipeTag_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.NoAction);
        private readonly ForeignKey<RecipeTagEntityBuilder> _recipeForeignKey = new("FK_GIBSRecipeTag_GIBSRecipe", x => x.RecipeId, "GIBSRecipe", "RecipeId", ReferentialAction.Cascade);
        private readonly ForeignKey<RecipeTagEntityBuilder> _tagForeignKey = new("FK_GIBSRecipeTag_GIBSTag", x => x.TagId, "GIBSRecipe_Tag", "TagId", ReferentialAction.NoAction);

        public RecipeTagEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
            ForeignKeys.Add(_recipeForeignKey);
            ForeignKeys.Add(_tagForeignKey);
        }

        protected override RecipeTagEntityBuilder BuildTable(ColumnsBuilder table)
        {
            RecipeTagId = AddAutoIncrementColumn(table, "RecipeTagId");
            ModuleId = AddIntegerColumn(table, "ModuleId", false);
            RecipeId = AddIntegerColumn(table, "RecipeId");
            TagId = AddIntegerColumn(table, "TagId");
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> RecipeTagId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> RecipeId { get; set; }
        public OperationBuilder<AddColumnOperation> TagId { get; set; }
    }
}