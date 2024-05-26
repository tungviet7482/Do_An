<template>
  <div class="slider-area">
    <!-- Mobile Menu -->
    <div class="slider-active">
      <div
        class="single-slider slider-height d-flex align-items-center"
        :style="{ backgroundImage: 'url(' + img + ')' }"
      ></div>

      <div class="container test">
        <div class="row">
          <div class="col-12">
            <div class="hero__caption">
              <h1>Tìm việc làm nhanh 24h, việc làm mới nhất.</h1>
            </div>
          </div>
        </div>
        <!-- Search Box -->
        <div class="row">
          <div class="col-lg-12">
            <!-- form -->
            <form action="#" class="search-box">
              <div class="col-lg-4 input-form">
                <el-input
                  v-model="listQuery.keyword"
                  placeholder="Vị trí tuyển dụng"
                >
                  <i slot="prefix" class="el-input__icon el-icon-search"></i>
                </el-input>
              </div>
              <div class="col-lg-2 input-form">
                <el-select
                  v-model="listQuery.locationId"
                  filterable
                  placeholder="Tất cả địa điểm"
                  size="large"
                >
                  <el-option
                    v-for="item in locations"
                    :key="item.id"
                    :label="item.name"
                    :value="item.id"
                  />
                  <i slot="prefix" class="el-input__icon el-icon-location"></i>
                </el-select>
              </div>
              <div class="col-lg-2 input-form">
                <el-select
                  v-model="listQuery.categoryId"
                  placeholder="Tất cả lĩnh vực"
                  size="large"
                >
                  <el-option
                    v-for="item in categories"
                    :key="item.id"
                    :label="item.name"
                    :value="item.id"
                  />
                  <i slot="prefix" class="el-input__icon el-icon-star-on"></i>
                </el-select>
              </div>
              <div class="col-lg-2 input-form">
                <el-select
                  v-model="listQuery.salary"
                  placeholder="Tất cả mức lương"
                  size="large"
                >
                  <el-option :key="1" :label="'Dưới 10 triệu'" :value="1" />
                  <el-option :key="2" :label="'10 - 20 triệu'" :value="2" />
                  <el-option :key="3" :label="'20 - 30 triệu'" :value="3" />
                  <el-option :key="4" :label="'30 - 40 triệu'" :value="4" />
                  <el-option :key="5" :label="'40 - 50 triệu'" :value="5" />
                  <el-option :key="6" :label="'Trên 50 triệu'" :value="6" />
                  <i slot="prefix" class="el-input__icon el-icon-money"></i>
                </el-select>
              </div>
              <div class="col-lg-2 input-form">
                <button-custom
                  :content="'Tìm kiếm'"
                  :height="45"
                  :style="{ width: '95%' }"
                  @click="handleSearch()"
                />
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import ButtonCustom from "@/components/Button/ButtonCustom.vue";
import img from "@/assets/img/banner/pexels-goumbik-590016.jpg";
export default {
  components: {
    ButtonCustom,
  },
  data() {
    return {
      img,
      listQuery: {
        pageIndex: 1,
        pageSize: 10,
        keyword: null,
        published: true,
        locationId: null,
        categoryId: null,
        experience: null,
        salary: null,
        jobType: null,
        workExperience: null,
      },
    };
  },
  created() {
    if (!this.locations.length) {
      this.$store.dispatch("location/getList", this.listQuery);
    }
  },
  computed: {
    ...mapGetters({
      locations: "location/getLocations",
      categories: "job-category/getCategories",
    }),
  },
  mounted() {
    var nice_Select = $("select");
    if (nice_Select.length) {
      nice_Select.niceSelect();
    }
  },
  methods: {
    handleSearch(){
      this.$router.push({name: 'FindJob', query: {listQuery: this.listQuery}})
    }
  },
};
</script>
<style scoped>
.slider-active {
  position: relative;
  z-index: 2;
}
.slider-active::before {
  content: "";
  position: absolute;
  top: 0;
  right: 0;
  left: 0;
  bottom: 0;
  z-index: 10;
  background: linear-gradient(
    45deg,
    rgba(162, 162, 162, 0.5),
    rgba(0, 0, 0, 0.6)
  );
}
.test {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate3d(-50%, -80%, 0);
  z-index: 30;
}
.hero__caption h1 {
  text-align: center;
  font-size: 50px;
  margin-bottom: 50px;
  color: white;
  font-weight: 600;
}
.search-box {
  background-color: rgba(255, 255, 255, 0.2);
  padding: 10px 0;
}
.col-lg-4,
.col-lg-2 {
  padding-left: 8px !important;
  padding-right: 8px !important;
}
.el-input__icon {
  line-height: 45px !important;
  font-size: 20px !important;
}
.el-select {
  width: 100%;
}
</style>
