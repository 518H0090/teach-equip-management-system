<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";
import eventBus from "@/eventBus";

import { useStore } from "vuex";
import { defineProps, onMounted, onUnmounted, reactive, ref } from "vue";
import router from "@/router";
import { useRoute } from "vue-router";
import axios from "axios";
import { useToast } from "vue-toastification";

const toast = useToast();

const route = useRoute();

const store = useStore();

const profileSrc = ref("http://localhost:5173/src/assets/avatarcapybara.jpg");

const props = defineProps({
  page_name: {
    type: String,
    default: "",
  },
});

const form = reactive({
  toolName: "",
  description: "",
  supplierId: -1,
  categories: [],
  avatar: "",
  unit: "-1"
});

onMounted(() => {
  const itemSelector = `aside .menu .${props.page_name}`;
  const aside_item = document.querySelector(itemSelector);

  aside_item.classList.add("router-link-active");
  aside_item.classList.add("router-link-exact-active");

  allSupplier();
  allCategory();
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
  const toolName = document.querySelector("#tool_name");
  const toolDescription = document.querySelector("#tool_description");
  const supplier = document.querySelector("#supplier");
  const unit = document.querySelector("#unit");
  const toolNameValue = toolName.value.trim();
  const toolDescriptionValue = toolDescription.value.trim();
  const supplierValue = supplier.value.trim();
  const unitValue = unit.value.trim();

  let isProcess = true;
  if (toolNameValue === "") {
    setError(toolName, "This is required");
    isProcess = false;
  } else {
    setSuccess(toolName);
  }
  if (toolDescriptionValue === "") {
    setError(toolDescription, "This is required");
    isProcess = false;
  } else {
    setSuccess(toolDescription);
  }

  if (supplierValue === "") {
    setError(supplier, "This is required");
    isProcess = false;
  } else if (supplierValue === String(-1)) {
    setError(supplier, "Must select suppliear instead of default");
    isProcess = false;
  } else {
    setSuccess(supplier);
  }

  if (unitValue === "") {
    setError(unit, "This is required");
    isProcess = false;
  } else if (unitValue === String(-1)) {
    setError(unit, "Must select Unit instead of default");
    isProcess = false;
  } else {
    setSuccess(unit);
  }

  if (isProcess) {
    const newTool = {
      toolName: form.toolName,
      description: form.description,
      supplierId: form.supplierId,
      unit: form.unit,
      fileUpload: typeof form.avatar === "string" ? null : form.avatar
    };

    const formData = new FormData();
    formData.append("ToolName", newTool.toolName);
    formData.append("Description", newTool.description);
    formData.append("SupplierId", newTool.supplierId);
    formData.append("Unit", newTool.unit);
    formData.append("FileUpload", newTool.fileUpload);

    const response = fetch("http://localhost:8080/api/toolmanage/create-tool", {
      method: "POST",
      headers: {
        Authorization: "Bearer " + localStorage.getItem("access_token"),
      },
      body: formData,
    })
      .then((response) => {
        return response.json();
      })
      .then(async (data) => {
        if (data.statusCode === 400) {
          console.log(data.message);
          setError(toolName, data.message);
        }

        if (data.statusCode === 201) {
          const toolId = data.data;

          if (form.categories.length > 0 && toolId > 0) {
            form.categories.forEach(async (categoryId) => {
              await relationToolCategory(toolId, categoryId);
            });
          }

          try {
            const newInventory = {
              toolId,
              totalQuantity: 0,
              amountBorrow: 0,
            };

            const response = await axios.post(
              "http://localhost:8080/api/inventorymanage/create-inventory",
              newInventory,
              {
                headers: {
                  Authorization: "Bearer " + localStorage.getItem("access_token"),
                },
              }
            );

            console.log(response.data.data);
          } catch (error) {
            console.log("Error Create Inventories", error);
          }

          await allToolCategories();
          toast.success("success create new tool");
          router.push("/tool/getpage");
          await allToolCategories();
        }
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
      });
  } else {
    toast.error("Something error");
  }
};

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
      "http://localhost:8080/api/toolmanage/all-supplier",
      {
        headers: {
          Authorization: "Bearer " + localStorage.getItem("access_token"),
        },
      }
    );
    suppliers.value = response.data.data;
  } catch (error) {
    console.log("Error Fetching jobs", error);
  }
};

const allCategory = async () => {
  try {
    const response = await axios.get(
      "http://localhost:8080/api/toolmanage/all-categories",
      {
        headers: {
          Authorization: "Bearer " + localStorage.getItem("access_token"),
        },
      }
    );
    categories.value = response.data.data;
  } catch (error) {
    console.log("Error Fetching jobs", error);
  }
};

const allCategoryName = async () => {
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
      "http://localhost:8080/api/toolmanage/create-tool-category",
      newRelation,
      {
        headers: {
          Authorization: "Bearer " + localStorage.getItem("access_token"),
        },
      }
    );

    console.log(response.data.data);
  } catch (error) {
    console.log("Error Fetching jobs", error);
  }
};

const relationShip = ref({});

const allToolCategories = async () => {
  try {
    const response = await axios.get(
      "http://localhost:8080/api/toolmanage/all-tool-categories",
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
  }
};

const onFileChange = (event) => {
  const file = event.target.files[0];
  if (file) {
    profileSrc.value = URL.createObjectURL(file);
    form.avatar = file;
  }
};
</script>

<template>
  <MainCard>
    <section class="bg-green-50">
      <div class="container m-auto">
        <div class="bg-white shadow-md rounded-md border m-4 md:m-0">
          <form>
            <h2 class="text-3xl text-center font-semibold mb-6">Add Tool</h2>

            <div class="hero">
              <div class="card">
                <img :src="`${profileSrc}`" alt="capybara" id="profile-pic" />
                <label for="input-file">Update Image</label>
                <input
                  @change="onFileChange"
                  type="file"
                  accept="image/jpeg, image/png, image/jpg"
                  id="input-file"
                />
              </div>
            </div>

            <div class="input-control mb-4 mt-6">
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
              <label for="type" class="block text-gray-700 font-bold mb-2">Unit</label>
              <select
                v-model="form.unit"
                id="unit"
                name="unit"
                class="border rounded w-full py-2 px-3"
                required
              >
                <option value="-1">Default</option>
                <option value="pieces">pieces</option>
                <option value="set">set</option>
                <option value="dozen">dozen</option>
                <option value="bottle">bottle</option>
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

.hero {
  width: 100%;
  height: 18rem;
  display: flex;
  align-items: center;
  justify-content: center;
}

.card {
  width: 480px;
  background: #fff;
  border-radius: 15px;
  text-align: center;
  color: #333;
  display: flex;
  flex-direction: column;
  align-items: center;
  box-shadow: 0.1rem 0.2rem 0.6rem var(--dark);

  img {
    width: 180x;
    height: 180px;
    margin-top: 40px;
    margin-bottom: 30px;
    justify-content: center;
  }

  label {
    display: block;
    width: 200px;
    background: #e3362c;
    color: #fff;
    padding: 12px;
    margin: 10px auto;
    border-radius: 5px;
    cursor: pointer;
  }

  input {
    display: none;
  }
}

</style>
