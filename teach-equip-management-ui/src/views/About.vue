<script setup>
import { RouterLink, RouterView } from "vue-router";
import { useRoute } from 'vue-router';
import { computed } from "vue";
import eventBus from '@/eventBus';

const route = useRoute();

const isRouteActive = computed(() => {
  return (prefix) => {
    return route.path.startsWith(prefix);
  };
});

const updatePath = (path) => {
  eventBus.emit('data-sent', path);
};
</script>


<template>
  <router-link to="/about/getpage" @click="updatePath('/home')">Get</router-link>
    <router-link to="/about/addpage" @click="updatePath('/home')">Add</router-link>
    <router-link to="/about/editpage" @click="updatePath('/home')">Edit</router-link>

  <router-view></router-view>
</template>

<style scoped>
h1 {
  margin-left: 20rem;
}
</style>
