<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";

import { useStore } from "vuex";
import { defineProps, onMounted, onUnmounted, ref } from "vue";
import DataTable from "@/components/DataTable.vue";

const store = useStore();

const props = defineProps({
  isShow: {
    type: Boolean,
    default: true,
  },
});

const items = ref([]);

onMounted(async () => {
  const aside_item = document.querySelector("aside .menu .about");

  aside_item.classList.add("router-link-active");
  aside_item.classList.add("router-link-exact-active");

  const response = await fetch("https://jsonplaceholder.typicode.com/todos");
  items.value = await response.json();
});

onUnmounted(() => {
  const aside_item = document.querySelector("aside .menu .about");

  aside_item.classList.remove("router-link-active");
  aside_item.classList.remove("router-link-exact-active");
});
</script>

<template>
  <MainCard>
    <DataTable :items="items" />
  </MainCard>
</template>
