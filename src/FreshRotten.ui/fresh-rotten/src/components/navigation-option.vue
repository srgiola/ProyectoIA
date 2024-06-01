<template>
  <div 
    class="flex flex-col h-[100vh] pl-3 pr-3 pt-10 pb-10"
  >
  
    <!--Usuario-->
    <section
      class="flex h-[10rem] bg-red-800 opacity-70 rounded-t-xl"
    >
      <q-item>
        <q-item-section
        class="text-sky-50"
        >
          <q-icon name="bi-person-check-fill" class="bg-black p-6 rounded-[2rem] shadow-lg shadow-black" />
          <q-item-label capitation class="mt-3">{{ userName }}</q-item-label>
          <q-tooltip anchor="center right" self="center left"> Bienvenido </q-tooltip>
        </q-item-section>
      </q-item>
    </section>

    <!--Opciones de para el usuario-->
    <section 
      class="flex-auto bg-red-800 opacity-85 rounded-b-xl"
    >
      <div class="flex flex-col h-full justify-center">
        <section
          v-for="(option, index) in options"
          :key="index"
          class="flex flex-col hover:opacity-45 hover:p-2"
        >
          <q-icon 
            :name="option.icon"
            size="xs"
            class="text-white mt-4 style-icon"
          >
            <q-tooltip  anchor="center right" self="center left" >{{ option.message }}</q-tooltip> 
          </q-icon>
        </section>
      </div>
    </section>
  </div>
</template>

<script setup>

import { ref, onMounted  } from 'vue'
import { LocalStorage } from 'quasar'

const userName = ref('')
const options = [
  { message: 'Favoritos', icon: 'bi-star-fill' },
  { message: 'Buscar', icon: 'bi-binoculars-fill' },
  { message: 'Compras', icon: 'bi-credit-card-fill' },
  { message: 'Historial', icon: 'bi-hourglass-bottom' }
]

onMounted(() => {
  userName.value =  LocalStorage.getItem('USER_NAME')
})



</script>

<style scoped>
@tailwind base;
@tailwind components;
@tailwind utilities;

.contedor-opciones {
  display: grid;
  grid-template-rows: 20%, 40% 60%;
}

.style-icon {
  font-size: 32px;
  border-left: solid 3px;
  width: 100%;
  height: 3.5rem;
}

</style>