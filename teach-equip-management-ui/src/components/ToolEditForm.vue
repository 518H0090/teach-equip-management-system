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
  id: "",
  toolName: "",
  description: "",
  supplierId: -1,
  categories: [],
  categoriesType: []
});

const relationShip = ref({});

onMounted(async () => {
  const itemSelector = `aside .menu .${props.page_name}`;
  const aside_item = document.querySelector(itemSelector);

  aside_item.classList.add("router-link-active");
  aside_item.classList.add("router-link-exact-active");

  await allSupplier();
  await allCategory();
  await allToolCategories();
  await toolById(2);
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

// const validateInputs = async () => {
//   const toolName = document.querySelector("#tool_name");
//   const toolDescription = document.querySelector("#tool_description");
//   const supplier = document.querySelector("#supplier");
//   const toolNameValue = toolName.value.trim();
//   const toolDescriptionValue = toolDescription.value.trim();
//   const supplierValue = supplier.value.trim();

//   let isProcess = true;
//   if (toolNameValue === "") {
//     setError(toolName, "This is required");
//     isProcess = false;
//   } else {
//     setSuccess(toolName);
//   }
//   if (toolDescriptionValue === "") {
//     setError(toolDescription, "This is required");
//     isProcess = false;
//   } else {
//     setSuccess(toolDescription);
//   }

//   if (supplierValue === "") {
//     setError(supplier, "This is required");
//     isProcess = false;
//   } else if (supplierValue === String(-1)) {
//     setError(supplier, "Must select suppliear instead of default");
//     isProcess = false;
//   } else {
//     setSuccess(supplier);
//   }

//   console.log(form);

//   if (isProcess) {
//     const newTool = {
//       toolName: form.toolName,
//       description: form.description,
//       supplierId: form.supplierId,
//     };

//     const responseTest =  fetch(
//       "https://localhost:7112/api/toolmanage/create-tool",
//       {
//         method: "POST",
//         headers: {
//           "Content-Type": "application/json",
//         },
//         body: JSON.stringify(newTool),
//       }
//     )
//       .then((response) => {
//         return response.json();
//       })
//       .then(async (data) => {
//         if (data.statusCode === 400) {
//           console.log(data.message);
//           setError(toolName, data.message);
//         }

//         if (data.statusCode === 201) {
//           const toolId = data.data;

//           if (form.categories.length > 0 && toolId > 0) {
//             form.categories.forEach(async (categoryId) => {
//               await relationToolCategory(toolId, categoryId);
//             });
//           }

//           router.push("/tool/getpage");
//           await allToolCategories();
//         }
//       })
//       .catch((error) => {
//         console.error("Error fetching data:", error);
//       });
//   }
// };

const dropdownOpen = ref(false);

const toggleDropdown = () => {
  dropdownOpen.value = !dropdownOpen.value;
};

const selectedOptions = ref([]);
const suppliers = ref({});
const categories = ref({});

const allSupplier = async () => {
  try {
    const response = await axios.get(
      "https://localhost:7112/api/toolmanage/all-supplier"
    );
    suppliers.value = response.data.data;
  } catch (error) {
    console.log("Error Fetching jobs", error);
  }
};

const allCategory = async () => {
  try {
    const response = await axios.get(
      "https://localhost:7112/api/toolmanage/all-categories"
    );
    categories.value = response.data.data;
  } catch (error) {
    console.log("Error Fetching jobs", error);
  }
};

const allCategoryName = async (event) => {
  let categoriesFilter = categories.value.filter((category) =>
    form.categories.includes(String(category.id))
  );

  selectedOptions.value = categoriesFilter.map((category) => category.type);
};

const relationToolCategory = async (toolId, categoryId) => {
  try {
    const newRelation = {
      toolId,
      categoryId,
    };

    const response = await axios.post(
      "https://localhost:7112/api/toolmanage/create-tool-category",
      newRelation
    );

    console.log(response.data.data);
  } catch (error) {
    console.log("Error Fetching jobs", error);
  }
};

const allToolCategories = async () => {
  try {
    const response = await axios.get(
      "https://localhost:7112/api/toolmanage/all-tool-categories"
    );

    const datajson = response.data.data;

    relationShip.value = datajson;
  } catch (error) {
    console.log("Error Fetching jobs", error);
  }
};

const toolById = async (itemId) => {
  try {
    const response = await axios.get(
      `https://localhost:7112/api/toolmanage/tool/find/${itemId}`
    );

    const datajson = response.data.data;
    form.id = datajson.id;
    form.toolName = datajson.toolName;
    form.description = datajson.description;
    form.supplierId = datajson.supplierId;

    const categoriesMapped = relationShip.value
      .filter((toolCategory) => toolCategory.tool.id === form.id)
      .map((toolCategory) => toolCategory.category);

      if (categoriesMapped.length > 0) {
        form.categories = categoriesMapped.map(category => category.id);
        selectedOptions.value = categoriesMapped.map(category => category.type);
      }

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
          <form>
            <h2 class="text-3xl text-center font-semibold mb-6">Edit Tool</h2>

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
              <label for="tool_description" class="block text-gray-700 font-bold mb-2"
                >Tool Description</label
              >
              <textarea
                v-model="form.description"
                id="tool_description"
                name="tool_description"
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
                id="supplier"
                v-model="form.supplierId"
                name="supplier"
                class="border rounded w-full py-2 px-3"
                required
              >
                <option value="-1">Default</option>
                <option
                  v-for="supplier in suppliers"
                  :key="supplier"
                  :value="`${supplier.id}`"
                >
                  {{ supplier.supplierName }}
                </option>
              </select>

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <div class="input-control mb-4">
              <div class="border rounded w-full py-2 px-3 dropdown">
                <button @click.prevent="toggleDropdown">
                  {{ dropdownOpen ? "Fetch Category" : "Show Category" }}
                </button>
                <div v-if="dropdownOpen" class="dropdown-content">
                  <label
                    v-for="category in categories"
                    :key="category.id"
                    class="block text-gray-700 font-bold mb-2"
                    @change="allCategoryName"
                  >
                    <input
                      type="checkbox"
                      :value="`${category.id}`"
                      v-model="form.categories"
                    />
                    {{ category.type }}
                  </label>
                </div>

                <p class="border rounded w-full py-2 px-3 mb-2">
                  Selected Options: {{ selectedOptions }}
                </p>
              </div>
            </div>

            <div>
              <button
                @click.prevent="validateInputs"
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
