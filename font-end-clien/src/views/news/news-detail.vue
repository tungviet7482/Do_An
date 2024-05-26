<template>
  <page-404 v-if="isNotFound"/>
  <div v-else>
    <slider-page
      :title="'Chi tiết bài viết'"
      :height="250"
      :fontSize="'30px'"
      :urlImg="news.imgUrl"
    ></slider-page>
    <section class="blog_area single-post-area section-padding">
      <div class="container">
        <div class="row">
          <div class="col-lg-8 posts-list">
            <div class="single-post">
              <div class="feature-img">
                <img
                  class="img-fluid"
                  src="assets/img/blog/single_blog_1.png"
                  alt=""
                />
              </div>
              <div class="blog_details">
                <h2>{{ news.title }}</h2>
                <br><br>
                <div class="blog-content" v-html="news.body"></div>
              </div>
            </div>
          </div>
          <div class="col-lg-4">
            <div class="blog_right_sidebar">
              <aside class="single_sidebar_widget search_widget">
                <form action="#">
                  <div class="form-group">
                    <div class="input-group mb-3">
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Nhập từ khóa"
                        onfocus="this.placeholder = ''"
                        onblur="this.placeholder = 'Nhập từ khóa'"
                      />
                      <div class="input-group-append">
                        <button class="btns" type="button">
                          <i class="ti-search"></i>
                        </button>
                      </div>
                    </div>
                  </div>
                  <button
                    class="button rounded-0 primary-bg text-white w-100 btn_1 boxed-btn"
                    type="submit"
                  >
                    Tìm kiếm
                  </button>
                </form>
              </aside>
              <aside class="single_sidebar_widget popular_post_widget">
                <h3 class="widget_title">Tin tức gần đây</h3>
                <div v-for="item in getNews" class="media post_item">
                  <img :src="item.previewImgUrl" alt="post" />
                  <div class="media-body">
                    <router-link :to="{ name: 'NewsDetail', params: {slug: item.slug} }">
                      <h3 class="text-truncate">{{ item.title }}</h3>
                    </router-link>
                    <p>{{ formatDateTime(item.creatAt) }}</p>
                  </div>
                </div>
              </aside>
              <aside class="single_sidebar_widget newsletter_widget">
                <h4 class="widget_title">Đăng ký nhận bản tin</h4>
                <form action="#">
                  <div class="form-group">
                    <input
                      type="email"
                      class="form-control"
                      onfocus="this.placeholder = ''"
                      onblur="this.placeholder = 'Nhập email của bạn'"
                      placeholder="Nhập email của bạn"
                      required
                    />
                  </div>
                  <button
                    class="button rounded-0 primary-bg text-white w-100 btn_1 boxed-btn"
                    type="submit"
                  >
                    Đăng ký
                  </button>
                </form>
              </aside>
            </div>
          </div>
        </div>
      </div>
    </section>
    <news-area />
  </div>
</template>

<script>
import SliderPage from "@/components/SliderArea/SliderPage";
import ButtonCustom from "@/components/Button/ButtonCustom.vue";
import NewsArea from "@/components/NewsArea";
import Page404 from "@/components/error-page/404"
import { formatDateTime } from "@/utils/common";
import { mapGetters } from "vuex";

export default {
  components: {
    SliderPage,
    ButtonCustom,
    NewsArea,
    Page404
  },
  data() {
    return {
      news: null,
      listQuery: {
        pageIndex: 1,
        pageSize: 9,
      },
      isNotFound: false,
    };
  },
  created() {
    this.news = this.$route.query.news;

    if (!this.news || !this.news.title) {
      this.$store
        .dispatch("news/getNewsBySlug", this.$route.params.slug)
        .then((data) => {
          if(!data){
            this.isNotFound = true;
          }
          this.news = data;
        });
    }
    if (!this.news.length) {
      this.$store.dispatch("news/getNews", this.listQuery);
    }
  },
  computed: {
    ...mapGetters({
      listNews: "news/getNews",
    }),
    getNews() {
      return this.listNews.slice(0, 5);
    },
  },
  methods: {
    formatDateTime,
  },
};
</script>
<style scoped>
.media .media-body{
  width: 75%;
}
.media.post_item img{
  width: 80px;
  height: 80px;
}
.blog_details h2{
  font-size: 35px;
  font-weight: 700;
}
</style>