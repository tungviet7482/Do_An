<template>
  <main class="">
    <div class="slider-area">
      <div class="slider-active">
        <div
          class="single-slider slider-height d-flex align-items-center"
          :style="{ backgroundImage: 'url(' + bgImg + ')' }"
        ></div>
        <div class="container test">
          <div class="row">
            <div class="col-lg-12">
              <form action="#" class="search-box">
                <div class="col-lg-9 input-form">
                  <el-input
                    v-model="keyword"
                    placeholder="Vị trí tuyển dụng"
                    @keyup.enter.native="search()"
                  >
                    <i slot="prefix" class="el-input__icon el-icon-search"></i>
                  </el-input>
                </div>
                <div class="col-lg-3 input-form">
                  <button-custom
                    :content="'Tìm kiếm'"
                    :height="45"
                    :style="{ width: '95%' }"
                    @click="search()"
                  />
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="job-listing-area pt-80 pb-120">
      <div class="container">
        <div class="row">
          <!-- Left content -->
          <div class="col-xl-3 col-lg-3 col-md-4">
            <div class="row">
              <div class="col-12">
                <div class="small-section-tittle2 mb-45">
                  <div class="ion">
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      xmlns:xlink="http://www.w3.org/1999/xlink"
                      width="20px"
                      height="12px"
                    >
                      <path
                        fill-rule="evenodd"
                        fill="rgb(27, 207, 107)"
                        d="M7.778,12.000 L12.222,12.000 L12.222,10.000 L7.778,10.000 L7.778,12.000 ZM-0.000,-0.000 L-0.000,2.000 L20.000,2.000 L20.000,-0.000 L-0.000,-0.000 ZM3.333,7.000 L16.667,7.000 L16.667,5.000 L3.333,5.000 L3.333,7.000 Z"
                      />
                    </svg>
                  </div>
                  <h4>Bộ lọc</h4>
                </div>
              </div>
            </div>
            <div class="job-category-listing mb-50">
              <!-- single one -->
              <div class="single-listing">
                <div class="small-section-tittle2">
                  <h4>Địa điểm</h4>
                </div>
                <!-- Select job items start -->
                <div class="select-job-items2">
                  <el-select
                    v-model="listQuery.locationId"
                    filterable
                    placeholder="Tất cả tỉnh/thành phố"
                  >
                    <el-option
                      v-for="item in locations"
                      :key="item.id"
                      :label="item.name"
                      :value="item.id"
                    />
                  </el-select>
                </div>
                <!--  Select job items End-->
                <!-- select-Categories start -->
                <div class="select-Categories pt-30 pb-20">
                  <div class="small-section-tittle2">
                    <h4>Hình thức công việc</h4>
                  </div>
                  <label class="container"
                    >Tất cả hình thức
                    <input
                      type="radio"
                      name="jobType"
                      v-model="listQuery.jobType"
                      checked
                    />
                    <span class="checkmark"></span>
                  </label>
                  <label class="container"
                    >Toàn thời gian
                    <input
                      type="radio"
                      name="jobType"
                      value="1"
                      v-model="listQuery.jobType"
                    />
                    <span class="checkmark"></span>
                  </label>
                  <label class="container"
                    >Bán thời gian
                    <input
                      type="radio"
                      name="jobType"
                      value="2"
                      v-model="listQuery.jobType"
                    />
                    <span class="checkmark"></span>
                  </label>
                  <label class="container"
                    >Thực tập
                    <input
                      type="radio"
                      name="jobType"
                      value="3"
                      v-model="listQuery.jobType"
                    />
                    <span class="checkmark"></span>
                  </label>
                </div>
                <!-- select-Categories End -->
              </div>
              <!-- single two -->
              <div class="single-listing">
                <div class="small-section-tittle2">
                  <h4>Ngành | Nghề</h4>
                </div>
                <!-- Select job items start -->
                <div class="select-job-items2">
                  <el-select
                    v-model="listQuery.categoryId"
                    filterable
                    placeholder="Tất cả lĩnh vực"
                  >
                    <el-option
                      v-for="item in categories"
                      :key="item.id"
                      :label="item.name"
                      :value="item.id"
                    >
                    </el-option>
                  </el-select>
                </div>
                <!--  Select job items End-->
                <!-- select-Categories start -->
                <div class="select-Categories pt-30 pb-20">
                  <div class="small-section-tittle2">
                    <h4>Kinh nghiệm</h4>
                  </div>
                  <label class="container"
                    >Tất cả kinh nghiệm
                    <input
                      type="radio"
                      name="workExperience"
                      v-model="listQuery.workExperience"
                      checked
                    />
                    <span class="checkmark"></span>
                  </label>
                  <label class="container"
                    >Chưa có kinh nghiệm
                    <input
                      type="radio"
                      value="0"
                      name="workExperience"
                      v-model="listQuery.workExperience"
                    />
                    <span class="checkmark"></span>
                  </label>
                  <label class="container"
                    >dưới 1 năm
                    <input
                      type="radio"
                      name="workExperience"
                      value="1"
                      v-model="listQuery.workExperience"
                    />
                    <span class="checkmark"></span>
                  </label>
                  <label class="container"
                    >1 - 2 năm
                    <input
                      type="radio"
                      name="workExperience"
                      value="2"
                      v-model="listQuery.workExperience"
                    />
                    <span class="checkmark"></span>
                  </label>
                  <label class="container"
                    >2 - 3 năm
                    <input
                      type="radio"
                      name="workExperience"
                      value="3"
                      v-model="listQuery.workExperience"
                    />
                    <span class="checkmark"></span>
                  </label>
                  <label class="container"
                    >3 - 4 năm
                    <input
                      type="radio"
                      name="workExperience"
                      value="4"
                      v-model="listQuery.workExperience"
                    />
                    <span class="checkmark"></span>
                  </label>
                  <label class="container"
                    >4 - 5 năm
                    <input
                      type="radio"
                      name="workExperience"
                      value="5"
                      v-model="listQuery.workExperience"
                    />
                    <span class="checkmark"></span>
                  </label>
                  <label class="container"
                    >trên 5 năm
                    <input
                      type="radio"
                      name="workExperience"
                      value="6"
                      v-model="listQuery.workExperience"
                    />
                    <span class="checkmark"></span>
                  </label>
                </div>
                <!-- select-Categories End -->
              </div>
              <!-- single three -->
              <div class="single-listing">
                <!-- select-Categories start -->
                <div class="select-Categories pb-50">
                  <div class="small-section-tittle2">
                    <h4>Mức lương</h4>
                  </div>
                  <label class="container"
                    >Tất cả mức lương
                    <input
                      type="radio"
                      name="salary"
                      v-model="listQuery.salary"
                      checked
                    />
                    <span class="checkmark"></span>
                  </label>
                  <label class="container"
                    >Dưới 10 triệu
                    <input
                      type="radio"
                      name="salary"
                      value="1"
                      v-model="listQuery.salary"
                    />
                    <span class="checkmark"></span>
                  </label>
                  <label class="container"
                    >10 - 20 triệu
                    <input
                      type="radio"
                      name="salary"
                      value="2"
                      v-model="listQuery.salary"
                    />
                    <span class="checkmark"></span>
                  </label>
                  <label class="container"
                    >20 - 30 triệu
                    <input
                      type="radio"
                      name="salary"
                      value="3"
                      v-model="listQuery.salary"
                    />
                    <span class="checkmark"></span>
                  </label>
                  <label class="container"
                    >30 - 40 triệu
                    <input
                      type="radio"
                      name="salary"
                      value="4"
                      v-model="listQuery.salary"
                    />
                    <span class="checkmark"></span>
                  </label>
                  <label class="container"
                    >40 - 50 triệu
                    <input
                      type="radio"
                      name="salary"
                      value="5"
                      v-model="listQuery.salary"
                    />
                    <span class="checkmark"></span>
                  </label>
                  <label class="container"
                    >Trên 50 triệu
                    <input
                      type="radio"
                      name="salary"
                      value="6"
                      v-model="listQuery.salary"
                    />
                    <span class="checkmark"></span>
                  </label>
                </div>
                <!-- select-Categories End -->
              </div>
            </div>
            <!-- Job Category Listing End -->
          </div>
          <!-- Right content -->
          <div class="col-xl-9 col-lg-9 col-md-8">
            <section class="featured-job-area v-100">
              <div class="container">
                <div class="row">
                  <div class="col-lg-12">
                    <div class="count-job mb-35 d-flex justify-content-left">
                      <span>{{ total }} công việc</span>
                      <!-- <div class="select-job-items">
                        <span>Sort by</span>
                        <select name="select">
                          <option value="">Mặc định</option>
                          <option value="">job list</option>
                          <option value="">job list</option>
                          <option value="">job list</option>
                        </select>
                      </div> -->
                    </div>
                  </div>
                </div>
                <list-job
                  :jobs="getActiveJobs"
                  :total="total"
                  :listQuery="listQuery"
                  @change-page-index="ScrollToTop"
                />
              </div>
            </section>
            <!-- Featured_job_end -->
          </div>
        </div>
      </div>
    </div>
  </main>
