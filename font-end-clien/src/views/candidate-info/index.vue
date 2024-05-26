<template>
  <div class="container">
    <slider-page :title="'Thông tin ứng viên'" :urlImg="imgBg" :height="200"></slider-page>
    <div class="row d-flex justify-content-center mt-60 mb-80">
      <div class="form-create col-lg-8">
        <el-form
          ref="account"
          :model="account"
          :rules="rules"
          label-position="top"
          label-width="300px"
        >
          <el-form-item label="Họ và tên" prop="fullName">
            <el-input
              placeholder="Họ và tên"
              v-model="account.fullName"
            >
              <i slot="prefix" class="el-input__icon fas fa-user"></i>
            </el-input>
          </el-form-item>
          <el-form-item label="Ngày sinh" prop="dob">
            <el-date-picker
              placeholder="Nhập ngày sinh"
              style="width: 100%"
              v-model="account.dob"
            >
            </el-date-picker>
          </el-form-item>
          <el-form-item label="Địa chỉ" prop="locationId">
            <el-col :span="10">
              <el-select
                v-model="account.locationId"
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
              v-model="account.fieldId"
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
              v-model="account.technology"
            >
              <i
                slot="prefix"
                class="el-input__icon fas fa-window-maximize"
              ></i>
            </el-input>
          </el-form-item>
          <el-form-item label="Email" prop="email">
            <el-input
              placeholder="Nhập email"
              v-model="account.email"
            >
              <i slot="prefix" class="el-input__icon fas fa-envelope"></i>
            </el-input>
          </el-form-item>
          <el-form-item label="Số diện thoại" prop="phoneNumber">
            <el-input
              placeholder="Nhập số diện thoại"
              v-model="account.phoneNumber"
            >
              <i slot="prefix" class="el-input__icon fas fa-mobile"></i>
            </el-input>
          </el-form-item>
          <el-form-item label="Ảnh đại diện">
            <el-upload
              action="https://localhost:7017/api/files"
              list-type="picture-card"
              :limit="1"
              :file-list="fileListImg"
              :on-preview="(file) => handlePictureCardPreview(file)"
              :on-success="(response) => uploadSuccess(response)"
            >
              <i class="el-icon-plus"></i>
            </el-upload>
            <el-dialog :visible.sync="dialogAvatarVisible">
              <img width="100%" :src="dialogAvatarUrl" alt="" />
            </el-dialog>
          </el-form-item>
          <el-form-item class="d-flex justify-content-center">
            <button-custom
              :content="'Lưu'"
              :bgColor="'rgb(221, 63, 36)'"
              :color="'#fff'"
              :height="40"
              :fontSize="30"
              :style="{ width: '100px' }"
              @click="update"
            />
          </el-form-item>
        </el-form>
      </div>
    </div>
  </div>
</template>
<script>
import ButtonCustom from "@/components/Button/ButtonCustom.vue";
import SliderPage from "@/components/SliderArea/SliderPage.vue";
import imgBg from "@/assets/img/banner/info.png";
import { checkEmail } from "@/utils/validate";
import { mapGetters } from "vuex";
export default {
  components: {
    ButtonCustom,
    SliderPage,
  },
  created() {
    this.$store
      .dispatch("user/getInfo")
      .then((data) => {
        this.account = data;
        this.account.dob = new Date(this.account.dob);
        this.fileListImg[0].url = this.account.avatarUrl;
      });
    this.$store.dispatch("location/getList", { pageIndex: 0 });
    this.$store.dispatch("job-category/getList", { pageIndex: 0 });
  },
  data() {
    return {
      account: {
        fullName: null,
        dob: null,
        email: null,
        phoneNumber: null,
        roles: ["user"],
        password: null,
        confirmPassword: null,
        avatarIdv: null,
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
      },
      fileListImg: [
        {
          url: null,
        },
      ],
      dialogAvatarVisible: false,
      dialogAvatarUrl: null,
      isEditing: true,
      imgBg
    };
  },
  computed: {
    ...mapGetters({
      locations: "location/getLocations",
      categories: "job-category/getCategories",
    }),
  },
  methods: {
    async update() {
      var isValided = await this.$refs["account"].validate();
      var res;
      if (isValided) {
        res = await this.$store.dispatch("user/update", this.account);
        if (res.status) {
          this.isEditing = false;
        }
      }
    },
    uploadSuccess(response) {
      this.account.avatarId = response.data.id;
      this.account.avatarUrl = response.data.url;
      this.dialogAvatarUrl = response.data.url;
    },
    handleRemove() {
      this.account.avatarId = null;
    },
    handlePictureCardPreview(file) {
      this.dialogAvatarUrl = file.url;
      this.dialogAvatarVisible = true;
    },
  },
};
</script>
<style scoped>
.el-row {
  display: flex;
  align-items: center;
}
.form-create {
  /* margin: 0 100px; */
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
.register-title {
  font-weight: 700;
  color: rgb(221, 63, 36);
}
</style>
