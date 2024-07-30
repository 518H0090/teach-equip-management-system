<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";

import { useStore } from "vuex";
import { defineProps, onMounted, onUnmounted, onActivated, ref } from "vue";
import DataTable from "@/components/DataTable.vue";
import axios from "axios";
import router from "@/router";

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
      "https://localhost:7112/api/inventorymanage/all-inventory-histories",
      {
        headers: {
          Authorization: "Bearer " + localStorage.getItem("access_token"),
        },
      }
    );
    const { data } = response.data;

    console.log(data);

    const mappedFilter = data.map(async (item) => ({
      id: item.id,
      account: await accountById(item.accountId),
      inventory: await inventoryById(item.inventoryId),
      quantity: item.quantity,
      actionType: item.actionType,
      actionDate: formatDate(item.actionDate),
    }));

    const promisesMappedData = await Promise.all(mappedFilter);

    console.log(promisesMappedData);

    items.value = promisesMappedData;

    let allKeys = promisesMappedData.reduce((keys, obj) => {
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

  const day = String(date.getDate()).padStart(2, "0");
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const year = date.getFullYear();
  const hours = String(date.getHours() % 12 || 12).padStart(2, "0");
  const minutes = String(date.getMinutes()).padStart(2, "0");
  const seconds = String(date.getSeconds()).padStart(2, "0");

  return `${day}/${month}/${year} ${hours}:${minutes}:${seconds}`;
}

const accountById = async (userId) => {
  try {
    const response = await axios.get(
      `https://localhost:7112/api/usermanage/user/find/${userId}`,
      {
        headers: {
          Authorization: "Bearer " + localStorage.getItem("access_token"),
        },
      }
    );

    const { data } = response.data;

    return data;
  } catch (error) {
    console.log("Error Fetching jobs", error);
    if (error.response.status === 401) {
      console.log("Error Fetching jobs", error);
    }
  }
};

const inventoryById = async (inventoryId) => {
  try {
    const inventory = await axios.get(
      `https://localhost:7112/api/inventorymanage/inventory/find/${inventoryId}`,
      {
        headers: {
          Authorization: "Bearer " + localStorage.getItem("access_token"),
        },
      }
    );

    const { data } = inventory.data;

    const { toolId, ...rest } = data;

    const tool = await toolById(toolId);

    let dataResponse = {
      ...rest,
      tool,
    };

    return dataResponse;
  } catch (error) {
    console.log("Error Fetching jobs", error);
    if (error.response.status === 401) {
      console.log("Error Fetching jobs", error);
    }
  }
};

const toolById = async (toolId) => {
  try {
    const tool = await axios.get(
      `https://localhost:7112/api/toolmanage/tool/find/${toolId}`,
      {
        headers: {
          Authorization: "Bearer " + localStorage.getItem("access_token"),
        },
      }
    );

    const { data } = tool.data;

    return data.toolName;
  } catch (error) {
    console.log("Error Fetching jobs", error);
    if (error.response.status === 401) {
      console.log("Error Fetching jobs", error);
    }
  }
};
</script>

<template>
  <MainCard>
    <DataTable
      :keys="keys"
      :items="items"
      page_name="request"
      :page_service="props.page_service"
    />
  </MainCard>
</template>

<style lang="scss" scoped></style>