</template>

<script>
import SliderPage from "@/components/SliderArea/SliderPage";
import ButtonCustom from "@/components/Button/ButtonCustom.vue";
import bgImg from "@/assets/img/banner/pexels-goumbik-574071.jpg";
import ListJob from "@/components/ListJob.vue";
import { mapGetters } from "vuex";
export default {
  components: {
    SliderPage,
    ListJob,
    ButtonCustom,
  },
  data() {
    return {
      bgImg,
      listLoading: true,
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
      keyword: null,
    };
  },
  created() {
    if (this.$route.query.categoryId) {
      this.listQuery.categoryId = this.$route.query.categoryId;
    }
    if (this.$route.query.listQuery && this.$route.query.listQuery.published) {
      this.keyword = this.$route.query.listQuery.keyword;
      this.listQuery = this.$route.query.listQuery;
    }
    if (!this.jobs.length) {
      this.$store.dispatch("job/getJobs", this.listQuery);
    }
    if (!this.locations.length) {
      this.$store.dispatch("location/getList", this.listQuery);
    }
    if (!this.categories.length) {
      this.$store.dispatch("job-category/getList", this.listQuery);
    }
  },
  computed: {
    ...mapGetters({
      jobs: "job/getActiveJobs",
      total: "job/getTotalActiveJobs",
      locations: "location/getLocations",
      categories: "job-category/getCategories",
    }),
    getActiveJobs() {
      this.listLoading = false;
      return this.jobs;
    },
  },
  methods: {
    search() {
      this.listQuery.keyword = this.keyword;
    },
    ScrollToTop() {
      window.scrollTo({ top: 200, behavior: "smooth" });
    },
  },
  watch: {
    listQuery: {
      deep: true,
      handler(newListQuery) {
        this.$store.dispatch("job/getJobs", newListQuery);
      },
    },
  },
};
</script>
<style scoped>
.slider-height {
  min-height: 300px;
}
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
  background-color: rgba(221, 63, 36, 0.2);
}
.test {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate3d(-50%, -50%, 0);
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
  max-width: 900px;
  margin: auto;
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