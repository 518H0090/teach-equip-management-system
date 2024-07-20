<script setup>
import { onBeforeMount } from "vue";
import { useStore } from "vuex";

const store = useStore();

onBeforeMount(async () => {
  await store.dispatch("setIsExpanded", false);
});
</script>

<template>
  <main :class="`${store.state.is_expanded ? 'is-expanded' : ''}`">
    <slot></slot>
  </main>
</template>

<style lang="scss" scoped>
main {
  width: calc(100vw - (2rem + 32px));
  padding: 2rem;
  margin-top: 4rem;

  * {
    max-width: 99%;
    word-wrap: break-word;
    overflow-wrap: break-word;
  }

  &.is-expanded {
    width: calc(100vw - var(--sidebar-width));
  }
}
</style>
