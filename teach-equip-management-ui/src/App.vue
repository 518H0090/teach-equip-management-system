<script setup>
import { RouterLink, RouterView } from "vue-router";
import SideBar from "@/components/SideBar.vue";
</script>

<template>
  <div class="app">
    <SideBar />
    <RouterView />
  </div>
</template>

<style lang="scss">
:root {
  --primary: #4ade80;
  --grey: #64748b;
  --dark: #1e293b;
  --dark-alt: #334155;
  --light: #f1f5f9;
  --sidebar-width: 300px;
}

* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  font-family: "Times New Roman", Times, serif;
}

body {
  background: var(--light);
}

button {
  cursor: pointer;
  appearance: none;
  border: none;
  outline: none;
  background: none;
}

.app {
  display: flex;
}

@media (max-width: 768px) {
  .app main {
    padding-left: 6rem;
  }
}

.dropdown {
  position: relative;
  display: inline-block;
}

.dropdown button {
  background-color: #f1f1f1;
  border: 1px solid #ccc;
  padding: 8px;
  cursor: pointer;
}

.dropdown-content {
  display: block;
  position: absolute;
  background-color: #f9f9f9;
  min-width: 160px;
  box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);
  z-index: 1;
  overflow-y: scroll;
  border-radius: 10px;
  height: 100%;
  padding: 1rem;
  scrollbar-width: thin;
}

.checkbox-label {
  display: block;
  padding: 8px 12px;
}

.checkbox-label:hover {
  background-color: #ddd;
}

p {
  margin-top: 10px;
}
</style>
