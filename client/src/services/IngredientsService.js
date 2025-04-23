import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Ingredient } from "@/models/Ingredient.js"
import { AppState } from "@/AppState.js"

class IngredientsService {
  async createIngredient(ingredientData, recipeId) {
    const response = await api.post('api/ingredients', { name: ingredientData.name, quantity: ingredientData.quantity, recipeId: recipeId })
    // logger.log('here is your new ingredient', response.data)
    const ingredient = new Ingredient(response.data)
    AppState.ingredients.push(ingredient)

  }
  async getIngredientsForRecipe(recipeId) {
    AppState.ingredients = []
    const response = await api.get(`api/recipes/${recipeId}/ingredients`)
    // logger.log('here are your ingredients', response.data)
    const ingredients = response.data.map(pojo => new Ingredient(pojo))
    AppState.ingredients = ingredients
  }

}

export const ingredientsService = new IngredientsService()