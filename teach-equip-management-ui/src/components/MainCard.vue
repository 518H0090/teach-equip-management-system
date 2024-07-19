<script setup>
import { useStore } from "vuex";

const store = useStore();
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

  * {
    max-width: 99%;
    overflow: hidden;
  }

  &.is-expanded {
    width: calc(100vw - var(--sidebar-width));
  }
}
</style>
