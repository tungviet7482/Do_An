<template>
  <div>
    <!-- Header Start -->
    <div class="header-area header-transparrent">
      <div class="headder-top header-sticky">
        <div class="mx-5">
          <div class="row align-items-center justify-content-between">
            <div class="col-lg-2 col-md-2">
              <!-- Logo -->
              <div class="logo">
                <router-link :to="{ name: 'HomePage' }"
                  ><img src="@/assets/img/logo/logo.png" alt=""
                /></router-link>
              </div>
            </div>
            <div class="col-lg-10 col-md-9">
              <div class="menu-wrapper">
                <!-- Main-menu -->
                <div class="main-menu  ml-80">
                  <nav class="d-none d-lg-block">
                    <ul id="navigation">
                      <li>
                        <router-link :to="{ name: 'HomePage' }"
                          >Trang chủ</router-link
                        >
                      </li>
                      <li>
                        <router-link :to="{ name: 'FindJob' }"
                          >Tìm việc làm</router-link
                        >
                      </li>
                      <li>
                        <router-link :to="{ name: 'Company' }"
                          >Công ty</router-link
                        >
                      </li>
                      <li>
                        <a href="#">Hồ sơ và CV</a>
                        <ul class="submenu">
                          <li><a @click="viewCV">Xem CV</a></li>
                          <li>
                            <router-link :to="{ name: 'UploadCV' }"
                              >Tải lên CV</router-link
                            >
                          </li>
                          <li>
                            <router-link :to="{ name: 'SavedJobs' }"
                              >Công việc đã lưu</router-link
                            >
                          </li>
                          <li>
                            <router-link :to="{ name: 'AppliedJobs' }"
                              >Công việc đã ứng tuyển</router-link
                            >
                          </li>
                          <li>
                            <router-link :to="{ name: 'CandidateInfo' }"
                              >Hồ sơ</router-link
                            >
                          </li>
                        </ul>
                      </li>
                      <li>
                        <router-link :to="{ name: 'News' }"
                          >Tin tức</router-link
                        >
                      </li>
                      <li>
                        <router-link :to="{ name: 'ContactUs' }"
                          >Liên hệ</router-link
                        >
                      </li>
                    </ul>
                  </nav>
                </div>
                <!-- Header-btn -->
                <div
                  v-if="currentRole && currentRole == 'user'"
                  class="header-btn d-none f-right d-lg-block"
                >
                  <account />
                </div>
                <div v-else class="header-btn d-none f-right d-lg-block">
                  <Button :content="'Đăng ký'" :routerName="'RegisterUser'" />
                  <button-custom
                    :content="'Đăng nhập'"
                    :bgColor="'rgb(221, 63, 36)'"
                    :color="'#fff'"
                    @click="loginDialogVisible = true"
                  />
                  <button-custom
                    :content="'Đăng tuyển'"
                    :bgColor="'#000'"
                    :color="'#fff'"
                    @click="openLoginCompanyWindow"
                  />
                </div>
              </div>
            </div>
            <!-- Mobile Menu -->
            <div class="col-12">
              <div class="mobile_menu d-block d-lg-none"></div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- Header End -->
    <el-dialog
      title="Đăng nhập"
      width="40%"
      :visible.sync="loginDialogVisible"
      center
    >
      <el-form :rules="rules" :model="account" ref="account" status-icon>
        <el-form-item :label-width="'0'" prop="email">
          <label for="">Tài khoản Email</label>
          <el-input
            class="custom-input"
            type="text"
            v-model="account.email"
            autocomplete="off"
            placeholder="vd: a@gmail.com"
            size="large"
          />
        </el-form-item>
        <el-form-item :label-width="'0'" prop="password">
          <label for="">Mật khẩu</label>
          <el-input
            class="custom-input"
            autocomplete="off"
            v-model="account.password"
            placeholder="vd: Van123*"
            size="large"
            show-password
          />
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <router-link :to="{ name: 'RegisterUser' }"
          ><el-link type="primary">Đăng ký</el-link></router-link
        >
        <Button :content="'Hủy'" @click="loginDialogVisible = false" />
        <button-custom
          :content="'Đăng nhập'"
          :bgColor="'rgb(221, 63, 36)'"
          :color="'#fff'"
          @click="login"
        />
      </span>
    </el-dialog>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import ButtonCustom from "@/components/Button/ButtonCustom.vue";
