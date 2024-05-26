<template>
  <footer>
    <!-- Footer Start-->
    <div class="footer-area footer-bg footer-padding">
      <div class="container">
        <div class="row d-flex justify-content-between">
          <div class="col-xl-3 col-lg-3 col-md-4 col-sm-6">
            <div class="single-footer-caption mb-50">
              <div class="single-footer-caption mb-30">
                <div class="footer-tittle">
                  <h4>Về chúng tôi</h4>
                  <div class="footer-pera">
                    <p>
                      Nền tảng công nghệ tuyển dụng thông minh.
                    </p>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="col-xl-3 col-lg-3 col-md-4 col-sm-5">
            <div class="single-footer-caption mb-50">
              <div class="footer-tittle">
                <h4>Thông tin liên hệ</h4>
                <ul>
                  <li>
                    <p>Địa chỉ: Số 298 Đ. Cầu Diễn, Minh Khai, Bắc Từ Liêm, Hà Nội</p>
                  </li>
                  <li><a href="#">Điện thoại : +8880 44338899</a></li>
                  <li><a href="#">Email : tungv122112@gmail.com</a></li>
                </ul>
              </div>
            </div>
          </div>
          <div class="col-xl-3 col-lg-3 col-md-4 col-sm-5">
            <div class="single-footer-caption mb-50">
              <div class="footer-tittle">
                <h4>Truy cập nhanh</h4>
                <ul>
                  <li><router-link :to="{name: 'FindJob'}" href="#">Tìm việc làm</router-link></li>
                  <li><router-link :to="{name: 'Company'}" href="#">Công ty</router-link></li>
                  <li><router-link :to="{name: 'UploadCV'}" href="#">Tải lên CV</router-link></li>
                  <li><router-link :to="{name: 'News'}" href="#">Các bài viết</router-link></li>
                  <li><router-link :to="{name: 'CandidateInfo'}" href="#">Thông tài khoản</router-link></li>
                </ul>
              </div>
            </div>
          </div>
          <div class="col-xl-3 col-lg-3 col-md-4 col-sm-5">
            <div class="single-footer-caption mb-50">
              <div class="footer-tittle">
                <h4>Đăng ký tư vấn</h4>
                <div class="footer-pera footer-pera2">
                  <!-- <p>
                    Heaven fruitful doesn't over lesser in days. Appear
                    creeping.
                  </p> -->
                </div>
                <!-- Form -->
                <div class="footer-form">
                  <div id="mc_embed_signup">
                    <form
                      target="_blank"
                      action="https://spondonit.us12.list-manage.com/subscribe/post?u=1462626880ade1ac87bd9c93a&amp;id=92a4423d01"
                      method="get"
                      class="subscribe_form relative mail_part"
                    >
                      <input
                        type="email"
                        name="email"
                        id="newsletter-form-email"
                        placeholder="Email của bạn"
                        class="placeholder hide-on-focus"
                        onfocus="this.placeholder = ''"
                        onblur="this.placeholder = ' Email Address '"
                      />
                      <div class="form-icon">
                        <button
                          type="submit"
                          name="submit"
                          id="newsletter-submit"
                          class="email_icon newsletter-submit button-contactForm"
                        >
                          <img src="assets/img/icon/form.png" alt="" />
                        </button>
                      </div>
                      <div class="mt-10 info"></div>
                    </form>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!--  -->
        <div class="row footer-wejed justify-content-between">
          <div class="col-xl-5 col-lg-5 col-md-7 col-sm-12 d-flex justify-content-center">
            <!-- logo -->
            <div class="mb-20">
              <a href="/"
                ><img :src="logo" alt=""
              /></a>
            </div>
          </div>
          <!-- <div class="col-xl-3 col-lg-3 col-md-4 col-sm-5">
            <div class="footer-tittle-bottom">
              <span>5000+</span>
              <p>Talented Hunter</p>
            </div>
          </div> -->
          <div class="col-xl-3 col-lg-3 col-md-4 col-sm-5 d-flex align-items-center">
            <div class="footer-tittle-bottom d-flex align-items-center">
              <span>500+</span>
              <p class="m-0">Công việc</p>
            </div>
          </div>
          <div class="col-xl-3 col-lg-3 col-md-4 col-sm-5  d-flex align-items-center">
            <!-- Footer Bottom Tittle -->
            <div class="footer-tittle-bottom d-flex align-items-center">
              <span>100+</span>
              <p class="m-0">Doanh nghiệp</p>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- footer-bottom area -->
    <div class="footer-bottom-area footer-bg">
      <div class="container">
        <div class="footer-border">
          <div class="row d-flex justify-content-between align-items-center">
            <div class="col-xl-10 col-lg-10">
              <div class="footer-copy-right"></div>
            </div>
            <div class="col-xl-2 col-lg-2">
              <div class="footer-social f-right">
                <a href="#"><i class="fab fa-facebook-f"></i></a>
                <a href="#"><i class="fab fa-twitter"></i></a>
                <a href="#"><i class="fas fa-globe"></i></a>
                <a href="#"><i class="fab fa-behance"></i></a>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- Footer End-->
  </footer>
</template>
<script>
import logo from "@/assets/img/logo/2.png";
export default {
  data(){
    return {
      logo
    }
  }
}
</script>
<style scoped>
.footer-logo img {
  width: 270px;
  height: 87px;
}
</style>