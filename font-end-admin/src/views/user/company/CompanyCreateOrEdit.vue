<template>
  <div class="app-container">
    <el-row>
      <el-col :span="9">
        <router-link :to="{ name: 'Company' }">
          <el-button type="primary">
            <i class="el-icon-back"></i>
          </el-button>
        </router-link>
      </el-col>
      <el-col :span="10">
        <h1 v-if="!isEditing">Thêm công ty mới</h1>
        <h1 v-else>Sửa thông tin công ty</h1>
      </el-col>
    </el-row>
    <div class="form-create">
      <el-form
        ref="company"
        :model="company"
        label-width="150px"
        label-position="left"
        :rules="rules"
      >
        <el-form-item label="Tên công ty" prop="companyName">
          <el-input v-model="company.companyName"></el-input>
        </el-form-item>
        <el-form-item label="Mô tả" prop="companyDescription">
          <el-input
            type="textarea"
            :rows="3"
            v-model="company.companyDescription"
          ></el-input>
        </el-form-item>
        <el-form-item label="Link website">
          <el-col :span="10">
            <el-input
              v-model="company.companyWebsiteUrl"
              placeholder="https://www.abc.com/"
            ></el-input>
          </el-col>
        </el-form-item>
        <el-form-item label="Số nhân sự" prop="companyEmployeeCount">
          <el-col :span="10">
            <el-input
              placeholder="ví dụ: 5-10"
              v-model="company.companyEmployeeCount"
              style="width: 100%"
            ></el-input>
          </el-col>
        </el-form-item>
        <el-form-item label="Địa chỉ" prop="companyAddress">
          <el-col :span="10">
            <el-input v-model="company.companyAddress"></el-input>
          </el-col>
        </el-form-item>
        <el-form-item label="Ảnh xem trước">
          <el-upload
            action="https://localhost:7017/api/files"
            list-type="picture-card"
            :file-list="fileListPreImg"
            :limit="1"
            :on-preview="(file) => handlePictureCardPreview(file, 'PreviewImg')"

            :on-success="(response) => uploadSuccess(response, 'PreviewImg')"
          >
            <i class="el-icon-plus"></i>
          </el-upload>
          <el-dialog :visible.sync="dialogPreviewImgVisible">
            <img width="100%" :src="dialogPreviewImagUrl" alt="" />
          </el-dialog>
        </el-form-item>
        <el-form-item label="Ảnh xem chi tiết">
          <el-upload
            action="https://localhost:7017/api/files"
            list-type="picture-card"
            :file-list="fileListImg"
            :limit="1"
            :on-preview="(file) => handlePictureCardPreview(file, 'Img')"
   
            :on-success="(response) => uploadSuccess(response, 'Img')"
          >
            <i class="el-icon-plus"></i>
          </el-upload>
          <el-dialog :visible.sync="dialogImgVisible">
            <img width="100%" :src="dialogImagUrl" alt="" />
          </el-dialog>
        </el-form-item>
        <el-form-item label="Họ và tên đại diện" prop="fullName">
          <el-col :span="8">
            <el-input
              v-model="company.fullName"
              placeholder="Nhập họ và tên"
            ></el-input>
          </el-col>
        </el-form-item>
        <el-form-item label="Ngày sinh" prop="dob">
          <el-col :span="8">
            <el-date-picker
              type="date"
              placeholder="Nhập ngày sinh"
              v-model="company.dob"
              style="width: 100%"
            ></el-date-picker>
          </el-col>
        </el-form-item>
        <el-form-item label="Email" prop="email">
          <el-col :span="8">
            <el-input v-model="company.email" placeholder="abc"></el-input>
          </el-col>
        </el-form-item>
        <el-form-item label="Số điện thoại" prop="phoneNumber">
          <el-col :span="8">
            <el-input
              v-model="company.phoneNumber"
              placeholder="abc"
            ></el-input>
          </el-col>
        </el-form-item>
        <el-form-item v-if="!isEditing" label="Mật khẩu" prop="password">
          <el-col :span="8">
            <el-input
              type="password"
              v-model="company.password"
              placeholder="abc"
            ></el-input>
          </el-col>
        </el-form-item>
        <el-form-item v-if="!isEditing" label="Xác nhận mật khẩu">
          <el-col :span="8">
            <el-input
              type="password"
              v-model="company.confirmPassword"
              placeholder="abc"
            ></el-input>
          </el-col>
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
      </el-form>
    </div>
  </div>
</template>

<script>
import Pagination from "@/components/Pagination";
import Tinymce from "@/components/Tinymce";
import { mapGetters } from "vuex";
import { checkEmail, checkPassword } from "@/utils/validate";

export default {
  name: "InlineEditTable",
  components: {
    Pagination,
    Tinymce,
  },
  data() {
    return {
      company: {
        companyName: null,
        companyDescription: null,
        companyEmployeeCount: null,
        companyWebsiteUrl: null,
        companyAddress: null,
        companyImageId: null,
        companyPreviewImageId: null,
        fullName: null,
        dob: null,
        email: null,
        phoneNumber: null,
        password: null,
        confirmPassword: null,
        roles: ["company"],
        previewImgUrl: null,
        imgUrl: null,
      },
      dialogPreviewImgVisible: false,
      dialogPreviewImagUrl: null,
      dialogImgVisible: false,
      dialogImagUrl: null,
      rules: {
        companyName: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        companyDescription: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        companyEmployeeCount: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        companyAddress: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        fullName: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        dob: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        phoneNumber: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        email: [{ validator: checkEmail, trigger: "blur" }],
        password: [{ validator: checkPassword, trigger: "blur" }],
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
    this.initialData();
  },
  computed: {
    ...mapGetters({
      getCompanyById: "user/getCompanyById",
    }),
  },
  methods: {
    initialData() {
      const id = this.$route.params.id;
      let user = this.getCompanyById(id);
      if (user) {
        this.company = user;
        this.fileListPreImg[0].url = user.previewImgUrl;
        this.fileListImg[0].url = user.imgUrl;
        this.isEditing = true;
      } else {
        this.fileListPreImg = null;
        this.fileListImg = null;
      }
    },
    async create() {
      var isValided = await this.$refs["company"].validate();
      if (isValided) {
        var res = await this.$store.dispatch(
          "user/create",
          this.company
        );
        if (res.status) {
          this.$router.push("/account/company");
        }
      }
    },
    async update() {
      var isValided = await this.$refs["company"].validate();
      var res;
      if (isValided) {
        console.log(this.company.companyImageId)
        res = await this.$store.dispatch("user/update", this.company);
        if (res.status) {
          this.$router.push("/account/company");
        }
      }
    },
    uploadSuccess(response, typeImg) {
      if (typeImg === "PreviewImg") {
        this.company.companyPreviewImageId = response.data.id;
        this.company.previewImgUrl = response.data.url;
        this.dialogPreviewImagUrl = response.data.url;
      } else {
        this.company.companyImageId = response.data.id;
        this.company.imgUrl = response.data.url;
        this.dialogImagUrl = response.data.url;
      }
    },
    handleRemove(typeImg) {
      if (typeImg == "Img") {
        this.company.companyImageId = null;
      } else if (typeImg == "PreviewImg") {
        this.company.companyPreviewImageId = null;
      }
        console.log("a;dfkja;skf;dkg")
    },
    handlePictureCardPreview(file, typeImg) {
      if (typeImg == "Img") {
        this.dialogPreviewImagUrl = file.url;
        this.dialogPreviewImgVisible = true;
      } else {
        this.dialogImagUrl = file.url;
        this.dialogImgVisible = true;
      }
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
  margin: 0 100px;
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
