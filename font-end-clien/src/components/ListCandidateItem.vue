<template>
  <div>
    <div class="row d-flex justify-content-center">
      <div class="col-xl-10">
        <candidate-item
          v-for="candidate in candidates"
          :key="candidate.id"
          :candidate="candidate"
          :showInterest="showInterest"
          :showExclude="showExclude"
          @view-cv="viewCV(candidate)"
          @add-interest="addInterest(candidate)"
          @add-exclude="addExclude(candidate)"
        />
        <div class="pagination-custom d-flex justify-content-center">
          <el-pagination
            :page-size="listQuery.pageSize"
            background
            layout="prev, pager, next"
            :total="total"
            :hide-on-single-page="total < listQuery.pageSize"
            @current-change="handleCurrentChange"
          >
          </el-pagination>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import CandidateItem from "@/components/CandidateItem.vue";

export default {
  components: {
    CandidateItem,
  },
  props: {
    candidates: {
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
    showExclude: {
      type: Boolean,
      default: true,
    },
    showInterest: {
      type: Boolean,
      default: true,
    },
  },
  data() {
    return {};
  },
  methods: {
    handleCurrentChange(val) {
      this.listQuery.pageIndex = val;
      this.$store.dispatch("candidate/getList", this.listQuery);
    },
    viewCV(candidate) {
      if (candidate.fileCVUrl) {
        window.open(candidate.fileCVUrl);
        if (this.listQuery.jobId) {
          this.listQuery.userId = candidate.id;
          this.listQuery.viewed = true;
          this.listQuery.interested = null;
          this.$store.dispatch(
            "candidate/candidateClassification",
            this.listQuery
          );
        }
      }
    },
    addInterest(candidate) {
      this.listQuery.userId = candidate.id;
      this.listQuery.viewed = true;
      this.listQuery.interested = true;
      this.$store.dispatch("candidate/candidateClassification", this.listQuery);
    },
    addExclude(candidate) {
      this.listQuery.userId = candidate.id;
      this.listQuery.viewed = true;
      this.listQuery.interested = false;
      this.$store.dispatch("candidate/candidateClassification", this.listQuery);
    },
  },
};
</script>