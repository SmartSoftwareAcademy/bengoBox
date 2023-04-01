<template>
    <!-- <v-bottom-navigation v-model="activeLink" :shift="shift" :show-arrows="showArrows" :absolute="absolute"
        :value="activeLink" class="bg-warning bg-gradient">
        <v-btn v-for="item in botton_menu_items" :key="item.link" @click="visitRoute(item.link)" text>
            <v-icon>{{ item.icon }}</v-icon>
            <span class="hidden-sm-and-down">{{ item.text }}</span>
        </v-btn>
    </v-bottom-navigation> -->
    <v-bottom-navigation>
        <v-btn value="Home" @click="visitRoute('home')">
            <v-icon class="text-warning">mdi-home</v-icon>
            Recent
        </v-btn>
        <v-btn value="Favorites" @click="visitRoute('Favorites')">
            <v-icon class="text-warning">mdi-heart</v-icon>
            Favorites
        </v-btn>
        <v-btn value="Cart" @click="visitRoute('Cart')">
            <v-icon class="text-warning">mdi-cart</v-icon>
            Cart
        </v-btn>
    </v-bottom-navigation>
</template>

<script>
export default {
    data() {
        return {
            activeLink: '',
            botton_menu_items: [
                { icon: 'mdi-home', text: 'Home', link: 'home' },
                { icon: 'mdi-account', text: 'Profile', link: 'Profile' },
                { icon: 'mdi-settings', text: 'Settings', link: 'Settings' }
            ]
        }
    },
    computed: {
        shift() {
            return this.$vuetify.breakpoint.width <= 600 ? false : true;
        },
        showArrows() {
            return this.$vuetify.breakpoint.width <= 960 ? false : true;
        },
        absolute() {
            return this.$vuetify.breakpoint.width <= 960 ? true : false;
        }
    },
    mounted() {

    },
    methods: {
        visitRoute(link) {
            this.$router.push({ name: link })
        },
    },
}
</script>
