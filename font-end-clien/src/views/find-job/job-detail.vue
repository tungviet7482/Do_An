<template>
  <page-404 v-if="isNotFound"/>
  <div  v-else  class="container">
    <slider-page
      :height="250"
      :showCompanyLogo="true"
      :companyName="job.companyName"
      :companyWebsiteUrl="job.companyWebsiteUrl"
      :companyEmployeeCount="job.companyEmployeeCount"
      :urlImg="job.companyImgUrl"
      :logoUrl="job.companyPreviewImgUrl"
      :slug="job.companySlug"
    />
    <div class="job-post-company pt-130">
      <div class="container">
        <div class="row justify-content-between">
          <!-- Left Content -->
          <div class="col-xl-8 col-lg-8 pl-0">
            <!-- job single -->
            <div class="single-job-items bg-white mb-3 px-3">
              <div class="job-items">
                <div class="job-tittle">
                  <a href="#">
                    <h4>{{ job.title }}</h4>
                  </a>
                  <ul>
                    <li>
                      <i class="fas fa-map-marker-alt"></i>
                      {{ job.locationName }}
                    </li>
                    <li v-if="job.minSalary == 0 || job.maxSalary == 0">
                      <i class="far fa-money-bill-alt"></i>Thỏa thuận
                    </li>
                    <li v-else-if="job.minSalary == job.maxSalary">
                      <i class="far fa-money-bill-alt"></i>{{ job.minSalary }}tr
                    </li>
                    <li v-else>
                      <i class="far fa-money-bill-alt"></i>{{ job.minSalary }}tr
                      - {{ job.maxSalary }}tr
                    </li>
                    <li>
                      <i class="fas fa-hourglass"></i
                      >{{ job.workExperience }} năm
                    </li>
                  </ul>
                  <div class="btn-apply d-flex justify-content-center mt-20">
                    <button-custom
                      :content="'Ứng tuyển ngay'"
                      :bgColor="'rgb(221, 63, 36)'"
                      :color="'#fff'"
                      :height="40"
                      :fontSize="30"
                      :style="{ width: '80%' }"
                      @click="dialogApplyVisible = true"
                    />
                  </div>
                </div>
              </div>
            </div>
            <!-- job single End -->

            <div class="job-post-details bg-white rounded px-3 py-3">
              <div class="box-header">
                <h3>Chi tiết tin tuyển dụng</h3>
              </div>
              <div class="box-content w-100 h-100">
                <div class="p-0" v-html="job.body"></div>
              </div>
            </div>
          </div>
          <!-- Right Content -->
          <div class="col-xl-4 col-lg-4 pr-0">
            <div class="post-details3 mb-50 bg-white">
              <!-- Small Section Tittle -->
              <div class="small-section-tittle">
                <h4 class="font-weight-bold d-flex justify-content-center">
                  Thông tin chung
                </h4>
              </div>
              <ul>
                <li>
                  <label>Ngày đăng:</label>
                  <span>{{ formatDateTime(job.startDate) }}</span>
                </li>
                <li>
                  <label>Địa chỉ :</label>
                  <span class="text-justify" >{{ job.address }}</span>
                </li>
                <li>
                  <label>Số lượng :</label>
                  <span>{{ job.quantity }}</span>
                </li>
                <li>
                  <label>Hình thức:</label>
                  <span>{{ mapJobType(job.jobType) }}</span>
                </li>
                <li>
                  <label>Ngày hết hạn:</label>
                  <span>{{ formatDateTime(job.endDate) }}</span>
                </li>
              </ul>
              <div class="apply-btn2 d-flex justify-content-center"></div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <news-area />
    <el-dialog
      :title="'Xác nhận ứng tuyển'"
      :visible.sync="dialogApplyVisible"
      width="600px"
      center
    >
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogApplyVisible = false"> Hủy </el-button>
        <el-button type="danger" @click="handleApply()"> Ứng tuyển </el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import SliderPage from "@/components/SliderArea/SliderPage";
import ButtonCustom from "@/components/Button/ButtonCustom.vue";
import { getToken } from "@/utils/auth";
import NewsArea from "@/components/NewsArea";
import { formatDateTime } from "@/utils/common";
import Page404 from "@/components/error-page/404"

export default {
  components: {
    SliderPage,
    ButtonCustom,
    NewsArea,
    Page404
  },
  data() {
    return {
      job: null,
      dialogApplyVisible: false,
      isNotFound: false,
    };
  },
  created() {
    this.job = this.$route.query.job;
    if (!this.job || !this.job.title) {
      this.$store
        .dispatch("job/getJobBySlug", { slug: this.$route.params.slug })
        .then((data) => {
          if(!data){
            this.isNotFound = true;
          }
          this.job = data;
        });
    }
  },
  computed: {},
  methods: {
    handleApply() {
      if (!getToken()) {
        window.bus.$emit("open-login", true);
        return;
      }
      this.$store.dispatch("job/applyJob", this.job).then(data => {
        if(data.status){
          this.dialogApplyVisible = false;
        }
      });
    },
    mapJobType(jobType) {
      var type = "";
      switch (parseInt(jobType)) {
        case 1:
          type = "Toàn thời gian";
          break;
        case 2:
          type = "Bán thời gian";
          break;
        case 3:
          type = "Thực tập";
          break;
      }
      return type;
    },
    formatDateTime,
  },
};
</script>
<style scoped>
.job-post-details {
  box-shadow: 0px 0px 1px 1px rgba(0, 0, 0, 0.1);
}
.single-job-items {
  padding: 36px 0px;
}
.job-tittle h4 {
  font-size: 25px;
  font-weight: 700;
}

.single-job-items .job-tittle ul li {
  margin-right: 130px;
  font-size: 16px;
  color: #3c3838;
  line-height: 2.9;
}

.single-job-items .job-tittle ul li i {
  width: 30px;
  height: 30px;
  text-align: center;
  line-height: 30px;
  border-radius: 50%;
  background-color: rgb(221, 63, 36);
}

.post-details3 li label {
  width: 120px;
  min-width: 120px;
}

.post-details3 ul {
  padding: 20px 30px;
}

.post-details3 .small-section-tittle {
  width: 100%;
  height: 50px;
  background-color: rgb(221, 63, 36);
  border-top-left-radius: 10px;
  border-top-right-radius: 10px;
  padding: 12px 20px;
}
.post-details3 .small-section-tittle h4 {
  color: #fff;
  line-height: 26px;
  font-size: 18px;
}
.post-details3 ul li span {
  text-align: right;
}
i {
  color: #fff !important;
}
</style>