import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      component: () => import('../views/pages/Layout/Layout'),
      children: [
        {
          name: 'Home',
          path: '/',
          component: () => import('../views/pages/dashboard/Home')
        },
        {
          path: '/shop',
          name: 'Shop',
          component: () => import('../views/pages/product/Shop')
        },
        {
          path: '/product',
          name: 'Product',
          component: () => import('../views/pages/product/Product')

        },
        {
          path: '/blog',
          component: () => import('../views/pages/blog/Blog'),
          name: 'Blog'
        },
        {
          path: '/post',
          name: 'Post',
          component: () => import('../views/pages/blog/Blog')
        },
        {
          path: '/cart',
          name: 'Cart',
          component: () => import('../views/pages/product/Cart')
        },

      ]

    },

  ],
  mode: 'history'
},

)
