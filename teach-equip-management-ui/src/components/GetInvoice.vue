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
  page_service: {
    type: String,
    default: "",
  },
});

onMounted(async () => {
  const itemSelector = `aside .menu .${props.page_name}`;
  const aside_item = document.querySelector(itemSelector);

  aside_item.classList.add("router-link-active");
  aside_item.classList.add("router-link-exact-active");

  await allInvoices();
});

onUnmounted(() => {
  const itemSelector = `aside .menu .${props.page_name}`;

  const aside_item = document.querySelector(itemSelector);

  aside_item.classList.remove("router-link-active");
  aside_item.classList.remove("router-link-exact-active");
});

const allInvoices = async () => {
  try {
    const response = await axios.get(
      "https://localhost:7112/api/toolmanage/all-invoices"
    );

    const tools = await axios.get("https://localhost:7112/api/toolmanage/all-tools");

    const mappedData = response.data.data.map((item) => ({
      id: item.id,
      tool: tools.data.data
        .filter((tool) => Number(tool.id) === Number(item.toolId))
        .map((tool) => tool.toolName),
      price: item.price,
      invoiceDate: formatDate(item.invoiceDate),
    }));

    items.value = mappedData;

    let allKeys = mappedData.reduce((keys, obj) => {
      return keys.concat(Object.keys(obj));
    }, []);

    let uniqueKeys = [...new Set(allKeys)];

    keys.value = uniqueKeys;
  } catch (error) {
    console.log("Error Fetching jobs", error);
  }
};

function formatDate(timestamp) {
    const date = new Date(timestamp);

    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0'); 
    const year = date.getFullYear();
    const hours = String(date.getHours() % 12 || 12).padStart(2, '0'); 
    const minutes = String(date.getMinutes()).padStart(2, '0');
    const seconds = String(date.getSeconds()).padStart(2, '0');

    return `${day}/${month}/${year} ${hours}:${minutes}:${seconds}`;
}

</script>

<template>
  <MainCard>
    <DataTable
      :keys="keys"
      :items="items"
      page_name="invoice"
      :page_service="props.page_service"
    />
  </MainCard>
</template>

<style lang="scss" scoped></style>
