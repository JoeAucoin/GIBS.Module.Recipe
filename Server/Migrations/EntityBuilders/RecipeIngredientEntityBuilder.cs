using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace GIBS.Module.Recipe.Migrations.EntityBuilders
{
    public class RecipeIngredientEntityBuilder : AuditableBaseEntityBuilder<RecipeIngredientEntityBuilder>
    {
        private const string _entityTableName = "GIBSRecipeIngredient";
        private readonly PrimaryKey<RecipeIngredientEntityBuilder> _primaryKey = new("PK_GIBSRecipeIngredient", x => x.RecipeIngredientId);
        private readonly ForeignKey<RecipeIngredientEntityBuilder> _moduleForeignKey = new("FK_GIBSRecipeIngredient_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.NoAction);
        private readonly ForeignKey<RecipeIngredientEntityBuilder> _recipeForeignKey = new("FK_GIBSRecipeIngredient_Recipe", x => x.RecipeId, "GIBSRecipe", "RecipeId", ReferentialAction.Cascade);
        private readonly ForeignKey<RecipeIngredientEntityBuilder> _ingredientForeignKey = new("FK_GIBSRecipeIngredient_Ingredient", x => x.IngredientId, "GIBSRecipe_Ingredient", "IngredientId", ReferentialAction.NoAction);
        private readonly ForeignKey<RecipeIngredientEntityBuilder> _unitForeignKey = new("FK_GIBSRecipeIngredient_Unit", x => x.UnitId, "GIBSRecipe_Unit", "UnitId", ReferentialAction.NoAction);

        public RecipeIngredientEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
            ForeignKeys.Add(_recipeForeignKey);
            ForeignKeys.Add(_ingredientForeignKey);
            ForeignKeys.Add(_unitForeignKey);
        }

        protected override RecipeIngredientEntityBuilder BuildTable(ColumnsBuilder table)
        {
            RecipeIngredientId = AddAutoIncrementColumn(table, "RecipeIngredientId");
            ModuleId = AddIntegerColumn(table, "ModuleId", false);
            RecipeId = AddIntegerColumn(table, "RecipeId");
            IngredientId = AddIntegerColumn(table, "IngredientId");
            Quantity = AddDecimalColumn(table, "Quantity", 18, 2);
            UnitId = AddIntegerColumn(table, "UnitId");
            Notes = AddMaxStringColumn(table, "Notes", true);
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> RecipeIngredientId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> RecipeId { get; set; }
        public OperationBuilder<AddColumnOperation> IngredientId { get; set; }
        public OperationBuilder<AddColumnOperation> Quantity { get; set; }
        public OperationBuilder<AddColumnOperation> UnitId { get; set; }
        public OperationBuilder<AddColumnOperation> Notes { get; set; }
    }
}