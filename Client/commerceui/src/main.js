import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'

import CoreuiVue from '@coreui/vue'
import CIcon from '@coreui/icons-vue'
import { iconsSet as icons } from '@/assets/icons'
import DocsExample from '@/components/DocsExample'
//import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
//set baseurl
window.$http = "http://localhost:8000/api/";

const app = createApp(App)
app.use(store)
app.use(router)
app.use(CoreuiVue)
app.provide('icons', icons)
// app.use(BootstrapVue)
// // Optionally install the BootstrapVue icon components plugin
// app.use(IconsPlugin)
app.component('CIcon', CIcon)
app.component('DocsExample', DocsExample)

app.mount('#app')
