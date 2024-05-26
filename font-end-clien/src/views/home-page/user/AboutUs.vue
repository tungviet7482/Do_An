<template>
  <div class="support-company-area support-padding fix mt-120">
    <div class="container">
      <div class="row align-items-center">
        <div class="col-xl-7 col-lg-7">
          <div class="right-caption">
            <!-- Section Tittle -->
            <div class="section-tittle section-tittle2">
              <span>Công nghệ tuyển dụng thông minh</span>
              <h2>Công nghệ tuyển dụng thông minh</h2>
            </div>
            <div class="support-caption">
              <p class="pera-top text-justify">
                Cung cấp hàng ngàn cơ hội việc làm IT từ các công ty uy tín trên
                toàn quốc, nền ITVIEC giúp bạn dễ dàng tìm thấy công việc phù
                hợp với chuyên môn và sở thích của mình. Với hệ thống lọc giúp
                bạn có thể tìm kiếm các vị trí từ lập trình viên, kỹ sư phần
                mềm, chuyên gia bảo mật đến quản trị mạng, khoa học dữ liệu và
                nhiều lĩnh vực khác. <br/> <br>Không chỉ vậy, chúng tôi còn cung cấp thông
                tin chi tiết về công ty, môi trường làm việc, văn hóa doanh
                nghiệp, giúp bạn có cái nhìn tổng quan và đưa ra quyết định đúng
                đắn. Hãy tham gia cùng ITVIEC để khám phá và nắm bắt những cơ
                hội việc làm IT tốt nhất, phát triển sự nghiệp và đạt được mục
                tiêu nghề nghiệp của bạn.
              </p>
              <router-link :to="{ name: 'FindJob' }" class="btn post-btn"
                >Tìm việc ngay</router-link
              >
            </div>
          </div>
        </div>
        <div class="col-xl-5 col-lg-5">
          <div class="support-location-img">
            <img :src="bgImage" alt="" />
            <!-- <div class="support-img-cap text-center">
              <p>Since</p>
              <span>1994</span>
            </div> -->
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import bgImage from "@/assets/img/banner/about-us.jpg";

export default {
  components: {},
  props: {},
  data() {
    return {
      bgImage,
    };
  },
  methods: {},
};
</script>
<style scoped>
</style>