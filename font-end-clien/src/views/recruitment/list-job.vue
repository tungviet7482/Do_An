<template>
  <div class="container mb-30">
    <div class="row">
      <div class="col-xl-12 col-lg-12">
        <list-job
          :jobs="jobs"
          :total="total"
          :listQuery="listQuery"
          :is-company-job="true"
        />
      </div>
    </div>
  </div>
</template>
<script>
import ListJob from "@/components/ListJob.vue";
import { mapGetters } from "vuex";

export default {
  components: {
    ListJob,
  },
  data() {
    return {
      listLoading: true,
      listQuery: {
        pageIndex: 1,
        pageSize: 10,
        companyId: null,
      },
      company: null,
    };
  },
  created() {
    this.listQuery.companyId = this.companyId;
    this.$store.dispatch("job/getJobs", this.listQuery);
  },
  computed: {
    ...mapGetters({
      companyId: "companyId",
      jobs: "job/getJobsInCompany",
      total: "job/getTotalJobsInCompany",
    }),
  },
  methods: {
  },
};
</script>
<style scoped>
ul li {
  margin: 30px 0;
}
ul div li {
  display: flex;
  align-content: center;
}
ul li i {
  flex-shrink: 0;
  width: 30px;
  height: 30px;
  text-align: center;
  line-height: 30px;
  border-radius: 50%;
  background-color: rgb(221, 63, 36);
  margin-right: 15px;
  color: #fff;
}
</style>