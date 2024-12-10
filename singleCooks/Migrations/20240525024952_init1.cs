using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace singleCooks.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UserId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    RecipeName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PreparationTime = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CookingTime = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Servings = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    OverallRating = table.Column<double>(type: "BINARY_DOUBLE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.RecipeId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TagName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    RegisteredUserId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Username = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Bio = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "RAW(2000)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "RAW(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.RegisteredUserId);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IngredientName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    RecipeId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.IngredientId);
                    table.ForeignKey(
                        name: "FK_Ingredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId");
                });

            migrationBuilder.CreateTable(
                name: "Instructions",
                columns: table => new
                {
                    InstructionId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Instructions = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    RecipeId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructions", x => x.InstructionId);
                    table.ForeignKey(
                        name: "FK_Instructions_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId");
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    RatingId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    RatingValue = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    RecipeId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.RatingId);
                    table.ForeignKey(
                        name: "FK_Rating_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId");
                });

            migrationBuilder.CreateTable(
                name: "RecipeTag",
                columns: table => new
                {
                    RecipeTagsTagId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    RecipesRecipeId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeTag", x => new { x.RecipeTagsTagId, x.RecipesRecipeId });
                    table.ForeignKey(
                        name: "FK_RecipeTag_Recipes_RecipesRecipeId",
                        column: x => x.RecipesRecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeTag_Tags_RecipeTagsTagId",
                        column: x => x.RecipeTagsTagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeRegisteredUser",
                columns: table => new
                {
                    FavoritedByRegisteredUserId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    userRecipesRecipeId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeRegisteredUser", x => new { x.FavoritedByRegisteredUserId, x.userRecipesRecipeId });
                    table.ForeignKey(
                        name: "FK_RecipeRegisteredUser_Recipes_userRecipesRecipeId",
                        column: x => x.userRecipesRecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeRegisteredUser_Users_FavoritedByRegisteredUserId",
                        column: x => x.FavoritedByRegisteredUserId,
                        principalTable: "Users",
                        principalColumn: "RegisteredUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFavouriteRecipes",
                columns: table => new
                {
                    UserFavoritesId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UserId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    RecipeLikedId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    RegisteredUserId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavouriteRecipes", x => x.UserFavoritesId);
                    table.ForeignKey(
                        name: "FK_UserFavouriteRecipes_Users_RegisteredUserId",
                        column: x => x.RegisteredUserId,
                        principalTable: "Users",
                        principalColumn: "RegisteredUserId");
                });

            migrationBuilder.CreateTable(
                name: "Nutrient",
                columns: table => new
                {
                    NutrientId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Amount = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    IngredientId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nutrient", x => x.NutrientId);
                    table.ForeignKey(
                        name: "FK_Nutrient_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId");
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    RecipeIngredientId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    RecipeId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IngredientId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Quantity = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredients", x => x.RecipeIngredientId);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_RecipeId",
                table: "Instructions",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Nutrient_IngredientId",
                table: "Nutrient",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_RecipeId",
                table: "Rating",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_IngredientId",
                table: "RecipeIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_RecipeId",
                table: "RecipeIngredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeRegisteredUser_userRecipesRecipeId",
                table: "RecipeRegisteredUser",
                column: "userRecipesRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTag_RecipesRecipeId",
                table: "RecipeTag",
                column: "RecipesRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavouriteRecipes_RegisteredUserId",
                table: "UserFavouriteRecipes",
                column: "RegisteredUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instructions");

            migrationBuilder.DropTable(
                name: "Nutrient");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "RecipeRegisteredUser");

            migrationBuilder.DropTable(
                name: "RecipeTag");

            migrationBuilder.DropTable(
                name: "UserFavouriteRecipes");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
