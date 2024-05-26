<template>
  <main>
    <div class="container"></div>
    <div class="cus-container">
      <div class="row">
        <div class="col-3">
          <sidebar />
        </div>
        <div class="col-8">
          <transition name="fade-transform" mode="out-in">
             <div class="content">
              
              <div class="content-head">
                <h3>{{ $route.meta.title }}</h3>
              </div>
              <div class="content-body">
                <router-view :key="key" />
              </div>
            </div>
          </transition>
        </div>
      </div>
    </div>
  </main>
</template>

<script>
import Sidebar from "./Sidebar.vue";
import { funcRender } from "@/assets/js/main";

export default {
  name: "CompanyAppMain",
  components: {
    Sidebar,
  },
  computed: {
    cachedViews() {
      return this.$store.state.tagsView.cachedViews;
    },
    key() {
      return this.$route.path;
    },
  },
  mounted() {
    funcRender();
  },
};
</script>
<style scoped>
.content-head{
  margin-bottom: 30px;
}
.content-head h3{
  font-weight: 700;
  text-align: center;
}
</style>