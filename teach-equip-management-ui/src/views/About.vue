<script setup>
import { RouterLink, RouterView } from "vue-router";
import { useRoute } from "vue-router";
import { computed, onMounted, ref } from "vue";
import Navbar from "@/components/Navbar.vue";

const route = useRoute();

const items = ref({});
const keys = ref([]);

onMounted(async () => {
  const response = await fetch("https://jsonplaceholder.typicode.com/todos");

  items.value = await response.json();

  let allKeys = items.value.reduce((keys, obj) => {
    return keys.concat(Object.keys(obj));
  }, []);

  let uniqueKeys = [...new Set(allKeys)];

  keys.value = uniqueKeys;

  console.log(keys.value);
  console.log(items.value);
});
</script>

<template>
  <div class="content">
    <Navbar>
      <li>
        <RouterLink to="/about/getpage" class="link">View</RouterLink>
      </li>
      <li>
        <RouterLink to="/about/addpage" class="link">Add</RouterLink>
      </li>
      <li>
        <RouterLink to="/about/editpage" class="link">Edit</RouterLink>
      </li>
    </Navbar>
    <RouterView :keys="keys" :items="items" page_name="about" />
  </div>
</template>

<style scoped>
router-link-active,
router-link-exact-active {
  border-right: 5px solid var(--primary);
}
</style>
