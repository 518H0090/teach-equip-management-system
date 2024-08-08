<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";

import { useStore } from "vuex";
import { defineProps, onMounted, onUnmounted, onActivated, ref } from "vue";
import DataTable from "@/components/DataTable.vue";
import axios from "axios";
import router from "@/router";
import ClipLoader from "vue-spinner/src/ClipLoader.vue";

const store = useStore();

const items = ref({});
const keys = ref([]);
const isLoading = ref(true);

const props = defineProps({
  page_name: {
    type: String,
    default: "",
  },
  page_service: {
    type: String,
    default: "",
  },
  role: {
    type: String,
    default: "",
  },
  username: {
    type: String,
    default: "",
  },
});

onMounted(async () => {
  const itemSelector = `aside .menu .${props.page_name}`;
  const aside_item = document.querySelector(itemSelector);

  aside_item.classList.add("router-link-active");
  aside_item.classList.add("router-link-exact-active");
  try {
    await allInventoryHistories();
  } catch (error) {
    console.log(error);
  } finally {
    isLoading.value = false;
  }
});

onUnmounted(() => {
  const itemSelector = `aside .menu .${props.page_name}`;

  const aside_item = document.querySelector(itemSelector);

  aside_item.classList.remove("router-link-active");
  aside_item.classList.remove("router-link-exact-active");
});

const allInventoryHistories = async () => {
  const response = await axios.get(
    "https://localhost:7112/api/inventorymanage/all-inventory-histories",
    {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("access_token"),
      },
    }
  );
  const { data } = response.data;

  const resultArray = calculateQuantitiesForEachItem(data).filter(
    (item) => item.NetQuantity > 0
  );

  const mappedFilter = resultArray.map(async (item) => ({
    account: await accountById(item.accountId),
    inventory: await inventoryById(item.inventoryId),
    Borrow: item.NetQuantity,
  }));

  let promisesMappedData = await Promise.all(mappedFilter);

  if (props.role === "user" && props.username !== null) {
    promisesMappedData = promisesMappedData.filter(
      (data) => data.account.username === props.username
    );
  }

  items.value = promisesMappedData;

  let allKeys = promisesMappedData.reduce((keys, obj) => {
    return keys.concat(Object.keys(obj));
  }, []);

  let uniqueKeys = [...new Set(allKeys)];

  keys.value = uniqueKeys;
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
  const response = await axios.get(
    `https://localhost:7112/api/usermanage/user/find/${userId}`,
    {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("access_token"),
      },
    }
  );

  const { data } = response.data;

  return { username: data.username, accountId: data.id };
};

const inventoryById = async (inventoryId) => {
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

  return { inventoryId: rest.id, toolName: tool };
};

const toolById = async (toolId) => {
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
};

const calculateQuantitiesForEachItem = (data) => {
  const result = {};
  data.forEach((item) => {
    const { accountId, inventoryId, actionType, quantity } = item;

    const key = `${accountId}_${inventoryId}`;

    if (!result[key]) {
      result[key] = {
        accountId,
        inventoryId,
        Borrow: 0,
        Return: 0,
        NetQuantity: 0,
      };
    }

    if (actionType === "Borrow") {
      result[key].Borrow += quantity;
    } else if (actionType === "Return") {
      result[key].Return += quantity;
    }
  });

  Object.keys(result).forEach((key) => {
    result[key].NetQuantity = result[key].Borrow - result[key].Return;
  });

  return Object.values(result);
};
</script>

<template>
  <MainCard>
    <div v-if="isLoading" class="text-center text-gray-500 py-6">
      <ClipLoader size="8rem" />
    </div>
    <DataTable
      v-else
      :keys="keys"
      :items="items"
      page_name="borrow"
      :page_service="props.page_service"
    />
  </MainCard>
</template>

<style lang="scss" scoped></style>
