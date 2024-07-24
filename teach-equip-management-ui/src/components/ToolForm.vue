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
  toolName: "",
  description: "",
  supplierId: "Remote",
});

onMounted(() => {
  const itemSelector = `aside .menu .${props.page_name}`;
  const aside_item = document.querySelector(itemSelector);

  aside_item.classList.add("router-link-active");
  aside_item.classList.add("router-link-exact-active");
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

  if (isProcess) {
    const newCategory = {
      type: form.type,
      unit: form.unit,
    };

    try {
      const response = await axios.post(
        "https://localhost:7112/api/toolmanage/create-category",
        newCategory
      );

      router.push("/category/getpage");
    } catch (error) {
      console.log("Error Fetching jobs", error);
    }
  }
};

const dropdownOpen = ref(false);
const selectedOptions = ref(["heheh", "fsfsa", "aaaa"]);

const toggleDropdown = () => {
  dropdownOpen.value = !dropdownOpen.value;
};
</script>

<template>
  <MainCard>
    <section class="bg-green-50">
      <div class="container m-auto">
        <div class="bg-white shadow-md rounded-md border m-4 md:m-0">
          <form @submit.prevent="validateInputs">
            <h2 class="text-3xl text-center font-semibold mb-6">Add Tool</h2>

            <div class="input-control mb-4">
              <label class="block text-gray-700 font-bold mb-2">ToolName</label>
              <input
                v-model="form.toolName"
                type="text"
                id="tool_name"
                name="tool_name"
                class="border rounded w-full py-2 px-3 mb-2"
                placeholder="eg. everyday"
              />

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <div class="input-control mb-4">
              <label
                for="company_description"
                class="block text-gray-700 font-bold mb-2"
                >Company Description</label
              >
              <textarea
                v-model="form.description"
                id="company_description"
                name="company_description"
                class="border rounded w-full py-2 px-3"
                rows="4"
                placeholder="What does your company do?"
              ></textarea>

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <div class="input-control mb-4">
              <label for="type" class="block text-gray-700 font-bold mb-2"
                >Supplier</label
              >
              <select
                id="type"
                v-model="form.supplierId"
                name="type"
                class="border rounded w-full py-2 px-3"
                required
              >
                <option value="Full-Time">Full-Time</option>
                <option value="Part-Time">Part-Time</option>
                <option value="Remote">Remote</option>
                <option value="Internship">Internship</option>
              </select>

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <div class="input-control mb-4">
              <div class="border rounded w-full py-2 px-3 dropdown">
                <button @click="toggleDropdown">
                  {{ dropdownOpen ? "Close Dropdown" : "Open Dropdown" }}
                </button>
                <div v-if="dropdownOpen" class="dropdown-content">
                  <label for="type" class="block text-gray-700 font-bold mb-2">
                    <input
                      type="checkbox"
                      value="heheh"
                      v-model="selectedOptions"
                    />
                    heheh
                  </label>

                  <label for="type" class="block text-gray-700 font-bold mb-2">
                    <input
                      type="checkbox"
                      value="fsfsa"
                      v-model="selectedOptions"
                    />
                    fsfsa
                  </label>

                  <label for="type" class="block text-gray-700 font-bold mb-2">
                    <input
                      type="checkbox"
                      value="aaaa"
                      v-model="selectedOptions"
                    />
                    aaaa
                  </label>
                </div>

                <p class="border rounded w-full py-2 px-3 mb-2">
                  Selected Options: {{ selectedOptions }}
                </p>
              </div>
            </div>

            <div>
              <button
                class="bg-green-500 hover:bg-green-600 text-white font-bold py-2 px-4 rounded-full w-full focus:outline-none focus:shadow-outline"
                type="submit"
              >
                Add Tool
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
