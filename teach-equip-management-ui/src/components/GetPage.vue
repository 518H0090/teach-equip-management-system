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
const relationShip = ref({});

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

onActivated(async () => {
  if (props.page_name === "supplier") {
    allSupplier();
  } else if (props.page_name === "about") {
    aboutFetchs();
  } else if (props.page_name === "category") {
    allCategory();
  } else if (props.page_name === "tool") {
    await allToolCategories();
    await allTool();
  } else if (props.page_name === "account") {
    await allRoles();
    await allAccount();
  } else if (props.page_name === "inventory") {
    await allInvoicess();
    await allInventories();
  }
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
  } else if (props.page_name === "category") {
    allCategory();
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
    await fetchValueForRequest();
    await allApprovalRequest();
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
      "https://localhost:7112/api/toolmanage/all-supplier",
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
  } catch (error) {
    console.log("Error Fetching jobs", error);
    if (error.response.status === 401) {
      router.push("/login");
    }
  }
};

const allCategory = async () => {
  try {
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
  } catch (error) {
    console.log("Error Fetching jobs", error);
    if (error.response.status === 401) {
      router.push("/login");
    }
  }
};

const allTool = async () => {
  try {
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
  } catch (error) {
    console.log("Error Fetching jobs", error);
    if (error.response.status === 401) {
      router.push("/login");
    }
  }
};

const allAccount = async () => {
  try {
    const response = await axios.get(
      "https://localhost:7112/api/usermanage/all-users"
    );
    const datajson = response.data.data.map(
      ({
        passwordHash,
        passwordSalt,
        refreshToken,
        refreshTokenExpiryTime,
        ...rest
      }) => rest
    );

    const mappedData = datajson.map((item) => ({
      id: item.id,
      username: item.username,
      email: item.email,
      role: roles.value.filter(
        (role) => Number(role.id) === Number(item.roleId)
      ),
    }));

    items.value = mappedData;

    let allKeys = items.value.reduce((keys, obj) => {
      return keys.concat(Object.keys(obj));
    }, []);

    let uniqueKeys = [...new Set(allKeys)];

    keys.value = uniqueKeys;
  } catch (error) {
    console.log("Error Fetching jobs", error);
    if (error.response.status === 401) {
      router.push("/login");
    }
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
};

const allToolCategories = async () => {
  try {
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
  } catch (error) {
    console.log("Error Fetching jobs", error);
    if (error.response.status === 401) {
      router.push("/login");
    }
  }
};

const roles = ref({});

const allRoles = async () => {
  try {
    const response = await axios.get(
      "https://localhost:7112/api/usermanage/all-roles"
    );
    roles.value = response.data.data;
  } catch (error) {
    console.log("Error Fetching jobs", error);
    if (error.response.status === 401) {
      router.push("/login");
    }
  }
};

const allInventories = async () => {
  try {
    const inventories = await axios.get(
      "https://localhost:7112/api/inventorymanage/all-inventories",
      {
        headers: {
          Authorization: "Bearer " + localStorage.getItem("access_token"),
        },
      }
    );

    const tools = await axios.get(
      "https://localhost:7112/api/toolmanage/all-tools",
      {
        headers: {
          Authorization: "Bearer " + localStorage.getItem("access_token"),
        },
      }
    );

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
  } catch (error) {
    console.log("Error Fetching jobs", error);
    if (error.response.status === 401) {
      router.push("/login");
    }
  }
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
        router.push("/login");
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
  try {
    const response = await axios.get(
      "https://localhost:7112/api/inventorymanage/all-approval-requests",
      {
        headers: {
          Authorization: "Bearer " + localStorage.getItem("access_token"),
        },
      }
    );

    const dataMapped = response.data.data.map(
      ({ approveDate, managerApprove, requestDate, ...rest }) => rest
    );

    const mappedFilter = dataMapped.map((item) => ({
      id: item.id,
      account: fetchAccount.value.filter(
        (account) => account.id === item.accountId
      ),
      inventory: fetchInventory.value
        .filter((inventory) => inventory.id === item.inventoryId)
        .map((inventory) => inventory.tool)
        .flat(),
      quantity: item.quantity,
      requestType: item.requestType,
      status: item.status,
      isApproved: item.isApproved,
    }));

    items.value = mappedFilter;

    let allKeys = mappedFilter.reduce((keys, obj) => {
      return keys.concat(Object.keys(obj));
    }, []);

    let uniqueKeys = [...new Set(allKeys)];

    keys.value = uniqueKeys;
  } catch (error) {
    console.log("Error Fetching jobs", error);
    if (error.response.status === 401) {
      router.push("/login");
    }
  }
};

const fetchAccount = ref({});
const fetchInventory = ref({});

const fetchValueForRequest = async () => {
  try {
    const accounts = await axios.get(
      `https://localhost:7112/api/usermanage/all-users`,
      {
        headers: {
          Authorization: "Bearer " + localStorage.getItem("access_token"),
        },
      }
    );

    const inventories = await axios.get(
      `https://localhost:7112/api/inventorymanage/all-inventories`,
      {
        headers: {
          Authorization: "Bearer " + localStorage.getItem("access_token"),
        },
      }
    );

    const tools = await axios.get(
      `https://localhost:7112/api/toolmanage/all-tools`,
      {
        headers: {
          Authorization: "Bearer " + localStorage.getItem("access_token"),
        },
      }
    );

    const toolJson = tools.data.data;
    const inventoryJson = inventories.data.data;
    const accountJson = accounts.data.data;

    const mappedInventory = inventoryJson.map((item) => ({
      id: item.id,
      tool: toolJson.filter((tool) => tool.id === item.toolId),
    }));

    const mappedUser = accountJson.map((user) => ({
      id: user.id,
      username: user.username,
    }));

    fetchAccount.value = mappedUser;
    fetchInventory.value = mappedInventory;
  } catch (error) {
    console.log("Error Fetching jobs", error);
    if (error.response.status === 401) {
      router.push("/login");
    }
  }
};
</script>

<template>
  <MainCard>
    <DataTable
      :keys="keys"
      :items="items"
      :page_name="props.page_name"
      :page_service="props.page_service"
    />
  </MainCard>
</template>

<style lang="scss" scoped></style>
