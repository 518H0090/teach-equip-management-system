<script setup>
import { RouterLink, RouterView } from "vue-router";
import { useRoute } from "vue-router";
import { computed, onMounted, ref } from "vue";
import Navbar from "@/components/Navbar.vue";
import axios from "axios";

const route = useRoute();

const items = ref();

onMounted(async () => {
  try {
    const response = await axios.get(
      "https://localhost:7112/api/toolmanage/all-supplier"
    );
    items.value = response.data.data;

    let allKeys = response.data.data.reduce((keys, obj) => {
      return keys.concat(Object.keys(obj));
    }, []);

    let uniqueKeys = [...new Set(allKeys)];

    console.log(uniqueKeys);
    console.log(response.data.data);
    console.log(items.value);
  } catch (error) {
    console.log("Error Fetching jobs", error);
  }
});
</script>

<template>
  <div class="content">
    <Navbar>
      <li>
        <RouterLink to="/supplier/getpage" class="link">View</RouterLink>
      </li>
      <li>
        <RouterLink to="/supplier/addpage" class="link">Add</RouterLink>
      </li>
      <li>
        <RouterLink to="/supplier/editpage" class="link">Edit</RouterLink>
      </li>
    </Navbar>
    <RouterView :items="items" page_name="supplier" />
  </div>
</template>

<style scoped>
router-link-active,
router-link-exact-active {
  border-right: 5px solid var(--primary);
}
</style>
