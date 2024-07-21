<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";
import DataTable from "@/components/DataTable.vue";
import { useStore } from "vuex";
import { onMounted, ref } from "vue";

const store = useStore();

const items = ref([]);

// Default Fetch Api
onMounted(async () => {
  const response = await fetch("https://jsonplaceholder.typicode.com/todos");
  items.value = await response.json();
});
</script>

<template>
  <div class="content">
    <Navbar />
    <MainCard>
      <DataTable :items="items" />
    </MainCard>
  </div>
</template>

<style lang="scss" scoped></style>
