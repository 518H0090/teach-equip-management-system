<script setup>
import { computed, defineProps, ref } from "vue";
import SearchForm from "@/components/SearchForm.vue";
import FilterRadio from "@/components/FilterRadio.vue";
import FilterDropdown from "@/components/FilterDropdown.vue";
import router from "@/router";
import axios from "axios";

const props = defineProps({
  items: Array,
  keys: Array,
  page_name: {
    type: String,
    default: "",
  },
  page_service: String
});

console.log(props)

const searchFilter = ref("");

const filteredItems = computed(() => {
  if (searchFilter.value !== "") {
    if (props.page_name === "about") {
      return props.items.filter((item) =>
        item.title.includes(searchFilter.value)
      );
    } else if (props.page_name === "supplier") {
      return props.items.filter(
        (item) =>
          item.supplierName.includes(searchFilter.value) ||
          item.phone.includes(searchFilter.value)
      );
    } else if (props.page_name === "category") {
      return props.items.filter(
        (item) =>
          item.type.includes(searchFilter.value) ||
          item.unit.includes(searchFilter.value)
      );
    } else if (props.page_name === "tool") {
      return props.items.filter(
        (item) =>
          item.toolName.includes(searchFilter.value) ||
          item.description.includes(searchFilter.value) ||
          item.supplier.supplierName.includes(searchFilter.value)
      );
    } else if (props.page_name === "account") {
      return props.items.filter(
        (item) =>
          item.username.includes(searchFilter.value) ||
          item.email.includes(searchFilter.value)
      );
    }

    else if (props.page_name === "inventory") {
      return props.items
    }
  }

  return props.items;
});

const handleSearch = (search) => {
  searchFilter.value = search;
};

const removeItem = async (id) => {
  var confirm = window.confirm("Are you sure you want to remove this item?");

  if (confirm) {
    try {
      const response = await axios.delete(
        `https://localhost:7112/api/${props.page_service}/remove-${props.page_name}/${id}`
      );
      
      router.push(`/${props.page_name}/getpage`).then(() => {
        router.go();
      })
    } catch (error) {
      console.log("Error Fetching SupplierInfo", error);
    }
  }
};
</script>

<template>
  <div class="flex flex-col bg-white relative border rounded-lg">
    <div class="overflow-x-auto sm:-mx-6 lg:-mx-8">
      <div class="inline-block min-w-full py-2 sm:px-6 lg:px-8">
        <div class="overflow-hidden">
          <div class="flex items-center justify-between">
            <!-- Search bar   -->
            <SearchForm @search="handleSearch" />

            <div class="flex items-center justify-end text-sm font-semibold">
              <!-- Radio buttons   -->
              <!-- <FilterRadio /> -->
              <!-- List of filters for statues   -->
              <!-- <FilterDropdown /> -->
            </div>
          </div>
          <table
            class="min-w-full text-left text-sm font-light text-surface dark:text-white"
          >
            <thead
              class="border-b border-neutral-200 font-medium dark:border-white/10"
            >
              <tr v-show="props.keys === null">
                <th class="px-4 py-3 uppercase">UserId</th>
                <th class="px-4 py-3 uppercase">Id</th>
                <th class="px-4 py-3 uppercase">Title</th>
                <th class="px-4 py-3 uppercase">Completed</th>
                <th class="px-4 py-3 uppercase">
                  <span class="sr-only">Actions</span>
                </th>
              </tr>
              <tr v-show="props.keys !== null">
                <th v-for="key in keys" :key="key" class="px-4 py-3 uppercase">
                  {{ key.split("_").length > 1 ? key.split("_").join(' ') : key }}
                </th>
              </tr>
            </thead>
            <tbody>
              <tr
                v-show="props.keys === null"
                v-for="item in filteredItems"
                :key="item.id"
                class="border-b"
              >
                <td class="px-4 py-3 font-medium text-gray-900">
                  {{ item.id }}
                </td>
                <td class="px-4 py-3 font-medium text-gray-900">
                  {{ item.userId }}
                </td>
                <td class="px-4 py-3">{{ item.title }}</td>
                <td class="px-4 py-3">{{ item.completed }}</td>
                <td class="px-4 py-3 flex items-center justify-end">
                  <RouterLink to="/" class="text-indigo-500 hover:underline"
                    >Details</RouterLink
                  >
                </td>
              </tr>

              <tr
                v-show="props.keys !== null"
                v-for="item in filteredItems"
                :key="item.id"
                class="border-b"
              >
                <td
                  v-for="value in item"
                  :key="value"
                  class="px-4 py-3 font-medium text-gray-900"
                >
                  <span
                    :id="`${value.supplierId}`"
                    v-if="value && value.supplierId && value.supplierName"
                  >
                    {{ value.supplierName }}
                  </span>
                  <span
                    v-else-if="
                      value &&
                      Array.isArray(value) &&
                      props.page_name === 'tool'
                    "
                  >
                    {{
                      value.length > 0
                        ? `Category: ${value
                            .map((category) => category.type)
                            .join(" - ")}`
                        : "Not contain Category"
                    }}
                  </span>
                  <span
                    v-else-if="
                      value &&
                      Array.isArray(value) &&
                      props.page_name === 'account'
                    "
                  >
                    {{
                      value.length > 0
                        ? ` ${value.map((role) => role.roleName)} `
                        : "Not contain role"
                    }}
                  </span>
                  <span
                  v-else-if="
                    value &&
                    Array.isArray(value) &&
                    props.page_name === 'inventory'
                  "
                >
                  {{
                    value.length > 0
                      ? `${value} `
                      : "Missing Tool - Something error please contact admin"
                  }}
                </span>
                <span
                v-else-if="
                  value &&
                  Array.isArray(value) &&
                  props.page_name === 'invoice'
                "
              >
                {{
                  value.length > 0
                    ? `${value} `
                    : "Missing Invoice"
                }}
              </span>
                  <span v-else>
                    {{ value }}
                  </span>
                </td>

                <td class="px-4 py-3 flex items-center justify-end" v-show="props.page_name !== 'inventory' && props.page_name !== 'invoice'">
                  <RouterLink
                    :to="`/${props.page_name}/editpage/${item.id}`"
                    class="text-indigo-500 hover:underline bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded mr-2"
                    >Edit</RouterLink
                  >
                  <button
                    @click="removeItem(item.id)"
                    class="text-indigo-500 hover:underline bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded ml-2"
                  >
                    Remove
                  </button>
                </td>

                <td class="px-4 py-3 flex items-center justify-end" v-show="props.page_name === 'invoice'">
                  <RouterLink
                    :to="`/inventory/edit-invoice/${item.id}`"
                    class="text-indigo-500 hover:underline bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded mr-2"
                    >Edit</RouterLink
                  >
                  <button
                    @click="removeItem(item.id)"
                    class="text-indigo-500 hover:underline bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded ml-2"
                  >
                    Remove
                  </button>
                </td>

                <td class="px-4 py-3 flex items-center justify-end" v-show="props.page_name === 'inventory'">
                  <RouterLink
                    :to="`/${props.page_name}/editpage/${item.id}`"
                    class="text-indigo-500 hover:underline bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded mr-2"
                    >Edit</RouterLink
                  >
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
@media (max-width: 768px) {
  div.flex.flex-col {
    width: fit-content;
  }
}
</style>
