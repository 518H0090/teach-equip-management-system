<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";
import DataTable from "@/components/DataTable.vue";
import SideBar from "@/components/SideBar.vue";
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
  <SideBar v-show="isShow" />
  <div v-show="isShow" class="content">
    <Navbar />
    <MainCard>
      <DataTable :items="items" />
    </MainCard>
  </div>
</template>

<style lang="scss" scoped></style>
