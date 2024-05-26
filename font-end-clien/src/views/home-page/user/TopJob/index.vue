<template>
  <section class="featured-job-area feature-padding">
    <div class="container">
      <!-- Section Tittle -->
      <div class="row">
        <div class="col-lg-12">
          <div class="section-tittle text-center">
            <span>Việc làm mới nhất</span>
            <h2>Việc làm mới nhất</h2>
          </div>
        </div>
      </div>
      <div class="row justify-content-center">
        <div class="col-xl-10">
          <list-job :jobs="jobs" :total="6" :listQuery="listQuery" />
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import ListJob from "@/components/ListJob.vue";
export default {
  components: {
    ListJob,
  },
  props: {
    jobs: {
      type: Array,
      default: [],
    },
  },
  data() {
    return {
      listQuery: {
        pageIndex: 1,
        pageSize: 10
      },
    };
  },
};
</script>