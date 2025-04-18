



namespace all_spice.Repositories;

public class IngredientsRepository
{
  public IngredientsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    string sql = @"
    INSERT INTO 
    ingredients(name, quantity, recipe_id)
    VALUES(@Name, @Quantity, @RecipeId);

    SELECT * FROM ingredients WHERE id = LAST_INSERT_ID();
    ";

    Ingredient ingredient = _db.Query<Ingredient>(sql, ingredientData).SingleOrDefault();
    return ingredient;
  }

  internal List<Ingredient> GetIngredientsForRecipe(int recipeId)
  {
    string sql = "SELECT * FROM ingredients WHERE recipe_id = @recipeId;";

    List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new { recipeId }).ToList();
    return ingredients;
  }

  internal Ingredient GetIngredientById(int ingredientId)
  {
    string sql = "SELECT * FROM ingredients WHERE id = @ingredientId";

    Ingredient ingredient = _db.Query<Ingredient>(sql, new { ingredientId }).SingleOrDefault();
    return ingredient;
  }

  internal void DeleteIngredient(int ingredientId)
  {
    string sql = "DELETE FROM ingredients WHERE id = @ingredientId;";

    int rowsAffected = _db.Execute(sql, new { ingredientId });
    if (rowsAffected != 1)
    {
      throw new Exception("YOU HAVE DELETED THE WRONG NUMBER OF ROWS FROM YOUR DATABASE. CHECK YOUR DATA FOR BAD DATA");
    }
  }
}