<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";

import { useStore } from "vuex";
import { defineProps, onMounted, onUnmounted, ref } from "vue";
import DataTable from "@/components/DataTable.vue";
import axios from "axios";
import router from "@/router";
import ClipLoader from "vue-spinner/src/ClipLoader.vue";

const store = useStore();

const items = ref({});
const keys = ref([]);
const relationShip = ref({});

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
    default: ""
  }
});

onMounted(async () => {
  const itemSelector = `aside .menu .${props.page_name}`;
  const aside_item = document.querySelector(itemSelector);

  aside_item.classList.add("router-link-active");
  aside_item.classList.add("router-link-exact-active");
  try {
    if (props.page_name === "supplier") {
      await allSupplier();
    } else if (props.page_name === "about") {
      await aboutFetchs();
    } else if (props.page_name === "category") {
      await allCategory();
    } else if (props.page_name === "tool") {
      await allToolCategories();
      await allTool();
    } else if (props.page_name === "account") {
      await allRoles();
      await allAccount();
    } else if (props.page_name === "inventory") {
      await allInvoicess();
      await allInventories();
    } else if (props.page_name === "request") {
      await allApprovalRequest();
    }
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

const allSupplier = async () => {
  const response = await axios.get("https://localhost:7112/api/toolmanage/all-supplier", {
    headers: {
      Authorization: "Bearer " + localStorage.getItem("access_token"),
    },
  });
  items.value = response.data.data;

  let allKeys = response.data.data.reduce((keys, obj) => {
    return keys.concat(Object.keys(obj));
  }, []);

  let uniqueKeys = [...new Set(allKeys)];

  keys.value = uniqueKeys;
};

const allCategory = async () => {
  const response = await axios.get(
    "https://localhost:7112/api/toolmanage/all-categories",
    {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("access_token"),
      },
    }
  );
  items.value = response.data.data;

  let allKeys = response.data.data.reduce((keys, obj) => {
    return keys.concat(Object.keys(obj));
  }, []);

  let uniqueKeys = [...new Set(allKeys)];

  keys.value = uniqueKeys;
};

const allTool = async () => {
  const response = await axios.get(
    "https://localhost:7112/api/toolmanage/all-tools-include-supplier",
    {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("access_token"),
      },
    }
  );

  const datajson = response.data.data;

  const mappedData = datajson.map((item) => ({
    id: item.id,
    toolName: item.toolName,
    description: item.description,
    supplier: {
      supplierId: item.supplier.id,
      supplierName: item.supplier.supplierName,
    },
    category: relationShip.value
      .filter((toolCategory) => toolCategory.tool.id === item.id)
      .map((toolCategory) => toolCategory.category),
  }));

  items.value = mappedData;

  let allKeys = mappedData.reduce((keys, obj) => {
    return keys.concat(Object.keys(obj));
  }, []);

  let uniqueKeys = [...new Set(allKeys)];

  keys.value = uniqueKeys;
};

const allAccount = async () => {
  const response = await axios.get("https://localhost:7112/api/usermanage/all-users");
  const datajson = response.data.data.map(
    ({ passwordHash, passwordSalt, refreshToken, refreshTokenExpiryTime, ...rest }) =>
      rest
  );

  let mappedData = datajson.map((item) => ({
    id: item.id,
    username: item.username,
    email: item.email,
    role: roles.value.filter((role) => Number(role.id) === Number(item.roleId)),
  }));

  if (props.role === 'manager') {
    mappedData = mappedData.filter(data => data.role[0].roleName === 'user');
  }

  items.value = mappedData;

  let allKeys = items.value.reduce((keys, obj) => {
    return keys.concat(Object.keys(obj));
  }, []);

  let uniqueKeys = [...new Set(allKeys)];

  keys.value = uniqueKeys;
};

const aboutFetchs = async () => {
  const response = await fetch("https://jsonplaceholder.typicode.com/todos");

  items.value = await response.json();

  let allKeys = items.value.reduce((keys, obj) => {
    return keys.concat(Object.keys(obj));
  }, []);

  let uniqueKeys = [...new Set(allKeys)];

  keys.value = uniqueKeys;
};

const allToolCategories = async () => {
  const response = await axios.get(
    "https://localhost:7112/api/toolmanage/all-tool-categories",
    {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("access_token"),
      },
    }
  );

  const datajson = response.data.data;

  relationShip.value = datajson;
};

