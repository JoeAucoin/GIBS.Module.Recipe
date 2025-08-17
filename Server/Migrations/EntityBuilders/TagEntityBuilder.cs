using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace GIBS.Module.Recipe.Migrations.EntityBuilders
{
    public class TagEntityBuilder : AuditableBaseEntityBuilder<TagEntityBuilder>
    {
        private const string _entityTableName = "GIBSRecipe_Tag";
        private readonly PrimaryKey<TagEntityBuilder> _primaryKey = new("PK_GIBSRecipe_Tag", x => x.TagId);
        private readonly ForeignKey<TagEntityBuilder> _moduleForeignKey = new("FK_GIBSRecipe_Tag_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public TagEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override TagEntityBuilder BuildTable(ColumnsBuilder table)
        {
            TagId = AddAutoIncrementColumn(table, "TagId");
            ModuleId = AddIntegerColumn(table, "ModuleId", false);
            Name = AddStringColumn(table, "Name", 255, false);
            AddAuditableColumns(table);
            return this;
        }

        public new void Create()
        {
            base.Create();
            AddIndex("IX_GIBSRecipe_Tag_Name", "Name", true);
        }

        public OperationBuilder<AddColumnOperation> TagId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
    }
}