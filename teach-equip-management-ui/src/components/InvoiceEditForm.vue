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
  isShow: {
    type: Boolean,
    default: true,
  },
  page_name: {
    type: String,
    default: "",
  },
});

const form = reactive({
  toolId: -1,
  price: 0,
});

onMounted(() => {
  const itemSelector = `aside .menu .${props.page_name}`;
  const aside_item = document.querySelector(itemSelector);

  aside_item.classList.add("router-link-active");
  aside_item.classList.add("router-link-exact-active");

  allTools();
  ItemById(1);
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
    const response = await axios.get("https://localhost:7112/api/toolmanage/all-tools");
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
  const toolId = document.querySelector("#toolId");
  const price = document.querySelector("#price");

  const toolIdValue = toolId.value.trim();
  const priceValue = price.value.trim();

  let isProcess = true;

  if (priceValue < 10000) {
    setError(price, "Price must be greater or equal 10000");
    isProcess = false;
  } else {
    setSuccess(price);
  }

  if (toolIdValue === "") {
    setError(toolId, "This is required");
    isProcess = false;
  } else if (toolIdValue === String(-1)) {
    setError(toolId, "Must select Role instead of default");
    isProcess = false;
  } else {
    setSuccess(toolId);
  }

  if (isProcess) {
    const newInvoice = {
      price: form.price,
      toolId: form.toolId,
    };

    const response = fetch(
      "https://localhost:7112/api/toolmanage/create-invoice",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(newInvoice),
      }
    )
      .then((response) => {
        return response.json();
      })
      .then(async (data) => {
        if (data.statusCode !== 201) {
          console.error("Error fetching data:", data.statusCode, data.message);
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

const ItemById = async (itemId) => {
  try {
    const response = await axios.get(
      `https://localhost:7112/api/toolmanage/invoice/find/${itemId}`
    );

    const datajson = response.data.data;
    form.id = datajson.id;
    form.type = datajson.type;
    form.unit = datajson.unit;

    console.log(response);
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
            <h2 class="text-3xl text-center font-semibold mb-6">Edit Invoice</h2>

            <div class="input-control mb-4">
              <label for="type" class="block text-gray-700 font-bold mb-2">Tool</label>
              <select
                v-model="form.toolId"
                id="toolId"
                name="toolId"
                class="border rounded w-full py-2 px-3"
                required
              >
                <option value="-1">Default</option>
                <option v-for="tool in tools" :key="tool.id" :value="`${tool.id}`">
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
                Add Category
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
