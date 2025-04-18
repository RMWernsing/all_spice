
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
}