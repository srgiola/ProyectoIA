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

export default {
  FailMessage
}