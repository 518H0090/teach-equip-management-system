<script setup>
import { RouterLink, RouterView } from "vue-router";
import { useRoute } from "vue-router";
import { onUnmounted, onMounted, ref } from "vue";
import Navbar from "@/components/Navbar.vue";

import { jwtDecode } from "jwt-decode";

onMounted(async () => {
  await decodeJwtToken(token.value, user);
})

onUnmounted(() => {
  user.value = {}
})

const token = ref(localStorage.getItem("access_token") || "");
const user = ref({});

const decodeJwtToken = async (token, userRef) => {
  try {
    if (token) {
      const decoded = jwtDecode(token);

      userRef.value = {
        exp: decoded.exp,
        id:
          decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"],
        name: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
        role: decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
      };
    }
  } catch (error) {
    console.error("Invalid token:", error);
  }
}

</script>

<template>
  <div class="content">
    <Navbar>
      <li>
        <RouterLink to="/request/getpage" class="link">Request</RouterLink>
      </li>
      <li>
        <RouterLink to="/request/history" class="link">History</RouterLink>
      </li>
      <li>
        <RouterLink to="/request/borrow" class="link">Borrow</RouterLink>
      </li>
    </Navbar>
    <RouterView page_name="request" page_service="inventorymanage" :role="user.role" />
  </div>
</template>

<style scoped>
router-link-active,
router-link-exact-active {
  border-right: 5px solid var(--primary);
}
</style>
