using GIBS.Module.Recipe.Migrations.EntityBuilders;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace GIBS.Module.Recipe.Migrations.EntityBuilders
{
    public class StepsEntityBuilder : AuditableBaseEntityBuilder<StepsEntityBuilder>
    {
        private const string _entityTableName = "GIBSRecipe_Step";
        private readonly PrimaryKey<StepsEntityBuilder> _primaryKey = new("PK_GIBSRecipe_Step", x => x.StepId);
        private readonly ForeignKey<StepsEntityBuilder> _moduleForeignKey = new("FK_GIBSRecipe_Step_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.NoAction);
        private readonly ForeignKey<StepsEntityBuilder> _recipeForeignKey = new("FK_GIBSRecipe_Step_Recipe", x => x.RecipeId, "GIBSRecipe", "RecipeId", ReferentialAction.Cascade);

        public StepsEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
            ForeignKeys.Add(_recipeForeignKey);
        }

        protected override StepsEntityBuilder BuildTable(ColumnsBuilder table)
        {
            StepId = AddAutoIncrementColumn(table, "StepId");
            ModuleId = AddIntegerColumn(table, "ModuleId", false);
            RecipeId = AddIntegerColumn(table, "RecipeId");
            Name = AddStringColumn(table, "Name", 50, false);
            Instructions = AddMaxStringColumn(table, "Instructions", false);
            ImageURL = AddMaxStringColumn(table, "ImageURL", true);
            AddAuditableColumns(table);
            return this;
        }


        public OperationBuilder<AddColumnOperation> StepId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> RecipeId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
        public OperationBuilder<AddColumnOperation> Instructions { get; set; }
        public OperationBuilder<AddColumnOperation> ImageURL { get; set; }
    }
}