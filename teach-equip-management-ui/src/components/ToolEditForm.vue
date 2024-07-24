<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";
import eventBus from "@/eventBus";

import { useStore } from "vuex";
import { defineProps, onMounted, onUnmounted, reactive } from "vue";
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
  id: "",
  type: "",
  unit: "",
});

let paramId = route.params.id;

onMounted(() => {
  const itemSelector = `aside .menu .${props.page_name}`;
  const aside_item = document.querySelector(itemSelector);

  aside_item.classList.add("router-link-active");
  aside_item.classList.add("router-link-exact-active");
  paramId = route.params.id;
  if (paramId !== undefined || paramId !== null) {
    ItemById(paramId);
  }
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
  const type = document.querySelector("#type");
  const unit = document.querySelector("#unit");

  const typeValue = type.value.trim();
  const unitValue = unit.value.trim();
  let isProcess = true;

  if (typeValue === "") {
    setError(type, "This is required");
    isProcess = false;
  } else {
    setSuccess(type);
  }

  if (unitValue === "") {
    setError(unit, "This is required");
    isProcess = false;
  } else {
    setSuccess(unit);
  }

  console.log(form);

  if (isProcess) {
    const updateCategory = {
      id: form.id,
      type: form.type,
      unit: form.unit,
    };

    try {
      const response = await axios.put(
        "https://localhost:7112/api/toolmanage/update-category",
        updateCategory
      );

      router.push("/category/getpage");
    } catch (error) {
      console.log("Error Fetching jobs", error);
    }
  }
};

const ItemById = async (itemId) => {
  try {
    const response = await axios.get(
      `https://localhost:7112/api/toolmanage/category/find/${itemId}`
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
            <h2 class="text-3xl text-center font-semibold mb-6">Edit Tool</h2>

            <div class="input-control mb-4">
              <label class="block text-gray-700 font-bold mb-2">Type</label>
              <input
                v-model="form.type"
                type="text"
                id="type"
                name="type"
                class="border rounded w-full py-2 px-3 mb-2"
                placeholder="eg. everyday"
              />

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <div class="input-control mb-4">
              <label class="block text-gray-700 font-bold mb-2">unit</label>
              <input
                v-model="form.unit"
                type="text"
                id="unit"
                name="unit"
                class="border rounded w-full py-2 px-3 mb-2"
                placeholder="eg. unit"
              />

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <div>
              <button
                class="bg-green-500 hover:bg-green-600 text-white font-bold py-2 px-4 rounded-full w-full focus:outline-none focus:shadow-outline"
                type="submit"
              >
                Edit Tool
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
