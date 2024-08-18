<script setup>

import { defineEmits, defineProps } from "vue";

const emit = defineEmits("filter");

const filter = (e) => {
  emit('filter', e.target.value)
}

const props = defineProps({
  page_name: String
})

</script>

<template>
  {{ props.page_name }}
  <label class="flex mr-4 items-center flex-nowrap">
    <input type="radio" name="show" value="all" checked @change="filter">
    <span class="whitespace-nowrap ml-1">Show All</span>
  </label>

  <label class="flex mr-4 items-center flex-nowrap">
    <input type="radio" name="show" value="today" @change="filter">
    <span class="whitespace-nowrap ml-1">Due Today</span>
  </label>

  <label class="flex mr-4 items-center flex-nowrap">
    <input type="radio" name="show" value="past" @change="filter">
    <span class="whitespace-nowrap ml-1">Past Due</span>
  </label>
</template>

<style lang="scss" scoped></style>
