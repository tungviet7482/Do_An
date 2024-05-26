<template>
  <div class="">
    <div class="row m-0">
      <div class="col-xl-8 col-lg-8">
        <div class="content-left row pt-60 d-flex justify-content-center">
          <div class="form-user-register mb-100">
            <el-form
              ref="account"
              :model="newAccount"
              :rules="rules"
              label-position="top"
              label-width="200px"
              style="width: 600px; margin-left: 50px"
            >
              <el-form-item>
                <h2 class="register-title text-center">
                  Đăng ký tài khoản ứng viên
                </h2>
              </el-form-item>
              <el-form-item label="Họ và tên" prop="fullName">
                <el-input
                  placeholder="Nhập họ và tên"
                  v-model="newAccount.fullName"
                >
                  <i slot="prefix" class="el-input__icon fas fa-user"></i>
                </el-input>
              </el-form-item>
              <el-form-item label="Ngày sinh" prop="dob">
                <el-input
                  type="date"
                  placeholder="Nhập ngày sinh"
                  style="width: 100%"
                  v-model="newAccount.dob"
                >
                  <i
                    slot="prefix"
                    class="el-input__icon fas fa-birthday-cake"
                  ></i>
                </el-input>
              </el-form-item>
              <el-form-item label="Địa chỉ" prop="locationId">
                <el-col :span="10">
                  <el-select
                    v-model="newAccount.locationId"
                    filterable
                    placeholder="Tên địa điểm"
                  >
                    <el-option
                      v-for="item in locations"
                      :key="item.id"
                      :label="item.name"
                      :value="item.id"
                    >
                    </el-option>
                    <i
                      slot="prefix"
                      class="el-input__icon fas fa-map-marker-alt"
                    ></i>
                  </el-select>
                </el-col>
              </el-form-item>
              <el-form-item label="Lĩnh vực" prop="fieldId">
                <el-select
                  v-model="newAccount.fieldId"
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
                  <i slot="prefix" class="el-input__icon fas fa-briefcase"></i>
                </el-select>
              </el-form-item>
              <el-form-item label="Công nghệ sử dụng" prop="technology">
                <el-input
                  placeholder="Nhận ngôn ngữ lập trình, framework, công nghệ, ...."
                  v-model="newAccount.technology"
                >
                  <i
                    slot="prefix"
                    class="el-input__icon fas fa-window-maximize"
                  ></i>
                </el-input>
              </el-form-item>
              <el-form-item label="Email" prop="email">
                <el-input placeholder="Nhập email" v-model="newAccount.email">
                  <i slot="prefix" class="el-input__icon fas fa-envelope"></i>
                </el-input>
              </el-form-item>
              <el-form-item label="Số diện thoại" prop="phoneNumber">
                <el-input
                  placeholder="Nhập số diện thoại"
                  v-model="newAccount.phoneNumber"
                >
                  <i slot="prefix" class="el-input__icon fas fa-mobile"></i>
                </el-input>
              </el-form-item>
              <el-form-item label="Mật khẩu" prop="password">
                <el-input
                  type="password"
                  placeholder="Nhập mật khẩu"
                  v-model="newAccount.password"
                >
                  <i slot="prefix" class="el-input__icon fas fa-key"></i>
                </el-input>
              </el-form-item>
              <el-form-item label="Xác nhận mật khẩu" prop="confirmPassword">
                <el-input type="password" v-model="newAccount.confirmPassword">
                  <i slot="prefix" class="el-input__icon fas fa-key"></i>
                </el-input>
              </el-form-item>

              <el-form-item label="Ảnh đại diện">
                <el-upload
                  action="https://localhost:7017/api/files"
                  list-type="picture-card"
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
              <el-form-item>
                <button-custom
                  :content="'Đăng ký'"
                  :bgColor="'rgb(221, 63, 36)'"
                  :color="'#fff'"
                  :height="40"
                  :fontSize="30"
                  :style="{ width: '100%' }"
                  @click="register"
                />
              </el-form-item>
            </el-form>
          </div>
        </div>
      </div>
      <div class="col-xl-4 col-lg-4 bg-danger m-0" :style="{backgroundImage: 'url(' + img + ')'}">
      </div>
    </div>
  </div>
</template>
<script>
import ButtonCustom from "@/components/Button/ButtonCustom.vue";
import { checkEmail, checkPassword, checkConfirmPass } from "@/utils/validate";
import img from "@/assets/img/banner/image.png"
import { mapGetters } from "vuex";
export default {
  components: {
    ButtonCustom,
  },
  created() {
    this.$store.dispatch("location/getList", { pageIndex: 0 });
    this.$store.dispatch("job-category/getList", { pageIndex: 0 });
  },
  data() {
    return {
      newAccount: {
        fullName: null,
        dob: null,
        email: null,
        phoneNumber: null,
        roles: ["user"],
        password: null,
        confirmPassword: null,
        avatarId: null,
        avatarUrl: null,
        technology: null,
        fieldId: null,
        locationId: null,
      },
      rules: {
        fullName: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        dob: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        fieldId: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        locationId: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        phoneNumber: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        email: [{ required: true, validator: checkEmail, trigger: "blur" }],
        password: [
          { required: true, validator: checkPassword, trigger: "blur" },
        ],
        confirmPassword: [
          {
            required: true,
            validator: (rule, value, callback) =>
              checkConfirmPass(rule, value, callback, this.newAccount.password),
            trigger: "blur",
          },
        ],
      },
      dialogAvatarVisible: false,
      dialogAvatarUrl: null,
      img
    };
  },
  computed: {
    ...mapGetters({
      locations: "location/getLocations",
      categories: "job-category/getCategories",
    }),
  },
  methods: {
    async register() {
      var isValided = await this.$refs["account"].validate();
      if (isValided) {
        var res = await this.$store.dispatch("user/register", this.newAccount);
        if (res.status) {
          //   this.$router.push({ name: this.toBack });
        }
      }
    },
    uploadSuccess(response) {
      this.newAccount.avatarId = response.data.id;
      this.newAccount.avatarUrl = response.data.url;
      this.dialogAvatarUrl = response.data.url;
    },
    handleRemove() {
      this.newAccount.avatarId = null;
    },
    handlePictureCardPreview(file) {
      this.dialogAvatarUrl = file.url;
      this.dialogAvatarVisible = true;
    },
  },
};
</script>
<style scoped>
i {
  color: rgb(221, 63, 36) !important;
}
.row{
  overflow: hidden;
}
.register-title {
  font-weight: 700;
  color: rgb(221, 63, 36);
}
.content-left{
  box-sizing: border-box;
  height: 100vh;
  overflow-y: auto;
}
.content-right{
  height: 100vh;
  overflow: hidden;
}
</style>