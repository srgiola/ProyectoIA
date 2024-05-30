<template>
  <q-page class="flex font-sans h-[100vh] bg-black opacity-90" style="overflow: hidden;">
    <section class="flex-none w-[6rem] relative">
      <Transition appear>
        <NavigationOption />
      </Transition>
    </section>
  
    <section class="flex-auto p-6">
      <MovieView />
    </section>
  </q-page>
</template>

<script setup>
import { Transition } from 'vue';
import NavigationOption from '../components/navigation-option.vue'
import MovieView from '../components/movie-view.vue'

</script>

<style scoped>
@tailwind base;
@tailwind components;
@tailwind utilities;

.v-enter-active,
.v-leave-active {
  transition: opacity 2s ease;
}

.v-enter-from,
.v-leave-to {
  opacity: 0;
}

</style>