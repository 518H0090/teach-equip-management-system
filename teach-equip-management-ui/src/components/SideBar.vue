<script setup>
import { onMounted, ref } from "vue";
import { useStore } from "vuex";

const store = useStore();

const is_expanded = ref(store.state.is_expanded === "true");

const ToggleMenu = async () => {
  is_expanded.value = !is_expanded.value;

  await store.dispatch("setIsExpanded", is_expanded.value);
};
</script>

<template>
  <aside :class="`${is_expanded ? 'is-expanded' : ''}`">
    <!-- Logo -->
    <div class="logo">
      <img src="../assets/logo.svg" alt="Vue" />
    </div>

    <!-- Button Toggle -->
    <div class="menu-toggle-wrap">
      <button class="menu-toggle" v-on:click="ToggleMenu">
        <span class="material-icons">keyboard_double_arrow_right</span>
      </button>
    </div>
    <!-- Menu -->
    <h3>Menu</h3>
    <div class="menu">
      <RouterLink class="button" to="/">
        <span class="material-icons">home</span>
        <span class="text">Home</span>
      </RouterLink>

      <RouterLink class="button" to="/dashboard">
        <span class="material-icons">group</span>
        <span class="text">Team</span>
      </RouterLink>
    </div>

    <!-- Settings -->
    <div class="flex"></div>
    <div class="menu">
      <RouterLink class="button" to="/settings">
        <span class="material-icons">settings</span>
        <span class="text">Settings</span>
      </RouterLink>
    </div>
  </aside>
</template>

<style lang="scss" scoped>
aside {
  display: flex;
  flex-direction: column;
  background-color: var(--dark);
  color: var(--light);
  width: calc(2rem + 32px);
  overflow: hidden;
  height: 100vh;
  padding: 1rem;
  transition: 0.2s ease-out;

  .flex {
    flex: 1 1 0;
  }

  .logo {
    margin-bottom: 1rem;

    img {
      width: 2rem;
    }
  }

  .menu-toggle-wrap {
    display: flex;
    justify-content: flex-end;
    margin-bottom: 1rem;
    position: relative;
    top: 1rem;
    transition: 0.2s ease-out;

    .menu-toggle {
      transition: 0.2s ease-out;

      .material-icons {
        font-size: 2rem;
        color: var(--light);
      }
    }

    &:hover {
      .material-icons {
        color: var(--primary);
        transform: translateX(0.5rem);
        transition: 0.2s ease-out;
      }
    }
  }

  h3,
  .button .text {
    opacity: 0;
    transition: 0.3s ease-out;
    margin-bottom: 0.5rem;
    text-transform: uppercase;
  }

  h3 {
    color: var(--grey);
    font-size: 0.875rem;
  }

  .menu {
    margin: 0 -1rem;

    .button {
      display: flex;
      align-items: center;
      text-decoration: none;

      padding: 0.5rem 1rem;
      transition: 0.2s ease-out;

      .material-icons {
        font-size: 2rem;
        color: var(--light);
        transition: 0.2s ease-out;
      }

      .text {
        color: var(--light);
        transition: 0.2s ease-out;
        align-self: flex-end;
        margin-top: 1rem;
      }

      &:hover,
      &.router-link-exact-active {
        background: var(--dark-alt);

        .material-icons,
        .text {
          color: var(--primary);
        }
      }

      &.router-link-exact-active {
        border-right: 5px solid var(--primary);
      }
    }
  }

  &.is-expanded {
    width: var(--sidebar-width);

    .menu-toggle-wrap {
      top: -3rem;

      .menu-toggle {
        transform: rotate(-180deg);
      }
    }

    h3,
    .button .text {
      opacity: 1;
    }

    .button {
      .material-icons {
        margin-right: 1rem;
      }
    }
  }
}

@media (max-width: 768px) {
  aside {
    position: fixed;
    z-index: 24;
  }
}
</style>
