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

const form = reactive({
  username: "",
  password: "",
  email: "",
  roleId: -1,
});

onMounted(() => {
  const itemSelector = `aside .menu .${props.page_name}`;
  const aside_item = document.querySelector(itemSelector);

  aside_item.classList.add("router-link-active");
  aside_item.classList.add("router-link-exact-active");

  allRoles();
});

onUnmounted(() => {
  const itemSelector = `aside .menu .${props.page_name}`;
  const aside_item = document.querySelector(itemSelector);

  aside_item.classList.remove("router-link-active");
  aside_item.classList.remove("router-link-exact-active");
});

const roles = ref({});

const allRoles = async () => {
  try {
    const response = await axios.get(
      "https://localhost:7112/api/usermanage/all-roles"
    );
    roles.value = response.data.data;
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
  const username = document.querySelector("#username");
  const password = document.querySelector("#password");
  const email = document.querySelector("#email");
  const roleId = document.querySelector("#roleId");

  const usernameValue = username.value.trim();
  const passwordValue = password.value.trim();
  const emailValue = email.value.trim();
  const roleIdValue = roleId.value.trim();

  const regexEmail = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
  let isProcess = true;

  if (usernameValue === "") {
    setError(username, "This is required");
    isProcess = false;
  } else if (usernameValue.length < 8) {
    setError(username, "Username must at least 8 character");
    isProcess = false;
  } else if (usernameValue.length > 20) {
    setError(username, "Username must less than 20 character");
    isProcess = false;
  } else {
    setSuccess(username);
  }

  if (passwordValue === "") {
    setError(password, "This is required");
    isProcess = false;
  } else if (passwordValue.length < 8) {
    setError(password, "Password must at least 8 character");
    isProcess = false;
  } else if (passwordValue.length > 20) {
    setError(password, "Password must less than 20 character");
    isProcess = false;
  } else {
    setSuccess(password);
  }

  if (emailValue === "") {
    setError(email, "This is required");
    isProcess = false;
  } else if (regexEmail.test(emailValue)) {
    setSuccess(email);
  } else if (!regexEmail.test(emailValue)) {
    setError(email, "Not follow format of email");
    isProcess = false;
  }

  if (roleIdValue === "") {
    setError(roleId, "This is required");
    isProcess = false;
  } else if (roleIdValue === String(-1)) {
    setError(roleId, "Must select Role instead of default");
    isProcess = false;
  } else {
    setSuccess(roleId);
  }

  if (isProcess) {
    const newAccount = {
      username: form.username,
      password: form.password,
      email: form.email,
      roleId: form.roleId,
    };

    console.log(newAccount);

    const response = fetch(
      "https://localhost:7112/api/usermanage/create-user",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(newAccount),
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
          router.push("/account/getpage");
        }
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
      });
  }
};

const dropdownOpen = ref(false);

const toggleDropdown = () => {
  dropdownOpen.value = !dropdownOpen.value;

  const inputPassword = document.getElementById("password");

  if (dropdownOpen.value) {
    inputPassword.type = "text";
  } else {
    inputPassword.type = "password";
  }
};
</script>

<template>
  <MainCard>
    <section class="bg-green-50">
      <div class="container m-auto">
        <div class="bg-white shadow-md rounded-md border m-4 md:m-0">
          <form @submit.prevent="validateInputs">
            <h2 class="text-3xl text-center font-semibold mb-6">Add Account</h2>

            <div class="input-control mb-4">
              <label class="block text-gray-700 font-bold mb-2">Username</label>
              <input
                v-model="form.username"
                type="text"
                id="username"
                name="username"
                class="border rounded w-full py-2 px-3 mb-2"
                placeholder="eg. Username"
              />

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <div class="input-control mb-4">
              <label class="block text-gray-700 font-bold mb-2">Password</label>
              <input
                v-model="form.password"
                type="password"
                id="password"
                name="password"
                class="border rounded w-full py-2 px-3 mb-2"
                placeholder="eg. Password"
              />

              <button @click.prevent="toggleDropdown">
                {{ dropdownOpen ? "Hide Password" : "Show Password" }}
              </button>

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <div class="input-control mb-4">
              <label class="block text-gray-700 font-bold mb-2">Email</label>
              <input
                v-model="form.email"
                type="text"
                id="email"
                name="email"
                class="border rounded w-full py-2 px-3 mb-2"
                placeholder="eg. Email"
              />

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <div class="input-control mb-4">
              <label for="type" class="block text-gray-700 font-bold mb-2"
                >Role</label
              >
              <select
                v-model="form.roleId"
                id="roleId"
                name="roleId"
                class="border rounded w-full py-2 px-3"
                required
              >
                <option value="-1">Default</option>
                <option
                  v-for="role in roles"
                  :key="role.id"
                  :value="`${role.id}`"
                >
                  {{ role.roleName }}
                </option>
              </select>

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
