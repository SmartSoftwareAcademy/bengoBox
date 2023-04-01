<template>
    <b-container class="overflow-auto bg-warning" max-height="200px">
        <b-scrollspy scroll-y class="overflow-auto" max-height="200px">
            <router-link :to="{ name: 'POS' }"><v-icon class="text-warning">mdi-stor</v-icon>POS</router-link>
            <h3 class="text-danger text-center"><v-chip closable prepend-icon="mdi-vuetify" class="bg-danger">HOT
                </v-chip>&nbsp;Weekly Deals!!
            </h3>
            <b-row class="overflow-auto" max-height="200px">
                <!-- Content goes here -->
                <v-card class="pa-0 mt-2" max-width="100%">
                    <v-img :src="require('@/assets/img/home/deal4.jpg')" height="100"></v-img>
                    <v-card-title class="">Fashion Shoes</v-card-title>
                    <v-card-actions>
                        <v-btn color="warning" @click="visitRoute()">Shop Now</v-btn>
                    </v-card-actions>
                </v-card>
                <!-- Content goes here -->
                <v-card class="pa-0 mt-2" max-width="100%">
                    <v-img :src="require('@/assets/img/home/deal3.jpg')" height="100"></v-img>
                    <v-card-title class="">Fashion Jeans</v-card-title>
                    <v-card-actions>
                        <v-btn color="warning" @click="visitRoute()">Shop Now</v-btn>
                    </v-card-actions>
                    <v-card-subtitle class="pa-2">Card Subtitle</v-card-subtitle>
                </v-card>
            </b-row>
        </b-scrollspy>
    </b-container>
</template>
<script>
export default {
    data() {
        return {

        }
    },
    mounted() {

    },
    methods: {
        visitRoute() {
            this.$router.push({ name: 'Products' })
        }
    },
}
</script>
<style>
.scrollable .v-card-text {
    max-height: 200px;
    /* Set a maximum height to enable scrolling */
    overflow-y: auto;
    /* Enable vertical scrolling */
}
</style>
