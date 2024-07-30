<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";
import SupplierForm from "@/components/SupplierForm.vue";
import CategoryForm from "@/components/CategoryForm.vue";

import { useStore } from "vuex";
import { defineProps } from "vue";

const store = useStore();
</script>

<template>
  <div class="content">
    <Navbar>
      <li>
        <RouterLink to="/dashboard" class="link">Dashboard</RouterLink>
      </li>
    </Navbar>
    <MainCard>
      <p>Dashboard</p>
    </MainCard>
  </div>
</template>
