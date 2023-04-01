<template>
    <div class="container">
        <div class="row">
            <div class="col-md-6 col-sm-6 col-xs-12">
                <v-card>
                    <v-img :src="require('@/assets/img/home/slider2.jpg')" class="white--text align-center"
                        gradient="to bottom, rgba(0,0,0,.1), rgba(0,0,0,.5)" height="400px">
                        <h1 class="text-center font-size text-white">Top Picks</h1>
                        <div class="text-center">
                            <v-btn @click="gotProducts()" class="white--text " outlined>SHOP NOW</v-btn>
                        </div>
                    </v-img>
                </v-card>
                <!--        </v-hover>-->
            </div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <v-card>
                    <v-img :src="require('@/assets/img/home/slider3.jpg')" class="white--text align-center"
                        gradient="to bottom, rgba(0,0,0,.1), rgba(0,0,0,.5)" height="400px">
                        <h1 class="text-center font-size text-white">New Arrivals</h1>
                        <div class="text-center">
                            <v-btn @click="gotProducts()" class="white--text" outlined>SHOP NOW</v-btn>
                        </div>
                    </v-img>
                </v-card>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12 col-sm-12 col-md-6  col-md-3 col-sm-3flex flex-row flex-column">
                <h3 class="bg-warning p-2 rounded-2">Flash Sale
                    <p class="badge badge-pill bg-danger d-inline"> days:</p>
                    <b> {{ days }} </b>
                    <p class="badge badge-pill bg-danger d-inline"> hours:</p>
                    <b> {{ hours }} </b>
                    <p class="badge badge-pill bg-danger d-inline"> minutes:</p>
                    <b> {{ minutes }} </b>
                    <p class="badge badge-pill bg-danger d-inline">seconds:</p>
                    <b> {{ seconds }} </b>
                </h3>
            </div>
            <div class="col-md-4 col-sm-4 col-xs-12">
                <v-card outlined>
                    <v-img :src="require('@/assets/img/home/deal2.jpg')" class="white--text align-center"
                        gradient="to bottom, rgba(0,0,0,.1), rgba(0,0,0,.5)" height="300px">
                        <h1 class="text-center font-size text-white">Shirts</h1>
                        <div class="text-center mt-2">
                            <v-btn class="white--text caption" @click="gotProducts()" text>SHOP NOW <v-icon
                                    class="white--text caption">mdi-arrow-right</v-icon></v-btn>
                        </div>
                    </v-img>
                </v-card>
            </div>
            <div class="col-md-4 col-sm-4 col-xs-12">
                <v-card outlined>
                    <v-img :src="require('@/assets/img/home/deal3.jpg')" class="white--text align-center"
                        gradient="to bottom, rgba(0,0,0,.1), rgba(0,0,0,.5)" height="300px">
                        <h1 class="text-center font-size text-white">Pants</h1>
                        <div class="text-center mt-2">
                            <v-btn class="white--text caption" @click="gotProducts()" text>SHOP NOW <v-icon
                                    class="white--text caption">mdi-arrow-right</v-icon></v-btn>
                        </div>
                    </v-img>
                </v-card>
            </div>
            <div class="col-md-4 col-sm-4 col-xs-12">
                <v-card outlined>
                    <v-img :src="require('@/assets/img/home/deal4.jpg')" class="white--text align-center"
                        gradient="to bottom, rgba(0,0,0,.1), rgba(0,0,0,.5)" height="300px">
                        <h1 class="text-center font-size text-white">Shoes</h1>
                        <div class="text-center mt-2">
                            <v-btn class="white--text caption" @click="gotProducts()" text>SHOP NOW <v-icon
                                    class="white--text caption">mdi-arrow-right</v-icon></v-btn>
                        </div>
                    </v-img>
                </v-card>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    data() {
        return {
            endTime: new Date('2023-04-10T00:00:00.000Z'),
            days: 0,
            hours: 0,
            minutes: 0,
            seconds: 0
        }
    }
    ,
    mounted() {
        setInterval(() => {
            const now = new Date().getTime();
            const distance = this.endTime - now;
            if (this.endTime > now) {
                this.days = Math.floor(distance / (1000 * 60 * 60 * 24));
                this.hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
                this.minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
                this.seconds = Math.floor((distance % (1000 * 60)) / 1000);
            }
        }, 1000);
    },
    methods: {
        gotProducts() {
            this.$router.push({ name: 'Products' })
        },

    },
}
</script>