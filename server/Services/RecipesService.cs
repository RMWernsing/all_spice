



namespace all_spice.Services;

public class RecipesService
{
  public RecipesService(RecipesRepository repository)
  {
    _repository = repository;
  }
  private readonly RecipesRepository _repository;

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    Recipe recipe = _repository.CreateRecipe(recipeData);
    return recipe;
  }

  internal List<Recipe> GetAllRecipes()
  {
    List<Recipe> recipes = _repository.GetAllRecipes();
    return recipes;
  }

  internal List<Recipe> GetAllRecipes(string category)
  {
    List<Recipe> recipes = _repository.GetRecipesByCategory(category);
    return recipes;
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    Recipe recipe = _repository.GetRecipeById(recipeId);
    if (recipe == null)
    {
      throw new Exception($"{recipeId} Id does not exist on an existing recipe");
    }
    return recipe;
  }

  internal Recipe UpdateRecipe(Recipe recipeData, int recipeId, Account userInfo)
  {
    Recipe recipe = GetRecipeById(recipeId);
    if (recipe.CreatorId != userInfo.Id)
    {
      throw new Exception($"YOU DO NOT HAVE ACCESS TO UPDATE SOMEONE ELSES RECIPE {userInfo.Name.ToUpper()}");
    }
    recipe.Title = recipeData.Title ?? recipe.Title;
    recipe.Instructions = recipeData.Instructions ?? recipe.Instructions;
    recipe.Img = recipeData.Img ?? recipe.Img;
    recipe.Category = recipeData.Category ?? recipe.Category;

    _repository.UpdateRecipe(recipe);
    return recipe;
  }

  internal void DeleteRecipe(int recipeId, Account userInfo)
  {
    Recipe recipe = GetRecipeById(recipeId);
    if (recipe.CreatorId != userInfo.Id)
    {
      throw new Exception($"YOU DO NOT HAVE PERMISSION TO DELETE THIS POST {userInfo.Name.ToUpper()}");
    }
    _repository.DeleteRecipe(recipeId);
  }

  internal List<Recipe> GetRecipeByAccount(string accountId)
  {
    List<Recipe> recipes = _repository.GetRecipeByAccount(accountId);
    return recipes;
  }
}