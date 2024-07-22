<script setup>

import { onMounted, onUnmounted } from "vue";

onMounted(() => {
  const aside_item = document.querySelector('aside .menu .about');

    aside_item.classList.add('router-link-active');
    aside_item.classList.add('router-link-exact-active')
});
</script>

<template>
  <h1>This Is Nested Edit Page</h1>
</template>

<style lang="scss" scoped>
h1 {
  margin-left: 20rem;
}
</style>
