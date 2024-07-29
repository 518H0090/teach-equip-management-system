<script setup>
import { defineProps, ref } from "vue";
import router from "@/router";
import axios from "axios";
import { useStore } from "vuex";

const store = useStore();

const props = defineProps({
  username: {
    type: String,
    default: "Not Contain User",
  },
});

const dropdownOpen = ref(false);

const toggleDropdown = () => {
  dropdownOpen.value = !dropdownOpen.value;
};

const logOut = async () => {
  const accessToken = localStorage.getItem("access_token");

  if (isNullOrUndefined(accessToken)) {
    router.push("/login");
  } else {
    const response = fetch(
      "https://localhost:7112/api/usermanage/revoke-token",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + localStorage.getItem("access_token"),
        },
      }
    )
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
        }
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
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
      <span class="username">
        {{ props.username }}
      </span>

      <span class="material-symbols-outlined">
        keyboard_double_arrow_down
      </span>
    </div>
    <div class="options-wrapper" v-if="dropdownOpen">
      <div class="option" @click="logOut">Logout</div>
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
  padding: 0.5rem 1rem;
  border-radius: 8px;
  box-sizing: border-box;
  display: flex;
  justify-content: space-between;
  max-width: 280px;
  background-color: var(--dark);
  text-transform: uppercase;
  font-weight: 700;
  margin-top: 0.4rem;

  span.username {
    flex: 1;
    text-align: center;
  }
}

.options-wrapper {
  background-color: var(--dark);
  border-radius: 8px;
  margin-top: 0.4rem;
}

.option:hover {
  border-right: 5px solid var(--primary);
  background: var(--dark-alt);
}

.option {
  padding: 16px;
  border: solid 1px #313131;
  box-sizing: border-box;
  border-radius: 8px;
}

.option:last-of-type {
  border-bottom-left-radius: 8px;
  border-bottom-right-radius: 8px;
}
</style>
