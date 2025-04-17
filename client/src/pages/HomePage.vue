<script setup>


</script>

<template>
  <section class="container-fluid">
    <div class="row spice-bg justify-content-end">
      <div class="col-md-3 mt-3 px-2">
        <form class="d-flex" role="search">
          <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
          <button class="btn btn-outline-success" type="submit">Search</button>
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
  </section>
</template>

<style scoped lang="scss">
.spice-bg {
  background-image: url(https://images.unsplash.com/photo-1502741384106-56538427cde9?q=80&w=2069&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D);
  background-position: center;
  background-size: cover;
  min-height: 57dvh;
}

.text-shadow {
  text-shadow: 1px 1px 5px black;
}
</style>
