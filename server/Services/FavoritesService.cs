


namespace all_spice.Services;

public class FavoritesService
{
  public FavoritesService(FavoritesRepository repository)
  {
    _repository = repository;
  }
  private readonly FavoritesRepository _repository;

  internal FavoriteRecipe CreateFavorite(Favorite favoriteData)
  {
    FavoriteRecipe favoriteRecipe = _repository.CreateFavorite(favoriteData);
    return favoriteRecipe;
  }

  internal List<FavoriteRecipe> GetFavoriteRecipeByAccount(string accountId)
  {
    List<FavoriteRecipe> favoriteRecipes = _repository.GetFavoriteRecipeByAccount(accountId);
    return favoriteRecipes;
  }

  private Favorite GetFavoriteById(int favoriteId)
  {
    Favorite favorite = _repository.GetFavoriteById(favoriteId);
    if (favorite == null)
    {
      throw new Exception("Could not find favorite with the id of " + favoriteId);
    }
    return favorite;
  }

  internal void DeleteFavorite(int favoriteId, Account userInfo)
  {
    Favorite favorite = GetFavoriteById(favoriteId);
    if (favorite.AccountID != userInfo.Id)
    {
      throw new Exception("YOU CANNOT DELETE ANOTHER USERS FAVORITE");
    }
    _repository.DeleteFavorite(favoriteId);
  }
}