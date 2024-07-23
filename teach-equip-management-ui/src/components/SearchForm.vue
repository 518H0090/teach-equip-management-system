<script setup>
import { defineEmits } from "vue";

const emit = defineEmits("search");

const search = (e) => {
  emit("search", e.target.value);
};
</script>

<template>
  <form class="py-3 px-4 flex items-center">
    <label class="sr-only">Search</label>
    <div class="relative w-full">
      <input
        type="text"
        placeholder="Search"
        @input="search"
        class="px-2 py-1 bg-gray-50 border border-gray-300 text-gray-900 outline-none focus:border-blue-300 rounded"
      />
    </div>
  </form>
</template>

<style lang="scss" scoped>
input[type="text"] {
  border-radius: 5px;
  padding-left: 1rem;
}
</style>
