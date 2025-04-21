<script setup>
import { AppState } from '@/AppState.js';
import CreateRecipeModal from '@/components/CreateRecipeModal.vue';
import RecipeCard from '@/components/RecipeCard.vue';
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted, ref } from 'vue';

onMounted(() => {
  getRecipes()
})
const recipes = computed(() => AppState.recipes)
const account = computed(() => AppState.account)

const editableSearchData = ref('')

const recipesType = ref('all')

async function getRecipes() {
  try {
    await recipesService.getRecipes()
  }
  catch (error) {
    Pop.error(error, "Could not get recipes")
    logger.error("COULD NOT GET RECIPES", error)
  }
}

async function getRecipesBySearch() {
  try {
    await recipesService.getRecipesBySearch(editableSearchData.value)
  }
  catch (error) {
    Pop.error(error, "Could not get recipes by search query")
    logger.error("COULD NOT GET RECIPES BY SEARCH QUERY", error)
  }
}

async function getMyRecipes() {
  try {
    await recipesService.getMyRecipes()
  }
  catch (error) {
    Pop.error(error, "Could not get your recipes")
    logger.error("COULD NOT GET YOUR RECIPES", error)
  }
}

async function getFavoriteRecipes() {
  try {
    await recipesService.getFavoriteRecipes()
  }
  catch (error) {
    Pop.error(error, "Could not get your favorite recipes")
    logger.error("COULD NOT GET YOUR FAVORITE RECIPES", error)
  }
}
</script>

<template>
  <section class="container-fluid position-relative">
    <div class="row spice-bg justify-content-end">
      <div class="col-md-3 mt-3 px-2">
        <form @submit.prevent="getRecipesBySearch()" class="d-flex" role="search">
          <input v-model="editableSearchData" class="form-control me-2" type="search" placeholder="Search By Category"
            aria-label="Search">
          <button class="btn btn-outline-dark" type="submit" title="search">Search</button>
        </form>
      </div>
      <div class="col-12">
        <div class="row align-items-center">
          <div class="col-12">
            <h1 class="text-light text-center text-shadow">All Spice</h1>
            <p class="text-light text-center text-shadow fs-5">Cherish Your Family And Their Cooking</p>
          </div>
        </div>
      </div>
    </div>
    <div v-if="account" class="row justify-content-center m-0 ">
      <div class="col-sm-10 col-md-5 position-absolute button-position">
        <div class="bg-light d-flex justify-content-between rounded p-3 shadow px-4 text-success fw-bold">
          <p @click="getRecipes()" type="button" title="Home" :class="{ 'text-danger': recipesType == 'all' }">Home</p>
          <p @click="getMyRecipes()" type="button" title="show my recipes">My Recipes</p>
          <p @click="getFavoriteRecipes()" type="button" title="show my favorite">Favorites</p>
        </div>
      </div>
    </div>
  </section>
  <section class="container">
    <div class="row mt-5">
      <div v-for="recipe in recipes" :key="recipe.id" class="col-md-4">
        <RecipeCard :recipe="recipe" />
      </div>
    </div>
  </section>
  <button v-if="account" type="button" class="btn btn-success fixed-button" data-bs-toggle="modal"
    data-bs-target="#createRecipe">+
    Recipe</button>
  <CreateRecipeModal />
</template>

<style scoped lang="scss">
.spice-bg {
  background-image: url(https://images.unsplash.com/photo-1502741384106-56538427cde9?q=80&w=2069&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D);
  background-position: center;
  background-size: cover;
  min-height: 40rem;
}

.text-shadow {
  text-shadow: 1px 1px 5px black;
}

.fixed-button {
  position: fixed;
  top: 94dvh;
  right: 20px;
}

.button-position {
  top: 37.5rem;
}
</style>
