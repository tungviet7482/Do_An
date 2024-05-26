<template>
  <div class="app-container">
    <div class="filter-container mb-30">
      <el-input
        v-model="listQuery.keyword"
        placeholder="Ngôn ngữ, framework, công nghệ..."
        style="width: 400px"
        class="filter-item"
      />
      <el-select
        v-model="listQuery.locationId"
        class="filter-item"
        style="width: 250px"
        filterable
        placeholder="Địa điểm"
      >
        <el-option :key="0" label="Tất cả địa điểm" :value="null"> </el-option>
        <el-option
          v-for="item in locations"
          :key="item.id"
          :label="item.name"
          :value="item.id"
        >
        </el-option>
      </el-select>
      <el-select
        v-model="listQuery.fieldId"
        style="width: 250px"
        filterable
        placeholder="Lĩnh vực"
        class="filter-item"
      >
        <el-option :key="0" label="Tất cả lĩnh vực" :value="null"> </el-option>
        <el-option
          v-for="item in categories"
          :key="item.id"
          :label="item.name"
          :value="item.id"
        >
        </el-option>
      </el-select>
      <el-button
        class="filter-item"
        type="primary"
        icon="el-icon-search"
        @click="handleFilter"
      >
        Tìm
      </el-button>
      <br>
      <label  style="width: 80px; line-height: 36px">Năm sinh</label>
       <el-input
        v-model="listQuery.minAge"
        class="filter-item"
        style="width: 150px"
      />
      <label style="margin: 0 10px 0 7px; line-height: 36px">-</label>
      <el-input
        v-model="listQuery.maxAge"
        class="filter-item"
        style="width: 150px"
      />
    </div>
    <div>
      <list-candidate-item
        :candidates="candidates"
        :total="total"
        :showInterest="false"
        :showExclude="false"
        :listQuery="listQuery"
      />
    </div>
  </div>
</template>

<script>
import Tinymce from "@/components/Tinymce";
import { mapGetters } from "vuex";
import ListCandidateItem from "@/components/ListCandidateItem";
export default {
  name: "InlineEditTable",
  components: {
    Tinymce,
    ListCandidateItem,
  },
  data() {
    return {
      listQuery: {
        pageIndex: 1,
        pageSize: 10,
        keyWord: null,
        locationId: null,
        fieldId: null,
        minAge: null,
        maxAge: null,
      },
    };
  },
  created() {
    this.$store.dispatch("location/getList", { pageIndex: 0 });
    this.$store.dispatch("job-category/getList", { pageIndex: 0 });
    this.$store.dispatch("candidate/getList", this.listQuery);
  },
  computed: {
    ...mapGetters({
      locations: "location/getLocations",
      categories: "job-category/getCategories",
      candidates: "candidate/getCandidates",
      total: "candidate/getTotal",
    }),
  },
  methods: {
    handleFilter() {
      this.listQuery.pageIndex = 1;
      this.$store.dispatch("candidate/getList", this.listQuery);
    },
  },
};
</script>

<style scoped>
.el-row {
  display: flex;
  align-items: center;
}

.edit-input {
  padding-right: 100px;
}
.cancel-btn {
  position: absolute;
  right: 15px;
  top: 10px;
}
.el-button {
  margin-left: 15px;
}
.el-table {
  margin-bottom: 80px;
}
.pagination-custom {
  position: fixed;
  bottom: 0;
  left: 0;
  right: 0;
}
.el-pagination {
  display: flex;
  justify-content: center;
  margin: 25px 0;
}
.line {
  text-align: center;
}
.filter-item {
  margin-right: 5px;
}
</style>
