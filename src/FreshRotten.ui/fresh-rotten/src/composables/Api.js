import axios from 'axios';
import variable from '../models/Variables'

function SendRequest () {
  const connection = axios.create()
  connection.defaults.headers.Accept = 'application/json'
  connection.defaults.baseURL = variable.API_URL
  connection.defaults.timeout = 550000
  return connection
}

function FailRequest (error) {
  try {
    console.log(error)

    const response = error.response
    const data = response.data
    const contenido = data.failReponse

    return contenido.exception
  }
  catch (e) {
    return `${e}`
  }
}

export default {
  SendRequest,
  FailRequest
}