<template>
  <div class="container">
    <slider-page
      :title="'Danh sách bài viết'"
      :height="200"
      :fontSize="'30px'"
      :urlImg="img"
    ></slider-page>
    <div class="row mt-4 mb-3">
      <div class="col-lg-6">
        <el-input
          placeholder="Nhập từ khóa"
          prefix-icon="el-icon-search"
          size="medium"
          v-model="listQuery.keyWord"
          @keyup.enter.native="filternews"
        >
        </el-input>
      </div>
      <div class="col-lg-2">
        <buttom-custom :content="'Tìm kiếm'" @click="filternews" />
      </div>
    </div>
    <div class="row flex-wrap">
      <news-item
        v-for="news in news"
        :key="news.id"
        :news="news"
        @click="viewNewsDetail(news)"
      />
    </div>
    <div class="d-flex justify-content-center mb-5">
      <el-pagination
        :page-size="listQuery.pageSize"
        background
        layout="prev, pager, next"
        :total="total"
        :hide-on-single-page="total <= listQuery.pageSize"
        @current-change="handleCurrentChange"
      >
      </el-pagination>
    </div>
  </div>
</template>
<script>
import SliderPage from "@/components/SliderArea/SliderPage.vue";
import { mapGetters } from "vuex";
import NewsItem from "@/components/NewsItem.vue";
import ButtomCustom from "@/components/Button/ButtonCustom.vue";
import img from "@/assets/img/banner/pexels-kaboompics-6335.jpg"

export default {
  components: {
    SliderPage,
    NewsItem,
    ButtomCustom,
  },
  data() {
    return {
      listLoading: true,
      listQuery: {
        pageIndex: 1,
        pageSize: 9,
      },
      img
    };
  },
  created() {
    if (!this.news.length) {
      this.$store.dispatch("news/getNews", this.listQuery);
    }
  },
  computed: {
    ...mapGetters({
      news: "news/getNews",
      total: "news/getTotal",
    }),
  },
  methods: {
    viewNewsDetail(news) {
      var newUrl = this.$router.resolve({
        path: `/tin-tuc/${news.slug}`,
        query: { news: news },
      });

      window.open(newUrl.href, "_blank");
    },
    filternews() {
      this.$store.dispatch("news/getNews", this.listQuery);
    },
    handleCurrentChange(val) {
      this.listQuery.pageIndex = val;
      this.$store.dispatch("news/getNews", this.listQuery);
    },
  },
};
</script>