import Button from "@/components/Button/Button.vue";
import Account from "../../../components/Account.vue";
import { getToken } from "@/utils/auth";
import { Message } from "element-ui";

export default {
  components: {
    ButtonCustom,
    Button,
    Account,
  },
  data() {
    var checkEmail = (rule, value, callback) => {
      if (!value) {
        return callback(new Error("Hãy nhập Email của bạn"));
      }
      var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      if (!emailPattern.test(value)) {
        callback(new Error("Email không hợp lệ"));
      } else {
        callback();
      }
    };

    var checkPassword = (rule, value, callback) => {
      if (!value) {
        return callback(new Error("Hãy nhập mật khẩu"));
      }
      // Kiểm tra xem mật khẩu có ít nhất 6 ký tự không
      if (value.length < 6) {
        return callback(new Error("Mật khẩu phải chứa ít nhất 6 ký tự"));
      }
      // Kiểm tra xem mật khẩu có ít nhất một chữ hoa không
      if (!/[A-Z]/.test(value)) {
        return callback(new Error("Mật khẩu phải chứa ít nhất một chữ hoa"));
      }
      // Kiểm tra xem mật khẩu có ít nhất một chữ thường không
      if (!/[a-z]/.test(value)) {
        return callback(new Error("Mật khẩu phải chứa ít nhất một chữ thường"));
      }
      // Kiểm tra xem mật khẩu có ít nhất một số không
      if (!/\d/.test(value)) {
        return callback(new Error("Mật khẩu phải chứa ít nhất một số"));
      }
      // Kiểm tra xem mật khẩu có ít nhất một ký tự đặc biệt không
      if (!/[^a-zA-Z0-9]/.test(value)) {
        return callback(
          new Error("Mật khẩu phải chứa ít nhất một ký tự đặc biệt")
        );
      }
      // Nếu mật khẩu thỏa mãn tất cả các yêu cầu, gọi callback mà không có lỗi
      callback();
    };

    return {
      registerDialogVisible: false,
      loginDialogVisible: false,
      account: {
        email: "",
        password: "",
      },
      rules: {
        email: [{ validator: checkEmail, trigger: "blur" }],
        password: [{ validator: checkPassword, trigger: "blur" }],
      },
    };
  },
  created() {
    if (getToken()) {
      if (!this.currentRole) {
        this.$store.dispatch("user/getInfo");
      }
    }
  },
  mounted() {
    window.bus.$on("open-login", this.openLoginUser);
  },
  computed: {
    ...mapGetters({
      currentRole: "role",
      cvUrl: "cvUrl",
    }),
  },
  methods: {
    login() {
      this.$store.dispatch("user/login", this.account).then((data) => {
        if (data.status) {
          this.loginDialogVisible = false;
        }
      });
    },
    openLoginCompanyWindow() {
      window.open(this.$router.resolve({ path: "/tuyen-dung" }).href, "_blank");
    },
    openLoginUser(isOpen) {
      this.loginDialogVisible = isOpen;
    },
    viewCV() {
      if (this.cvUrl) {
        window.open(this.cvUrl);
      }
    },
  },
};
</script>

<style lang="scss" scoped>
.custom-input {
  max-width: 100%;
  min-width: 100px;
  height: 30px;
  margin-bottom: 30px;
  font-size: 18px;
}
.el-link {
  font-size: 17px;
  color: #409eff !important;
  margin-right: 10px;
}
.row {
  margin-right: 0;
  margin-left: 0;
}
img {
  width: auto;
}
</style>
