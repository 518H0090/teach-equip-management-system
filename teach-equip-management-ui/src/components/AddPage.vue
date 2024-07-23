<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";

import { useStore } from "vuex";
import { defineProps, onMounted, onUnmounted } from "vue";

const store = useStore();

const props = defineProps({
  isShow: {
    type: Boolean,
    default: true,
  },
});

onMounted(() => {
  const aside_item = document.querySelector("aside .menu .about");

  aside_item.classList.add("router-link-active");
  aside_item.classList.add("router-link-exact-active");
});

onUnmounted(() => {
  const aside_item = document.querySelector("aside .menu .about");

  aside_item.classList.remove("router-link-active");
  aside_item.classList.remove("router-link-exact-active");
});

const setError = (element, message) => {
  const inputControl = element.parentElement;
  const errorDisplay = inputControl.querySelector(".error");
  errorDisplay.innerText = message;
  console.log(inputControl);
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

const validateInputs = () => {
  const test = document.querySelector("#name");
  const testValue = test.value.trim();

  if (testValue === "") {
    setError(test, "This is required");
  } else {
    setSuccess(test);
  }
};
</script>

<template>
  <MainCard>
    <section class="bg-green-50">
      <div class="container m-auto">
        <div class="bg-white shadow-md rounded-md border m-4 md:m-0">
          <form @submit.prevent="validateInputs">
            <h2 class="text-3xl text-center font-semibold mb-6">Add Job</h2>

            <div class="input-control mb-4">
              <label for="type" class="block text-gray-700 font-bold mb-2"
                >Job Type</label
              >
              <select
                id="type"
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
              <label class="block text-gray-700 font-bold mb-2"
                >Job Listing Name</label
              >
              <input
                type="text"
                id="name"
                name="name"
                class="border rounded w-full py-2 px-3 mb-2"
                placeholder="eg. Beautiful Apartment In Miami"
              />

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <div class="input-control mb-4">
              <label for="type" class="block text-gray-700 font-bold mb-2"
                >Salary</label
              >
              <select
                id="salary"
                name="salary"
                class="border rounded w-full py-2 px-3"
              >
                <option value="Under $50K">under $50K</option>
                <option value="$50K - $60K">$50 - $60K</option>
                <option value="$60K - $70K">$60 - $70K</option>
                <option value="$70K - $80K">$70 - $80K</option>
                <option value="$80K - $90K">$80 - $90K</option>
                <option value="$90K - $100K">$90 - $100K</option>
                <option value="$100K - $125K">$100 - $125K</option>
                <option value="$125K - $150K">$125 - $150K</option>
                <option value="$150K - $175K">$150 - $175K</option>
                <option value="$175K - $200K">$175 - $200K</option>
                <option value="Over $200K">Over $200K</option>
              </select>

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <div class="input-control mb-4">
              <label class="block text-gray-700 font-bold mb-2">
                Location
              </label>
              <input
                type="text"
                id="location"
                name="location"
                class="border rounded w-full py-2 px-3 mb-2"
                placeholder="Company Location"
              />

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <h3 class="text-2xl mb-5">Company Info</h3>

            <div class="input-control mb-4">
              <label for="company" class="block text-gray-700 font-bold mb-2"
                >Company Name</label
              >
              <input
                type="text"
                id="company"
                name="company"
                class="border rounded w-full py-2 px-3"
                placeholder="Company Name"
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
                id="company_description"
                name="company_description"
                class="border rounded w-full py-2 px-3"
                rows="4"
                placeholder="What does your company do?"
              ></textarea>

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <div>
              <button
                class="bg-green-500 hover:bg-green-600 text-white font-bold py-2 px-4 rounded-full w-full focus:outline-none focus:shadow-outline"
                type="submit"
              >
                Add Job
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
  max-width: 980px;

  form {
    padding: 1rem 0;
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
