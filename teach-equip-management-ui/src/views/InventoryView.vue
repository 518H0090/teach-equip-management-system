<script setup>
import { RouterLink, RouterView } from "vue-router";
import { useRoute } from "vue-router";
import { computed, onMounted, ref } from "vue";
import Navbar from "@/components/Navbar.vue";
</script>

<template>
  <div class="content">
    <Navbar>
      <li>
        <RouterLink to="/inventory/getpage"  class="link">Inventory</RouterLink>
      </li>
      <li>
        <RouterLink to="/inventory/addpage" class="link">Price</RouterLink>
      </li>
      <li>
        <RouterLink to="/inventory/addpage" class="link">Latest Price</RouterLink>
      </li>
    </Navbar>
    <RouterView page_name="inventory" page_service="toolmanage" />
  </div>
</template>

<style scoped>
router-link-active,
router-link-exact-active {
  border-right: 5px solid var(--primary);
}
</style>
