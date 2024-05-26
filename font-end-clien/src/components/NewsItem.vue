<template>
  <div class="col-md-3 col-sm-12 mb-60" @click="$emit('click')">
    <div class="news-item">
      <div class="news-banner">
        <a>
          <div class="cover-wraper">
            <img :src="news.previewImgUrl" alt="" />
          </div>
        </a>
      </div>
      <div :title="news.title" class="news-info">
        <h5>{{ news.title }}</h5>
        <div class="news-description">
          <p v-html="news.shortDescription"></p>
        </div>
        <p class="text-left font-italic">
          <i class="far fa-clock text-danger"></i>  Ngày đăng:  {{ formatDateTime(news.creatAt) }}
        </p>
      </div>
    </div>
  </div>
</template>
<script>
import {formatDateTime} from "@/utils/common";
export default {
  props: {
    news: {
      type: Object,
      default: () => {},
    },
  },
  methods: {
    formatDateTime
  },
};
</script>
<style scoped>
.news-item {
  height: 260px;
  margin: 5px;
  box-sizing: border-box;
  margin-bottom: 30px;
}
img {
  height: 100%;
  width: 100%;
}
.news-banner {
  position: relative;
}
.news-banner .cover-wraper {
  height: 150px;
  margin-bottom: 10px;
}
.news-banner .cover-wraper img {
  border-top-left-radius: 10px;
  border-top-right-radius: 10px;
}
.news-item .news-info {
  padding: 10px;
}
.news-item .news-info h5 {
  font-size: 15px;
  font-weight: 800;
  text-align: left;
  display: -webkit-box;
  -webkit-box-orient: vertical;
  overflow: hidden;
  text-overflow: ellipsis;
  -webkit-line-clamp: 2;
}
.news-item .news-info p {
  width: 100%;
  font-size: 13px;
  text-align: justify;
  display: -webkit-box;
  -webkit-box-orient: vertical;
  overflow: hidden;
  text-overflow: ellipsis;
  -webkit-line-clamp: 2;
}
</style>