<script>
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import slider from '@/components/widgets/slider/slider.vue';
import Deals from '@/components/Deals';
import Policy from '../ecommerce/product/parts/Policy.vue';
import FlashSale from '@/components/FlashSale.vue';
import Categories from '../ecommerce/product/parts/Categories.vue';
import LeftSideCard from "./LeftSideCard.vue";
import playVue from "../audio/play.vue";

export default {
  page: {
    title: "Home",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },
  components: {
    Categories,
    playVue,
    Deals,
    FlashSale,
    LeftSideCard,
    Policy,
    PageHeader,
    slider,
  },
  data() {
    return {
      title: "Home",
      items: [
        {
          text: "Home",
        },
        {
          text: "",
          active: true,
        },
      ],
    };
  },
};
</script>

<template>
  <div class="container-fluid">
    <PageHeader :title="title" :items="items" />
    <div class="row">
      <div class="col-sm-3 col-md-2 overflow-auto">
        <Categories />
        <playVue />
      </div>
      <div class="col-sm-8 col-md-8 col-sm-6 col-md-6 col-sm-3">
        <slider />
      </div>
      <div class="col-sm-2 col-md-2">
        <LeftSideCard />
      </div>
    </div>
    <div class="row">
      <div class="col-md-12 col-sm-12 col-md-6  col-sm-6 col-sm-3 col-md-3">
        <Policy />
      </div>
      <FlashSale />
      <Deals />
    </div>
  </div>
</template>