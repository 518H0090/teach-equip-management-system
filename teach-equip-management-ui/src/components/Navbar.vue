<script setup>
import { computed, defineProps, onBeforeMount, onMounted, ref } from "vue";

import { useStore } from "vuex";

const store = useStore();

const props = defineProps({
  title: {
    type: String,
    default: "ppepepepepepepepepepepepep",
  },
});

const mobile = ref(false);
const mobileNav = ref(false);
const windowWidth = ref(0);

const ToggleMobileNav = async () => {
  mobileNav.value = !mobileNav.value;
};

const CheckScreen = () => {
  windowWidth.value = window.innerWidth;
  if (windowWidth.value <= 768) {
    mobile.value = true;
    return;
  }
  mobile.value = false;
  mobileNav.value = false;
  return;
};

const aside = document.querySelector('aside');

onBeforeMount(async () => {

  if (!aside.classList.contains('is-expanded')) {
    await store.dispatch("setIsExpanded", false);
  }

  await store.dispatch("setIsExpanded", localStorage.getItem("is_expanded"));
});

onMounted(() => {
  window.addEventListener("resize", CheckScreen);
  CheckScreen();
});
</script>

<template>
  <header :class="`${store.state.is_expanded ? 'is-expanded' : ''}`">
    <nav>
      <ul class="navigation" v-show="!mobile">
        <li>
          <RouterLink class="link">Add</RouterLink>
        </li>
        <li>
          <RouterLink class="link">Update</RouterLink>
        </li>
        <li>
          <RouterLink class="link">Edit</RouterLink>
        </li>
        <li>
          <RouterLink class="link">Delete</RouterLink>
        </li>
      </ul>
      <div :class="`icon ${mobileNav ? 'icon-active' : ''}`" v-show="mobile">
        <span class="material-icons" v-on:click="ToggleMobileNav">menu</span>
      </div>
      <div class="mobile-nav">
        <ul class="dropdown-nav" v-show="mobileNav">
          <li>
            <RouterLink class="link">Add</RouterLink>
          </li>
          <li>
            <RouterLink class="link">Update</RouterLink>
          </li>
          <li>
            <RouterLink class="link">Edit</RouterLink>
          </li>
          <li>
            <RouterLink class="link">Delete</RouterLink>
          </li>
        </ul>
      </div>
    </nav>
  </header>
</template>

<style lang="scss" scoped>
header {
  width: calc(100vw - (2rem + 32px));
  background-color: rgba(0, 0, 0, 0.6);
  z-index: 20;
  position: fixed;
  transition: 0.4s ease-out all;
  color: var(--light);

  * {
    max-width: 99%;
    overflow: hidden;
  }

  &.is-expanded {
    width: calc(100vw - var(--sidebar-width));
  }

  @media (max-width: 768px) {
    margin-left: calc(2rem + 32px);

    &.is-expanded {
      transform: translateX(-100%);
      transition: 0.2s ease-in transform;
    }
  }

  nav {
    display: flex;
    padding: 12px 0;
    transition: 0.4s ease-out all;
    width: 90%;
    margin: 0 auto;
    position: relative;
    height: 4rem;

    @media (min-width: 1140px) {
      max-width: 1280px;
    }

    ul,
    .link {
      font-weight: 500;
      color: var(--light);
      list-style: none;
      text-decoration: none;
    }

    li {
      text-transform: uppercase;
      padding: 16px;
      margin-left: 16px;
    }

    .link {
      font-size: 14px;
      transition: 0.3s ease-out;
      padding-bottom: 4px;
      border-bottom: 1px solid transparent;

      &:hover {
        color: #00afea;
        border-color: #00afea;
      }
    }

    .navigation {
      display: flex;
      align-items: center;
      flex: 1;
      justify-content: flex-end;
    }

    .icon {
      display: flex;
      align-items: center;
      position: absolute;
      top: 0;
      right: 24px;
      height: 100%;

      span.material-icons {
        cursor: pointer;
        font-size: 24px;
        transition: 0.4s ease-in;
      }

      &.icon-active {
        span.material-icons {
          transform: rotate(180deg);
        }
      }
    }

    .dropdown-nav {
      display: flex;
      flex-direction: column;
      position: fixed;
      width: 100%;
      max-width: 250px;
      height: fit-content;
      background-color: var(--dark);
      top: 3.4rem;
      right: 0;
      box-shadow: 1px 1px 3px rgba(0, 0, 0, 0.6);

      li {
        margin-left: 0;
        cursor: pointer;

        &:hover {
          background: var(--dark-alt);
          max-width: 100%;
          border-right: 5px solid var(--primary);

          .link {
            color: var(--primary);
            transition: 0.2s ease-out;
          }
        }

        .link {
          color: var(--light);
        }
      }
    }
  }
}
</style>
