<template>
  <div class="account">
    <div class="user-account">
      <img :src="avatar" alt="" />
      <span class="fullName">{{ name }}</span>
    </div>
    <ul class="submenu">
      <li><a @click="logout">Đăng xuất</a></li>
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
  methods: {
    logout() {
      this.$store.dispatch("user/logout");
    },
  },
};
</script>

<style scoped>
.user-account {
  display: flex;
  height: 36px;
  /* background-color: #ccc; */
  align-items: center;
  max-width: 480px;
  justify-content: flex-end;
  cursor: pointer;
  padding: 0 10px;
  border-radius: 20px;
  background-color: #f4f5f5;
}
.user-account img {
  height: 28px;
  width: 28px;
  border-radius: 50%;
  aspect-ratio: 1 / 1;
  margin-right:15px;
  box-shadow: 0 0 3px black;
}
.user-account span {
  font-weight: 650;
}
.account {
  position: relative;
}
.account:hover ul.submenu {
  visibility: visible;
  opacity: 1;
  /* top: 100%; */
}
.account .submenu {
  position: absolute;
  min-width: 180px;
  background: #fff;
  left: 10px;
  top: 155%;
  visibility: hidden;
  opacity: 0;
  border-top: 5px solid rgb(221, 63, 36);
  -webkit-box-shadow: 0 0 10px 3px rgba(0, 0, 0, 0.05);
  box-shadow: 0 0 10px 3px rgba(0, 0, 0, 0.05);
  padding: 17px 0;
  -webkit-transition: 0.3s;
  transition: 0.3s;
  z-index: 999;
}

.submenu li a{
  color: black !important;
}
</style>