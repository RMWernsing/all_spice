import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Recipe } from "@/models/Recipe.js"
import { AppState } from "@/AppState.js"

class RecipesService {
  async deleteRecipe(recipeId) {
    const response = await api.delete(`api/recipes/${recipeId}`)
    logger.log('you deleted a recipe!', response.data)
    const recipes = AppState.recipes
    const index = recipes.findIndex(recipe => recipe.id == recipeId)
    recipes.splice(index, 1)
  }
  async updateRecipe(recipeData, recipeId) {
    const response = await api.put(`api/recipes/${recipeId}`, { instructions: recipeData })
    // logger.log('here is your new recipe', response.data)
    const recipe = new Recipe(response.data)
    AppState.activeRecipe = recipe
  }
  async getRecipeById(recipeId) {
    AppState.activeRecipe = null
    const response = await api.get(`api/recipes/${recipeId}`)
    // logger.log('here is your selected recipe', response.data)
    const recipe = new Recipe(response.data)
    AppState.activeRecipe = recipe
  }
  async getFavoriteRecipes() {
    const response = await api.get('account/favorites')
    // logger.log('here are your favorite recipes', response.data)
    const recipes = response.data.map(pojo => new Recipe(pojo))
    AppState.recipes = recipes
  }
  async getMyRecipes() {
    const response = await api.get('account/recipes')
    // logger.log('here are your recipes that you made', response.data)
    const recipes = response.data.map(pojo => new Recipe(pojo))
    AppState.recipes = recipes
  }
  async getRecipesBySearch(category) {
    const response = await api.get(`api/recipes?category=${category}`)
    // logger.log('here are your searched recipes', response.data)
    const recipes = response.data.map(pojo => new Recipe(pojo))
    AppState.recipes = recipes
  }
  async createRecipe(recipeData) {
    const response = await api.post('api/recipes', recipeData)
    const recipe = new Recipe(response.data)
    AppState.recipes.push(recipe)
  }
  async getRecipes() {
    const response = await api.get('api/recipes')
    // logger.log('here are your recipes', response.data)
    const recipes = response.data.map(pojo => new Recipe(pojo))
    AppState.recipes = recipes
  }

}

export const recipesService = new RecipesService()