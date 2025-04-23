<script setup>
import { AppState } from '@/AppState.js';
import { Recipe } from '@/models/Recipe.js';
import { ingredientsService } from '@/services/IngredientsService.js';
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed } from 'vue';

const account = computed(() => AppState.account)

defineProps({
  recipe: { type: Recipe, required: true }
})

async function getRecipeById(recipeId) {
  try {
    await recipesService.getRecipeById(recipeId)
    getIngredientsForRecipe(recipeId)
  }
  catch (error) {
    Pop.error(error, 'Could not get recipe')
    logger.log('COULD NOT GET RECIPE BY ID', error)
  }
}

async function getIngredientsForRecipe(recipeId) {
  try {
    await ingredientsService.getIngredientsForRecipe(recipeId)
  }
  catch (error) {
    Pop.error(error, 'Could not get ingredients')
    logger.error('COULD NOT GET INGREDIENTS', error)
  }
}

async function deleteRecipe(recipeId) {
  try {
    const confirmed = await Pop.confirm('Are You Sure You Want To Delete Your Recipe?')
    if (!confirmed) {
      return
    }
    else {
      await recipesService.deleteRecipe(recipeId)
    }
  }
  catch (error) {
    Pop.error(error, 'Could not delete recipe')
    logger.error('COULD NOT DELETE RECIPE', error)
  }
}
</script>


<template>
  <div class="position-relative">
    <div @click="getRecipeById(recipe.id)" type="button" :title="`see recipe details for ${recipe.title}`"
      data-bs-toggle="modal" data-bs-target="#recipeDetailsModal"
      class="card-style mt-5 d-flex flex-column justify-content-between shadow-lg rounded"
      :style="{ backgroundImage: `url(${recipe.img})` }">
      <div>
        <div class="m-2 d-flex justify-content-between">
          <span class="title-bg-color rounded-pill px-2 text-light">{{ recipe.category }}</span>
          <span class="mdi mdi-heart text-light favorite-bg-color px-1"></span>
        </div>
      </div>
      <div class="m-2 title-bg-color rounded">
        <p class="text-light fs-5 fw-bold ps-3 pt-2">{{ recipe.title }}</p>
      </div>
    </div>
    <button @click="deleteRecipe(recipe.id)" v-if="account?.id == recipe?.creatorId"
      class="btn btn-danger position-absolute" type="button" title="delete recipe"><span
        class="mdi mdi-trash-can"></span></button>
  </div>
</template>


<style lang="scss" scoped>
.card-style {
  min-height: 22rem;
  background-position: center;
  background-size: cover;
}

.title-bg-color {
  background-color: rgba(128, 128, 128, 0.616);
  backdrop-filter: blur(6px);
}

.favorite-bg-color {
  background-color: rgba(128, 128, 128, 0.616);
  backdrop-filter: blur(4px);
  aspect-ratio: 1/1;
  border-radius: 50%;
}

button {
  top: 9px;
  right: 40px;
}
</style>