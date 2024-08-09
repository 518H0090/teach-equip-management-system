<script setup>
import { RouterLink, RouterView } from "vue-router";
import { useRoute } from "vue-router";
import { ref, onMounted, onUnmounted } from "vue";
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
        <RouterLink to="/inventory/getpage" class="link">Inventory</RouterLink>
      </li>
      <li v-if="user.role === 'admin' || user.role === 'manager'">
        <RouterLink to="/inventory/get-invoice" class="link" >Invoice</RouterLink>
      </li>
      <li v-if="user.role === 'admin' || user.role === 'manager'">
        <RouterLink to="/inventory/add-invoice" class="link" >Create Invoice</RouterLink>
      </li>
    </Navbar>
    <RouterView page_name="inventory" page_service="toolmanage" :role="user.role" />
  </div>
</template>

<style scoped>
router-link-active,
router-link-exact-active {
  border-right: 5px solid var(--primary);
}
</style>
