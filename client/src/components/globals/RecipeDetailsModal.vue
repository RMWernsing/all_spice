<script setup>
import { AppState } from '@/AppState.js';
import { ingredientsService } from '@/services/IngredientsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';


const recipe = computed(() => AppState.activeRecipe)
const ingredients = computed(() => AppState.ingredients)

</script>


<template>
  <div class="modal fade" id="recipeDetailsModal" tabindex="-1" aria-labelledby="recipeDetailsModalLabel"
    aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <!-- <div class="modal-header">
          <h1 class="modal-title fs-5" id="recipeDetailsModalLabel">{{ recipe?.title }}</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div> -->
        <div class="modal-body">
          <div class="container">
            <div class="row">
              <div class="col-md-5">
                <img :src="recipe?.img" :alt="`an image of ${recipe?.title}`">
              </div>
              <div class="col-md-7">
                <div class="d-flex align-items-center gap-4">
                  <h1 class="text-success fs-2">{{ recipe?.title }}</h1>
                  <span class="bg-grey rounded-pill px-2 text-light">{{ recipe?.category }}</span>
                </div>
                <div class="row justify-content-between mt-4">
                  <div class="col-md-6">
                    <div class="shadow rounded-3 ingredient-card-bg">
                      <h2 class="fs-4 text-center bg-success p-2 text-light rounded-top-3">Recipe Instructions</h2>
                      <p v-if="recipe?.instructions" class="px-2 pb-3">{{ recipe?.instructions }}</p>
                      <p v-else class="px-2 pb-3">No instructions have been submitted for this recipe</p>
                      <form>
                        <div class="mb-3 m-2 pb-2">
                          <label for="exampleInputEmail1" class="form-label">Edit Instructions</label>
                          <div class="d-flex gap-2">
                            <textarea type="email" class="form-control" id="exampleInputEmail1"></textarea>
                            <div>
                              <button class="btn btn-success" type="submit"><span class="mdi mdi-plus"></span></button>
                            </div>
                          </div>
                        </div>
                      </form>
                    </div>
                  </div>
                  <div class="col-md-6">
                    <div class="ingredient-card-bg rounded-3">
                      <div v-if="ingredients.length > 0" class="shadow rounded-3">
                        <h2 class="fs-4 text-center bg-success p-2 text-light rounded-top-3">Ingredients</h2>
                        <div class="d-flex justify-content-between px-2" v-for="ingredient in ingredients"
                          :key="ingredient.id">
                          <p>{{ ingredient?.quantity }}</p>
                          <p>{{ ingredient?.name }}</p>
                        </div>
                        <form>
                          <div class="mb-3 m-2 pb-2">
                            <label for="exampleInputEmail1" class="form-label">Ingredient Name</label>
                            <div class="d-flex gap-2">
                              <input type="email" class="form-control" id="exampleInputEmail1">
                            </div>
                            <label for="exampleInputEmail1" class="form-label">Quantity</label>
                            <div class="d-flex gap-2">
                              <input type="email" class="form-control" id="exampleInputEmail1">
                              <div>
                                <button class="btn btn-success"><span class="mdi mdi-plus"></span></button>
                              </div>
                            </div>
                          </div>
                        </form>
                      </div>
                      <div v-else>
                        <div class="shadow rounded-3">
                          <h2 class="fs-4 text-center bg-success p-2 text-light rounded-top-3">Ingredients</h2>
                          <div class="d-flex justify-content-between px-2">
                            <p>No ingredients have been submitted for this recipe</p>
                          </div>
                          <form>
                            <div class="mb-3 m-2 pb-2">
                              <label for="exampleInputEmail1" class="form-label">Ingredient Name</label>
                              <div class="d-flex gap-2">
                                <input type="email" class="form-control" id="exampleInputEmail1">
                              </div>
                              <label for="exampleInputEmail1" class="form-label">Quantity</label>
                              <div class="d-flex gap-2">
                                <input type="email" class="form-control" id="exampleInputEmail1">
                                <div>
                                  <button class="btn btn-success"><span class="mdi mdi-plus"></span></button>
                                </div>
                              </div>
                            </div>
                          </form>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
          <button type="button" class="btn btn-primary">Save changes</button>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
img {
  height: 60dvh;
  width: 100%;
  object-fit: cover;
  object-position: center;
}

.ingredient-card-bg {
  background-color: rgb(255, 246, 235);
}
</style>