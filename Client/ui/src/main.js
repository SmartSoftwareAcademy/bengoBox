import Vue from 'vue'
import BootstrapVue from 'bootstrap-vue'
import router from './router'

import vco from "v-click-outside"
import VueApexCharts from 'vue-apexcharts'
import VueSweetalert2 from 'vue-sweetalert2';
import VueSlideBar from 'vue-slide-bar'
import Vuelidate from 'vuelidate'
import i18n from './i18n'
import store from '@/state/store'
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';

import App from './App.vue'
// As a plugin
Vue.config.productionTip = false

import Axios from 'axios'
Vue.prototype.$axios = Axios
var CryptoJS = require("crypto-js");
// As a plugin
import VueMask from "v-mask";
Vue.config.productionTip = false;
//API ENDPOINT CONFIG
var href = window.location.href;
var arr = href.split("/");
var arr2 = arr[2].split(":");

window.$localurl = arr2[0] + "";
var ref = arr[0] + "//" + arr2[0] + ":58856";
window.$http = ref + "/gateway/";


//window.$http = "https://localhost:4445/gateway/";
//var baseURL = "http://localhost:58856/gateway/";
var baseURL = "http://localhost:4445/gateway/";
Vue.prototype.$http = Axios;
Axios.defaults.baseURL = (process.env.API_PATH !== 'production') ? baseURL : window.$http;
window.addEventListener("beforeunload", function (e) {
    alert("loging out");
    delete e["returnValue"];
});
var tokenString = "";

try {
    if (localStorage.user.trim() != "") {
        tokenString = CryptoJS.AES.decrypt(
            JSON.parse(localStorage.user).token,
            "mnopqr",
        )
            .toString(CryptoJS.enc.Utf8)
            .trim();
    }
} catch (e) {
    tokenString = "";
}
window.$tokenString = tokenString;
window.$headers = {
    Authorization: `Bearer ${window.$tokenString}`,
};
Axios.defaults.headers.common = window.$headers;

import * as VueGoogleMaps from "vue2-google-maps";
import Lightbox from "vue-easy-lightbox";

// Make BootstrapVue available throughout your project
Vue.use(BootstrapVue);
// Optionally install the BootstrapVue icon components plugin
//Vue.use(IconsPlugin);
Vue.use(Lightbox);
//Vue.use(ProgressBar);
Vue.use(VueGoogleMaps, {
    load: {
        key: "AIzaSyAbvyBxmMbFhrzP9Z8moyYr6dCr-pzjhBE",
        libraries: "places",
    },
    installComponents: true,
});

import { initFirebaseBackend } from "./helpers/firebase/authUtils";

import { configurebengoboxAuth } from "./helpers/bengoboxAuth/bengoboxAuth";

const firebaseConfig = {
    apiKey: process.env.VUE_APP_APIKEY,
    authDomain: process.env.VUE_APP_AUTHDOMAIN,
    databaseURL: process.env.VUE_APP_VUE_APP_DATABASEURL,
    projectId: process.env.VUE_APP_PROJECTId,
    storageBucket: process.env.VUE_APP_STORAGEBUCKET,
    messagingSenderId: process.env.VUE_APP_MESSAGINGSENDERID,
    appId: process.env.VUE_APP_APPId,
    measurementId: process.env.VUE_APP_MEASUREMENTID,
};

if (process.env.VUE_APP_DEFAULT_AUTH === "firebase") {
    initFirebaseBackend(firebaseConfig);
} else if (process.env.VUE_APP_DEFAULT_AUTH === "bengoboxAuth") {
    configurebengoboxAuth();
}

import '@/assets/scss/app.scss'

Vue.component('VueSlideBar', VueSlideBar)
Vue.use(BootstrapVue)
Vue.use(DatePicker)
Vue.use(vco)
Vue.component('apexchart', VueApexCharts)
Vue.use(Vuelidate)
Vue.use(VueSweetalert2);

Vue.use(require("vue-chartist"));
Vue.use(VueMask);
//const eventsHub = new Vue();
// Vue.use(IdleVue, {
//     eventEmitter: eventsHub,
//     store,
//     idleTime: 18000000, // 3 seconds,
//     startAtIdle: false,
// });

new Vue({
    router,
    store,
    i18n,
    render: (h) => h(App),
}).$mount("#app");
