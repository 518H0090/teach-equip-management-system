<script setup>
import { RouterLink, RouterView } from "vue-router";
import { useRoute } from "vue-router";
import { computed } from "vue";
import eventBus from "@/eventBus";
import Navbar from "@/components/Navbar.vue";

const route = useRoute();

const isRouteActive = computed(() => {
  return (prefix) => {
    return route.path.startsWith(prefix);
  };
});

const updatePath = (path) => {
  eventBus.emit("data-sent", path);
};
</script>

<template>
  <div class="content">
    <Navbar />
    <RouterView />
  </div>
</template>

<style scoped>
router-link-active,
router-link-exact-active {
  border-right: 5px solid var(--primary);
}
</style>
