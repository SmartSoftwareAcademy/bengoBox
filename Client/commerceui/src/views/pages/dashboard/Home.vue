<template>
    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-3 col-md-2 overflow-auto">
                <Categories />
            </div>
            <div class="col-sm-8 col-md-8 col-sm-6 col-md-6 col-sm-3">
                <slider />
            </div>
            <div class="col-sm-2 col-md-2">
                <Categories />
            </div>
        </div>
        <div class="row p-2 m-2">
            <div class="col-md-12 col-sm-12 col-md-6 col-sm-3 col-md-3">
                <Policy />
            </div>
            <FlashSale />
            <Deals />
        </div>

    </div>
</template>
<script>
import slider from '@/components/slider/slider.vue';
import Policy from './Policy.vue';
import FlashSale from '@/components/FlashSale.vue';
import Deals from '@/components/Deals.vue';
import Categories from '../Categories.vue';

export default {
    data() {
        return {

        }
    },
    components: {
        Deals,
        Categories,
        FlashSale,
        Policy,
        slider,

    },
    mounted: {

    },
    methods: {

    },
}

</script>