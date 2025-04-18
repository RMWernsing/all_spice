import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Recipe } from "@/models/Recipe.js"
import { AppState } from "@/AppState.js"

class RecipesService {
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