namespace all_spice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
  public RecipesController(Auth0Provider auth0Provider, RecipesService recipesService)
  {
    _auth0Provider = auth0Provider;
    _recipesService = recipesService;
  }
  private readonly Auth0Provider _auth0Provider;
  private readonly RecipesService _recipesService;


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
      throw new Exception(error.Message);
    }
  }
}