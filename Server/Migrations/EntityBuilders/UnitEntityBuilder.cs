using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace GIBS.Module.Recipe.Migrations.EntityBuilders
{
    public class UnitEntityBuilder : AuditableBaseEntityBuilder<UnitEntityBuilder>
    {
        private const string _entityTableName = "GIBSRecipe_Unit";
        private readonly PrimaryKey<UnitEntityBuilder> _primaryKey = new("PK_GIBSRecipe_Unit", x => x.UnitId);
        private readonly ForeignKey<UnitEntityBuilder> _moduleForeignKey = new("FK_GIBSRecipe_Unit_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public UnitEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override UnitEntityBuilder BuildTable(ColumnsBuilder table)
        {
            UnitId = AddAutoIncrementColumn(table, "UnitId");
            ModuleId = AddIntegerColumn(table, "ModuleId", false);
            Name = AddStringColumn(table, "Name", 50, false);
            AddAuditableColumns(table);
            return this;
        }

        public new void Create()
        {
            base.Create();
            AddIndex("IX_GIBSRecipe_Unit_Name", "Name", true);
        }

        public OperationBuilder<AddColumnOperation> UnitId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
    }
}