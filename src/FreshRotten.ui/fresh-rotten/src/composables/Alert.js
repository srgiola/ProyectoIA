import { Notify } from 'quasar'

function FailMessage ({ message }) {
  Notify.create({
    message: message,
    type: 'negative',
    position: 'center',
    color: 'white',
    iconColor: 'red-10',
    textColor: 'red-10',
    icon: 'bi-bug-fill',
    actions: [
      { label: 'Ok', color: 'red-10', handler: () => { /* ... */ } }
    ]
  })
}

function OkMessage ({ message }) {
  Notify.create({
    message: message,
    type: 'positive',
    position: 'center',
    color: 'white',
    iconColor: 'black',
    textColor: 'black',
    icon: 'bi-bug-fill',
    actions: [
      { label: 'Ok', color: 'black', handler: () => { /* ... */ } }
    ]
  })
}

export default {
  FailMessage,
  OkMessage
}