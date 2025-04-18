<script setup>
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { ref } from 'vue';


const editableRecipeData = ref({
  title: '',
  category: '',
  img: ''
})

async function createRecipe() {
  try {
    await recipesService.createRecipe(editableRecipeData.value)
    // Modal.getInstance('#createRecipe').hide()
  }
  catch (error) {
    Pop.error(error, "Could not create new recipe")
    logger.error("COULD NOT CRETE RECIPE", error)
  }
}
</script>


<template>
  <div class="modal fade" id="createRecipe" tabindex="-1" aria-labelledby="createRecipeLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="createRecipeLabel">Create Your Own Recipe</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="createRecipe()">
            <div class="row">
              <div class="col-6 mb-3">
                <label for="title">Title</label>
                <input v-model="editableRecipeData.title" id="title" type="text" class="form-control"
                  placeholder="Title..." aria-label="First name" required minlength="5" maxlength="255">
              </div>
              <div class="col-6">
                <label for="category">Category</label>
                <select required v-model="editableRecipeData.category" id="category" class="form-select"
                  aria-label="Default select example">
                  <option selected>Open this select menu</option>
                  <option value="breakfast">Breakfast</option>
                  <option value="lunch">Lunch</option>
                  <option value="dinner">Dinner</option>
                  <option value="snack">Snack</option>
                  <option value="dessert">Dessert</option>
                </select>
              </div>
              <div class="col mb-3">
                <label for="img">Image</label>
                <input v-model="editableRecipeData.img" id="img" type="text" class="form-control"
                  placeholder="Image URL..." aria-label="First name" required minlength="5" maxlength="255">
              </div>
              <div class="col-12">
                <img width="100%" v-if="editableRecipeData.img" :src="editableRecipeData.img" alt="image of recipe">
              </div>
            </div>
            <hr>
            <div class="d-flex justify-content-end">
              <button data-bs-dismiss="modal" class="btn btn-success" type="submit">Submit Recipe</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped></style>