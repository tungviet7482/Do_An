<template>
  <page-404 v-if="isNotFound" />
  <div v-else class="container">
    <slider-page
      :height="250"
      :showCompanyLogo="true"
      :companyName="company.name"
      :companyWebsiteUrl="company.websiteUrl"
      :companyEmployeeCount="company.employeeCount"
      :urlImg="company.imgUrl"
      :logoUrl="company.previewImgUrl"
    />
    <div class="container pt-130">
      <div class="row">
        <div class="col-xl-9 col-lg-9">
          <el-tabs class="review" v-model="activeName" @tab-click="handleClick">
            <el-tab-pane label="Giới thiệu" name="first">
              <div class="re-box row">
                <div class="box-header">
                  <h3>Giới thiệu công ty</h3>
                </div>
                <div class="box-content v-100 h-100">
                  <p>{{ company.description }}</p>
                </div>
              </div>
              <div class="re-box row">
                <div class="box-header">
                  <h3>Tuyển dụng</h3>
                </div>
                <div class="box-content w-100 h-100">
                  <list-job
                    v-if="jobs.length"
                    :jobs="jobs"
                    :total="total"
                    :listQuery="listQuery"
                    :companyId="company.id"
                  />
                  <p v-else>Không có tin nào</p>
                </div>
              </div>
            </el-tab-pane>
            <el-tab-pane label="Đánh giá" name="second">
              <div class="re-box row">
                <div class="box-header">
                  <h3>Đánh giá chung</h3>
                </div>
                <div class="box-content w-100">
                  <div class="d-flex justify-content-between">
                    <div
                      class="rate-common d-flex flex-column justify-content-center align-items-center"
                    >
                      <h4 class="font-weight-bold">
                        {{ avgStars.toFixed(1) }}
                      </h4>
                      <el-rate v-model="avgStars" disabled text-color="#ff9900">
                      </el-rate>
                      <h5 class="mt-20">{{ totalReviews }} đánh giá</h5>
                    </div>
                    <div style="width: 70%">
                      <div
                        class="d-flex justify-content-between align-items-center"
                      >
                        <div style="width: 60px">
                          5 <i class="el-icon-star-on"></i>
                        </div>
                        <div style="width: 100%">
                          <el-progress
                            :percentage="
                              calculatePercentage(starRating[4]) || 0
                            "
                            :color="'rgb(247, 186, 42)'"
                          ></el-progress>
                        </div>
                      </div>
                      <div
                        class="d-flex justify-content-between align-items-center"
                      >
                        <div style="width: 60px">
                          4 <i class="el-icon-star-on"></i>
                        </div>
                        <div style="width: 100%">
                          <el-progress
                            :percentage="
                              calculatePercentage(starRating[3]) || 0
                            "
                            :color="'rgb(247, 186, 42)'"
                          ></el-progress>
                        </div>
                      </div>
                      <div
                        class="d-flex justify-content-between align-items-center"
                      >
                        <div style="width: 60px">
                          3 <i class="el-icon-star-on"></i>
                        </div>
                        <div style="width: 100%">
                          <el-progress
                            :percentage="
                              calculatePercentage(starRating[2]) || 0
                            "
                            :color="'rgb(247, 186, 42)'"
                          ></el-progress>
                        </div>
                      </div>
                      <div
                        class="d-flex justify-content-between align-items-center"
                      >
                        <div style="width: 60px">
                          2 <i class="el-icon-star-on"></i>
                        </div>
                        <div style="width: 100%">
                          <el-progress
                            :percentage="
                              calculatePercentage(starRating[1]) || 0
                            "
                            :color="'rgb(247, 186, 42)'"
                          ></el-progress>
                        </div>
                      </div>
                      <div
                        class="d-flex justify-content-between align-items-center"
                      >
                        <div style="width: 60px">
                          1 <i class="el-icon-star-on"></i>
                        </div>
                        <div style="width: 100%">
                          <el-progress
                            :percentage="
                              calculatePercentage(starRating[0]) || 0
                            "
                            :color="'rgb(247, 186, 42)'"  
                          ></el-progress>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="btn-apply d-flex justify-content-center mt-20">
                    <button-custom
                      :content="'Viết đánh giá'"
                      :bgColor="'rgb(221, 63, 36)'"
                      :color="'#fff'"
                      :height="40"
                      :fontSize="30"
                      :style="{ width: '200px' }"
                      @click="handleWrite"
                    />
                  </div>
                </div>
              </div>
              <div class="re-box row">
                <div class="box-header">
                  <h3>Đánh giá</h3>
                </div>
                <div class="box-content w-100 h-100">
                  <review-item v-for="review in reviews" :review="review" />
                  <div class="pagination-custom d-flex justify-content-center">
                    <el-pagination
                      :page-size="listQuery.pageSize"
                      background
                      layout="prev, pager, next"
                      :total="totalReviews"
                      :hide-on-single-page="totalReviews <= listQuery.pageSize"
                      @current-change="handleCurrentChange"
                    >
                    </el-pagination>
                  </div>
                </div>
              </div>
            </el-tab-pane>
          </el-tabs>
        </div>
        <div class="col-xl-3 col-lg-3 pr-0">
          <div class="re-box row">
            <div class="box-header">
              <h3>Thông tin liên hệ</h3>
            </div>
            <div class="box-content w-100 h-100">
              <ul>
                <li>
                  <i class="fas fa-envelope"></i
                  ><span>{{ company.email }}</span>
                </li>
                <li>
                  <i class="fas fa-phone"></i
                  ><span>{{ company.phoneNumber }}</span>
                </li>
                <li>
                  <div>
                    <i class="fas fa-map-marker-alt"></i>
                    <label>Địa chỉ công ty</label>
                  </div>
                  <p>{{ company.address }}</p>
                </li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>
    <news-area />
    <el-dialog
      title="Viết đánh giá"
      width="60%"
      :visible.sync="reviewDialogVisible"
      center
    >
      <el-form
        label-position="left"
        :rules="rules"
        :model="newReview"
        ref="review"
        status-icon
        label-width="200px"
      >
        <el-form-item label="Đánh giá chung" prop="rate">
          <div class="review-form d-flex align-items-center">
            <el-rate
              v-model="newReview.rate"
              :texts="[
                'Rất tệ',
                'Cần cải thiện nhiều',
                'Tốt',
                'Rất tốt',
                'Tuyệt vời',
              ]"
              show-text
            >
            </el-rate>
          </div>
        </el-form-item>
        <el-form-item label="Tiêu đề" prop="title">
          <el-input v-model="newReview.title"></el-input>
        </el-form-item>
        <el-form-item label="Điều bạn thích" prop="advantages">
          <el-input
            type="textarea"
            :rows="5"
            v-model="newReview.advantages"
          ></el-input>
        </el-form-item>
        <el-form-item label="Đề nghị cải thiện" prop="disadvantages">
          <el-input
            type="textarea"
            :rows="5"
            v-model="newReview.disadvantages"
          ></el-input>
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <Button :content="'Hủy'" @click="reviewDialogVisible = false" />
        <button-custom
          :content="'Gửi'"
          :bgColor="'rgb(221, 63, 36)'"
          :color="'#fff'"
          @click="createReview()"
        />
      </span>
    </el-dialog>
  </div>
