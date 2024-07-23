<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";
import DataTable from "@/components/DataTable.vue";
import { onMounted, ref, defineProps } from "vue";

const props = defineProps({
  isShow: {
    type: Boolean,
    default: true,
  },
});

const items = ref([]);

// Default Fetch Api
onMounted(async () => {
  const response = await fetch("https://jsonplaceholder.typicode.com/todos");
  items.value = await response.json();
});
</script>

<template>
  <div class="content">
    <Navbar>
      <li>
        <RouterLink to="/" class="link">View</RouterLink>
      </li>
      <li>
        <RouterLink to="/h" class="link">Add</RouterLink>
      </li>
      <li>
        <RouterLink to="/h2" class="link">Edit</RouterLink>
      </li>
    </Navbar>
    <MainCard>
      <DataTable :items="items" />
    </MainCard>
  </div>
  <RouterView />
</template>

<style lang="scss" scoped></style>
