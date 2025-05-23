namespace all_spice.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly FavoritesService _favoritesService;
  private readonly RecipesService _recipesService;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, FavoritesService favoritesService, RecipesService recipesService = null)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _favoritesService = favoritesService;
    _recipesService = recipesService;
  }

  [HttpGet]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateAccount(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("favorites")]
  public async Task<ActionResult<List<FavoriteRecipe>>> GetFavoriteRecipeByAccount()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<FavoriteRecipe> favoriteRecipes = _favoritesService.GetFavoriteRecipeByAccount(userInfo.Id);
      return Ok(favoriteRecipes);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("recipes")]
  [Authorize]
  public async Task<ActionResult<List<Recipe>>> GetRecipeByAccount()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Recipe> recipes = _recipesService.GetRecipeByAccount(userInfo.Id);
      return recipes;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}
