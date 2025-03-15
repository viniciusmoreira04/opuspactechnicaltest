import { createRouter, createWebHistory } from "vue-router";

import LoginPage from "@/components/LoginPage.vue";
import DashboardPage from "@/components/DashboardPage.vue";

const routes = [
  {
    path: '/',
    name: 'LoginPage',
    component: LoginPage,
  },
  {
    path: '/dashboard',
    name: 'DashboardPage',
    component: DashboardPage,
    meta: { requiresAuth: true },
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
  const token = localStorage.getItem('token'); 

  if (to.meta.requiresAuth && !token) {
    next({ name: 'LoginPage' });
  } else {
    next();
  }
});

export default router;
