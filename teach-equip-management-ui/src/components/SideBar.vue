<script setup>
</script>

<template>
    <aside>
        SideBar
    </aside>
</template>

<style lang="scss" scoped>

aside {
    display: flex;
    flex-direction: column;
    background-color: var(--dark);
    color: var(--light);
    width: var(--sidebar-width);
    overflow: hidden;
    height: 100vh;
    padding: 1rem;
    transition: .2s ease-out;
}

@media (max-width: 768px) {
    aside {
        position: fixed;
        z-index: 24;
    }
  }

</style>