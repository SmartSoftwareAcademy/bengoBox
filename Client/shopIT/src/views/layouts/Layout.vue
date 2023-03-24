<template>
  <v-app id="inspire">
    <v-toolbar color="dark" dark>
      <v-app-bar-nav-icon @click="menuOpen = !menuOpen"></v-app-bar-nav-icon>
      <!--      <v-app-bar-nav-icon @click.stop="drawer = !drawer" />-->
      <v-text-field flat solo-inverted hide-details prepend-inner-icon="mdi-magnify" label="Search"
        class="hidden-sm-and-down pl-10 ml-4" />
      <v-spacer />
      <v-btn icon>
        <v-icon>mdi-account-circle</v-icon>
      </v-btn>
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
      <v-btn href="/cart" icon>
        <v-badge :content="cart_items_count || 0" :value="cart_items_count || 1" color="red" overlap>
          <v-icon class="text-warning">mdi-cart</v-icon>
        </v-badge>
      </v-btn>
    </v-toolbar>
    <v-content>
      <v-bottom-navigation :value="activeBtn" color="dark" horizontal>
        <router-link :to="{ name: 'home' }" class="v-btn">
          <span>Home</span>
        </router-link>
        <router-link :to="{ name: 'Shops' }" class="v-btn">
          <span>Sellers</span>
        </router-link>
        <v-menu v-model="menuOpen" :close-on-content-click="false" offset-y>
          <template v-slot:activator="{ on }">
            <v-icon @click.stop="menuOpen = !menuOpen" v-on="on" title="Cat">mdi-menu</v-icon>
          </template>

          <v-list>
            <v-list-item v-for="(item, i) in items" :key="i" @click="menuOpen = false">
              <router-link :to="{ name: 'Products' }">{{ item.title }}</router-link>
            </v-list-item>
          </v-list>
        </v-menu>
        <v-btn href="#">
          <router-link :to="{ name: 'Blog' }">Trends</router-link>
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
export default {
  data() {
    return {
      menuOpen: false,
      //cart_items_count: 0,
      items: [
        { title: 'T-Shirts' },
        { title: 'Jackets' },
        { title: 'Shirts' },
        { title: 'Jeans' },
        { title: 'Shoes' },
      ],
      activeBtn: 1,
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
  methods: {

  },
  mounted() {

  },
}
</script>
