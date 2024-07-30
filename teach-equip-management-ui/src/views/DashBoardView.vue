<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";
import SupplierForm from "@/components/SupplierForm.vue";
import CategoryForm from "@/components/CategoryForm.vue";
import Dashboard from "@/components/Dashboard.vue";

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
      <div class="flex flex-col bg-white relative border rounded-lg">
        <div class="overflow-x-auto sm:-mx-6 lg:-mx-8">
          <div class="inline-block min-w-full py-2 sm:px-6 lg:px-8">
            <div class="overflow-hidden">
              <Dashboard />
            </div>
          </div>
        </div>
      </div>
    </MainCard>
  </div>
</template>
