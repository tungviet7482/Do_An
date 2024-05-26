<template>
  <div>
    <h5 class="text-truncate">{{ job.title }}</h5>
    <el-tabs type="card" @tab-click="handleClick">
      <el-tab-pane label="Ứng viên mới">
        <list-candidate-item
          :candidates="candidates"
          :total="total"
          :listQuery="listQuery"
        />
      </el-tab-pane>
      <el-tab-pane label="Ứng viên đã xem">
        <list-candidate-item
          :candidates="candidates"
          :total="total"
          :listQuery="listQuery"
        />
      </el-tab-pane>
      <el-tab-pane label="Ứng viên quan tâm">
        <list-candidate-item
          :candidates="candidates"
          :total="total"
          :showInterest="false"
          :listQuery="listQuery"
        />
      </el-tab-pane>
      <el-tab-pane label="Ứng viên bị loại">
        <list-candidate-item
          :candidates="candidates"
          :total="total"
          :showExclude="false"
          :listQuery="listQuery"
        />
      </el-tab-pane>
    </el-tabs>
  </div>
</template>
<script>
import { mapGetters } from "vuex";
import ListCandidateItem from "@/components/ListCandidateItem";
export default {
  components: {
    ListCandidateItem,
  },
  data() {
    return {
      activeName: "first",
      job: null,
      listQuery: {
        pageIndex: 1,
        pageSize: 10,
        viewed: false,
        applied: true,
        interested: null,
        jobId: null,
        userId: null,
      },
    };
  },
  created() {
    this.job = this.$route.query.job;
    if (!this.job || !this.job.title) {
      this.$store
        .dispatch("job/getJobBySlug", { slug: this.$route.params.slug })
        .then((data) => {
          this.job = data;
          this.fetchCandidates();
        });
    } else {
      this.fetchCandidates();
    }
  },
  computed: {
    ...mapGetters({
      candidates: "candidate/getCandidates",
      total: "candidate/getTotal",
    }),
  },
  methods: {
    fetchCandidates() {
      this.listQuery.jobId = this.job.id;
      this.$store.dispatch("candidate/getListApplied", this.listQuery);
    },
    handleClick(tab, event) {
      if (tab.index == 0) {
        this.listQuery.viewed = false;
      } else if (tab.index == 1) {
        this.listQuery.viewed = true;
        this.listQuery.interested = null;
      } else if (tab.index == 2) {
        this.listQuery.viewed = true;
        this.listQuery.interested = true;
      } else if (tab.index == 3) {
        this.listQuery.viewed = true;
        this.listQuery.interested = false;
      }

      this.$store.dispatch("candidate/getListApplied", this.listQuery);
    },
  },
};
</script>