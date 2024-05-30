<template>
  <q-layout view="lHh Lpr lFf">
    <section>
      <Transition appear>
        <video autoplay loop muted>
          <source type="video/mp4" src="/statics/videos/onepiece.mp4">
        </video>
      </Transition>

      <!--Contedor del input-->
      <Transition appear>
        <section class="grid grid-rows-3 grid-flow-col gap-4 h-[40rem] w-[25rem] bg-stone-400  bg-opacity-90  absolute top-[10%] left-10 rounded-3xl overflow-auto">
          <div class="col-span-2 ...">
            <img src="/statics/img/logo-rt.png" 
            class="absolute bottom-[17rem]">
          </div>
          <div class="row-span-2 col-span-2 ...">

            <!--Inputs-->
            <section>
                <q-input 
                  bottom-slots
                  v-model="userName"
                  label="Ingresa tu usuario" 
                  dense
                  class="pl-[2rem] pr-[3rem] text-white mt-[6rem]"
                >
                  <template v-slot:before>
                    <q-icon name="bi-person-check-fill"  class="mr-[1rem] mt-2 text-sky-950"/>
                  </template>
                </q-input>
                
                <q-input 
                  bottom-slots
                  v-model="password"
                  label="Ingresa tus contraseña" 
                  dense
                  class="pl-[2rem] pr-[3rem]"
                  :type="isShowPassword ? 'text' : 'password'"
                >
                  <template v-slot:before>
                    <q-icon name="bi-key" size=""  class="mr-[1rem] mt-2 text-sky-950"/>
                  </template>
                  <template v-slot:after>
                    <q-icon 
                      :name="isShowPassword ? 'las la-eye' : 'las la-low-vision'" 
                      size="md"  
                      class="mr-[1rem]"
                      @click="isShowPassword = !isShowPassword" 
                    />
                  </template>
                </q-input>
            </section>


            <!--Buttns-->
            <section>
              <q-btn 
                rounded 
                label="iniciar sesión"
                class="w-[15rem] absolute left-[20%] mt-5 bg-red-800 text-cyan-50 lowercase font-bold"
                @click="AuthenticaUser()"
              />
            </section>
          </div>
        </section>
      </Transition>
    </section>
  </q-layout>
</template>

<script setup>

import { ref } from 'vue'
import { useRouter } from 'vue-router';
import { LocalStorage } from 'quasar'

import Api from '../composables/Api'
import Alert from '../composables/Alert'
import Variables from 'src/models/Variables';
import Load from '../composables/Load'

const userName = ref('')
const password = ref('')
const isShowPassword = ref(false)
const router = useRouter()

async function AuthenticaUser () {
  const isInvalid = this.HasInputInvalid()
  if (isInvalid) {
    return
  }

  const hasAcces = await this.HasAcces()
  if (!hasAcces) {
    return    
  }

  LocalStorage.setItem('USER_NAME', this.userName)
  console.log(LocalStorage.getItem('USER_NAME'))
  router.push({ name: 'main-page' })
  
}

function HasInputInvalid () {
  let hasError = false

  try {
    if (this.userName.length < 1) {
      throw Error('Campo, usuario. Requerido')
    }

    if (this.password.length < 1) {
      throw Error('Campo, contraseña. Requerido')
    }

    hasError = false
  }
  catch (error) {
    Alert.FailMessage({ message: `${error}` })
    hasError = true
  }

  return hasError
}

async function HasAcces () {
  let hasAcces = false
  
  try {
    Load.ShowLoading()
    const response = await Api.SendRequest().get(`${Variables.ENPOINT_VALIDATE_USER}?user=${this.userName}&password=${this.password}`)
    const data = response.data
    const result = data.data

    hasAcces = result.hasAccess
    if (!hasAcces) {
      throw Error("No tienes acceso")
    }
  }
  catch (error) {
    const messageFail = Api.FailRequest(error)
    Alert.FailMessage({ message: messageFail })
    hasAcces = false
  }

  setTimeout(function() {
    Load.HideLoading()
  }, 2000)
  
  return hasAcces
}

</script>

<style scoped>
@tailwind base;
@tailwind components;
@tailwind utilities;

video {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.v-enter-active,
.v-leave-active {
  transition: opacity 4s ease;
}

.v-enter-from,
.v-leave-to {
  opacity: 0;
}

</style>