<template>
  <h1>About HEHEHE</h1>
  <router-link class="button" to="/about/newnewnew">
    <span class="material-icons">group</span>
    <span class="text">Team</span>
  </router-link>

  <router-view></router-view>
</template>

<script setup>
import { RouterLink, RouterView } from "vue-router";
</script>

<style scoped>
h1 {
  margin-left: 20rem;
}
</style>
