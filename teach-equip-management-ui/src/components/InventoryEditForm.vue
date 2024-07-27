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
  totalQuantity: 0,
  amountBorrow: 0,
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
  const totalQuantity = document.querySelector("#total_quantity");
  const amountBorrow = document.querySelector("#amount_borrow");

  const totalQuantityValue = totalQuantity.value.trim();
  const amountBorrowValue = amountBorrow.value.trim();
  let isProcess = true;

  if (totalQuantityValue < 0) {
    setError(totalQuantity, "Total Quantity must greater or equal 0");
    isProcess = false;
  } else {
    setSuccess(totalQuantity);
  }

  if (amountBorrowValue < 0) {
    setError(amountBorrow, "Amount Borrow must greater or equal 0");
    isProcess = false;
  } else {
    setSuccess(amountBorrow);
  }

  console.log(form);

  if (isProcess) {
    const updateInventory = {
      id: form.id,
      totalQuantity: form.totalQuantity,
      amountBorrow: form.amountBorrow,
    };

    const response = fetch(
      "https://localhost:7112/api/inventorymanage/update-inventory",
      {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(updateInventory),
      }
    )
      .then((response) => {
        return response.json();
      })
      .then(async (data) => {
        if (data.statusCode !== 202) {
          setError(type, data.message);
        }

        if (data.statusCode === 202) {
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
      `https://localhost:7112/api/inventorymanage/inventory/find/${itemId}`
    );

    const datajson = response.data.data;
    form.id = datajson.id;
    form.totalQuantity = datajson.totalQuantity;
    form.amountBorrow = datajson.amountBorrow;

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
            <h2 class="text-3xl text-center font-semibold mb-6">
              Edit Inventory
            </h2>

            <div class="input-control mb-4">
              <label class="block text-gray-700 font-bold mb-2">Total Quantity</label>
              <input
                v-model="form.totalQuantity"
                type="number"
                id="total_quantity"
                name="total_quantity"
                class="border rounded w-full py-2 px-3 mb-2"
                placeholder="eg. 10"
              />

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <div class="input-control mb-4">
              <label class="block text-gray-700 font-bold mb-2">Amount Borrow</label>
              <input
                v-model="form.amountBorrow"
                type="number"
                id="amount_borrow"
                name="amount_borrow"
                class="border rounded w-full py-2 px-3 mb-2"
                placeholder="eg. 10"
              />

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <div>
              <button
                class="bg-green-500 hover:bg-green-600 text-white font-bold py-2 px-4 rounded-full w-full focus:outline-none focus:shadow-outline"
                type="submit"
              >
                Edit Inventory
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
