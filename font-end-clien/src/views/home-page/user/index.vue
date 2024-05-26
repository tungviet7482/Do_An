<template>
  <main class="mb-150">
    <slider-home></slider-home>
    <top-job :jobs="jobs.slice(0, 6)" />
    <c-v-area :img="bgImgCVArea"></c-v-area>
    <top-categories :categories="categories.slice(0, 8)" />
    <user-manual />
    <about-us />
    <news-area />
  </main>
</template>

<script>
import SliderHome from "./SliderHome";
import TopCategories from "./TopCategories";
import UserManual from "./UserManual.vue";
import TopJob from "./TopJob";
import CVArea from "./CVArea.vue";
import AboutUs from "./AboutUs.vue";
import NewsArea from "@/components/NewsArea";
import bgImgCVArea from "@/assets/img/gallery/cv_bg.jpg";
import { mapGetters } from "vuex";

export default {
  components: {
    SliderHome,
    TopCategories,
    TopJob,
    CVArea,
    UserManual,
    AboutUs,
    NewsArea,
  },
  data() {
    return {
      bgImgCVArea: bgImgCVArea,
    };
  },
  created() {
    if (!this.categories.length) {
      this.$store.dispatch("job-category/getList", {
        pageIndex: 0,
        pageSize: 0,
      });
    }
    this.$store.dispatch("job/getJobs", {
      pageIndex: 1,
      pageSize: 10,
      jobStatus: 0,
      published: true,
    });
  },
  mounted() {
    if (this.$route.meta.saveScrollPosition) {
      window.scrollTo(0, this.$route.meta.savedPosition);
    }
  },
  computed: {
    ...mapGetters({
      categories: "job-category/getCategories",
      jobs: "job/getActiveJobs",
    }),
  },
  beforeRouteLeave(to, from, next) {
    this.$route.meta.savedPosition = window.scrollY;
    next();
  },
};
</script>