
<template>
    <aside :class="`${is_expanded ? 'is-expanded' : ''}`">
        
        <!-- Logo -->
        <div class="logo">
            <img src="../assets/logo.svg" alt="Vue">
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

            <RouterLink class="button" to="/">
                <span class="material-icons">visibility</span>
                <span class="text">About</span>
            </RouterLink>

            <RouterLink class="button" to="/">
                <span class="material-icons">group</span>
                <span class="text">Team</span>
            </RouterLink>

            <RouterLink class="button" to="/">
                <span class="material-icons">email</span>
                <span class="text">Contact</span>
            </RouterLink>
         </div>

    </aside>
</template>

<script setup>

import { ref } from 'vue';

const is_expanded = ref(false)

const ToggleMenu = () => {
    is_expanded.value = !is_expanded.value
}

</script>

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
    transition: .2s ease-out;

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
        top:1rem;
        transition: .2s ease-out;

        .menu-toggle {
            transition: .2s ease-out;

            .material-icons {
                font-size: 2rem;
                color: var(--light);
            }
        }

        &:hover {
            .material-icons {
                color: var(--primary);
                transform: translateX(0.5rem);
                transition: .2s ease-out;
            }
        }

    }

    h3, .button .text {
        opacity: 0;
        transition: .3s ease-out ;
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
                transition: .2s ease-out;
            }

            .text {
                color: var(--light);
                transition: .2s ease-out;
            }

            &:hover {
                background: var(--dark-alt);

                .material-icons, .text {
                    color: var(--primary);
                }
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

        h3, .button .text {
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