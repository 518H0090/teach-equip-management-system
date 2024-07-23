<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";

import { useStore } from "vuex";
import { defineProps, onMounted, onUnmounted } from "vue";

const store = useStore();

const props = defineProps({
  isShow: {
    type: Boolean,
    default: true,
  },
});

onMounted(() => {
  const aside_item = document.querySelector("aside .menu .about");

  aside_item.classList.add("router-link-active");
  aside_item.classList.add("router-link-exact-active");
});

onUnmounted(() => {
  const aside_item = document.querySelector("aside .menu .about");

  aside_item.classList.remove("router-link-active");
  aside_item.classList.remove("router-link-exact-active");
});
</script>

<template>
  <MainCard>
    <h1>Home</h1>
    <p>This is home Edit Test</p>
    <img
      src="https://trunghieu1204.sharepoint.com/:i:/s/FamilyTree/EQ_iu-huGVxDua8NcXLOkyUBn8z3Z1czavxDt0vZCVyARg"
      alt=""
    />
    <img
      src="https://trunghieu1204.sharepoint.com/sites/FamilyTree/Avatars/landscape-countryside-cartoon-scene-background-green-field-with-dirt-path-and-mountain-background-generative-ai-photo.jpg"
      alt=""
    />
  </MainCard>
</template>
