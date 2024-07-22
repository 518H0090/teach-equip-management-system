<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";
import SideBar from "@/components/SideBar.vue";
import { useStore } from "vuex";

const store = useStore();
</script>

<template>
  <SideBar />
  <div class="content">
    <Navbar />
    <MainCard>
      <h1>Home</h1>
      <p>
        This is home
        pagaaaaeaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
      </p>
      <p>{{ store.state.is_expanded }}</p>
      <img
        src="https://trunghieu1204.sharepoint.com/:i:/s/FamilyTree/EQ_iu-huGVxDua8NcXLOkyUBn8z3Z1czavxDt0vZCVyARg"
        alt=""
      />
      <img
        src="https://trunghieu1204.sharepoint.com/sites/FamilyTree/Avatars/landscape-countryside-cartoon-scene-background-green-field-with-dirt-path-and-mountain-background-generative-ai-photo.jpg"
        alt=""
      />
    </MainCard>
  </div>
</template>
