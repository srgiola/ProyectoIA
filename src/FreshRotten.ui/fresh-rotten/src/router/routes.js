
const routes = [
  {
    path: '/',
    component: () => import('layouts/login-layout.vue'),
    children: [
    ]
  },

  {
    path: '/review',
    name: 'review-page',
    component: () => import('layouts/main-layout.vue'),
    children: [
      { path: 'main', name: 'main-page', component: () => import('pages/ViewPage.vue') }
    ]
  },

  // Always leave this as last one,
  // but you can also remove it
  {
    path: '/:catchAll(.*)*',
    component: () => import('pages/ErrorNotFound.vue')
  }
]

export default routes
