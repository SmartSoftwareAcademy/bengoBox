<template>
  <div class="row">
    <div class="col-sm-2">

    </div>
    <div class="col-sm-8">
      <slider />
      <Deals />
      <!-- <Policy /> -->
    </div>
  </div>
</template>

<script>
import slider from './slider/slider.vue';
import Deals from './Deals.vue';
//import Policy from '../views/pages/dashboard/Policy.vue';
export default {
  name: 'home',
  components: {
    slider,
    Deals,
    //Policy,
  },
  data() {
    return {
      items: [
        { title: 'Click Me' },
        { title: 'Click Me' },
        { title: 'Click Me' },
        { title: 'Click Me 2' },
      ],
      activeBtn: 1,
      colors: [
        'indigo',
        'warning',
        'pink darken-2',
        'red lighten-1',
        'deep-purple accent-4',
      ],
      slides: [
        'First',
        'Second',
        'Third',
        'Fourth',
        'Fifth',
      ],
    }
  },
}
</script>
<style>
.v-card--reveal {
  align-items: center;
  bottom: 0;
  justify-content: center;
  opacity: .5;
  position: absolute;
  width: 100%;
}
</style>

