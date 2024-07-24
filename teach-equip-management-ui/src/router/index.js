import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import DashBoardView from '../views/DashBoardView.vue'
import LoginView from '../views/LoginView.vue'
import About from '@/views/About.vue'
import AddPage from '@/components/AddPage.vue'
import EditPage from '@/components/EditPage.vue'
import GetPage from '@/components/GetPage.vue'
import Supplier from '@/views/Supplier.vue'
import SupplierForm from '@/components/SupplierForm.vue'
import SupplierEditForm from '@/components/SupplierEditForm.vue'
import Category from '@/views/Category.vue'
import CategoryForm from '@/components/CategoryForm.vue'
import CategoryEditForm from '@/components/CategoryEditForm.vue'
import ToolView from '@/views/ToolView.vue'
import ToolForm from '@/components/ToolForm.vue'
import ToolEditForm from '@/components/ToolEditForm.vue'

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
          path: "",
          redirect: "getpage"
        },
        {
          path: "getpage",
          component: GetPage
        },
        {
          path: "editpage",
          component: EditPage
        },
        {
          path: "addpage",
          component: AddPage
        }
      ]
    },
    {
      path: '/supplier',
      component: Supplier,
      children: [
        {
          path: "",
          redirect: "getpage"
        },
        {
          path: "getpage",
          component: GetPage
        },
        {
          path: "editpage/:id",
          component: SupplierEditForm
        },
        {
          path: "addpage",
          component: SupplierForm
        }
      ]
    },
    {
      path: '/category',
      component: Category,
      children: [
        {
          path: "",
          redirect: "getpage"
        },
        {
          path: "getpage",
          component: GetPage
        },
        {
          path: "editpage/:id",
          component: CategoryEditForm
        },
        {
          path: "addpage",
          component: CategoryForm
        }
      ]
    },
    {
      path: '/tool',
      component: ToolView,
      children: [
        {
          path: "",
          redirect: "getpage"
        },
        {
          path: "getpage",
          component: GetPage
        },
        {
          path: "editpage/:id",
          component: ToolForm
        },
        {
          path: "addpage",
          component: ToolEditForm
        }
      ]
    }
  ]
})

export default router
