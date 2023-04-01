<template>
  <v-app id="inspire">
    <v-toolbar color="dark" dark>
      <v-app-bar-nav-icon @click="menuOpen = !menuOpen"></v-app-bar-nav-icon>
      <!--      <v-app-bar-nav-icon @click.stop="drawer = !drawer" />-->
      <v-text-field flat solo-inverted hide-details prepend-inner-icon="mdi-magnify" label="Search"
        class="hidden-sm-and-down pl-10 ml-4" />
      <v-spacer />
      <div class="text-center" v-if="loggedIn">
        <v-menu v-model="menuOpen" :close-on-content-click="false" offset-y class="mt-2 pt-2">
          <template v-slot:activator="{ on }">
            <v-btn value="" @click.stop="toggleProfile = !toggleProfile" v-on="on">
              <v-icon class="text-warning">mdi-account-circle</v-icon>
            </v-btn>
          </template>
          <v-list class="mt-2">
            <v-list-item v-for="(item, i) in menu_items" :key="i" @click="toggleProfile = false">
              <router-link :to="{ name: item.link }">{{ item.title }}</router-link>
            </v-list-item>
          </v-list>
        </v-menu>
      </div>
      <div class="text-center" v-else>
        <v-btn icon to="/login">
          <v-icon>mdi-account-circle</v-icon>
        </v-btn>
      </div>
      <v-btn icon>
        <v-badge content="2" value="2" color="red" overlap>
          <v-icon class="text-white-50">mdi-bell</v-icon>
        </v-badge>
      </v-btn>
      <v-btn href="/cart" icon>
        <v-badge content="0" value="0" color="red" overlap>
          <v-icon class="text-warning">mdi-heart</v-icon>
        </v-badge>
      </v-btn>
      <v-btn @click="visitRoute('Cart')" icon>
        <v-badge :content="cart_items_count || 0" :value="cart_items_count || 1" color="red" overlap>
          <v-icon class="text-warning">mdi-cart</v-icon>
        </v-badge>
      </v-btn>
    </v-toolbar>
    <v-content>
      <v-bottom-navigation>
        <v-btn value="Home" @click="visitRoute('home')">
          <v-icon class="text-warning">mdi-home</v-icon>
          HOME
        </v-btn>
        <v-menu v-model="menuOpen" :close-on-content-click="false" offset-y>
          <template v-slot:activator="{ on }">
            <v-btn value="CATEGORIES" @click.stop="menuOpen = !menuOpen" v-on="on">
              <v-icon class="text-warning">mdi-widgets</v-icon>
              CATEGORIES
            </v-btn>
          </template>
          <v-list>
            <v-list-item v-for="(item, i) in items" :key="i" @click="menuOpen = false">
              <router-link :to="{ name: 'Products' }">{{ item.title }}</router-link>
            </v-list-item>
          </v-list>
        </v-menu>
        <v-btn value="Cart" @click="visitRoute('Shops')">
          <v-icon class="text-warning">mdi-store</v-icon>
          TOP SELLERS
        </v-btn>
        <v-btn value="Cart" @click="visitRoute('Blog')">
          <v-icon class="text-warning">mdi-book</v-icon>
          TRENDS
        </v-btn>
      </v-bottom-navigation>
    </v-content>
    <router-view />
    <v-footer :padless="true">
      <BottomNav />
      <v-card flat tile width="100%" class="secondary bg-gradient white--text text-center">
        <v-card-text class="white--text">
          Copyright &copy; {{ new Date().getFullYear() }}. All Rights Reserved — <a href="https://www.bengohub.co.ke"
            target="_blank"><strong>BengoBox Comerce</strong></a> Build .Grow
          .Excel
        </v-card-text>
      </v-card>
    </v-footer>
  </v-app>
</template>
<script>
import { mapState } from 'vuex';
import BottomNav from '../../components/BottomNav.vue';
//import store from '@/state/store';
export default {
  data() {
    return {
      menuOpen: false,
      toggleProfile: false,
      //cart_items_count: 0,
      items: [
        { title: 'T-Shirts' },
        { title: 'Jackets' },
        { title: 'Shirts' },
        { title: 'Jeans' },
        { title: 'Shoes' },
      ],
      menu_items: [
        { title: 'Account', link: 'Profile' },
        { title: 'Orders', link: 'Orders' },
        { title: 'Inbox', link: 'Chat' },
        { title: 'LogOut', link: 'logout' },
      ],
      activeBtn: 1,
      loggedIn: false,
    }
  },
  components: {
    BottomNav,
  },
  computed: {
    ...mapState({
      cart_items_count: state => state.cart.cart_items_count,
    }),
  },
  mounted() {
    //alert(localStorage.isadmin)
    if (localStorage.user) {
      this.loggedIn = true
    }
    if (localStorage.isadmin) {
      this.$router.push({ name: 'dashboard' });
    }
  },
  methods: {
    visitRoute(link) {
      this.$router.push({ name: link })
    },
  },
}
</script>
