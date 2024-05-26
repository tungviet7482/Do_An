<template>
  <div class="side-bar">
    <div class="user-info">
      <div class="img">
        <img :src="avatar" alt="error" />
      </div>
      <div class="info">
        <div class="name">{{ name }}</div>
        <div class="role">Nhà tuyển dụng</div>
      </div>
    </div>
    <ul class="side-bar-ls bg-black">
      <span class="devide"></span>
      <li class="side-bar-it">
        <router-link :to="{ name: 'Recruitment' }" class="side-bar-li">
          <i class="fas fa-list"></i>
          <div class="title">Danh sách tin đăng tuyển</div>
        </router-link>
      </li>
      <li class="side-bar-it">
        <a class="side-bar-li">
          <i class="fas fa-shopping-cart"></i>
          <div class="title">Mua dịch vụ</div>
        </a>
      </li>
      <li class="side-bar-it">
        <a class="side-bar-li">
          <i class="fas fa-check-circle"></i>
          <div class="title">Dịch vụ của tôi</div>
        </a>
      </li>
      <br />
      <br />
      <br />
      <br />
      <br />
      <br />
      <br />
      <span class="devide"></span>
      <li class="side-bar-it">
        <router-link :to="{ name: 'CompanyInfo' }" class="side-bar-li">
          <i class="fas fa-info-circle"></i>
          <div class="title">Thông tin công ty</div>
        </router-link>
      </li>
    </ul>
  </div>
</template>
<script>
import { mapGetters } from "vuex";
export default {
  computed: {
    ...mapGetters({
      name: "name",
      avatar: "avatar",
    }),
  },
};
</script>
<style lang="scss" scoped>
li {
  margin: 15px 0;
}
</style>