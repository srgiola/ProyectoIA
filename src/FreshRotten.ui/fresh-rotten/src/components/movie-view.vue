<template>
  <div class="grid grid-rows-[40%_60%] h-[100vh]">
    <Transition appear>
      <PortadaMovie></PortadaMovie>
    </Transition>
    
    <div class="grid-container overflow-auto">
        <div 
          class="grid-item"
          v-for="(movie, index) in moviesShow"
          :key="index"
          @click="ShowDetails(movie)"
        >
            <img 
              :src="movie.url" 
              style="width: 8rem; height: 7.5rem;"
            >
            <div>
                  <h6 style="width: 7rem;overflow-x: hidden;margin-left: 1rem;color: white;max-height: 7rem;overflow: hidden;">
                    {{ movie.title }}
                  </h6>
            </div>
            <q-tooltip>CLICK VER DETALLES</q-tooltip>
        </div>
    </div>
  </div>

  <q-dialog v-model="showDetails" transition-show="rotate" transition-hide="rotate" style="overflow: hidden;">
    <q-card class="card-style">
      <q-toolbar class="w-[35rem] h-1 bg-red-900">
        <q-toolbar-title >
          <span class="text-white ml-[1rem] font-boldr">Detalles</span>
        </q-toolbar-title>
        <q-btn flat v-close-popup round dense icon="close" class="bg-white text-red-900 shadow-black shadow-lg" />
      </q-toolbar>

      <q-separetor />
      <q-card-section
        class="movie-container"
      >
      
        <!--Informacion-->
        <section class="bg-red-900 opacity-90 info-container"
        >
          <img 
            :src="movieSelected.url" 
            style="width: 20rem; height: 8rem; padding: 1rem;"
            class="shadow-md shadow-black"
          >
          <div>
            <q-input 
              bottom-slots
              v-model="movieSelected.title" 
              dense
              dark
              class="ml-[1rem]"
              readonly
            ></q-input>
            <q-input 
              bottom-slots
              v-model="movieSelected.description" 
              dense
              dark
              class="ml-[1rem]"
              readonly
            ></q-input>
          </div>
        </section>

        <!--Criticas-->
        <section class="conteiner-chat bg-black">
          <div class="chat-info bg-white">
            <section
              v-for="(review, index) in Reviews.userReviews.value"
              :key="index"
            >
              <q-chat-message v-if="review.isMeMessage" :text="[`${review.message}`]" sent style="padding: 1rem;"  />
              <q-chat-message v-else :text="[`${review.message}`]" style="padding: 1rem;" />
            </section>
          </div>
          
          <div
            class="max-h-[5.7rem] pt-3"
            style="overflow-y: auto;display: grid; grid-template-columns: 25% 75%;"
          >
            <q-input
             v-model="score" 
             label="Puntuacion"
             dark
             class="ml-[1rem]"
             type="number"
             :error="score > 10 || score < 0"
            />
            <q-input 
              bottom-slots 
              v-model="criticMessage" 
              label="Critica" 
              dark
            >
              <template v-slot:before>
                <q-icon name="las la-user"></q-icon>
              </template>
  
              <template v-slot:after>
                <q-btn round dense flat icon="send" @click="SendCritic()" />
              </template>
            </q-input>
          </div>
              
        </section>
      </q-card-section>

    </q-card>
  </q-dialog>


</template>

<script setup>

import { onMounted, ref } from 'vue'
import { LocalStorage } from 'quasar'
import PortadaMovie from '../components/portada-movie.vue'
import Alert from '../composables/Alert'
import Movie from '../composables/Movie'
import Reviews from '../composables/Reviews'

const moviesShow = ref([]) 
const showDetails = ref(false)
const movieSelected = ref(null)
const score = ref(0)
const criticMessage = ref('')

onMounted(async () => {
  await Movie.GetMovies()
  moviesShow.value = Movie.movies.value
})


async function ShowDetails (movie) {
  movieSelected.value = movie
  showDetails.value = true

  await Reviews.GetUserReviews(movieSelected.value.id)
}

async function SendCritic () {
  if (criticMessage.value === '' || criticMessage.value === null || criticMessage.value.length < 1) {
    Alert.FailMessage({ message: 'Debes de ingresar la critica' })
    return
  }

  const userConnect = LocalStorage.getItem('USER_NAME')
  await Reviews.SendReviw({ movieId: movieSelected.value.id, userName: userConnect, score: score.value, critic: criticMessage.value  })
  await Reviews.GetUserReviews(movieSelected.value.id)
}

</script>

<style>
@tailwind base;
@tailwind components;
@tailwind utilities;

.contedor-movies {
  max-height: 20rem;
  background: white;
  overflow-y: auto;
}

.grid-container {
  max-height: 80%;
  overflow-y: hidden;
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  grid-auto-rows: 120px; 
  gap: 40px;
  margin-left: 1rem;
  margin-right: 1rem;
}

.grid-item {
  display: grid;
  grid-template-columns: 40% 60%;
}

.grid-item:hover {
  padding: 1rem;
  background: rgb(127,29,29);
  border-left: solid 5px;
  border-color: white;
  border-top-right-radius: 1rem;
  border-bottom-right-radius: 1rem;
}


.v-enter-active,
.v-leave-active {
  transition: opacity 4s ease;
}

.v-enter-from,
.v-leave-to {
  opacity: 0;
}

.card-style {
  height: 25rem;
  width: 35rem;
}

.movie-container {
  display: grid;
  height: 21.9rem;
  grid-template-columns: 30% 70%;
  padding: 0rem;
}

.info-container {
  display: grid;
  grid-template-rows: 50% 50%;
  width: 11rem;
  overflow: auto;
}

.conteiner-chat {
  display: grid;
  grid-template-rows: 70% 30%;
}

.chat-info {
  width: 24rem;
  height: 16.2rem;
  overflow: auto;
  padding: 1rem;
}

</style>