<template>
  <div>
    <slider-page
      :title="'Danh sách các công ty nổi bật'"
      :height="300"
      :fontSize="'40px'"
      :urlImg="img"
    ></slider-page>
    <div class="container">
      <div class="row mt-20 mb-20">
        <div class="col-lg-6">
          <el-input
            placeholder="Nhập từ khóa"
            prefix-icon="el-icon-search"
            size="medium"
            v-model="listQuery.keyWord"
          >
          </el-input>
        </div>
        <div class="col-lg-2">
          <buttom-custom :content="'Tìm kiếm'" @click="filterCompanies" />
        </div>
      </div>
      <div class="row flex-wrap">
        <company-item
          v-for="company in companies"
          :key="company.id"
          :company="company"
          @click="viewCompanyDetail(company)"
        />
      </div>
      <div class="d-flex justify-content-center mb-5">
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
  </div>
</template>
<script>
import SliderPage from "@/components/SliderArea/SliderPage.vue";
import { mapGetters } from "vuex";
import CompanyItem from "./CompanyItem";
import ButtomCustom from "@/components/Button/ButtonCustom.vue";
import img from "@/assets/img/banner/pexels-pixabay-269077.jpg";

export default {
  components: {
    SliderPage,
    CompanyItem,
    ButtomCustom,
  },
  data() {
    return {
      listLoading: true,
      listQuery: {
        pageIndex: 1,
        pageSize: 15,
      },
      img,
    };
  },
  created() {
    if (!this.companies.length) {
      this.$store.dispatch("company/getList", this.listQuery);
    }
  },
  computed: {
    ...mapGetters({
      companies: "company/getCompanies",
      total: "company/getTotal",
    }),
  },
  methods: {
    viewCompanyDetail(company) {
      var companyUrl = this.$router.resolve({
        path: `/cong-ty/${company.slug}`,
        query: { company: company },
      });
      window.open(companyUrl.href, "_blank");
    },
    filterCompanies() {
      this.$store.dispatch("company/getList", this.listQuery);
    },
    handleCurrentChange(val) {
      this.listQuery.pageIndex = val;
      this.$store.dispatch("company/getList", this.listQuery);
    },
  },
};
</script>