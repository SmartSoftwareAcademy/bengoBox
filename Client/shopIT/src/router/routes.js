import store from '@/state/store'
import Layout from "../views/layouts/Layout";
import PageNotFound from '../components/404/PageNotFound.vue';


export default [{
  path: '/login',
  name: 'login',
  component: () => import('../views/pages/account/login'),
  meta: {
    beforeResolve(routeTo, routeFrom, next) {
      // If the user is already logged in
      if (store.getters['auth/loggedIn']) {
        // Redirect to the home page instead
        if (!this.$store.state.layout.admin_layout) {
          next({
            name: 'home'
          })
        } else {
          next({
            name: 'dashboard'
          })
        }
      } else {
        // Continue to the login page
        next()
      }
    },
  },
},
{
  path: '/register',
  name: 'register',
  component: () => import('../views/pages/account/register'),
  meta: {
    beforeResolve(routeTo, routeFrom, next) {
      // If the user is already logged in
      if (store.getters['auth/loggedIn']) {
        if (!this.$store.state.layout.admin_layout) {
          next({
            name: 'home'
          })
        } else {
          next({
            name: 'adminhome'
          })
        }
      } else {
        // Continue to the login page
        next()
      }
    },
  },
},
{
  path: '/forgot-password',
  name: 'Forgot-password',
  component: () => import('../views/pages/account/forgot-password'),
  meta: {
    beforeResolve(routeTo, routeFrom, next) {
      // If the user is already logged in
      if (store.getters['auth/loggedIn']) {
        // Redirect to the home page instead
        if (!this.$store.state.layout.admin_layout) {
          next({
            name: 'home'
          })
        } else {
          next({
            name: 'adminhome'
          })
        }
      } else {
        // Continue to the login page
        next()
      }
    },
  },
},
{
  path: '/logout',
  name: 'logout',
  meta: {
    authRequired: true,
    beforeResolve(routeTo, routeFrom, next) {
      if (process.env.VUE_APP_DEFAULT_AUTH === "firebase") {
        store.dispatch('auth/logOut')
      } else if (process.env.VUE_APP_DEFAULT_AUTH === "fakebackend") {
        store.dispatch('authfack/logout')
      }
      const authRequiredOnPreviousRoute = routeFrom.matched.some(
        (route) => route.push('/login')
      )
      // Navigate back to previous page, or home as a fallback
      next(authRequiredOnPreviousRoute ? {
        name: 'home'
      } : {
        ...routeFrom
      })
    },
  },
},
{
  path: '*',
  name: 'notFound',
  component: PageNotFound
},
{
  path: "/",
  component: Layout,
  children: [
    {
      path: '/',
      name: 'home',
      meta: {
        authRequired: false,
      },
      component: () => import('../views/pages/dashboard/index')
    },
    {
      path: '/Dashboard',
      name: 'dashboard',
      meta: {
        authRequired: true,
      },
      component: () => import('../views/pages/dashboard/Dashboard')
    },
    {
      path: '/blog',
      name: 'Blog',
      meta: {
        authRequired: true,
      },
      component: () => import('../views/pages/blog/Blog')
    },
    {
      path: '/ecommerce/products',
      name: 'Products',
      meta: {
        authRequired: true
      },
      component: () => import('../views/pages/ecommerce/products')
    },
    {
      path: '/ecommerce/product-detail/:id',
      name: 'Product Detail',
      meta: {
        authRequired: true
      },
      component: () => import('../views/pages/ecommerce/product-detail')
    },
    {
      path: '/ecommerce/orders',
      name: 'Orders',
      meta: {
        authRequired: true
      },
      component: () => import('../views/pages/ecommerce/orders')
    },
    {
      path: '/ecommerce/customers',
      name: 'Customers',
      meta: {
        authRequired: true
      },
      component: () => import('../views/pages/ecommerce/customers')
    },
    {
      path: '/ecommerce/cart',
      name: 'Cart',
      meta: {
        authRequired: true
      },
      component: () => import('../views/pages/ecommerce/cart')
    },
    {
      path: '/ecommerce/checkout',
      name: 'Checkout',
      meta: {
        authRequired: true
      },
      component: () => import('../views/pages/ecommerce/checkout')
    },
    {
      path: '/ecommerce/shops',
      name: 'Shops',
      meta: {
        authRequired: true
      },
      component: () => import('../views/pages/ecommerce/shops')
    },
    {
      path: '/ecommerce/add-product',
      name: 'Add Product',
      meta: {
        authRequired: true
      },
      component: () => import('../views/pages/ecommerce/add-product')
    },
    {
      path: '/email/inbox',
      name: 'Inbox',
      meta: {
        authRequired: true
      },
      component: () => import('../views/pages/email/inbox')
    },
    {
      path: '/email/reademail/:id',
      name: 'Read Email',
      meta: {
        authRequired: true
      },
      component: () => import('../views/pages/email/reademail')
    },
    {
      path: '/invoices/detail',
      name: 'Invoice Detail',
      meta: {
        authRequired: true
      },
      component: () => import('../views/pages/invoices/detail')
    },
    {
      path: '/invoices/list',
      name: 'Invoice List',
      meta: {
        authRequired: true
      },
      component: () => import('../views/pages/invoices/list')
    },
    {
      path: '/contacts/grid',
      name: 'User Grid',
      meta: {
        authRequired: true
      },
      component: () => import('../views/pages/contacts/grid')
    },
    {
      path: '/contacts/list',
      name: 'User List',
      meta: {
        authRequired: true
      },
      component: () => import('../views/pages/contacts/list')
    },
    {
      path: '/contacts/profile',
      name: 'Profile',
      meta: {
        authRequired: true
      },
      component: () => import('../views/pages/contacts/profile')
    },
    {
      path: '/charts/apex',
      name: 'apex',
      meta: {
        authRequired: true,
      },
      component: () => import('../views/pages/charts/apex/index')
    },
    {
      path: '/charts/chartist',
      name: 'chartist',
      meta: {
        authRequired: true,
      },
      component: () => import('../views/pages/charts/chartist/index')
    },
    {
      path: '/charts/chartjs',
      name: 'chartjs',
      meta: {
        authRequired: true,
      },
      component: () => import('../views/pages/charts/chartjs/index')
    },
    {
      path: '/charts/echart',
      name: 'echart',
      meta: {
        authRequired: true,
      },
      component: () => import('../views/pages/charts/echart/index')
    },
  ]
},
]