import { createRouter, createWebHistory } from "vue-router";

import LoginPage from "@/components/LoginPage.vue";
import DashboardPage from "@/components/DashboardPage.vue";
import ProductsPage from "@/components/ProductsPage.vue";
import OrdersPage from "@/components/OrdersPage.vue";

const routes = [
  {
    path: '/',
    name: 'LoginPage',
    component: LoginPage
  },
  {
    path: '/dashboard',
    name: 'DashboardPage',
    component: DashboardPage
  },
  {
    path: '/products',
    name: 'ProductsPage',
    component: ProductsPage
  },
  {
    path: '/orders',
    name: 'OrdersPage',
    component: OrdersPage
  },
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
})

export default router;
