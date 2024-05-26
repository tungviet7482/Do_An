<template>
  <div class="col-xl-3 col-lg-3 col-md-4 col-sm-6" @click="$emit('click')">
    <div class="single-services text-center mb-30">
      <div class="services-ion m-4">
        <i :class="'fas ' + icon"></i>
      </div>
      <div class="services-cap">
        <h5><router-link :to="{name: routerName}">{{title}}</router-link></h5>
        <span>({{totalJobs}})</span>
      </div>
    </div>
  </div>
</template>

<script>
export default {
    name: 'TopCategoryItem',
    props:{
        icon:{
            type: String,
            default: 'flaticon-tour'
        },
        title: {
            type: String,
            default: ''
        },
        totalJobs: {
            type: Number,
            default: 0
        },
        routerName:{
          type: String,
          default: ''
        }
    }
};
</script>
<style scoped>
.services-ion i{
  font-size: 50px;
}
</style>