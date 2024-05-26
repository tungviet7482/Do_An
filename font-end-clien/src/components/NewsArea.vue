<template>
  <div class="home-blog-area blog-h-padding mb-120">
    <div class="container">
      <!-- Section Tittle -->
      <div class="row">
        <div class="col-lg-12">
          <div class="section-tittle text-center">
            <span>Tin tức mới nhất</span>
            <h2>Tin tức mới nhất</h2>
          </div>
        </div>
      </div>
      <div class="row d-flex">
        <div
          class="col-xl-4 col-lg-5 col-md-5"
          v-for="item in getTopNews"
          :key="item.id"
        >
          <div class="home-blog-single mb-30">
            <div class="blog-img-cap" @click="viewNewsDetail(item)">
              <div class="blog-img">
                <img :src="item.previewImgUrl" alt="" />
              </div>
              <div class="blog-cap">
                <h3>
                  <a @click="viewNewsDetail(item)">{{ item.title }}</a>
                </h3>
                <a @click="viewNewsDetail(item)" class="more-btn"
                  >Xem chỉ tiết »</a
                >
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import JobItem from "@/components/JobItem.vue";
import NewsItem from "@/components/NewsItem.vue";
import { mapGetters } from "vuex";

export default {
  components: {
    JobItem,
    NewsItem,
  },
  data() {
    return {};
  },
  created() {
    if (!this.news.length) {
      this.$store.dispatch("news/getNews", this.listQuery);
    }
  },
  computed: {
    ...mapGetters({
      news: "news/getNews",
    }),
    getTopNews() {
      return this.news.slice(0, 3);
    },
  },
  methods: {
    viewNewsDetail(news) {
      var newUrl = this.$router.resolve({
        path: `/tin-tuc/${news.slug}`,
        query: { news: news },
      });

      window.open(newUrl.href, "_blank");
    },
  },
};
</script>
<style scoped>
.home-blog-single .blog-img img {
  height: 180px !important;
}
</style>