

using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace all_spice.Repositories;

public class FavoritesRepository
{
  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal FavoriteRecipe CreateFavorite(Favorite favoriteData)
  {
    string sql = @"
    INSERT INTO 
    favorites(recipe_id, account_id)
    VALUES(@RecipeId, @AccountId);

    SELECT 
    favorites.*,
    recipes.*,
    accounts.*
    FROM favorites
    INNER JOIN recipes ON recipes.id = favorites.recipe_id
    INNER JOIN accounts ON accounts.id = recipes.creator_id
    WHERE favorites.id = LAST_INSERT_ID();";
    FavoriteRecipe favoriteRecipe = _db.Query(sql, (Favorite favorite, FavoriteRecipe recipe, Profile account) =>
    {
      recipe.AccountId = favorite.AccountID;
      recipe.FavoriteId = favorite.Id;
      recipe.Creator = account;
      return recipe;
    }, favoriteData).SingleOrDefault();
    return favoriteRecipe;
  }

  internal List<FavoriteRecipe> GetFavoriteRecipeByAccount(string accountId)
  {
    string sql = @"
    SELECT 
    favorites.*,
    recipes.*,
    accounts.*  
    FROM favorites 
    INNER JOIN recipes ON recipes.id = favorites.recipe_id
    INNER JOIN accounts ON accounts.id = favorites.account_id
    WHERE favorites.account_id = @accountId;";

    List<FavoriteRecipe> favoriteRecipes = _db.Query(sql, (Favorite favorite, FavoriteRecipe recipe, Profile account) =>
    {
      recipe.AccountId = favorite.AccountID;
      recipe.FavoriteId = favorite.Id;
      recipe.Creator = account;
      return recipe;
    }, new { accountId }).ToList();
    return favoriteRecipes;
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
    string sql = "SELECT * FROM favorites WHERE favorites.id = @favoriteId";

    Favorite favorite = _db.Query<Favorite>(sql, new { favoriteId }).SingleOrDefault();
    return favorite;
  }

  internal void DeleteFavorite(int favoriteId)
  {
    string sql = "DELETE FROM favorites WHERE id = @favoriteId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { favoriteId });
    if (rowsAffected != 1)
    {
      throw new Exception("YOU HAVE DELETED MORE OR LESS THAN ON ROW. PLEASE CHECK YOUR DATA FOR UNEXPECTED CHANGES");
    }
  }
}