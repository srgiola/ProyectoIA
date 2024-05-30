//const API_URL = 'http://localhost:8080/api'
const API_URL = 'http://ia-api:8080/api'
//const API_URL = 'http://localhost:5079/api'
const CONTROLLER_USER = 'User'
const CONTROLLER_MOVIE = 'Movie'
const CONTROLLER_REVIEW = 'Review'
const DEV_MODE = true

const ENPOINT_VALIDATE_USER = `/${CONTROLLER_USER}/validate-user`
const ENPOINT_GET_MOVIES = `/${CONTROLLER_MOVIE}/all-movies`
const ENPOINT_GET_USER_REVIEW = `/${CONTROLLER_REVIEW}/user-reviews`
const ENPOINT_SEND_CRITIC = `${CONTROLLER_REVIEW}/create-review`

export default {
  API_URL,
  ENPOINT_VALIDATE_USER,
  ENPOINT_GET_MOVIES,
  ENPOINT_GET_USER_REVIEW,
  ENPOINT_SEND_CRITIC,
  DEV_MODE
}