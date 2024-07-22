import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import DashBoardView from '../views/DashBoardView.vue'
import LoginView from '../views/LoginView.vue'
import About from '@/views/About.vue'
import AddPage from '@/components/AddPage.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/dashboard',
      name: 'dashboard',
      component: DashBoardView
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView
    },
    {
      path: '/about',
      component: About,
      children: [
        {
          path: "newnewnew",
          component: AddPage
        }
      ]
    },
  ]
})

export default router
