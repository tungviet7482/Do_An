<template>
  <div class="h-100">
    <div class="row h-100 m-0" style="height: 400px">
      <div class="col-xl-8 col-lg-8">
        <div class="content-left row pt-60 d-flex justify-content-center">
          <div class="form-user-register mb-100">
            <el-form
              ref="company"
              :model="company"
              label-position="top"
              :rules="rules"
              label-width="200px"
              style="width: 600px; margin-left: 50px"
            >
              <el-form-item>
                <h2 class="register-title text-center">
                  Đăng ký tài khoản tuyển dụng
                </h2>
              </el-form-item>
              <el-form-item label="Tên công ty" prop="companyName">
                <el-input
                  v-model="company.companyName"
                  placeholder="nhập tên công ty"
                >
                  <i slot="prefix" class="el-input__icon fas fa-building"></i>
                </el-input>
              </el-form-item>
              <el-form-item label="Link website">
                <el-input
                  v-model="company.companyWebsiteUrl"
                  placeholder="https://www.abc.com/"
                >
                  <i slot="prefix" class="el-input__icon fas fa-link"></i>
                </el-input>
              </el-form-item>
              <el-form-item label="Số nhân sự" prop="companyEmployeeCount">
                <el-input
                  placeholder="ví dụ: 5-10"
                  v-model="company.companyEmployeeCount"
                  style="width: 100%"
                >
                  <i slot="prefix" class="el-input__icon fas fa-users"></i>
                </el-input>
              </el-form-item>
              <el-form-item label="Địa chỉ" prop="companyAddress">
                <el-input
                  v-model="company.companyAddress"
                  placeholder="nhập địa chỉ"
                >
                  <i
                    slot="prefix"
                    class="el-input__icon fas fa-map-marker-alt"
                  ></i>
                </el-input>
              </el-form-item>
              <el-form-item label="Họ và tên đại diện" prop="fullName">
                <el-input
                  v-model="company.fullName"
                  placeholder="nhập họ và tên"
                >
                  <i slot="prefix" class="el-input__icon fas fa-link"></i>
                </el-input>
              </el-form-item>
              <el-form-item label="ngày sinh" prop="dob">
                <el-input
                  type="date"
                  placeholder="nhập ngày sinh"
                  style="width: 100%"
                  v-model="company.dob"
                >
                  <i
                    slot="prefix"
                    class="el-input__icon fas fa-birthday-cake"
                  ></i>
                </el-input>
              </el-form-item>
              <el-form-item label="Email" prop="email">
                <el-input v-model="company.email" placeholder="Nhập email">
                  <i slot="prefix" class="el-input__icon fas fa-envelope"></i>
                </el-input>
              </el-form-item>
              <el-form-item label="Số điện thoại" prop="phoneNumber">
                <el-input
                  v-model="company.phoneNumber"
                  placeholder="nhập số điện thoại"
                >
                  <i slot="prefix" class="el-input__icon fas fa-mobile"></i>
                </el-input>
              </el-form-item>
              <el-form-item label="Mô tả" prop="companyDescription">
                <el-input
                  type="textarea"
                  :rows="3"
                  v-model="company.companyDescription"
                ></el-input>
              </el-form-item>
              <el-form-item label="Logo">
                <el-upload
                  action="https://localhost:7017/api/files"
                  list-type="picture-card"
                  :limit="1"
                  :on-preview="
                    (file) => handlePictureCardPreview(file, 'PreviewImg')
                  "
                  :on-success="
                    (response) => uploadSuccess(response, 'PreviewImg')
                  "
                >
                  <i class="el-icon-plus"></i>
                </el-upload>
                <el-dialog :visible.sync="dialogPreviewImgVisible">
                  <img width="100%" :src="dialogPreviewImagUrl" alt="" />
                </el-dialog>
              </el-form-item>
              <el-form-item label="Ảnh chi tiết">
                <el-upload
                  action="https://localhost:7017/api/files"
                  list-type="picture-card"
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
              <el-form-item label="Ảnh đại diện">
                <el-upload
                  action="https://localhost:7017/api/files"
                  list-type="picture-card"
                  :limit="1"
                  :on-preview="
                    (file) => handlePictureCardPreview(file, 'avatar')
                  "
                  :on-success="(response) => uploadSuccess(response, 'avatar')"
                >
                  <i class="el-icon-plus"></i>
                </el-upload>
                <el-dialog :visible.sync="dialogAvatarVisible">
                  <img width="100%" :src="dialogAvatarUrl" alt="" />
                </el-dialog>
              </el-form-item>
              <el-form-item label="Mật khẩu" prop="password">
                <el-input
                  type="password"
                  v-model="company.password"
                  placeholder="nhập mật khẩu"
                >
                  <i slot="prefix" class="el-input__icon fas fa-key"></i>
                </el-input>
              </el-form-item>
              <el-form-item label="Xác nhận mật khẩu" prop="confirmPassword">
                <el-input type="password" v-model="company.confirmPassword">
                  <i slot="prefix" class="el-input__icon fas fa-key"></i>
                </el-input>
              </el-form-item>
              <el-form-item class="mt-50">
                <button-custom
                  :content="'Đăng ký'"
                  :bgColor="'rgb(221, 63, 36)'"
                  :color="'#fff'"
                  :height="40"
                  :fontSize="30"
                  :style="{ width: '95%' }"
                  @click="register"
                />
              </el-form-item>
            </el-form>
          </div>
        </div>
      </div>
      <div
        class="col-xl-4 col-lg-4 bg-danger m-0"
        :style="{ backgroundImage: 'url(' + img + ')' }"
      ></div>
    </div>
  </div>
</template>
<script>
import Tinymce from "@/components/Tinymce";
import { mapGetters } from "vuex";
import ButtonCustom from "@/components/Button/ButtonCustom.vue";
import { checkEmail, checkPassword, checkConfirmPass } from "@/utils/validate";
import img from "@/assets/img/banner/image.png";
export default {
  name: "InlineEditTable",
  components: {
    Tinymce,
    ButtonCustom,
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
        avatarId: null,
        avatarUrl: null
      },
      dialogPreviewImgVisible: false,
      dialogPreviewImagUrl: null,
      dialogImgVisible: false,
      dialogImagUrl: null,
      dialogAvatarVisible: false,
      dialogAvatarUrl: null,
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
        confirmPassword: [
          {
            required: true,
            validator: (rule, value, callback) =>
              checkConfirmPass(rule, value, callback, this.company.password),
            trigger: "blur",
          },
        ],
      },
      isEditing: false,
      img,
    };
  },
  created() {},
  computed: {
    ...mapGetters({
      getCompanyById: "user/getCompanyById",
    }),
  },
  methods: {
    async register() {
      var isValided = await this.$refs["company"].validate();
      if (isValided) {
        this.$store.dispatch("user/register", this.company);
      }
    },
    uploadSuccess(response, typeImg) {
      if (typeImg === "PreviewImg") {
        this.company.companyPreviewImageId = response.data.id;
        this.company.previewImgUrl = response.data.url;
        this.dialogPreviewImagUrl = response.data.url;
      } else if (typeImg == "avatar") {
        this.company.avatarId = response.data.id;
        this.company.avatarUrl = response.data.url;
        this.dialogAvatarVisible = response.data.url;
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
i {
  color: rgb(221, 63, 36) !important;
}
.register-title {
  font-weight: 700;
  color: rgb(221, 63, 36);
}
.content-left {
  box-sizing: border-box;
  height: 100vh;
  overflow-y: auto;
}
.content-right {
  height: 100vh;
  overflow: hidden;
}
</style>