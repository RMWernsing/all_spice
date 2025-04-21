


namespace all_spice.Services;

public class IngredientsService
{
  public IngredientsService(IngredientsRepository repository, RecipesService recipesService)
  {
    _repository = repository;
    _recipesService = recipesService;
  }
  private readonly IngredientsRepository _repository;
  private readonly RecipesService _recipesService;

  internal Ingredient CreateIngredient(Ingredient ingredientData, Account userInfo)
  {
    Recipe recipe = _recipesService.GetRecipeById(ingredientData.RecipeId);
    if (recipe.CreatorId != userInfo.Id)
    {
      throw new Exception("YOU CANNOT ADD AN INGREDIENT TO SOMEONE ELSES RECIPE");
    }
    Ingredient ingredient = _repository.CreateIngredient(ingredientData);
    return ingredient;
  }

  internal List<Ingredient> GetIngredientsForRecipe(int recipeId)
  {
    List<Ingredient> ingredients = _repository.GetIngredientsForRecipe(recipeId);
    return ingredients;
  }

  private Ingredient GetIngredientById(int ingredientId)
  {
    Ingredient ingredient = _repository.GetIngredientById(ingredientId);
    if (ingredient == null)
    {
      throw new Exception($"The id {ingredientId} does not exist in the database");
    }
    return ingredient;
  }

  internal void DeleteIngredient(int ingredientId, Account userInfo)
  {
    Ingredient ingredient = GetIngredientById(ingredientId);
    Recipe recipe = _recipesService.GetRecipeById(ingredient.RecipeId);
    if (recipe.CreatorId != userInfo.Id)
    {
      throw new Exception("YOU CANNOT DELETE ANOTHER PERSONS INGREDIENT");
    }

    // if (ingredient.Recipe.CreatorId != userInfo.Id)
    // {
    //   throw new Exception("YOU CANNOT DELETE ANOTHER PERSONS INGREDIENT");
    // }
    _repository.DeleteIngredient(ingredientId);
  }
}