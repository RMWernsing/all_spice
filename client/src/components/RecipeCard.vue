<script setup>
import { Recipe } from '@/models/Recipe.js';


defineProps({
  recipe: { type: Recipe, required: true }
})
</script>


<template>
  <div class="card-style mt-5 d-flex flex-column justify-content-between shadow-lg rounded"
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
</template>


<style lang="scss" scoped>
.card-style {
  min-height: 40dvh;
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
</style>