<template>
  <div class="app-container">
    <el-row>
      <el-col :span="8">
        <router-link :to="{ name: toBack }">
          <el-button type="primary">
            <i class="el-icon-back"></i>
          </el-button>
        </router-link>
      </el-col>
      <el-col :span="10">
        <h1 v-if="!isEditing">Đăng tuyển công việc mới</h1>
        <h1 v-else>Sửa thông tin công việc</h1>
      </el-col>
    </el-row>
    <div class="form-create">
      <el-form
        ref="job"
        :model="job"
        label-width="180px"
        label-position="left"
        :rules="rules"
      >
        <el-form-item label="Công ty" prop="userId">
          <el-select v-if="!isEditing" v-model="job.userId" filterable placeholder="Tên công ty">
            <el-option
              v-for="item in getCompaniesInfo"
              :key="item.userId"
              :label="item.companyName"
              :value="item.userId"
            >
            </el-option>
          </el-select>
          <span>{{ job.companyName }}</span>
        </el-form-item>
        <el-form-item label="Tiêu đề" prop="title">
          <el-input v-model="job.title"></el-input>
        </el-form-item>
        <el-form-item label="Mô tả ngắn" prop="shortDescription">
          <el-input v-model="job.shortDescription"></el-input>
        </el-form-item>
        <el-form-item label="Lĩnh vực" prop="userId">
          <el-select
            v-model="job.categoryId"
            filterable
            placeholder="Tên lĩnh vực"
          >
            <el-option
              v-for="item in categories"
              :key="item.id"
              :label="item.name"
              :value="item.id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="Hình thức công việc">
          <el-select
            v-model="job.jobType"
          >
            <el-option
              :label="'Toàn thời gian'"
              :key="1"
              :value="1"
            >
            </el-option>
            <el-option
              :label="'Bán thời gian'"
              :key="2"
              :value="2"
            >
            </el-option>
            <el-option
              :label="'Thực tập'"
              :key="3"
              :value="3"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="Số nhân sự" prop="quantity">
          <el-col :span="10">
            <el-input
              type="number"
              placeholder="ví dụ: 5"
              v-model="job.quantity"
              style="width: 100%"
            ></el-input>
          </el-col>
        </el-form-item>
        <el-form-item label="Địa chỉ" prop="locationId">
          <el-col :span="10">
            <el-select v-model="job.locationId" filterable>
              <el-option
                v-for="item in locations"
                :key="item.id"
                :label="item.name"
                :value="item.id"
              >
              </el-option>
            </el-select>
          </el-col>
        </el-form-item>

        <el-form-item label="Địa chỉ chỉ tiết" prop="address">
          <el-col :span="10">
            <el-input v-model="job.address" style="width: 100%"></el-input>
          </el-col>
        </el-form-item>
        <el-form-item label="Ngày hết hạn" prop="endDate">
          <el-col :span="5">
            <el-date-picker
              type="date"
              v-model="job.endDate"
              style="width: 100%"
            ></el-date-picker>
          </el-col>
        </el-form-item>
        <el-form-item label="Số năm kình nghiệm" prop="workExperience">
          <el-select
            v-model="job.workExperience"
            placeholder="Tất cả kinh nghiệm"
          >
            <el-option label="Chưa có kinh nghiệm" value="0"></el-option>
            <el-option label="1 - 2 năm" value="1"></el-option>
            <el-option label="2 - 3 năm" value="2"></el-option>
            <el-option label="3 - 4 năm" value="3"></el-option>
            <el-option label="trên 5 năm" value="4"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="Khoảng lương">
          <el-col :span="5">
            <el-form-item prop="minSalary">
              <el-input
                type="number"
                placeholder="từ"
                v-model="job.minSalary"
                style="width: 100%"
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col class="line" :span="2">-</el-col>
          <el-col :span="5">
            <el-form-item prop="maxSalary">
              <el-input
                type="number"
                placeholder="đến"
                v-model="job.maxSalary"
                style="width: 100%"
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col class="line" :span="3">triệu đồng</el-col>
        </el-form-item>
        <el-form-item label="Hiển thị">
          <el-switch v-model="job.published"></el-switch>
        </el-form-item>
        <div>
          <el-form-item label="Nội dung công việc" label-position="top">
          </el-form-item>
          <el-form-item prop="body">
            <tinymce v-model="job.body" :height="400" />
          </el-form-item>
        </div>

        <el-form-item>
          <el-button v-if="!isEditing" type="primary" @click="create()"
            >Tạo</el-button
          >
          <el-button v-if="isEditing" type="primary" @click="update()"
            >Lưu</el-button
          >
          <router-link :to="{ name: toBack }">
            <el-button>Hủy</el-button>
          </router-link>
        </el-form-item>
      </el-form>
    </div>
  </div>
</template>

<script>
import Tinymce from "@/components/Tinymce";
import { mapGetters } from "vuex";

export default {
  name: "InlineEditTable",
  components: {
    Tinymce,
  },
  data() {
    return {
      job: {
        id: null,
        title: null,
        shortDescription: null,
        body: null,
        quantity: null,
        minSalary: null,
        maxSalary: null,
        workExperience: null,
        address: null,
        endDate: null,
        imageId: null,
        previewImageId: null,
        published: null,
        userId: null,
        categoryId: null,
        locationId: null,
        jobType: null
      },
      listQuery: {
        page: 0,
        limit: 0,
        keyword: null,
      },
      dialogPreviewImgVisible: false,
      dialogPreviewImagUrl: null,
      dialogImgVisible: false,
      dialogImagUrl: null,
      rules: {
        title: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        shortDescription: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        body: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        quantity: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        minSalary: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        maxSalary: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        workExperience: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        address: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        endDate: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        userId: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        locationId: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
      },
      fileListPreImg: [
        {
          url: null,
        },
      ],
      fileListImg: [
        {
          url: null,
        },
      ],
      isEditing: false,
    };
  },
  created() {
    this.$store.dispatch("user/getCompanies", this.listQuery);
    this.$store.dispatch("location/getList", this.listQuery);
    this.$store.dispatch("job-category/getList", this.listQuery);
    this.initialData();
  },
  computed: {
    ...mapGetters({
      companies: "user/getCompanies",
      getJobById: "job/getJobById",
      locations: "location/getLocations",
      categories: "job-category/getCategories",
    }),
    getCompaniesInfo() {
      return this.companies.map((job) => ({
        companyName: job.companyName,
        userId: job.id,
      }));
    },
    toBack(){
      return this.$route.query.back;
    }
  },
  methods: {
    initialData() {
      const id = this.$route.params.id;
      let job = this.getJobById(id);
      if (job) {
        this.job = job;
        this.fileListPreImg[0].url = job.previewImgUrl;
        this.fileListImg[0].url = job.imgUrl;
        this.isEditing = true;
      } else {
        this.fileListPreImg = null;
        this.fileListImg = null;
      }
    },
    async create() {
      var isValided = await this.$refs["job"].validate();
      if (isValided) {
        var res = await this.$store.dispatch("job/create", this.job);
        if (res.status) {
          this.$router.push({name: this.toBack});
        }
      }
    },
    async update() {
      var isValided = await this.$refs["job"].validate();
      var res;
      if (isValided) {
        res = await this.$store.dispatch("job/update", this.job);
        if (res.status) {
          this.$router.push({name: this.toBack});
        }
      }
    },
  }
};
</script>

<style scoped>
.el-row {
  display: flex;
  align-items: center;
}
.form-create {
  margin: 0 100px;
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
</style>
