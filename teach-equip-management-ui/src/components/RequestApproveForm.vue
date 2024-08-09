<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";
import eventBus from "@/eventBus";

import { useStore } from "vuex";
import { defineProps, onMounted, onUnmounted, reactive, ref, computed } from "vue";
import router from "@/router";
import { useRoute } from "vue-router";
import axios from "axios";
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
  requestType: "",
  toolName: "",
  totalAvailabel: "",
  status: "",
});

const items = computed(() => store.state.request_return_item);

const totalBorrows = ref(0);

onMounted(async () => {
  const itemSelector = `aside .menu .${props.page_name}`;
  const aside_item = document.querySelector(itemSelector);

  aside_item.classList.add("router-link-active");
  aside_item.classList.add("router-link-exact-active");

  if (Object.keys(items.value).length === 0) {
    router.push("/request/getpage");
  }

  form.accountId = items.value.accountId;
  form.id = items.value.id;
  form.inventoryId = items.value.inventoryId;
  form.totalAvailabel = items.value.totalQuantity;
  form.toolName = items.value.toolName;
  form.quantity = items.value.quantity;
  form.requestType = items.value.requestType;
  form.status = items.value.status;

  await allInventoryHistories(form.accountId, form.inventoryId);
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
  const totalAvailable = document.querySelector("#total_available");
  const quantityValue = quantity.value.trim();
  const totalAvailableValue = totalAvailable.value.trim();

  let isProcess = true;

  if (form.requestType === "Borrow" || form.requestType === "Sell") {
    if (form.quantity > form.totalAvailabel) {
      console.log(quantityValue);
      console.log(totalAvailableValue);
      setError(quantity, "You can't get greater than total available");
      isProcess = false;
    } else if (quantityValue <= 0) {
      setError(quantity, "Quantity return must be greater than 0");
      isProcess = false;
    } else {
      setSuccess(quantity);
    }
  } else {
    setSuccess(quantity);
  }

  if (isProcess) {
    const updateRequest = {
      id: form.id,
      accountId: form.accountId,
      inventoryId: form.inventoryId,
      quantity: form.quantity,
      status: form.status,
    };

    console.log(updateRequest);

    try {
      const response = await axios.put(
        `https://localhost:7112/api/inventorymanage/update-approval-request`,
        updateRequest,
        {
          headers: {
            Authorization: "Bearer " + localStorage.getItem("access_token"),
          },
        }
      );

      if (response.status === 202) {
        toast.success("success approve request");
        router.push("/request/getpage");
      }
    } catch (error) {
      console.log("Error Fetching SupplierInfo", error);
    }
  } else {
    toast.error("Something error");
  }
};

const TurnBackToRequest = () => {
  router.push("/request/getpage");
};

const allInventoryHistories = async (accountId, inventoryId) => {
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
    item => 
  item.NetQuantity > 0
  );

  const mappingData = resultArray.filter(item => item.accountId === accountId && item.inventoryId)

  totalBorrows.value = mappingData.map(item => item.NetQuantity);
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
    <section class="bg-green-50">
      <div class="container m-auto">
        <div class="bg-white shadow-md rounded-md border m-4 md:m-0">
          <form @submit.prevent="validateInputs" v-if="form.requestType !== 'Return'">
            <h2 class="text-3xl text-center font-semibold mb-6">Approve Request</h2>

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
              <label class="block text-gray-700 font-bold mb-2">Total Available</label>
              <input
                v-model="form.totalAvailabel"
                type="number"
                id="total_available"
                name="total_available"
                class="border rounded w-full py-2 px-3 mb-2"
                placeholder="eg. Total Available"
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
                disabled
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

            <div class="input-control mb-4">
              <label for="type" class="block text-gray-700 font-bold mb-2"
                >Status Change</label
              >
              <input
                v-model="form.status"
                type="text"
                id="quantity"
                name="quantity"
                class="border rounded w-full py-2 px-3 mb-2"
                placeholder="eg. 10"
                disabled
              />

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>
            <div class="grid grid-cols-2">
              <button
                class="bg-green-500 hover:bg-green-600 text-white font-bold py-2 px-4 rounded-full w-full focus:outline-none focus:shadow-outline"
                type="submit"
              >
                Confirm
              </button>
              <button
                @click="TurnBackToRequest"
                class="bg-slate-500 hover:bg-slate-600 text-white font-bold py-2 px-4 rounded-full w-full focus:outline-none focus:shadow-outline"
                type="submit"
              >
                Back
              </button>
            </div>
          </form>

          <form @submit.prevent="validateInputs" v-else>
            <h2 class="text-3xl text-center font-semibold mb-6">Approve Request</h2>

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
                v-model="totalBorrows"
                type="number"
                id="total_available"
                name="total_available"
                class="border rounded w-full py-2 px-3 mb-2"
                placeholder="eg. Total Available"
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
                disabled
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

            <div class="input-control mb-4">
              <label for="type" class="block text-gray-700 font-bold mb-2"
                >Status Change</label
              >
              <input
                v-model="form.status"
                type="text"
                id="quantity"
                name="quantity"
                class="border rounded w-full py-2 px-3 mb-2"
                placeholder="eg. 10"
                disabled
              />

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>
            <div class="grid grid-cols-2">
              <button
                class="bg-green-500 hover:bg-green-600 text-white font-bold py-2 px-4 rounded-full w-full focus:outline-none focus:shadow-outline"
                type="submit"
              >
                Confirm
              </button>
              <button
                @click="TurnBackToRequest"
                class="bg-slate-500 hover:bg-slate-600 text-white font-bold py-2 px-4 rounded-full w-full focus:outline-none focus:shadow-outline"
                type="submit"
              >
                Back
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
