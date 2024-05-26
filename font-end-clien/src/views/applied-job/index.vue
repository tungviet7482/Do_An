<template>
  <div class="container">
    <slider-page
      :title="'Công việc đã ứng tuyển'"
      :height="200"
      :urlImg="imgBg"
    />
    <div class="row justify-content-center mt-5">
      <div class="col-xl-10 mb-120">
        <list-job
          :jobs="getAppliedJobs"
          :total="total"
          :listQuery="listQuery"
          :show-reapply="true"
        />
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
    if (!this.appliedJobs.length) {
      this.$store.dispatch("job/getAppliedJobs", this.listQuery);
    }
  },
  computed: {
    ...mapGetters({
      appliedJobs: "job/getAppliedJobs",
      total: "job/getTotalAppliedJobs",
    }),
    getAppliedJobs() {
      this.listLoading = false;
      window.scrollTo(0, 0);
      return this.appliedJobs;
    },
  },
};
</script>