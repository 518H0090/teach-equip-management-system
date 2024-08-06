<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";
import eventBus from "@/eventBus";

import { useStore } from "vuex";
import { defineProps, onMounted, onUnmounted, reactive, ref, computed } from "vue";
import router from "@/router";
import { useRoute } from "vue-router";
import axios from "axios";
import { jwtDecode } from "jwt-decode";
import { useToast } from "vue-toastification";

const toast = useToast();

const route = useRoute();

const store = useStore();

const props = defineProps({
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
  requestType: "Return",
  toolName: "",
  totalBorrow: "",
});

const items = computed(() => store.state.request_return_item);

onMounted(async () => {
  const itemSelector = `aside .menu .${props.page_name}`;
  const aside_item = document.querySelector(itemSelector);

  aside_item.classList.add("router-link-active");
  aside_item.classList.add("router-link-exact-active");

  if (Object.keys(items.value).length === 0) {
    router.push("/request/borrow");
  }

  form.accountId = items.value.account.accountId;
  form.totalBorrow = items.value.Borrow;
  form.toolName = items.value.inventory.toolName;
  form.inventoryId = items.value.inventory.inventoryId;
});

onUnmounted(() => {
  const itemSelector = `aside .menu .${props.page_name}`;
  const aside_item = document.querySelector(itemSelector);

  aside_item.classList.remove("router-link-active");
  aside_item.classList.remove("router-link-exact-active");
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
  const quantity = document.querySelector("#quantity");
  const quantityValue = quantity.value.trim();

  let isProcess = true;
  if (quantityValue > form.totalBorrow) {
    setError(quantity, "You can't return greater than total borrow");
    isProcess = false;
  } else if (quantityValue <= 0) {
    setError(quantity, "Quantity return must be greater than 0");
    isProcess = false;
  } else {
    setSuccess(quantity);
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
          toast.success("success create new request return tool");
          router.push("/request/getpage");
        }
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
      });
  } else {
    toast.error("Something error");
  }
};
</script>

<template>
  <MainCard>
    <section class="bg-green-50">
      <div class="container m-auto">
        <div class="bg-white shadow-md rounded-md border m-4 md:m-0">
          <form @submit.prevent="validateInputs">
            <h2 class="text-3xl text-center font-semibold mb-6">Return Request</h2>

            <div class="input-control mb-4">
              <label class="block text-gray-700 font-bold mb-2">Tool Name</label>
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
              <label class="block text-gray-700 font-bold mb-2">Total Borrow</label>
              <input
                v-model="form.totalBorrow"
                type="number"
                id="total_borrow"
                name="total_borrow"
                class="border rounded w-full py-2 px-3 mb-2"
                placeholder="eg. total borrow"
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
                disabled
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
