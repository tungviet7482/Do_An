<template>
  <div class="col-md-4 col-sm-12" @click="$emit('click')">
    <div class="company-item">
      <div class="company-banner">
        <a>
          <div class="cover-wraper">
            <img
              :src="company.imgUrl"
              alt=""
            />
          </div>
        </a>
        <div class="company-logo">
          <a>
            <img
              :src="company.previewImgUrl"
              alt=""
            />
          </a>
        </div>
      </div>
      <div :title="company.name.toUpperCase()" class="company-info">
        <h5 >{{ company.name.toUpperCase() }}</h5>
        <div class="company-description">
          <p>
            "{{ company.description }}"
          </p>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  props: {
    company:{
      type: Object,
      default: () => {}
    }
  },
};
</script>
<style scoped>
.company-item {
  height: 424px;
  margin: 5px;
  /* background-color: antiquewhite; */
  box-sizing: border-box;
  margin-bottom: 30px;
}
img {
  height: 100%;
  width: 100%;
}
.company-banner {
  position: relative;
}
.company-banner .cover-wraper {
  height: 150px;
  margin-bottom: 35px;
}
.company-banner .cover-wraper img {
  border-top-left-radius: 10px;
  border-top-right-radius: 10px;
}
.company-banner .company-logo {
  position: absolute;
  width: 62px;
  height: 62px;
  left: 15px;
  bottom: -31px;
}
.company-item .company-info {
  padding: 10px;
}
.company-item .company-info h5 {
  font-size: 15px;
  font-weight: 800;
  text-align: center;
  display: -webkit-box;
  -webkit-box-orient: vertical;
  overflow: hidden;
  text-overflow: ellipsis;
  -webkit-line-clamp: 1;
}
.company-item .company-info p {
  width: 100%;
  font-size: 13px;
  text-align: justify;
  display: -webkit-box;
  -webkit-box-orient: vertical;
  overflow: hidden;
  text-overflow: ellipsis;
  -webkit-line-clamp: 4;
}
</style>