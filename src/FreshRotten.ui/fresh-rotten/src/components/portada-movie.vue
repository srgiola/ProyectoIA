<template>
  <div
    style="padding: 1rem;"
  >
    <q-skeleton 
      height="200px"
      square
      v-if="moviesShow.length < 1"
      class="carousel bg-red-800 opacity-70"
      animation-speed="5000"
      animation="pulse"
    />
    <q-carousel
        v-else
        animated
        v-model="slide"
        infinite
        style="height: 15rem;filter: opacity(0.8);"
        :autoplay="autoPlay"
        class="carousel"
      >
        <q-carousel-slide :name="index"
          v-for="(movie, index) in moviesShow"
          :key="movie.url"
          :img-src="movie.url"
        >
      </q-carousel-slide>
    </q-carousel>
  </div>
</template>

<script setup>
import { ref, onMounted, watch } from 'vue'
import Movie from '../composables/Movie'

const slide = ref(1)
const autoPlay = ref(true)
const moviesShow = ref([])

watch(Movie.movies, async(actual, old) => {
  moviesShow.value = actual
})

</script>

<style scoped>

.carousel {
  border-top-left-radius: 4rem;
  border-bottom-right-radius: 4rem;
  
}

</style>