<template>
  <div class="container">
    <slider-page :title="'Công việc đã lưu'" :height="200" :urlImg="imgBg" />
    <div class="row justify-content-center mt-5">
      <div class="col-xl-10">
        <list-job :jobs="getSavedJobs" :total="total" :listQuery="listQuery" />
      </div>
    </div>
  </div>
</template>

<script>
import SliderPage from "@/components/SliderArea/SliderPage";
import ListJob from "@/components/ListJob.vue";
import imgBg from "@/assets/img/banner/5.jpg";
import { mapGetters } from "vuex";
export default {
  components: {
    SliderPage,
    ListJob,
  },
  data() {
    return {
      listLoading: true,
      listQuery: {
        pageIndex: 1,
        pageSize: 10,
        keyword: null,
      },
      imgBg,
    };
  },
  created() {
    if (!this.savedJobs.length) {
      this.$store.dispatch("job/getSavedJobs", this.listQuery);
    }
  },
  computed: {
    ...mapGetters({
      savedJobs: "job/getSavedJobs",
      total: "job/getTotalSavedJobs",
    }),
    getSavedJobs() {
      this.listLoading = false;
      window.scrollTo(0, 0);
      return this.savedJobs;
    },
  },
};
</script>