const roles = ref({});

const allRoles = async () => {
  const response = await axios.get("https://localhost:7112/api/usermanage/all-roles");
  roles.value = response.data.data;
};

const allInventories = async () => {
  const inventories = await axios.get(
    "https://localhost:7112/api/inventorymanage/all-inventories",
    {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("access_token"),
      },
    }
  );

  const tools = await axios.get("https://localhost:7112/api/toolmanage/all-tools", {
    headers: {
      Authorization: "Bearer " + localStorage.getItem("access_token"),
    },
  });

  let mappedData;

  if (invoices.value.length > 0) {
    mappedData = inventories.data.data.map((item) => ({
      id: item.id,
      tool: tools.data.data
        .filter((tool) => Number(tool.id) === Number(item.toolId))
        .map((tool) => tool.toolName),
      total_quantity: item.totalQuantity,
      amount_borrow: item.amountBorrow,
      latest_prices: getLatestPriceByToolId(invoices.value, item.toolId)
        ? getLatestPriceByToolId(invoices.value, item.toolId)
        : 0,
    }));
  } else {
    mappedData = inventories.data.data.map((item) => ({
      id: item.id,
      tool: tools.data.data
        .filter((tool) => Number(tool.id) === Number(item.toolId))
        .map((tool) => tool.toolName),
      total_quantity: item.totalQuantity,
      amount_borrow: item.amountBorrow,
    }));
  }

  items.value = mappedData;

  let allKeys = mappedData.reduce((keys, obj) => {
    return keys.concat(Object.keys(obj));
  }, []);

  let uniqueKeys = [...new Set(allKeys)];

  keys.value = uniqueKeys;
};

const invoices = ref({});

const allInvoicess = async () => {
  const response = fetch("https://localhost:7112/api/toolmanage/all-invoices", {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + localStorage.getItem("access_token"),
    },
  })
    .then((response) => {
      return response.json();
    })
    .then(async (data) => {
      if (data.statusCode === 200) {
        invoices.value = data.data;
      } else if (data.statusCode !== 200) {
        console.error("Error fetching data:", data.message);
      }
    })
    .catch((error) => {
      console.error("Error fetching data:", error);
      if (error.response.status === 401) {
        console.log("Error Fetching jobs", error);
      }
    });
};

function getLatestPriceByToolId(data, toolId) {
  const filteredData = data.filter((item) => item.toolId === toolId);

  if (filteredData.length === 0) return null;

  const latestDateItem = filteredData.reduce((latest, item) => {
    const currentDate = new Date(item.invoiceDate);
    const latestDate = new Date(latest.invoiceDate);

    return currentDate > latestDate ? item : latest;
  });

  return latestDateItem.price;
}

const allApprovalRequest = async () => {
  const response = await axios.get(
    "https://localhost:7112/api/inventorymanage/all-approval-requests",
    {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("access_token"),
      },
    }
  );

  const mappedFilter = response.data.data.map(async (item) => ({
    id: item.id,
    account: await accountById(item.accountId),
    inventory: await inventoryById(item.inventoryId),
    quantity: item.quantity,
    requestType: item.requestType,
    status: item.status,
  }));

  const promisesMappedData = await Promise.all(mappedFilter);

  items.value = promisesMappedData;

  let allKeys = promisesMappedData.reduce((keys, obj) => {
    return keys.concat(Object.keys(obj));
  }, []);

  let uniqueKeys = [...new Set(allKeys)];

  keys.value = uniqueKeys;
};

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

  return data;
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

  let dataResponse = {
    ...rest,
    tool,
  };

  return dataResponse;
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
      :page_name="props.page_name"
      :page_service="props.page_service"
      :role="props.role"
    />
  </MainCard>
  
</template>

<style lang="scss" scoped></style>
