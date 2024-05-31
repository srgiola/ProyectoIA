import { ref } from 'vue'
import { LocalStorage } from 'quasar'
import Api from './Api'
import Alert from './Alert'
import Variables from 'src/models/Variables'

const userReviews = ref([])

async function GetUserReviews (movieId) {
  try {
    // Review/user-reviews?movieId=1
    const userConnect = LocalStorage.getItem('USER_NAME')
    const response = await Api.SendRequest().get(`${Variables.ENPOINT_GET_USER_REVIEW}?movieId=${movieId}`);
    const content = response.data
    const result = content.data
    
    userReviews.value = result.map(m => { return { message: m.critic, userName: m.userName , isMeMessage: userConnect === m.userName } })
    console.log(userReviews)
  }
  catch (err) {
    const errorResponse = Api.FailRequest(err)
    console.log(errorResponse)
  }
}

async function SendReviw ({ movieId, userName, score, critic }) {
  try {
    const data = {
      movie: movieId,
      user: userName,
      critc: critic,
      score: score
    }

    const response = await Api.SendRequest().post(`${Variables.ENPOINT_SEND_CRITIC}`, data);
    console.log(response)
  }
  catch (err) {
    const errorResponse = Api.FailRequest(err)
    console.log(errorResponse)
  }
}

export default {
  GetUserReviews,
  SendReviw,
  userReviews
}