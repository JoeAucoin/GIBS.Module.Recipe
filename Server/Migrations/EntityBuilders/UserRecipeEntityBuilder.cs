using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace GIBS.Module.Recipe.Migrations.EntityBuilders
{
    public class UserRecipeEntityBuilder : AuditableBaseEntityBuilder<UserRecipeEntityBuilder>
    {
        private const string _entityTableName = "GIBSRecipe_UserRecipe";
        private readonly PrimaryKey<UserRecipeEntityBuilder> _primaryKey = new("PK_GIBSRecipe_UserRecipe", x => x.UserRecipeId);
        private readonly ForeignKey<UserRecipeEntityBuilder> _moduleForeignKey = new("FK_GIBSRecipe_UserRecipe_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.NoAction);
        private readonly ForeignKey<UserRecipeEntityBuilder> _userForeignKey = new("FK_GIBSRecipe_UserRecipe_User", x => x.UserId, "User", "UserId", ReferentialAction.NoAction);
        private readonly ForeignKey<UserRecipeEntityBuilder> _recipeForeignKey = new("FK_GIBSRecipe_UserRecipe_GIBSRecipe", x => x.RecipeId, "GIBSRecipe", "RecipeId", ReferentialAction.Cascade);

        public UserRecipeEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
            ForeignKeys.Add(_userForeignKey);
            ForeignKeys.Add(_recipeForeignKey);
        }

        protected override UserRecipeEntityBuilder BuildTable(ColumnsBuilder table)
        {
            UserRecipeId = AddAutoIncrementColumn(table, "UserRecipeId");
            ModuleId = AddIntegerColumn(table, "ModuleId", false);
            UserId = AddIntegerColumn(table, "UserId");
            RecipeId = AddIntegerColumn(table, "RecipeId");
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> UserRecipeId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> UserId { get; set; }
        public OperationBuilder<AddColumnOperation> RecipeId { get; set; }
    }
}