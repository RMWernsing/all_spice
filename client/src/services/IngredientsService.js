import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Ingredient } from "@/models/Ingredient.js"
import { AppState } from "@/AppState.js"

class IngredientsService {
  async getIngredientsForRecipe(recipeId) {
    AppState.ingredients = []
    const response = await api.get(`api/recipes/${recipeId}/ingredients`)
    // logger.log('here are your ingredients', response.data)
    const ingredients = response.data.map(pojo => new Ingredient(pojo))
    AppState.ingredients = ingredients
  }

}

export const ingredientsService = new IngredientsService()