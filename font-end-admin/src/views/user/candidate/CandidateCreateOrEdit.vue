<template>
  <div class="app-container">
    <el-row>
      <el-col :span="9">
        <router-link :to="{ name: 'Candidate' }">
          <el-button type="primary">
            <i class="el-icon-back"></i>
          </el-button>
        </router-link>
      </el-col>
      <el-col :span="10">
        <h1 v-if="!isEditing">Thêm ứng viên mới</h1>
        <h1 v-else>Sửa thông tin ứng viên</h1>
      </el-col>
    </el-row>
    <div class="form-create">
      <el-form
        ref="candidate"
        :model="candidate"
        :rules="rules"
        label-position="top"
        label-width="200px"
      >
        <el-form-item label="Họ và tên" prop="fullName">
          <el-input placeholder="Nhập họ và tên" v-model="candidate.fullName">
            <i slot="prefix" class="el-input__icon fas fa-user"></i>
          </el-input>
        </el-form-item>
        <el-form-item label="Ngày sinh" prop="dob">
           <el-date-picker
              type="date"
            placeholder="Nhập ngày sinh"
            style="width: 100%"
            v-model="candidate.dob"
            >
            <i slot="prefix" class="el-input__icon fas fa-birthday-cake"></i>
            </el-date-picker>
        </el-form-item>
         <el-form-item label="Ảnh đại diện">
          <el-upload
            action="https://localhost:7017/api/files"
            list-type="picture-card"
            :file-list="fileListAvatar"
            :limit="1"
            :on-preview="(file) => handlePictureCardPreview(file)"
   
            :on-success="(response) => uploadSuccess(response)"
          >
            <i class="el-icon-plus"></i>
          </el-upload>
          <el-dialog :visible.sync="dialogAvatarVisible">
            <img width="100%" :src="dialogAvatarUrl" alt="" />
          </el-dialog>
        </el-form-item>
         <el-form-item label="Địa chỉ" prop="locationId">
          <el-col :span="10">
            <el-select v-model="candidate.locationId" filterable>
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
         <el-form-item label="Lĩnh vực" prop="fieldId">
          <el-select
            v-model="candidate.fieldId"
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
        <el-form-item label="Công nghệ sử dụng" prop="technology">
          <el-input placeholder="Nhận ngôn ngữ lập trình, framework, công nghệ, ...." v-model="candidate.technology">
            <i slot="prefix" class="el-input__icon fas fa-user"></i>
          </el-input>
        </el-form-item>
        <el-form-item label="Email" prop="email">
          <el-input placeholder="Nhập email" v-model="candidate.email">
            <i slot="prefix" class="el-input__icon fas fa-envelope"></i>
          </el-input>
        </el-form-item>
        <el-form-item label="Số diện thoại" prop="phoneNumber">
          <el-input
            placeholder="Nhập số diện thoại"
            v-model="candidate.phoneNumber"
          >
            <i slot="prefix" class="el-input__icon fas fa-mobile"></i>
          </el-input>
        </el-form-item>
         <el-form-item>
          <el-button v-if="!isEditing" type="primary" @click="create()"
            >Tạo</el-button
          >
          <el-button v-if="isEditing" type="primary" @click="update()"
            >Lưu</el-button
          >
          <router-link :to="{ name: 'Company' }">
            <el-button>Hủy</el-button>
          </router-link>
        </el-form-item>
        </el-form-item>
      </el-form>
    </div>
  </div>
</template>

<script>
import Pagination from "@/components/Pagination";
import Tinymce from "@/components/Tinymce";
import { mapGetters } from "vuex";
import { checkEmail } from "@/utils/validate";

export default {
  name: "InlineEditTable",
  components: {
    Pagination,
    Tinymce,
  },
  data() {
    return {
      candidate: {
        fullName: null,
        dob: null,
        email: null,
        phoneNumber: null,
        roles: ["user"],
        avatarId: null,
        avatarUrl: null,
        technology: null,
        fieldId: null,
        locationId: null,
      },
      dialogAvatarVisible: false,
      dialogAvatarUrl: null,
      rules: {
        fullName: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        fieldId: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        locationId: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        dob: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        phoneNumber: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        email: [{ required: true, validate: checkEmail, trigger: "blur" }],
      },
      fileListAvatar: [
        {
          url: null,
        },
      ],
      isEditing: false,
    };
  },
  created() {
    this.$store.dispatch("location/getList", { pageIndex: 0 });
    this.$store.dispatch("job-category/getList", { pageIndex: 0 });
    this.initialData();
  },
  computed: {
    ...mapGetters({
      getCandidateById: "user/getCandidateById",
      locations: "location/getLocations",
      categories: "job-category/getCategories",
    }),
  },
  methods: {
    initialData() {
      const id = this.$route.params.id;
      let candidate = this.getCandidateById(id);
      if (candidate) {
        this.candidate = candidate;
        this.fileListAvatar[0].url = candidate.avatarUrl;
        this.isEditing = true;
      } else {
        this.fileListAvatar = null;
      }
    },
    async create() {
      var isValided = await this.$refs["candidate"].validate();
      if (isValided) {
        var res = await this.$store.dispatch("user/create", this.candidate);
        if (res.status) {
          this.$router.push("/account/candidate");
        }
      }
    },
    async update() {
      var isValided = await this.$refs["candidate"].validate();
      var res;
      if (isValided) {
        res = await this.$store.dispatch("user/update", this.candidate);
        if (res.status) {
          this.$router.push("/account/candidate");
        }
      }
    },
    uploadSuccess(response) {
      this.candidate.avatarId = response.data.id;
      this.candidate.avatarUrl = response.data.url;
      this.dialogAvatarUrl = response.data.url;
    },
    handleRemove() {
      this.candidate.avatarId = null;
    },
    handlePictureCardPreview(file) {
      this.dialogAvatarUrl = file.url;
      this.dialogAvatarVisible = true;
    },
  },
};
</script>

<style scoped>
.app-container {
  /* display: flex;
  flex-direction: column; */
}
.el-row {
  display: flex;
  align-items: center;
}
.form-create {
  margin: 0 300px;
  /* height: 100%;
  overflow-y: auto; */
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
