<template>
    <v-container>
        <v-row no-gutters>
            <v-col :cols="12">
                <v-card-text class="" tile outlined>
                    <v-card-title class="subheading ">Deals of the Day</v-card-title>
                    <v-divider></v-divider>
                    <div class="row">
                        <div class="col-12 col-md-3 col-sm-6 col-xs-6 text-center">
                            <v-hover v-slot:default="{ hover }" open-delay="200">
                                <v-card :elevation="hover ? 16 : 2">
                                    <v-img class="white--text align-end" height="200px"
                                        :src="require('@/assets/img/home/deal1.jpg')">
                                        <v-card-title>Bags & Purses </v-card-title>
                                    </v-img>

                                    <v-card-text class="text--primary text-center">
                                        <div>Upto 60% + Extra 10%</div>
                                        <div>Baggit, Zara, Fossil</div>
                                    </v-card-text>

                                    <div class="text-center">
                                        <v-btn @click="gotProducts()" class="ma-2" outlined color="info">
                                            Explore
                                        </v-btn>
                                    </div>
                                </v-card>
                            </v-hover>
                        </div>
                        <div class="col-12 col-md-3 col-sm-6 col-xs-6 text-center">
                            <v-hover v-slot:default="{ hover }" open-delay="200">
                                <v-card :elevation="hover ? 16 : 2">
                                    <v-img class="white--text align-end" height="200px"
                                        :src="require('@/assets/img/home/deal2.jpg')">
                                        <v-card-title>T-Shirt </v-card-title>
                                    </v-img>

                                    <v-card-text class="text--primary text-center">
                                        <div>Upto 50%</div>
                                        <div>Zara, Selected, Celio</div>
                                    </v-card-text>

                                    <div class="text-center">
                                        <v-btn @click="gotProducts()" class="ma-2" outlined color="info">
                                            Explore
                                        </v-btn>
                                    </div>
                                </v-card>
                            </v-hover>
                        </div>
                        <div class="col-12 col-md-3 col-sm-6 col-xs-6 text-center">
                            <v-hover v-slot:default="{ hover }" open-delay="200">
                                <v-card :elevation="hover ? 16 : 2">
                                    <v-img class="white--text align-end" height="200px"
                                        :src="require('@/assets/img/home/deal3.jpg')">
                                        <v-card-title>Jeans </v-card-title>
                                    </v-img>

                                    <v-card-text class="text--primary text-center">
                                        <div>Upto 60% + Extra 10%</div>
                                        <div>Jack & Jones, Levis</div>
                                    </v-card-text>

                                    <div class="text-center">
                                        <v-btn @click="gotProducts()" class="ma-2" outlined color="info">
                                            Explore
                                        </v-btn>
                                    </div>
                                </v-card>
                            </v-hover>
                        </div>
                        <div class="col-12 col-md-3 col-sm-6 col-xs-6 text-center">
                            <v-hover v-slot:default="{ hover }" open-delay="200">
                                <v-card :elevation="hover ? 16 : 2">
                                    <v-img class="white--text align-end" height="200px"
                                        :src="require('@/assets/img/home/deal4.jpg')">
                                        <v-card-title>Shoes </v-card-title>
                                    </v-img>

                                    <v-card-text class="text--primary text-center">
                                        <div>Upto 60% + Extra 10%</div>
                                        <div>Nike, Adidas, Puma</div>
                                    </v-card-text>

                                    <div class="text-center">
                                        <v-btn @click="gotProducts()" class="ma-2" outlined color="info">
                                            Explore
                                        </v-btn>
                                    </div>
                                </v-card>
                            </v-hover>
                        </div>
                    </div>
                </v-card-text>
            </v-col>
        </v-row>
    </v-container>
</template>
<script>
export default {
    data() {
        return {

        }
    }
    ,
    mounted() {

    },
    methods: {
        gotProducts() {
            this.$router.push({ name: 'Products' })
        }
    },
}
</script>