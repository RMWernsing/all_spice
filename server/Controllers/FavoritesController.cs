namespace all_spice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
  public FavoritesController(FavoritesService favoritesService, Auth0Provider auth0Provider)
  {
    _favoritesService = favoritesService;
    _auth0Provider = auth0Provider;
  }
  private readonly FavoritesService _favoritesService;
  private readonly Auth0Provider _auth0Provider;

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<FavoriteRecipe>> CreateFavorite([FromBody] Favorite favoriteData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      favoriteData.AccountID = userInfo.Id;
      FavoriteRecipe favoriteRecipe = _favoritesService.CreateFavorite(favoriteData);
      return favoriteRecipe;
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

  [HttpDelete("{favoriteId}")]
  [Authorize]
  public async Task<ActionResult<String>> DeleteFavorite(int favoriteId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      _favoritesService.DeleteFavorite(favoriteId, userInfo);
      return "You have deleted your favorite recipe";
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

}