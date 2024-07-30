<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";
import { onMounted, ref, defineProps, onUnmounted } from "vue";
import Dashboard from "@/components/Dashboard.vue";
import router from "@/router";
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
