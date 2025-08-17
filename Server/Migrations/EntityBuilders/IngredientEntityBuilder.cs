using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace GIBS.Module.Recipe.Migrations.EntityBuilders
{
    public class IngredientEntityBuilder : AuditableBaseEntityBuilder<IngredientEntityBuilder>
    {
        private const string _entityTableName = "GIBSRecipe_Ingredient";
        private readonly PrimaryKey<IngredientEntityBuilder> _primaryKey = new("PK_GIBSRecipe_Ingredient", x => x.IngredientId);
        private readonly ForeignKey<IngredientEntityBuilder> _moduleForeignKey = new("FK_GIBSRecipe_Ingredient_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public IngredientEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override IngredientEntityBuilder BuildTable(ColumnsBuilder table)
        {
            IngredientId = AddAutoIncrementColumn(table, "IngredientId");
            ModuleId = AddIntegerColumn(table, "ModuleId", false);
            Name = AddStringColumn(table, "Name", 255, false);
            AddAuditableColumns(table);
            return this;
        }

        public new void Create()
        {
            base.Create();
            AddIndex("IX_GIBSRecipe_Ingredient_Name", "Name", true);
        }

        public OperationBuilder<AddColumnOperation> IngredientId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
    }
}