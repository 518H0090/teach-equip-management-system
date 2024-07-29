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
import AccountView from '@/views/AccountView.vue'
import AccountEditForm from '@/components/AccountEditForm.vue'
import AccountForm from '@/components/AccountForm.vue'
import InventoryView from '@/views/InventoryView.vue'
import InventoryEditForm from '@/components/InventoryEditForm.vue'
import InvoiceForm from '@/components/InvoiceForm.vue'
import GetInvoice from '@/components/GetInvoice.vue'
import InvoiceEditForm from '@/components/InvoiceEditForm.vue'
import RequestForm from '@/components/RequestForm.vue'
import RequestView from '@/views/RequestView.vue'

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
          component: ToolEditForm
        },
        {
          path: "addpage",
          component:  ToolForm
        }
      ]
    },
    {
      path: '/account',
      component: AccountView,
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
          component: AccountEditForm
        },
        {
          path: "addpage",
          component:  AccountForm
        }
      ]
    },
    {
      path: '/inventory',
      component: InventoryView,
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
          component: InventoryEditForm
        },
        {
          path: "add-invoice",
          component:  InvoiceForm
        },
        {
          path: "get-invoice",
          component:  GetInvoice
        },
        {
          path: "edit-invoice/:id",
          component: InvoiceEditForm
        },
        {
          path: "request-form/:id",
          component: RequestForm
        },
      ]
    },
    {
      path: '/request',
      component: RequestView,
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
          component: AccountEditForm
        },
        {
          path: "addpage",
          component:  AccountForm
        }
      ]
    },
  ]
})

export default router
