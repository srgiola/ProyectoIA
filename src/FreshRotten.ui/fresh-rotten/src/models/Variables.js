const API_URL = 'http://ia-api:8080/api'
//const API_URL = 'http://localhost:5079/api'
const CONTROLLER_USER = 'User'
const CONTROLLER_MOVIE = 'Movie'
const DEV_MODE = false

const ENPOINT_VALIDATE_USER = `/${CONTROLLER_USER}/validate-user`
const ENPOINT_GET_MOVIES = `/${CONTROLLER_MOVIE}/all-movies`

export default {
  API_URL,
  ENPOINT_VALIDATE_USER,
  ENPOINT_GET_MOVIES,
  DEV_MODE
}