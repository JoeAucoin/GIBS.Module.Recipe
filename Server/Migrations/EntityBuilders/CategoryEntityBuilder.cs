using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace GIBS.Module.Recipe.Migrations.EntityBuilders
{
    public class CategoryEntityBuilder : AuditableBaseEntityBuilder<CategoryEntityBuilder>
    {
        private const string _entityTableName = "GIBSRecipe_Category";
        private readonly PrimaryKey<CategoryEntityBuilder> _primaryKey = new("PK_GIBSRecipe_Category", x => x.CategoryId);
        private readonly ForeignKey<CategoryEntityBuilder> _moduleForeignKey = new("FK_GIBSRecipe_Category_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public CategoryEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override CategoryEntityBuilder BuildTable(ColumnsBuilder table)
        {
            CategoryId = AddAutoIncrementColumn(table, "CategoryId");
            ModuleId = AddIntegerColumn(table, "ModuleId", false);
            Name = AddStringColumn(table, "Name", 255, false);
            Slug = AddStringColumn(table, "Slug", 250, true);
            AddAuditableColumns(table);
            return this;
        }

        public new void Create()
        {
            base.Create();
            AddIndex("IX_GIBSRecipe_Category_Name", "Name", true);
        }

        public OperationBuilder<AddColumnOperation> CategoryId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
        public OperationBuilder<AddColumnOperation> Slug { get; set; }
    }
}