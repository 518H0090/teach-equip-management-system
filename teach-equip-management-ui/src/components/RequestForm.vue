<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";
import eventBus from "@/eventBus";

import { useStore } from "vuex";
import { defineProps, onMounted, onUnmounted, reactive, ref } from "vue";
import router from "@/router";
import { useRoute } from "vue-router";
import axios from "axios";
import { jwtDecode } from "jwt-decode";

const route = useRoute();

const store = useStore();

const props = defineProps({
  isShow: {
    type: Boolean,
    default: true,
  },
  page_name: {
    type: String,
    default: "",
  },
  user: Array,
});

const form = reactive({
  accountId: "",
  inventoryId: "",
  quantity: 0,
  requestType: -1,
  toolName: "",
});

let paramId = route.params.id;

onMounted(async () => {
  const itemSelector = `aside .menu .${props.page_name}`;
  const aside_item = document.querySelector(itemSelector);

  aside_item.classList.add("router-link-active");
  aside_item.classList.add("router-link-exact-active");

  decodeJwtToken(token.value, user);

  paramId = route.params.id;
  if (paramId !== undefined || paramId !== null) {
    await inventoryById(paramId);
  }
});

onUnmounted(() => {
  const itemSelector = `aside .menu .${props.page_name}`;
  const aside_item = document.querySelector(itemSelector);

  aside_item.classList.remove("router-link-active");
  aside_item.classList.remove("router-link-exact-active");

  user.value = {};
});

const setError = (element, message) => {
  const inputControl = element.parentElement;
  const errorDisplay = inputControl.querySelector(".error");
  errorDisplay.innerText = message;
  inputControl.classList.add("error");
  inputControl.classList.remove("success");
};

const setSuccess = (element) => {
  const inputControl = element.parentElement;
  const errorDisplay = inputControl.querySelector(".error");
  errorDisplay.innerText = "";
  inputControl.classList.add("success");
  inputControl.classList.remove("error");
};

const validateInputs = async () => {
  console.log(form);

  const quantity = document.querySelector("#quantity");
  const requestType = document.querySelector("#request_type");
  const quantityValue = quantity.value.trim();
  const requestTypeValue = requestType.value.trim();

  let isProcess = true;

  if (quantityValue <= 0) {
    setError(quantity, "Quantiy must be greater than 0");
    isProcess = false;
  } else {
    setSuccess(quantity);
  }

  if (requestTypeValue === "") {
    setError(requestType, "This is required");
    isProcess = false;
  } else if (requestTypeValue === String(-1)) {
    setError(requestType, "Must select Role instead of default");
    isProcess = false;
  } else {
    setSuccess(requestType);
  }

  if (isProcess) {
    const newRequest = {
      accountId: form.accountId,
      inventoryId: form.inventoryId,
      quantity: form.quantity,
      requestType: form.requestType,
    };

    const response = fetch(
      "https://localhost:7112/api/inventorymanage/create-approval-request",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + localStorage.getItem("access_token"),
        },
        body: JSON.stringify(newRequest),
      }
    )
      .then((response) => {
        return response.json();
      })
      .then(async (data) => {
        if (data.statusCode !== 201) {
        }
        if (data.statusCode === 201) {
          router.push("/inventory/getpage");
        }
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
      });
  }
};

const token = ref(localStorage.getItem("access_token") || "");
const user = ref({});

function decodeJwtToken(token, userRef) {
  try {
    if (token) {
      const decoded = jwtDecode(token);

      userRef.value = {
        exp: decoded.exp,
        id: decoded[
          "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
        ],
        name: decoded[
          "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
        ],
        role: decoded[
          "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        ],
      };

      form.accountId = userRef.value.id;
      console.log(userRef.value);
    }
  } catch (error) {
    console.error("Invalid token:", error);
  }
}

const inventoryById = async (inventoryId) => {
  try {
    const response = await axios.get(
      `https://localhost:7112/api/inventorymanage/inventory/find/${inventoryId}`,
      {
        headers: {
          Authorization: "Bearer " + localStorage.getItem("access_token"),
        },
      }
    );
    const inventory = response.data.data;

    const toolInfo = await axios.get(
      `https://localhost:7112/api/toolmanage/tool/find/${inventory.toolId}`,
      {
        headers: {
          Authorization: "Bearer " + localStorage.getItem("access_token"),
        },
      }
    );

    const tool = toolInfo.data.data;

    form.toolName = tool.toolName;
    form.inventoryId = inventory.id;

    console.log(inventory);
    console.log(tool);
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
    <section class="bg-green-50">
      <div class="container m-auto">
        <div class="bg-white shadow-md rounded-md border m-4 md:m-0">
          <form @submit.prevent="validateInputs">
            <h2 class="text-3xl text-center font-semibold mb-6">New Request</h2>

            <div class="input-control mb-4">
              <label class="block text-gray-700 font-bold mb-2"
                >Tool Name</label
              >
              <input
                v-model="form.toolName"
                type="text"
                id="tool_name"
                name="tool_name"
                class="border rounded w-full py-2 px-3 mb-2"
                placeholder="eg. Tool Name"
                disabled
              />

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <div class="input-control mb-4">
              <label class="block text-gray-700 font-bold mb-2">Quantity</label>
              <input
                v-model="form.quantity"
                type="number"
                id="quantity"
                name="quantity"
                class="border rounded w-full py-2 px-3 mb-2"
                placeholder="eg. 10"
              />

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <div class="input-control mb-4">
              <label for="type" class="block text-gray-700 font-bold mb-2"
                >Request Type</label
              >
              <select
                v-model="form.requestType"
                id="request_type"
                name="request_type"
                class="border rounded w-full py-2 px-3"
                required
              >
                <option value="-1">Default</option>
                <option value="Borrow">Borrow</option>
                <option value="Return">Return</option>
                <option value="Buy">Buy</option>
                <option value="Sell">Sell</option>
              </select>

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>
            <div>
              <button
                class="bg-green-500 hover:bg-green-600 text-white font-bold py-2 px-4 rounded-full w-full focus:outline-none focus:shadow-outline"
                type="submit"
              >
                Create Request
              </button>
            </div>
          </form>
        </div>
      </div>
    </section>
  </MainCard>
</template>

<style lang="scss" scoped>
.container {
  max-width: 786px;
  margin-top: 4rem;

  form {
    padding: 2rem 2rem;
    button {
      padding: 1rem;
    }
  }
}

.input-control.success {
  border: 2px solid #09c372;
  padding: 0.4rem;
  border-radius: 10px;
  display: block;

  input:focus,
  textarea:focus {
    outline: 0;
  }
}

.input-control.error {
  border: 2px solid #ff3860;
  padding: 0.4rem;
  border-radius: 10px;
  display: block;
  color: inherit;

  input:focus,
  textarea:focus {
    outline: 0;
  }
}

.error {
  color: #ff3860;
  padding: 1rem 0.4rem;
}
</style>
