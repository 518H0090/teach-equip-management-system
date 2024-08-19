<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";
import eventBus from "@/eventBus";

import { useStore } from "vuex";
import { defineProps, onMounted, onUnmounted, reactive } from "vue";
import router from "@/router";
import { useRoute } from "vue-router";
import axios from "axios";
import { useToast } from "vue-toastification";

const route = useRoute();

const store = useStore();

const toast = useToast();

const props = defineProps({
  page_name: {
    type: String,
    default: "",
  },
});

const form = reactive({
  type: "",
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

  const typeValue = type.value.trim();
  let isProcess = true;

  if (typeValue === "") {
    setError(type, "This is required");
    isProcess = false;
  } else {
    setSuccess(type);
  }

  if (isProcess) {
    const newCategory = {
      type: form.type,
    };

    const response = fetch("https://localhost:7112/api/toolmanage/create-category", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("access_token"),
      },
      body: JSON.stringify(newCategory),
    })
      .then((response) => {
        return response.json();
      })
      .then(async (data) => {
        if (data.statusCode !== 201) {
          toast.error("Error with some error");
          setError(type, data.message);
        }

        if (data.statusCode === 201) {
          toast.success("success add new category");
          router.push("/category/getpage");
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
            <h2 class="text-3xl text-center font-semibold mb-6">Add Category</h2>

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
