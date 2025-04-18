using System.ComponentModel;
using Microsoft.AspNetCore.Http.HttpResults;

namespace all_spice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
  public RecipesController(Auth0Provider auth0Provider, RecipesService recipesService, IngredientsService ingredientsService)
  {
    _auth0Provider = auth0Provider;
    _recipesService = recipesService;
    _ingredientsService = ingredientsService;
  }
  private readonly Auth0Provider _auth0Provider;
  private readonly RecipesService _recipesService;
  private readonly IngredientsService _ingredientsService;


  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      recipeData.CreatorId = userInfo.Id;
      Recipe recipe = _recipesService.CreateRecipe(recipeData);
      return Ok(recipe);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

  [HttpGet]
  public ActionResult<List<Recipe>> GetRecipes([FromQuery] string category)
  {
    try
    {
      List<Recipe> recipes;
      if (category == null)
      {
        recipes = _recipesService.GetAllRecipes();
      }
      else
      {
        recipes = _recipesService.GetAllRecipes(category);
      }
      return Ok(recipes);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

  [HttpGet("{recipeId}")]
  public ActionResult<Recipe> GetRecipeById(int recipeId)
  {
    try
    {
      Recipe recipe = _recipesService.GetRecipeById(recipeId);
      return Ok(recipe);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

  [HttpPut("{recipeId}")]
  [Authorize]
  public async Task<ActionResult<Recipe>> UpdateRecipe([FromBody] Recipe recipeData, int recipeId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Recipe recipe = _recipesService.UpdateRecipe(recipeData, recipeId, userInfo);
      return Ok(recipe);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

  [HttpDelete("{recipeId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteRecipe(int recipeId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      _recipesService.DeleteRecipe(recipeId, userInfo);
      return "You have successfully deleted your recipe!";
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

  [HttpGet("{recipeId}/ingredients")]
  public ActionResult<List<Ingredient>> GetIngredientsForRecipe(int recipeId)
  {
    try
    {
      List<Ingredient> ingredients = _ingredientsService.GetIngredientsForRecipe(recipeId);
      return ingredients;
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }
}