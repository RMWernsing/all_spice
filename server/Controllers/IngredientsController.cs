namespace all_spice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
  public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0Provider)
  {
    _ingredientsService = ingredientsService;
    _auth0Provider = auth0Provider;
  }
  private readonly IngredientsService _ingredientsService;
  private readonly Auth0Provider _auth0Provider;

  [HttpPost]
  public ActionResult<Ingredient> CreateIngredient([FromBody] Ingredient ingredientData)
  {
    try
    {
      Ingredient ingredient = _ingredientsService.CreateIngredient(ingredientData);
      return Ok(ingredient);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

  [HttpDelete("{ingredientId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteIngredient(int ingredientId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      _ingredientsService.DeleteIngredient(ingredientId, userInfo);
      return "You deleted the ingredient";
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }
}