<script setup>
import { defineProps, ref } from "vue";
import router from "@/router";
import axios from "axios";
import { useStore } from "vuex";

const store = useStore();

const props = defineProps({
  username: {
    type: String,
    default: "_",
  }
});

const dropdownOpen = ref(false);

const profileSrc = ref(localStorage.getItem("profileSrc"));

const toggleDropdown = () => {
  dropdownOpen.value = !dropdownOpen.value;
};

const logOut = async () => {
  const accessToken = localStorage.getItem("access_token");

  if (isNullOrUndefined(accessToken)) {
    router.push("/login");
  } else {
    const response = fetch("http://localhost:8080/api/usermanage/revoke-token", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("access_token"),
      },
    })
      .then((response) => {
        return response.json();
      })
      .then(async (data) => {
        if (data.statusCode === 200) {
          localStorage.removeItem("access_token");
          localStorage.removeItem("refresh_token");
          await store.dispatch("setAuth", false);
          // await store.dispatch("setIsExpanded", false);
          await router.push("/login");
        } else if (data.statusCode === 400) {
          await router.push("/login");
        }
      })
      .catch(async (error) => {
        console.error("Error fetching data:", error);
        await router.push("/login");
      });
  }
};

function isNullOrUndefined(value) {
  return value === null || value === undefined;
}
</script>

<template>
  <div class="dropdown-wrapper">
    <div class="dropdown-selected-option" @click="toggleDropdown">
      <span class="avatar">
        <img :src="`${profileSrc}`" />
      </span>

      <span class="username">
        {{
          props.username.length > 12
            ? props.username.slice(0, 12) + "..."
            : props.username
        }}
      </span>

      <span class="material-symbols-outlined"> keyboard_double_arrow_down </span>
    </div>
    <div class="options-wrapper" v-if="dropdownOpen">
      <div class="option" @click="logOut">
        <span class="material-symbols-outlined">
          logout
          </span>
        <span>Logout</span>  
        </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.dropdown-wrapper {
  cursor: pointer;
  max-width: 200px;
  margin: 0 auto;
  height: fit-content;
  margin-top: -0.6rem;
  position: relative;
  flex: 1;
}

.dropdown-selected-option {
  box-sizing: border-box;
  display: flex;
  justify-content: space-between;
  align-items: center;
  text-transform: uppercase;
  font-weight: 700;
  border: 1px solid #f5eded;
  min-width: 12rem;
  max-width: 16rem;
  border-radius: 4rem;
  margin-top: 0.6rem;
  height: 2.4rem;
  span.avatar {
    img {
      border-radius: 100%;
      width: 1.8rem;
    }
  }
}

.options-wrapper {
  background-color: #2a629a;
  border-radius: 8px;
  margin-top: 0.4rem;
}

.option:hover {
  background: #a0deff;
}

.option {
  padding: 1rem;
  box-sizing: border-box;
  border-radius: 8px;
  text-transform: capitalize;
  font-size: 0.8rem;
  font-weight: 600;
  display: flex;
  align-items: center;
}

.option:last-of-type {
  border-bottom-left-radius: 8px;
  border-bottom-right-radius: 8px;
}
</style>
