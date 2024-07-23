<script setup>
import { RouterLink, RouterView } from "vue-router";
import { useRoute } from "vue-router";
import { computed, onMounted, ref } from "vue";
import Navbar from "@/components/Navbar.vue";

const route = useRoute();

const items = ref([]);

onMounted(async () => {
  const response = await fetch("https://jsonplaceholder.typicode.com/todos");
  items.value = await response.json();
});
</script>

<template>
  <div class="content">
    <Navbar>
      <li>
        <RouterLink to="/about/getpage" class="link">View</RouterLink>
      </li>
      <li>
        <RouterLink to="/about/editpage" class="link">Add</RouterLink>
      </li>
      <li>
        <RouterLink to="/about/addpage" class="link">Edit</RouterLink>
      </li>
    </Navbar>
    <RouterView :items="items" />
  </div>
</template>

<style scoped>
router-link-active,
router-link-exact-active {
  border-right: 5px solid var(--primary);
}
</style>
