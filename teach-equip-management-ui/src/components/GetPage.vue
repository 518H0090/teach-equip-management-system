<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";

import { useStore } from "vuex";
import { defineProps, onMounted, onUnmounted, onActivated, ref } from "vue";
import DataTable from "@/components/DataTable.vue";
import axios from "axios";

const store = useStore();

const items = ref({});
const keys = ref([]);

const props = defineProps({
  page_name: {
    type: String,
    default: "",
  },
});

onMounted(async () => {
  const itemSelector = `aside .menu .${props.page_name}`;
  const aside_item = document.querySelector(itemSelector);

  aside_item.classList.add("router-link-active");
  aside_item.classList.add("router-link-exact-active");

  if (props.page_name === "supplier") {
    allSupplier();
  } else if (props.page_name === "about") {
    aboutFetchs();
  }
});

onUnmounted(() => {
  const itemSelector = `aside .menu .${props.page_name}`;

  const aside_item = document.querySelector(itemSelector);

  aside_item.classList.remove("router-link-active");
  aside_item.classList.remove("router-link-exact-active");
});

const allSupplier = async () => {
  try {
    const response = await axios.get(
      "https://localhost:7112/api/toolmanage/all-supplier"
    );
    items.value = response.data.data;

    let allKeys = response.data.data.reduce((keys, obj) => {
      return keys.concat(Object.keys(obj));
    }, []);

    let uniqueKeys = [...new Set(allKeys)];

    keys.value = uniqueKeys;
  } catch (error) {
    console.log("Error Fetching jobs", error);
  }
};

const aboutFetchs = async () => {
  const response = await fetch("https://jsonplaceholder.typicode.com/todos");

  items.value = await response.json();

  let allKeys = items.value.reduce((keys, obj) => {
    return keys.concat(Object.keys(obj));
  }, []);

  let uniqueKeys = [...new Set(allKeys)];

  keys.value = uniqueKeys;

  console.log(keys.value);
  console.log(items.value);
};
</script>

<template>
  <MainCard>
    <DataTable :keys="keys" :items="items" :page_name="page_name" />
  </MainCard>
</template>

<style lang="scss" scoped></style>
