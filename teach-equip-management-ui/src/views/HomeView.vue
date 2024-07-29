<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";
import { onMounted, ref, defineProps, onUnmounted } from "vue";
import Dashboard from "@/components/Dashboard.vue";
import router from "@/router";

const props = defineProps({
  isShow: {
    type: Boolean,
    default: true,
  },
});

const items = ref([]);

onMounted(async () => {
  const response = await fetch("https://jsonplaceholder.typicode.com/todos");
  items.value = await response.json();
  if (localStorage.getItem('access_token') === null) {
    router.push("/login")
  }
});

onUnmounted(async () => {
  if (localStorage.getItem('access_token') === null) {
    router.push("/login")
  }
})
</script>

<template>
  <div class="content">
    <Navbar>
      <li>
        <RouterLink to="/" class="link">DashBoard</RouterLink>
      </li>
    </Navbar>
    <MainCard>
      <Dashboard />
    </MainCard>
  </div>
  <RouterView />
</template>

<style lang="scss" scoped></style>
