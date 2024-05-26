<template>
  <div class="w-100">
    <div v-if="isCompanyJob">
      <company-job-item
        v-for="job in jobs"
        :key="job.id"
        :job="job"
        @click="viewJobDetail(job)"
        @view-candidates="viewCandidates(job)"
        @edit="editRecruitment(job)"
      />
    </div>
    <div v-else>
      <job-item
        v-for="job in jobs"
        :key="job.id"
        :job="job"
        :show-reapply="showReapply"
        @click="viewJobDetail(job)"
      />
    </div>
    <div class="pagination-custom d-flex justify-content-center">
      <el-pagination
        :page-size="listQuery.pageSize"
        background
        layout="prev, pager, next"
        :total="total"
        :hide-on-single-page="total <= listQuery.pageSize"
        @current-change="handleCurrentChange"
      >
      </el-pagination>
    </div>
  </div>
</template>
<script>
import JobItem from "@/components/JobItem.vue";
import CompanyJobItem from "@/components/CopmanyJobItem.vue";

export default {
  components: {
    JobItem,
    CompanyJobItem,
  },
  props: {
    jobs: {
      type: Array,
      require: true,
    },
    total: {
      type: Number,
      require: true,
    },
    listQuery: {
      type: Object,
      require: true,
    },
    showReapply: {
      type: Boolean,
      default: false,
    },
    isCompanyJob: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {};
  },
  methods: {
    handleCurrentChange(val) {
      this.$emit("change-page-index");
      this.listQuery.pageIndex = val;
      this.$store.dispatch("job/getJobs", this.listQuery);
    },
    viewJobDetail(job) {
      var jobUrl = this.$router.resolve({
        path: `/cong-viec/${job.slug}`,
        query: { job: job },
      });

      window.open(jobUrl.href, "_blank");
    },

    viewCandidates(job) {
      this.$router.push({
        path: `/tuyen-dung/danh-sach-ung-vien/${job.slug}`,
        query: { job: job },
      });
    },
    editRecruitment(job) {
      this.$router.push({
        name: "EditRecruitment",
        params: { slug: job.slug },
      });
    },
  },
};
</script>