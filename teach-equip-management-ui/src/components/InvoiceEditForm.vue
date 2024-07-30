<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";
import eventBus from "@/eventBus";

import { useStore } from "vuex";
import { defineProps, onMounted, onUnmounted, reactive, ref } from "vue";
import router from "@/router";
import { useRoute } from "vue-router";
import axios from "axios";

const route = useRoute();

const store = useStore();

const props = defineProps({
  page_name: {
    type: String,
    default: "",
  },
});

let paramId = route.params.id;

const form = reactive({
  toolId: -1,
  price: 0,
});

onMounted(async () => {
  const itemSelector = `aside .menu .${props.page_name}`;
  const aside_item = document.querySelector(itemSelector);

  aside_item.classList.add("router-link-active");
  aside_item.classList.add("router-link-exact-active");

  await allTools();

  paramId = route.params.id;
  if (paramId !== undefined || paramId !== null) {
    await ItemById(paramId);
  }
});

onUnmounted(() => {
  const itemSelector = `aside .menu .${props.page_name}`;
  const aside_item = document.querySelector(itemSelector);

  aside_item.classList.remove("router-link-active");
  aside_item.classList.remove("router-link-exact-active");
});

const tools = ref({});

const allTools = async () => {
  try {
    const response = await axios.get(
      "https://localhost:7112/api/toolmanage/all-tools",
      {
        headers: {
          Authorization: "Bearer " + localStorage.getItem("access_token"),
        },
      }
    );
    tools.value = response.data.data;
  } catch (error) {
    console.log("Error Fetching jobs", error);
  }
};

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
  const price = document.querySelector("#price");

  const priceValue = price.value.trim();

  let isProcess = true;

  if (priceValue < 10000) {
    setError(price, "Price must be greater or equal 10000");
    isProcess = false;
  } else {
    setSuccess(price);
  }

  if (isProcess) {
    const updateInvoice = {
      id: form.id,
      price: form.price,
    };

    const response = fetch(
      "https://localhost:7112/api/toolmanage/update-invoice",
      {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + localStorage.getItem("access_token"),
        },
        body: JSON.stringify(updateInvoice),
      }
    )
      .then((response) => {
        return response.json();
      })
      .then(async (data) => {
        if (data.statusCode !== 202) {
          console.error("Error fetching data:", data.statusCode, data.message);
        }

        if (data.statusCode === 202) {
          router.push("/inventory/get-invoice");
        }
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
      });
  }
};

const ItemById = async (itemId) => {
  try {
    const response = await axios.get(
      `https://localhost:7112/api/toolmanage/invoice/find/${itemId}`,
      {
        headers: {
          Authorization: "Bearer " + localStorage.getItem("access_token"),
        },
      }
    );

    const datajson = response.data.data;
    form.id = datajson.id;
    form.toolId = datajson.toolId;
    form.price = datajson.price;
  } catch (error) {
    console.log("Error Fetching SupplierInfo", error);
  }
};
</script>

<template>
  <MainCard>
    <section class="bg-green-50">
      <div class="container m-auto">
        <div class="bg-white shadow-md rounded-md border m-4 md:m-0">
          <form @submit.prevent="validateInputs">
            <h2 class="text-3xl text-center font-semibold mb-6">
              Edit Invoice
            </h2>

            <div class="input-control mb-4">
              <label for="type" class="block text-gray-700 font-bold mb-2"
                >Tool Edit:</label
              >
              <select
                v-model="form.toolId"
                id="toolId"
                name="toolId"
                class="border rounded w-full py-2 px-3"
                required
                disabled
              >
                <option value="-1">Default</option>
                <option
                  v-for="tool in tools"
                  :key="tool.id"
                  :value="`${tool.id}`"
                >
                  {{ tool.toolName }}
                </option>
              </select>

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <div class="input-control mb-4">
              <label class="block text-gray-700 font-bold mb-2">Price</label>
              <input
                v-model="form.price"
                type="number"
                id="price"
                name="price"
                class="border rounded w-full py-2 px-3 mb-2"
                placeholder="eg. 100000"
              />

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <div>
              <button
                class="bg-green-500 hover:bg-green-600 text-white font-bold py-2 px-4 rounded-full w-full focus:outline-none focus:shadow-outline"
                type="submit"
              >
                Edit Category
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
