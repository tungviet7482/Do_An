<template>
  <header>
    <!-- Header Start -->
    <div class="company-header-area header-transparrent">
      <div class="cus-container">
        <div class="headder-top header-sticky">
          <div class="row align-items-center">
            <div class="col-lg-2 col-md-2">
              <!-- Logo -->
              <div class="logo">
                <a href="index.html">
                  <span class="w-100 d-block">ITVIEC</span>
                </a>
              </div>
            </div>
            <div class="col-lg-10 col-md-9">
              <!-- Main-menu -->
              <div
                class="main-menu d-flex justify-content-between align-items-center"
              >
                <nav class="d-none d-lg-block">
                  <ul
                    id="navigation"
                    class="d-flex justify-content-between align-items-center"
                  >
                    <li class="active">
                      <router-link :to="{ path: '/tuyen-dung' }">
                        <i class="fas fa-home"></i>
                        <span>Trang chủ</span></router-link
                      >
                    </li>
                    <li>
                      <router-link :to="{ name: 'CreateRecruitment' }">
                        <i class="el-icon-edit"></i>
                        <span>Đăng tin</span></router-link
                      >
                    </li>
                    <li>
                      <router-link :to="{name: 'FindCandidate'}">
                        <i class="el-icon-edit"></i>
                        <span>Tim CV</span></router-link
                      >
                    </li>
                  </ul>
                </nav>
                <div
                  v-if="currentRole && currentRole == 'company'"
                  class="header-btn d-none f-right d-lg-block"
                >
                  <account />
                </div>
              </div>
              <!-- Header-btn -->
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
  </header>
</template>
<script>
import { mapGetters } from "vuex";
import Account from "@/components/Account.vue";
import { getToken } from "@/utils/auth";

export default {
  components: {
    Account,
  },
  data() {
    return {};
  },
  created() {
    if (getToken()) {
      if (!this.currentRole) {
        this.$store.dispatch("user/getInfo");
      }
    }
  },
  computed: {
    ...mapGetters({
      currentRole: "role",
    }),
  },
};
</script>
<style scoped>
.logo a span {
  color: #fff;
}
#navigation li {
  margin: 0 20px;
}
</style>