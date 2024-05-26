<template>
  <!-- Hero Area Start-->
  <div class="slider-area">
    <div
      class="single-slider section-overly slider-height2 d-flex align-items-center"
      :style="{ backgroundImage: 'url(' + urlImg + ')', height: height + 'px' }"
    >
      <div class="container">
        <div class="row">
          <div class="col-xl-12">
            <div class="hero-cap text-center">
              <h2 :style="{ fontSize }">{{ title }}</h2>
            </div>
          </div>
        </div>
      </div>
      <div
        v-if="showCompanyLogo"
        class="company-area d-flex justify-conten-center align-items-center bg-white w-100"
      >
        <div class="logo-company">
          <img
            :src="logoUrl"
          />
        </div>
        <div class="info-company" @click="viewCompanyDetail">
          <h4 style="cursor: pointer;">{{ companyName }}</h4>
          <ul class="d-flex">
            <li v-if="companyEmployeeCount"><i class="fas fa-users"></i> {{ companyEmployeeCount }} nhân sự</li>
            <li v-if="companyWebsiteUrl">
              <a :href="companyWebsiteUrl" target="_blank">
              <i class="fas fa-link"></i> {{companyWebsiteUrl}}</i></a>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </div>
  <!-- Hero Area End -->
</template>

<script>
export default {
  name: "SliderPage",
  props: {
    title: {
      type: String,
      default: "",
    },
    urlImg: {
      type: String,
      default: "",
    },
    height: {
      type: Number,
      default: 400,
    },
    fontSize: {
      type: String,
      default: null,
    },
    logoUrl: {
      type: String,
      default: "",
    },
    showCompanyLogo: {
      type: Boolean,
      default: false,
    },
    companyName: {
      type: String,
      default: "",
    },
    companyWebsiteUrl: {
      type: String,
      default: "",
    },
    companyEmployeeCount: {
      type: String,
      default: "",
    },
    slug: {
      type: String,
      default: ""
    }
  },
  methods:{
    viewCompanyDetail() {
      var companyUrl = this.$router.resolve({
        path: `/cong-ty/${this.slug}`
      });
      window.open(companyUrl.href, "_blank");
    },
  }
};
</script>
<style scoped>
img {
  width: 100%;
  height: 100%;
}
.slider-area .single-slider {
  position: relative;
}

.company-area {

  position: absolute;
  top: 100%;
  padding-bottom: 30px;
  padding-top: 20px;
  background-image: linear-gradient(to right,rgba(221, 63, 36,1),rgba(221, 63, 36,0.8));
  color:white !important;
  
  }

.company-area h4 {
  font-weight: 700;
  color: #fff;
}
.company-area a,
.company-area i
{
  color: #fff;
}

.logo-company {
  position: absolute;
  top:0;
  left:20px;
  transform: translateY(-50%);
  width: 150px;
  height: 150px;
  padding: 30px;
  border-radius: 50%;
  border: 1px #b7a5a5 solid;
  background: #fff;
  margin-right: 30px;
}

.logo-company img {
  border-radius: 50%;
}

.info-company{
  padding-left: 190px;
}
ul li {
  margin-right: 48px;
  font-size: 16px;
}

ul li i {
  color: #886d6d;
  font-size: medium;
}

i{
  color: #fff !important;;
}
</style>
