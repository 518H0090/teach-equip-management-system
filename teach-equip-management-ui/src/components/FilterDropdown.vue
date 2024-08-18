<script setup>
import { onMounted, defineProps, ref, computed } from "vue";
import axios from "axios";

const show = ref(false)

const props = defineProps({
  items: Array,
});

onMounted(async () => {
  console.log(props.items)
})

const uniqueCategoryTypes = computed(() => {
  const typesSet = new Set();
  props.items.forEach(item => {
    item.category.forEach(cat => {
      typesSet.add(cat.type);
    });
  });
  return Array.from(typesSet);
});

import { defineEmits } from "vue";

const emit = defineEmits("filter");

const filter = (e) => {
  emit('filter', e.target.value)
}
</script>

<template>
  <div class="relative flex items-center w-full px-4">
    <button @click="show = !show" class="w-full flex items-center justify-center hover:underline bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded ml-2">
      Filter
    </button>

    <div v-if="show" class="absolute top-12 right-0 z-10 w-72 p-3 bg-white rounded-lg shadow">
      <h6 class="mb-3 text-sm font-medium text-gray-900">
        Category
      </h6>
      <ul class="space-y-2 text-sm">
          <li
          v-for="category in uniqueCategoryTypes"
          :key="category"
          class="block text-gray-700 font-bold mb-2"
        >
          <input
            type="checkbox"
            @change="filter"
            :value="`${category}`"
          />
          {{ category }}
        </li>
      </ul>
    </div>
  </div>
</template>

<style lang="scss" scoped>
</style>
