import { ref } from 'vue'
import Api from './Api'
import Alert from './Alert'
import Variables from 'src/models/Variables'

const movies = ref([])

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
                              url: Variables.DEV_MODE ? movie.movieDevURL : movie.movieProdURL
                            }
                          })

    console.log(movies.value)

  }
  catch (error) {
    const errorResponse = Api.FailRequest(error)
    console.log(errorResponse)
  } 
}


export default {
  movies,
  GetMovies
}