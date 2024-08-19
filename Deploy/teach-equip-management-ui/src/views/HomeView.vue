<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";
import { onMounted, ref, defineProps, onUnmounted } from "vue";
import Dashboard from "@/components/Dashboard.vue";
import router from "@/router";
import axios from "axios";

const users = ref({});
const tools = ref({});

onMounted(async () => {
  await allUsers();
  await allTools();
});

const allUsers = async () => {
  try {
    const response = await axios.get(`http://localhost:8080/api/usermanage/all-users`, {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("access_token"),
      },
    });

    users.value = response.data.data.length;
  } catch (error) {
    console.log("Error Fetching jobs", error);
    // if (error.response.status === 401) {
    //   console.log("Error Fetching jobs", error);
    // }
  }
};

const allTools = async () => {
  try {
    const response = await axios.get(`http://localhost:8080/api/toolmanage/all-tools`, {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("access_token"),
      },
    });

    tools.value = response.data.data.length;
  } catch (error) {
    console.log("Error Fetching jobs", error);
    // if (error.response.status === 401) {
    //   console.log("Error Fetching jobs", error);
    // }
  }
};

</script>

<template>
  <div class="content">
    <Navbar>
      <li>
        <RouterLink to="/" class="link">DashBoard</RouterLink>
      </li>
    </Navbar>
    <MainCard>
      <Dashboard
        :users="users"
        :tools="tools"
      />
    </MainCard>
  </div>
  <RouterView />
</template>

<style lang="scss" scoped></style>