</template>

<script>
import SliderPage from "@/components/SliderArea/SliderPage";
import ButtonCustom from "@/components/Button/ButtonCustom.vue";
import ListJob from "@/components/ListJob.vue";
import NewsArea from "@/components/NewsArea";
import ReviewItem from "@/components/ReviewItem.vue";
import Page404 from "@/components/error-page/404";
import Button from "@/components/Button/Button.vue";
import { mapGetters } from "vuex";

export default {
  components: {
    SliderPage,
    ButtonCustom,
    ListJob,
    NewsArea,
    Page404,
    ReviewItem,
    Button,
  },
  data() {
    return {
      listLoading: true,
      listQuery: {
        pageIndex: 1,
        pageSize: 10,
        published: true,
        companyId: null,
      },
      newReview: {
        userId: null,
        companyId: null,
        rate: null,
        title: null,
        advantages: null,
        disadvantages: null,
      },
      rules: {
        title: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        advantages: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        disadvantages: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        rate: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
      },
      company: null,
      isNotFound: false,
      activeName: "first",
      reviewDialogVisible: false,
    };
  },
  created() {
    this.company = this.$route.query.company;
    if (!this.company || !this.company.name) {
      this.$store
        .dispatch("company/getCompanyBySlug", this.$route.params.slug)
        .then((data) => {
          if (!data) {
            this.isNotFound = true;
          }
          this.company = data;
          this.listQuery.companyId = data.id;
          this.getJobs();
        });
    } else this.getJobs();
  },
  computed: {
    ...mapGetters({
      jobs: "job/getJobsInCompany",
      total: "job/getTotalJobsInCompany",
      reviews: "review/getReviews",
      starRating: "review/getStarRating",
      totalReviews: "review/getTotal",
      avgStars: "review/getAvgStars",
      role: "role",
    }),
  },
  methods: {
    getJobs() {
      this.listQuery.companyId = this.company.id;
      this.$store.dispatch("job/getJobs", this.listQuery);
    },
    async createReview() {
      var isValided = await this.$refs["review"].validate();
      if (isValided) {
        this.newReview.companyId = this.company.id;
        await this.$store
          .dispatch("review/create", this.newReview)
          .then((data) => {
            if (data.status) {
              this.reviewDialogVisible = false;
              this.newReview.rate = null;
              this.newReview.title = null;
              this.newReview.advantages = null;
              this.newReview.disadvantages = null;
            }
          });
      }
    },
    handleClick(tab) {
      if (tab.index == 1) {
        this.listQuery.companyId = this.company.id;
        this.$store
          .dispatch("review/getList", this.listQuery)
          .then((data) => {});
      }
    },
    handleWrite() {
      if (this.role != "user") {
        window.bus.$emit("open-login", true);
        return;
      }
      this.reviewDialogVisible = true;
    },
    calculatePercentage(numStars) {
      return (numStars * 100) / this.totalReviews;
    },
    handleCurrentChange(val) {
      this.listQuery.pageIndex = val;
      this.$store.dispatch("review/getList", this.listQuery);
    },
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
.review i {
  font-size: 18px;
  color: rgb(247, 186, 42);
}
</style>