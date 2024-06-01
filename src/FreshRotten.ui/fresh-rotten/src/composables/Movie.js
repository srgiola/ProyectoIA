import { ref } from 'vue'
import Api from './Api'
import Alert from './Alert'
import Variables from 'src/models/Variables'

const movies = ref([])
const hasMovies = ref(false)

async function GetMovies () {
  try {
    const response = await Api.SendRequest().get(Variables.ENPOINT_GET_MOVIES);
    const content = response.data
    const result = content.data
    
    movies.value = result.movies.map(movie => 
                          {
                            return {
                              id: movie.id,
                              title: movie.title,
                              description: movie.description,
                              url: movie.movieImageB64
                            }
                          })

    hasMovies.value = movies.value.length > 1
  }
  catch (error) {
    const errorResponse = Api.FailRequest(error)
    console.log(errorResponse)
  } 
}


export default {
  movies,
  hasMovies,
  GetMovies
}