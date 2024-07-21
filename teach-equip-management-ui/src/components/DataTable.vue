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
  <div class="flex flex-col bg-white relative border rounded-lg">
    <div class="overflow-x-auto sm:-mx-6 lg:-mx-8">
      <div class="inline-block min-w-full py-2 sm:px-6 lg:px-8">
        <div class="overflow-hidden">
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
          <table
            class="min-w-full text-left text-sm font-light text-surface dark:text-white">
            <thead
              class="border-b border-neutral-200 font-medium dark:border-white/10">
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
                <td class="px-4 py-3 flex items-center justify-end">
                  <RouterLink to="/" class="text-indigo-500 hover:underline"
                    >Details</RouterLink
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

@media (max-width:768px) {
  table {
    width: 60%;
  }
}


@media (max-width:468px) {
  table {
    width: 10%;
  }
}

</style>
