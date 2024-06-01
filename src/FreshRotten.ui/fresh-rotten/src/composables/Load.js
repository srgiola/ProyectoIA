import { Loading, QSpinnerPuff  } from 'quasar'

function ShowLoading () {
  Loading.show({
    spinner: QSpinnerPuff,
    message: 'Preparando todo...',
    messageColor: 'red-11',
    spinnerColor: 'red-12'
  })
}

function HideLoading () {
  Loading.hide()
}

export default {
  ShowLoading,
  HideLoading
}