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
  page_name: {
    type: String,
    default: "",
  },
});

const form = reactive({
  supplierName: "",
  contactName: "",
  address: "",
  phone: "",
});

let paramId = route.params.id;

onMounted(() => {
  const itemSelector = `aside .menu .${props.page_name}`;
  const aside_item = document.querySelector(itemSelector);

  aside_item.classList.add("router-link-active");
  aside_item.classList.add("router-link-exact-active");
  paramId = route.params.id;
  if (paramId !== undefined || paramId !== null) {
    supplierById(paramId);
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
  const supplierName = document.querySelector("#supplier_name");
  const contactName = document.querySelector("#contact_name");
  const address = document.querySelector("#address");
  const phone = document.querySelector("#phone");

  const supplierNameValue = supplierName.value.trim();
  const contactNameValue = contactName.value.trim();
  const addressValue = address.value.trim();
  const phoneValue = phone.value.trim();
  const regexPhone = /^0?\d{8,14}$/;
  let isProcess = true;

  if (supplierNameValue === "") {
    setError(supplierName, "This is required");
    isProcess = false;
  } else {
    setSuccess(supplierName);
  }

  if (contactNameValue === "") {
    setError(contactName, "This is required");
    isProcess = false;
  } else {
    setSuccess(contactName);
  }

  if (addressValue === "") {
    setError(address, "This is required");
    isProcess = false;
  } else {
    setSuccess(address);
  }

  if (phoneValue === "") {
    setError(phone, "This is required");
    isProcess = false;
  } else if (!regexPhone.test(phoneValue)) {
    setError(phone, "Not follow format of phone");
    isProcess = false;
  } else if (regexPhone.test(phoneValue)) {
    setSuccess(phone);
  }

  if (isProcess) {
    const updateSupplier = {
      id: paramId,
      supplierName: form.supplierName,
      contactName: form.contactName,
      address: form.address,
      phone: form.phone,
    };

    try {
      const response = await axios.put(
        "https://localhost:7112/api/toolmanage/update-supplier",
        updateSupplier,
        {
          headers: {
            Authorization: "Bearer " + localStorage.getItem("access_token"),
          },
        }
      );

      router.push("/supplier/getpage");
    } catch (error) {
      console.log("Error Fetching jobs", error);
    }
  }
};

const supplierById = async (supplierId) => {
  try {
    const response = await axios.get(
      `https://localhost:7112/api/toolmanage/supplier/find/${supplierId}`,
      {
        headers: {
          Authorization: "Bearer " + localStorage.getItem("access_token"),
        },
      }
    );

    const datajson = response.data.data;
    form.id = datajson.id;
    form.supplierName = datajson.supplierName;
    form.contactName = datajson.contactName;
    form.address = datajson.address;
    form.phone = datajson.phone;
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
              Edit Supplier
            </h2>

            <div class="input-control mb-4">
              <label class="block text-gray-700 font-bold mb-2"
                >Supplier Name</label
              >
              <input
                v-model="form.supplierName"
                type="text"
                id="supplier_name"
                name="supplier_name"
                class="border rounded w-full py-2 px-3 mb-2"
                placeholder="eg. Công ty TNHH một trăm thành Viên"
              />

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <div class="input-control mb-4">
              <label class="block text-gray-700 font-bold mb-2"
                >Contact Name</label
              >
              <input
                v-model="form.contactName"
                type="text"
                id="contact_name"
                name="contact_name"
                class="border rounded w-full py-2 px-3 mb-2"
                placeholder="eg. Nguyễn Văn A"
              />

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <div class="input-control mb-4">
              <label class="block text-gray-700 font-bold mb-2">Address</label>
              <input
                v-model="form.address"
                type="text"
                id="address"
                name="address"
                class="border rounded w-full py-2 px-3 mb-2"
                placeholder="eg. 193 Điện Biên Phủ, ..."
              />

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <div class="input-control mb-4">
              <label class="block text-gray-700 font-bold mb-2">Phone</label>
              <input
                v-model="form.phone"
                type="text"
                id="phone"
                name="phone"
                class="border rounded w-full py-2 px-3 mb-2"
                placeholder="eg. 0123456789"
              />

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
