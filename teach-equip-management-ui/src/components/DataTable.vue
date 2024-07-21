<script setup>
import { computed, defineProps, ref } from "vue";
import SearchForm from "@/components/SearchForm.vue";
import FilterRadio from "@/components/FilterRadio.vue";
import FilterDropdown from "@/components/FilterDropdown.vue";

const props = defineProps({
  items: Array,
});

const searchFilter = ref('');

const filteredItems = computed(() => {
    if (searchFilter.value !== '') {
        return props.items.filter(item => item.title.includes(searchFilter.value));
    }

    return props.items;
});

const handleSearch = (search) => {
    searchFilter.value = search;
}

</script>

<template>
  <div class="bg-white relative border rounded-lg">
    <div class="flex items-center justify-between">
        <!-- Search bar   -->
        <SearchForm  @search="handleSearch"/>

        <div class="flex items-center justify-end text-sm font-semibold">
          <!-- Radio buttons   -->
          <FilterRadio />
          <!-- List of filters for statues   -->
          <FilterDropdown />
        </div>
    </div>

    <table class="w-full text-sm text-left text-gray-500">
      <thead class="text-xs text-gray-700 uppercase bg-gray-50">
        <tr>
          <th class="px-4 py-3">UserId</th>
          <th class="px-4 py-3">Id</th>
          <th class="px-4 py-3">Title</th>
          <th class="px-4 py-3">Completed</th>
          <th class="px-4 py-3">
            <span class="sr-only">Actions</span>
          </th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in filteredItems" :key="item.id" class="border-b">
          <td class="px-4 py-3 font-medium text-gray-900">{{ item.id }}</td>
          <td class="px-4 py-3 font-medium text-gray-900">{{ item.userId }}</td>
          <td class="px-4 py-3">{{ item.title }}</td>
          <td class="px-4 py-3">{{ item.completed }}</td>
          <td class="px-4 py-3">{{ item.id }}</td>
          <td class="px-4 py-3 flex items-center justify-end">
            <RouterLink to="/" class="text-indigo-500 hover:underline"
              >Details</RouterLink
            >
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<style lang="scss" scoped></style>
